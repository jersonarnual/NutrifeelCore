namespace NutrifeelCore.Api.Model.MiSalud
{
    public class CalculoCaloriasDiariasViewModel
    {
        public double Peso { get; set; }
        public double Altura { get; set; }
        public int Edad { get; set; }
        public char Genero { get; set; }
        public int NivelActividad { get; set; }
    }
}
