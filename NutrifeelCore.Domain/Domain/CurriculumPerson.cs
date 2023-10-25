using NutrifeelCore.Domain.Common;
using NutrifeelCore.Domain.Domain.MasterTable;

namespace NutrifeelCore.Domain.Domain
{
    public class CurriculumPerson : EntityBase
    {

        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Graduate { get; set; }
        public Guid PersonId { get; set; }
        public Guid LanguagesId { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Languages> Languages{ get; set; }
    }
}
