using System.ComponentModel.DataAnnotations;

namespace FinalProject.MVC.ViewModels;

public class CreateRoleFormViewModel
{
    [Required]
    [Display(Name = "Role Name")]
    [MinLength(3)]
    public string RoleName { get; set; }
}
