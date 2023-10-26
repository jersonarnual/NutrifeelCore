using NutrifeelCore.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NutrifeelCore.Domain.Domain.MasterTable
{
    public class Profession : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
