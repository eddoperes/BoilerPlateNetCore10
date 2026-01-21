using BoilerPlateNetCore10.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoilerPlateNetCore10.Infra.Data.EntityConfiguration
{
    public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
    {

        public void Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            builder.ComplexProperty(c => c.CNPJ).Property(c=> c.Number).HasMaxLength(14).IsRequired();
            builder.ComplexProperty(c => c.Email).Property(c => c.Address).HasMaxLength(100).IsRequired();
            builder.ComplexProperty(c => c.Phone).Property(c => c.Number).HasMaxLength(11).IsRequired();            
            builder.ComplexProperty(c => c.Address).Property(c => c.Street).HasMaxLength(50).IsRequired();
            builder.ComplexProperty(c => c.Address).Property(c => c.Neighborhood).HasMaxLength(50).IsRequired();
            builder.ComplexProperty(c => c.Address).Property(c => c.Complement).HasMaxLength(50).IsRequired();
            builder.ComplexProperty(c => c.Address).Property(c => c.State).HasMaxLength(2).IsRequired();
            builder.ComplexProperty(c => c.Address).Property(c => c.City).HasMaxLength(50).IsRequired();
            builder.ComplexProperty(c => c.Address).Property(c => c.ZipCode).HasMaxLength(8).IsRequired();            
            builder.ToTable("Enterprises");
        }

    }
}
