using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.MVC.ViewModels;
public class ProductViewModel
{
    public string Name { get; set; }
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    public decimal Price { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
}

