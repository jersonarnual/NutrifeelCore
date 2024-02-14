namespace NutrifeelCore.Infraestructure.DTO
{
    public class AllyDTO
    {
        public ICollection<DiplomaDTO> ListDiplomaDTO { get; set; }
    }

    public class DiplomaDTO
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string GraduationYear { get; set; }
        public string University { get; set; }
    }
}
