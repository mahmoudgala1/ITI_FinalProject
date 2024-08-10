using FinalProject.MVC.Models;
using FinalProject.MVC.ViewModels;

namespace FinalProject.MVC.Services.Interfaces;

public interface IUserService
{
    Task<List<UserViewModel>> GetAllAsync();
    Task<Employee> FindByEmailAsync(string email);
    Task<Employee> FindByIdAsync(string id);
    Task CreateAsync(CreateUserFormViewModel model);
    Task UpdateAsync(EditUserFormViewModel employee);
    Task<bool> DeleteAsync(string id);
    Task<List<UserRoles>> GetRolesAsync(Employee employee);
    Task<bool> IsInRoleAsync(Employee employee, string role);
    bool IsUserInRole(string userId, string roleName);
    Task AddToRolesAsync(Employee employee, List<string> roles);
    Task RemoveFromRoleAsync(Employee employee, string role);
}
