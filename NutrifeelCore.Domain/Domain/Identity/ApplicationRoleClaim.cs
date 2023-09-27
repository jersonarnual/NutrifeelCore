using Microsoft.AspNetCore.Identity;

namespace NutrifeelCore.Domain.Domain.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }

    }
}
