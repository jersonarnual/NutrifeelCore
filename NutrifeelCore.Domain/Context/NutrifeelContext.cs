using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NutrifeelCore.Domain.Domain;
using NutrifeelCore.Domain.Domain.Identity;
using NutrifeelCore.Domain.Domain.MasterTable;
using System.ComponentModel.DataAnnotations;

namespace NutrifeelCore.Domain.Context
{
    public class NutrifeelContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim,
                                                        ApplicationUserRole, ApplicationUserLogin,
                                                        ApplicationRoleClaim, ApplicationUserToken>
    {
        public NutrifeelContext()
        {
        }

        public NutrifeelContext(DbContextOptions<NutrifeelContext> options)
            : base(options)
        {
            // this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Coutry { get; set; }
        public virtual DbSet<IdentifierType> IdentifierTypes { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<CurriculumPerson> CurriculumPeople { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonDetail> PersonDetails { get; set; }
        public virtual DbSet<ProfessionalService> ProfessionalServices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasOne(t => t.ApplicationUser)
                .WithOne(u => u.Person)
                .HasForeignKey<Person>(t => t.ApplicationUserId);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }
}
