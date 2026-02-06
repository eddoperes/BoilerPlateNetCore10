using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BoilerPlateNetCore10.Domain.Entities;

namespace BoilerPlateNetCore10.Infra.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            builder.Property(c => c.PermissionLevel).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Login).HasMaxLength(100).IsRequired();
            builder.Property(c => c.RefreshToken).HasMaxLength(500);
            builder.ToTable("Users");
        }

    }
}
