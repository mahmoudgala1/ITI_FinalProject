using FinalProject.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.MVC.Configurations;

public class UserRolesEntityTypeConfiguration : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.HasKey(ur => new { ur.EmployeeId, ur.RoleId });

        builder.Property(ur => ur.EmployeeId).IsRequired(true);

        builder.Property(ur => ur.RoleId).IsRequired(true);

        builder.HasOne(ur => ur.Employee).WithMany().HasForeignKey(ur => ur.EmployeeId);

        builder.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId);
    }
}
