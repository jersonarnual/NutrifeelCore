using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NutrifeelCore.Domain.Context;
using NutrifeelCore.Domain.Domain.Identity;
using NutrifeelCore.Domain.Security;
using NutrifeelCore.Infraestructure.Service;
using NutrifeelCore.Infraestructure.Settings;
using System.Security.Claims;

namespace NutrifeelCore.Api
{
    public static class NutrifeelService
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            var services = builder.Services;
         
            
            services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            services.Configure<Tokenconfiguration>(builder.Configuration.GetSection("TokenConfiguration"));


            services.AddDbContext<NutrifeelContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DevDB")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                                .AddEntityFrameworkStores<NutrifeelContext>()
                                .AddDefaultTokenProviders();

            services.AddScoped<ICustomEmailSender, CustomEmailSender>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("ShouldContainRole", options => options.RequireClaim(ClaimTypes.Role));

                foreach (var item in PolicyTypes.ListAllClaims)
                {
                    options.AddPolicy(item.Value.Value, policy => { policy.RequireClaim(item.Value.Type, item.Value.Value); });
                }
            });

        }
    }
}
