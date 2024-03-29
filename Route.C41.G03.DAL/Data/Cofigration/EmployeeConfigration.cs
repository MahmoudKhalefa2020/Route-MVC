using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.G03.DAL.Models;
using System;

namespace Route.C41.G03.DAL.Data.Cofigration
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(E => E.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(E => E.Address)
                   .IsRequired();

            builder.Property(E => E.Salary)
                   .HasColumnType("decimal(12,2)");

            builder.Property(E => E.Gender)
                   .HasConversion(
                    (Gender) => Gender.ToString(),
                    (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true)
                    );


        }
    }
}
