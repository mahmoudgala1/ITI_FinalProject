using FinalProject.MVC.ViewModels;

namespace FinalProject.MVC.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Login(AuthFormViewModel model);
    Task<bool> Register(AuthFormViewModel model);
}
