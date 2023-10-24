using NutrifeelCore.Infraestructure.Settings;

namespace NutrifeelCore.Business.MiSalud
{
    public static class MiSaludBusiness
    {

        public static double CalculoImc(double peso, double altura)
        {
            if (peso > 0 && altura > 0)
                return peso / (altura * altura);
            else return 0;
        }

        public static double CalculoIngestaAgua(double peso, double altura, int edad, int nivelActividad)
        {

            double tmb = 0;
            if (nivelActividad == (int)EnumNivelActividad.Sedentario)
                tmb = 655 + 9.6 * peso + 1.8 * altura - 4.7 * edad;

            if (nivelActividad == (int)EnumNivelActividad.Moderado)
            {
                tmb = 655 + 9.6 * peso + 1.8 * altura - 4.7 * edad;
                tmb *= 1.375; // Factor de actividad
            }
            if (nivelActividad == (int)EnumNivelActividad.Activo)
            {
                tmb = 655 + 9.6 * peso + 1.8 * altura - 4.7 * edad;
                tmb *= 1.55; // Factor de actividad
            }

            if (nivelActividad == (int)EnumNivelActividad.Activo)
            {
                tmb = 655 + 9.6 * peso + 1.8 * altura - 4.7 * edad;
                tmb *= 1.725; // Factor de actividad
            }

            // La ingesta diaria recomendada de agua es aproximadamente 35 ml por cada kilogramo de peso corporal
            // Se multiplica por 1.2 para ajustar según las necesidades del cuerpo
            double ingestaAgua = tmb * 35 * 1.2;

            return ingestaAgua;

        }

        public static ResultBase CalculoMiPesoIdeal(double peso, double altura)
        {
            ResultBase result = new();
            double imc = CalculoImc(peso, altura);

            if (imc.Equals(0))
            {
                result.State = false;
                result.MessageException = "imc es igual a 0";
                return result;
            }
            result.Model = imc;
            result.State = true;
            if (imc < 18.5)
                result.Message = "Bajo peso";
            else if (imc >= 18.5 && imc < 25)
                result.Message = "Peso Normal";
            else if (imc >= 25 && imc < 30)
                result.Message = "Sobrepeso";
            else
                result.Message = "Obesidad";

            return result;
        }

        public static double CalculoCaloriasDiarias(double peso, double altura, int edad, char genero, int nivelActividad)
        {
            double tmb = 0;
            double requerimientoCalorico = 0;

            if (genero == 'M' || genero == 'm')
                tmb = 88.362 + (13.397 * peso) + (4.799 * altura) - (5.677 * edad);

            if (genero == 'F' || genero == 'f')
                tmb = 447.593 + (9.247 * peso) + (3.098 * altura) - (4.330 * edad);

            switch (nivelActividad)
            {
                case (int)EnumNivelActividad.Sedentario:
                    requerimientoCalorico = tmb * 1.2; // Sedentario
                    break;
                case (int)EnumNivelActividad.Moderado:
                    requerimientoCalorico = tmb * 1.375; // Moderadamente activo
                    break;
                case (int)EnumNivelActividad.Activo:
                    requerimientoCalorico = tmb * 1.55; // Activo
                    break;
                case (int)EnumNivelActividad.SuperActivo:
                    requerimientoCalorico = tmb * 1.725; // Muy activo
                    break;
                default:
                    requerimientoCalorico = 0;
                    break;
            }
            return requerimientoCalorico;
        }

        public static int CalculoEdadMetabolica(int edadCronologica, double imc)
        {
            int edadMetabolica = edadCronologica;

            // Si el IMC está fuera del rango normal (18.5 - 24.9), ajusta la edad metabólica
            if (imc < 18.5)
                // Si el IMC es bajo, la edad metabólica es mayor
                edadMetabolica += 5;

            if (imc > 24.9)
                // Si el IMC es alto, la edad metabólica es menor
                edadMetabolica -= 5;

            return edadMetabolica;
        }

    }
}
