using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.MVC.Services.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<SelectListItem>> GetSelectList();
}
