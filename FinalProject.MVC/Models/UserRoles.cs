using System.Data;

namespace FinalProject.MVC.Models;

public class UserRoles
{
    public string EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public string RoleId { get; set; }
    public virtual Role Role { get; set; }
}
