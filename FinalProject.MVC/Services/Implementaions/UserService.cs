using FinalProject.MVC.Data;
using FinalProject.MVC.Models;
using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.Settings;
using FinalProject.MVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.MVC.Services.Implementaions;

public class UserService : IUserService
{
    #region Fields
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _imagesPath;
    #endregion

    #region Constructors
    public UserService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
    }
    #endregion

    #region Handle Functions
    public async Task AddToRolesAsync(Employee employee, List<string> roles)
    {
        var Roles = new List<string>();
        foreach (var role in roles)
        {
            var roleObj = await _context.Roles.SingleOrDefaultAsync(r => r.Name == role);
            if (roleObj != null)
                Roles.Add(roleObj.Id);
        }
        var userRoles = Roles.Select(r => new UserRoles { EmployeeId = employee.Id, RoleId = r });
        await _context.UserRoles.AddRangeAsync(userRoles);
        await _context.SaveChangesAsync();
    }

    public async Task CreateAsync(CreateUserFormViewModel model)
    {
        var imageName = await SaveCover(model.Image);
        var user = new Employee
        {
            Id = Guid.NewGuid().ToString(),
            FName = model.FirstName,
            LName = model.LastName,
            Email = model.Email,
            Password = model.Password,
            DepartmentId = model.DepartmentId,
            Image = imageName,
        };
        var userRoles = model.SelectedRoles.Select(r => new UserRoles { EmployeeId = user.Id, RoleId = r });
        await _context.Employees.AddAsync(user);
        await _context.UserRoles.AddRangeAsync(userRoles);
        await _context.SaveChangesAsync();
    }

    public async Task<Employee> FindByEmailAsync(string email)
    {
        return await _context.Employees.SingleOrDefaultAsync(e => e.Email == email);
    }

    public async Task<Employee> FindByIdAsync(string id)
    {
        return await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<UserViewModel>> GetAllAsync()
    {
        var users = await _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .OrderBy(e => e.FName)
            .ThenBy(e => e.LName)
            .ToListAsync();

        var userS = users.Select(user => new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FName,
            LastName = user.LName,
            Email = user.Email,
            Phone = user.Phone ?? string.Empty,
            Department = user.DepartmentId != null ? user.Department.Name : string.Empty,
            Roles = GetRolesAsync(user).Result.Select(ur => ur.Role.Name) ?? new List<string>(),
            Image = user.Image ?? "default-image.png",
        }).ToList();
        return userS;
    }

    public async Task<List<UserRoles>> GetRolesAsync(Employee employee)
    {
        return await _context.UserRoles
            .AsNoTracking()
            .Include(ur => ur.Role)
            .Where(ur => ur.EmployeeId
            .Equals(employee.Id))
            .OrderBy(ur => ur.Role.Name)
            .ToListAsync();
    }

    public async Task<bool> IsInRoleAsync(Employee employee, string role)
    {
        var Role = await _context.UserRoles
            .SingleOrDefaultAsync(UR => UR.EmployeeId == employee.Id && UR.RoleId == role);
        if (Role == null)
            return false;
        return true;
    }

    public bool IsUserInRole(string userId, string roleName)
    {
        return _context.UserRoles
            .Include(ur => ur.Role)
            .Any(ur => ur.EmployeeId == userId && ur.Role.Name == roleName);
    }

    public async Task RemoveFromRoleAsync(Employee employee, string role)
    {
        var Role = await _context.UserRoles
            .SingleOrDefaultAsync(UR => UR.EmployeeId == employee.Id && UR.RoleId == role);
        if (Role != null)
        {
            _context.UserRoles.Remove(Role);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(EditUserFormViewModel model)
    {
        var user = await FindByIdAsync(model.Id);

        var hasNewImage = model.Image is not null;
        var oldImage = user.Image;

        user.FName = model.FirstName;
        user.LName = model.LastName;
        user.Email = model.Email;
        user.Phone = model.Phone;
        user.Password = model.Password;
        user.DepartmentId = model.DepartmentId != null ? model.DepartmentId : user.DepartmentId;
        if (hasNewImage)
        {
            user.Image = await SaveCover(model.Image!);
            if (oldImage != null)
            {
                var cover = Path.Combine(_imagesPath, oldImage);
                File.Delete(cover);
            }
        }
        var userRoles = await GetRolesAsync(user);
        if (model.SelectedRoles != null)
        {
            var deletedRoles = model.SelectedRoles.Count < userRoles.Count ?
                userRoles.Select(ur => ur.RoleId).Except(model.SelectedRoles) : null;
            if (deletedRoles != null)
            {
                foreach (var role in deletedRoles)
                {
                    await RemoveFromRoleAsync(user, role);
                }
            }
            foreach (var role in model.SelectedRoles)
            {
                if (!await IsInRoleAsync(user, role))
                {
                    await _context.UserRoles.AddAsync(new UserRoles { EmployeeId = user.Id, RoleId = role });
                }
            }
        }
        await _context.SaveChangesAsync();

    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await FindByIdAsync(id);
        if (user == null)
            return false;
        _context.Employees.Remove(user);
        var userRoles = await _context.UserRoles.Where(ur => ur.EmployeeId == id).ToListAsync();
        if (userRoles != null)
            _context.UserRoles.RemoveRange(userRoles);
        File.Delete(Path.Combine(_imagesPath, user.Image));
        await _context.SaveChangesAsync();
        return true;
    }
    private async Task<string> SaveCover(IFormFile image)
    {
        var iamgeName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var path = Path.Combine(_imagesPath, iamgeName);
        using var stream = File.Create(path);
        await image.CopyToAsync(stream);
        return iamgeName;
    }
    #endregion
}
