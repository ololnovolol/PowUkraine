using IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.BirthDay).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.NormalizedUserName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(250).IsRequired();
            builder.Property(x => x.EmailConfirmed).HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.PhoneNumberConfirmed).HasMaxLength(20);
        }
    }
}
