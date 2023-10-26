using NutrifeelCore.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NutrifeelCore.Domain.Domain.MasterTable
{
    public class Country : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Guid CityId { get; set; }
        public virtual ICollection<City> Cities{ get; set;}
    }
}
