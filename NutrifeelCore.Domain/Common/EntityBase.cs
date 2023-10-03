using System.ComponentModel.DataAnnotations;

namespace NutrifeelCore.Domain.Common
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
