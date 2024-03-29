﻿using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutrifeelCore.Api.Model;
using NutrifeelCore.Domain.Domain.Identity;
using NutrifeelCore.Infraestructure.Service;
using NutrifeelCore.Infraestructure.Settings;
using NutrifeelCore.Secutity;

namespace NutrifeelCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ICustomEmailSender _customEmailSender;
        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<ApplicationRole> roleManager,
           ILogger<AccountController> logger, IConfiguration configuration,
           ICustomEmailSender customEmailSender)
        {
            _customEmailSender = customEmailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser == null)
                {
                    return BadRequest(new
                    {
                        Errors = "Invalid login request",
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, model.Password);

                if (!isCorrect)
                {
                    return BadRequest(new
                    {
                        Errors = "Invalid login request",
                        Success = false
                    });
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var claims = await ApplicationUserService.GetClaimsByUser(existingUser, _roleManager, _userManager);
                    return Ok(CustomToken.GenerateToken(model.Email, _configuration["TokenConfiguration:JwtKey"],
                                                             _configuration["TokenConfiguration:ExpireHours"],
                                                             _configuration["TokenConfiguration:Issuer"],
                                                             _configuration["TokenConfiguration:Audience"],
                                                             claims));
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return BadRequest(new
                    {
                        Errors = "User account locked out.",
                        Success = false
                    });
                }
            }
            _logger.LogWarning("Invalid login attempt.");
            return BadRequest(new
            {
                Errors = "Invalid login attempt.",
                Success = false
            });
        }

        [HttpPost("Register")]
        public async Task<object> RegisterClient([FromBody] RegisterViewModel model)
        {
            ResultBase response = new ResultBase();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.State = false;
                    response.MessageException = ModelState.ToString();
                    return response;
                }

                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    response.State = false;
                    response.Message = "El correo se encuentra registrado";
                    return response;
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    response.State = false;
                    response.MessageException = result.Errors.ToString();
                    return response;
                }

                var role = model.IsAlly ? "Aliado" : "Cliente";
                await _userManager.AddToRoleAsync(user, role);

                _logger.LogInformation("User created a new account with password.");

                var codeEmailConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _customEmailSender.SendEmailAsync(user.Email, "Nutrifeel TOKEN", codeEmailConfirmation);

                response.State = true;
                response.Message = "Usuario creado";
                return response;
            }
            catch (Exception ex)
            {
                response.State = false;
                response.MessageException = ex.ToString();
                return response;
            }
        }

       
        [HttpPost("ValidateRegister")]
        public async Task<object> ValidateRegister([FromBody] LoginViewModel model, string emailCode)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest("Not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, emailCode);
            if (result.Succeeded)
            {
                return Ok(); // passtoken                        
            }

            return BadRequest("Error to validate token");
        }

        [HttpPost("SendTokenEmail")]
        public async Task<object> SendTokenEmail([FromBody] LoginViewModel model)
        {
            string jwtToken = string.Empty;

            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    _logger.LogInformation("User created a new account with password.");
                    try
                    {
                        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(existingUser);
                        await _customEmailSender.SendEmailAsync(existingUser.Email, "StudyProject TOKEN", emailConfirmationToken);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                }
                else
                    return BadRequest("User not found");
            }
            else
                return BadRequest(ModelState);

            return Ok(); // passtoken
        }

        [HttpPost("CreateRole")]
        public async Task<object> CreateRole()
        {
            ResultBase response = new();
            var roleClient = await _roleManager.RoleExistsAsync("Cliente");
            var roleAliado = await _roleManager.RoleExistsAsync("Aliado");
            var roleAdmi = await _roleManager.RoleExistsAsync("Admi");

            if (!roleClient)
            {
                var newRole = new ApplicationRole { Name = "Cliente" };
                await _roleManager.CreateAsync(newRole);
            }
            if (!roleAliado)
            {
                var newRole = new ApplicationRole { Name = "Aliado" };
                await _roleManager.CreateAsync(newRole);
            }
            if (!roleAdmi)
            {
                var newRole = new ApplicationRole { Name = "Administrador" };
                await _roleManager.CreateAsync(newRole);
            }
            return response;

        }
    }
}
