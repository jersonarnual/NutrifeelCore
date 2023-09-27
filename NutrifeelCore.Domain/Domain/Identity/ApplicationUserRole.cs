using Microsoft.AspNetCore.Identity;

namespace NutrifeelCore.Domain.Domain.Identity
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
