namespace NutrifeelCore.Infraestructure.Settings
{
    public class Tokenconfiguration
    {
        public string JwtKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpireHours { get; set; }
    }

}
