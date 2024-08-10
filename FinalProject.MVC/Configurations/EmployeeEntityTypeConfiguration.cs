using FinalProject.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.MVC.Configurations;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired(true);

        builder.Property(e => e.FName).HasMaxLength(50).IsRequired(true);

        builder.Property(e => e.LName).HasMaxLength(50).IsRequired(true);

        builder.Property(e => e.Email).IsRequired(true);

        builder.Property(e => e.Password).IsRequired(true);

        builder.Property(e => e.DepartmentId).IsRequired(false);

        builder.Property(e => e.Phone).IsRequired(false);

        builder.Property(e => e.Image).IsRequired(false);

        builder.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId);

    }
}
