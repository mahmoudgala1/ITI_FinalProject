using FinalProject.MVC.Data;
using FinalProject.MVC.Models;
using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.MVC.Services.Implementaions;

public class RoleService : IRoleService
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion

    #region Constructors
    public RoleService(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Handle Functions
    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles.AsNoTracking().OrderBy(r => r.Name).ToListAsync();
    }

    public async Task<bool> RoleExistsAsync(string role)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == role) != null ? true : false;
    }
    public async Task<bool> CreateAsync(CreateRoleFormViewModel model)
    {
        if (await RoleExistsAsync(model.RoleName))
            return false;
        await _context.Roles.AddAsync(new Role { Id = Guid.NewGuid().ToString(), Name = model.RoleName });
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Role> GetByIdAsync(string id)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id.Equals(id)); ;
    }

    public async Task<bool> UpdateAsync(EditRoleFormViewModel model)
    {
        var role = await GetByIdAsync(model.Id);
        if (role is null)
            return false;
        var roleWithSameName = await GetByNameAsync(model.RoleName);
        if (roleWithSameName != null && role.Id != roleWithSameName.Id)
            return false;
        role.Name = model.RoleName;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Role> GetByNameAsync(string name)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals(name));
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await GetByIdAsync(id);
        if (result is null)
            return false;
        _context.Roles.Remove(result);
        _context.SaveChanges();
        return true;
    }
    public async Task<IEnumerable<SelectListItem>> GetSelectList()
    {
        return await _context.Roles
            .Select(r => new SelectListItem { Value = r.Id, Text = r.Name })
            .OrderBy(c => c.Text)
            .AsNoTracking()
            .ToListAsync();
    }
    #endregion
}
