using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BoilerPlateNetCore10.Domain.Entities;

namespace BoilerPlateNetCore10.Infra.Data.EntityConfiguration
{
    public class UserSensitiveDataConfiguration : IEntityTypeConfiguration<UserSensitiveData>
    {
        public void Configure(EntityTypeBuilder<UserSensitiveData> builder)
        {
            builder.HasKey(c => c.UserId);
            builder.Property(c => c.Password).HasMaxLength(200).IsRequired(); ;
            builder.ToTable("UsersSensitiveData");
        }
    }
}
