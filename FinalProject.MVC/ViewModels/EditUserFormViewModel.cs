using FinalProject.MVC.Settings;

namespace FinalProject.MVC.ViewModels;

public class EditUserFormViewModel : UserFormViewModel
{
    public string Id { get; set; }
    public string Phone { get; set; }
    public string? CurrentImage { get; set; }
    public IFormFile? Image { get; set; } = default!;
}
