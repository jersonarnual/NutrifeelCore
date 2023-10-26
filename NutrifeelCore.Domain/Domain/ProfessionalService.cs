using NutrifeelCore.Domain.Common;
using NutrifeelCore.Domain.Domain.MasterTable;
using System.ComponentModel.DataAnnotations;

namespace NutrifeelCore.Domain.Domain
{
    public class ProfessionalService : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Guid ProfessionId { get; set; }
        public Guid CurriculumPersonId { get; set; }
        public Profession Profession { get; set; }
        public virtual CurriculumPerson CurriculumPerson { get; set; }

    }
}
