using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class AuthorizationDbContext : IdentityDbContext<AppUser>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity => entity.ToTable("Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable("Roles"));

            builder.Entity<IdentityUserRole<string>>(
                entity =>
                    entity.ToTable("UsersRoles"));

            builder.Entity<IdentityUserClaim<string>>(
                entity =>
                    entity.ToTable("UserClaim"));

            builder.Entity<IdentityUserLogin<string>>(
                entity =>
                    entity.ToTable("UserLogin"));

            builder.Entity<IdentityUserToken<string>>(
                entity =>
                    entity.ToTable("UserTokens"));

            builder.Entity<IdentityRoleClaim<string>>(
                entity =>
                    entity.ToTable("RoleClaims"));

            builder.ApplyConfiguration(new AppUserConfiguration());
        }
    }
}
