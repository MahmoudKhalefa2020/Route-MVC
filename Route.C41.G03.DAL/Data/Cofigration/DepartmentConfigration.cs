using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.G03.DAL.Models;

namespace Route.C41.G03.DAL.Data.Cofigration
{
    public class DepartmentConfigration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(D => D.Code)
                   .HasColumnType("varchar")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasMany(D => D.Employees)
                    .WithOne(E => E.Department)
                    .HasForeignKey(E => E.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
