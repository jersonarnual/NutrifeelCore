using Microsoft.AspNetCore.Identity;

namespace NutrifeelCore.Domain.Domain.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
