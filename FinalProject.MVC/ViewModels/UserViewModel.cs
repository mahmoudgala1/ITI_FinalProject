namespace FinalProject.MVC.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public string Image { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
