using FinalProject.MVC.Data;
using FinalProject.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.MVC.Services.Implementaions;

public class DepartmentService : IDepartmentService
{
    #region Fields
    private readonly ApplicationDbContext _context;
    #endregion

    #region Constructors
    public DepartmentService(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Handle Functions
    public async Task<IEnumerable<SelectListItem>> GetSelectList()
    {
        return await _context.Departments
            .Select(d => new SelectListItem { Value = d.Id, Text = d.Name })
            .OrderBy(c => c.Text)
            .AsNoTracking()
            .ToListAsync();
    }
    #endregion
}
