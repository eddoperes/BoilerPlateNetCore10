using BoilerPlateNetCore10.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Infra.Data.EntityConfiguration
{
    /*
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Since).IsRequired();
            builder.ToTable("People");
        }

    }
    */
}
