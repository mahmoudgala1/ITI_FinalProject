using FinalProject.MVC.Data;
using FinalProject.MVC.Models;
using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.MVC.Services.Implementaions;

public class AuthService : IAuthService
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion

    #region Constructors
    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Handle Functions
    public async Task<bool> Login(AuthFormViewModel model)
    {
        var user = await _context.Employees.SingleOrDefaultAsync(e => e.Email == model.LoginEmail);
        if (user == null)
        {
            model.Message = "The email or password you entered is incorrect. Please try again.";
            return false;
        }
        if (user.Password != model.LoginPassword)
        {
            model.Message = "The email or password you entered is incorrect. Please try again.";
            return false;
        }
        return true;
    }
    public async Task<bool> Register(AuthFormViewModel model)
    {
        var user = new Employee
        {
            Id = Guid.NewGuid().ToString(),
            FName = model.FirstName,
            LName = model.LastName,
            Email = model.Email,
            Password = model.Password
        };
        await _context.Employees.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }
    #endregion
}
