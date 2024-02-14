using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutrifeelCore.Api.Model.Ally;
using NutrifeelCore.Business.Ally;
using NutrifeelCore.Infraestructure.DTO;
using NutrifeelCore.Infraestructure.Settings;

namespace NutrifeelCore.Api.Controllers
{
    public class AllyController : Controller
    {
        private readonly AllyBusiness _allyBusiness;
        private readonly IMapper _mapper;
        public AllyController(AllyBusiness allyBusiness,
                              IMapper mapper)
        {
            allyBusiness= _allyBusiness;
            _mapper = mapper;
        }

        [HttpPost("SaveDocumentation")]
        public async Task<object> SaveDocumentation(AllyViewModel model)
        {
            ResultBase response = new();

            if (!ModelState.IsValid && model.Diplomas.Any())
            {
                response.State = false;
                response.MessageException = ModelState.ToString();
                return response;
            }

            try
            {
                var result = await _allyBusiness.SaveDocumentation(_mapper.Map<AllyDTO>(model));
                response.State = result;
            }
            catch (Exception ex)
            {
                response.State = false;
                response.MessageException = ex.Message;
            }


            return response;
        }
    }
}
