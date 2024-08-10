using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.MVC.ViewModels;

public class CreateUserFormViewModel:UserFormViewModel
{
    public IFormFile Image { get; set; } = default!;
}
