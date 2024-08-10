using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.MVC.Services.Interfaces;
public interface ICategoriesService
{
    IEnumerable<SelectListItem> GetSelectList();
}

