using Microsoft.AspNetCore.Identity;

namespace NutrifeelCore.Domain.Domain.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public virtual ApplicationUser User { get; set; }

    }
}
