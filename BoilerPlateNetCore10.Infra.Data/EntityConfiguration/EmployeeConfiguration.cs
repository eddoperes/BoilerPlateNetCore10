using BoilerPlateNetCore10.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlateNetCore10.Infra.Data.EntityConfiguration
{
    /*
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Admission).IsRequired();
            builder.ToTable("People");
        }

    }
    */
}
