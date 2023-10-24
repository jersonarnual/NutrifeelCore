using Microsoft.AspNetCore.Mvc;
using NutrifeelCore.Api.Model.MiSalud;
using NutrifeelCore.Business.MiSalud;
using NutrifeelCore.Infraestructure.Settings;

namespace NutrifeelCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiSaludController : ControllerBase
    {
        public MiSaludController()
        {
        }

        [HttpPost("CalculoImc")]
        public object CalculoImc([FromBody] CalculoIMCViewModel model)
        {
            ResultBase result = new();
            double imc = MiSaludBusiness.CalculoImc(model.Peso, model.Altura);
            if (imc.Equals(0))
            {
                result.State = false;
                result.MessageException = "Operacion Invalida";
                result.Model = 0;
            }

            result.State = true;
            result.Message = "Operacion Exitosa";
            result.Model = imc;

            return result;

        }

        [HttpPost("CalculoIngestaAgua")]
        public object CalculoIngestaAgua([FromBody] CalculoIngestaAguaViewModel model)
        {
            ResultBase result = new();
            double ingesta = MiSaludBusiness.CalculoIngestaAgua(model.Peso, model.Altura, model.Edad, model.NivelActividad);
            if (ingesta.Equals(0))
            {
                result.State = false;
                result.MessageException = "Operacion Invalida";
                result.Model = 0;
            }


            result.State = true;
            result.Message = "Operacion Exitosa";
            result.Model = ingesta;


            return result;

        }

        [HttpPost("CalculoMiPesoIdeal")]
        public object CalculoMiPesoIdeal([FromBody] CalculoMiPesoIdealViewModel model)
        {
            ResultBase result = MiSaludBusiness.CalculoMiPesoIdeal(model.Peso, model.Altura);
            return result;
        }

        [HttpPost("CalculoCaloriasDiarias")]
        public object CalculoCaloriasDiarias([FromBody] CalculoCaloriasDiariasViewModel model)
        {
            ResultBase result = new();

            double calorias = MiSaludBusiness.CalculoCaloriasDiarias(model.Peso, model.Altura, model.Edad, model.Genero, model.NivelActividad);
            if (calorias.Equals(0))
            {
                result.State = false;
                result.MessageException = "Operacion Invalida";
                result.Model = 0;
            }

            result.State = true;
            result.Message = "Operacion Exitosa";
            result.Model = calorias;
            return result;

        }

        [HttpPost("CalculoEdadMetabolica")]
        public object CalculoEdadMetabolica([FromBody] CalculoEdadMetabolicaViewModel model)
        {
            ResultBase result = new();

            double edad = MiSaludBusiness.CalculoEdadMetabolica(model.EdadCronologica, model.Imc);
            if (edad.Equals(0))
            {
                result.State = false;
                result.MessageException = "Operacion Invalida";
                result.Model = 0;
            }

            result.State = true;
            result.Message = "Operacion Exitosa";
            result.Model = edad;
            return result;

        }
    }
}
