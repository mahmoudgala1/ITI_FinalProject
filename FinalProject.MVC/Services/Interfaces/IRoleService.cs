using FinalProject.MVC.Models;
using FinalProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.MVC.Services.Interfaces;

public interface IRoleService
{
    Task<List<Role>> GetAllAsync();
    Task<Role> GetByIdAsync(string id);
    Task<Role> GetByNameAsync(string name);
    Task<bool> RoleExistsAsync(string role);
    Task<bool> CreateAsync(CreateRoleFormViewModel model);
    Task<bool> UpdateAsync(EditRoleFormViewModel model);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<SelectListItem>> GetSelectList();
}
