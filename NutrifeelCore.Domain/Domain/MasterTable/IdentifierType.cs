using NutrifeelCore.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NutrifeelCore.Domain.Domain.MasterTable
{
    public class IdentifierType : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Guid PersonId { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}