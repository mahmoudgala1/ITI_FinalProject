namespace FinalProject.MVC.Models;

public class Employee
{
    public string Id { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Image { get; set; }
    public string DepartmentId { get; set; }
    public virtual Department Department { get; set; }
}
