using System.ComponentModel.DataAnnotations;

namespace FinalProject.MVC.ViewModels;

public class EditRoleFormViewModel
{
    public string Id { get; set; }
    [Required]
    [Display(Name = "Role Name")]
    [MinLength(3)]
    public string RoleName { get; set; }
}
