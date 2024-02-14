using NutrifeelCore.Domain.Common;
using NutrifeelCore.Domain.Domain.MasterTable;

namespace NutrifeelCore.Domain.Domain
{
    public class PersonDetail : EntityBase
    {
        public string Description { get; set; }
        public string ExplanationFirstSession { get; set; }
        public string Style { get; set; }
        public Guid PersonId { get; set; }
        public Guid CityId { get; set; }
        public virtual ICollection<Person> Persons { get; set;}
        public virtual ICollection<City> City { get; set; }

    }
}
