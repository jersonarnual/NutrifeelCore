using NutrifeelCore.Domain.Common;
using NutrifeelCore.Domain.Domain.Identity;
using NutrifeelCore.Domain.Domain.MasterTable;

namespace NutrifeelCore.Domain.Domain
{
    public class Person : EntityBase
    {
        public string Number { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid? ApplicationUserId { get; set; }
        public int MyProperty { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual PersonDetail PersonDetail { get; set; }
        public virtual IdentifierType IdentifierType { get; set; }

    }
}
