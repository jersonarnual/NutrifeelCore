namespace NutrifeelCore.Api.Model.Ally
{
    public class AllyViewModel
    {
        public string Document { get; set; }
        public ICollection<DiplomaViewModel> Diplomas { get; set; }
    }

    public class DiplomaViewModel
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string GraduationYear { get; set; }
        public string University { get; set; }
    }
}
