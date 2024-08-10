using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;


namespace FinalProject.MVC.Controllers;

public class ProductController : Controller
{
    private readonly IProductServices _productServices;
    private readonly ICategoriesService _categoriesService;

    public ProductController(IProductServices productServices, ICategoriesService categoriesService)
    {
        _productServices = productServices;
        _categoriesService = categoriesService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _productServices.GetAll());
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _productServices.GetById(id));
    }

    [HttpGet]
    public IActionResult Add()
    {
        var product = new ProductViewModel
        {
            Categories = _categoriesService.GetSelectList()
        };
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = _categoriesService.GetSelectList();
            return View(model);
        }
        var result = await _productServices.Add(model);
        if (!result)
        {
            ModelState.AddModelError("Name", "There Is Product With Same Name");
            return View(model);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productServices.GetById(id);
        if (product is null)
            return NotFound();
        var model = new EditProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Categories = _categoriesService.GetSelectList(),
            Price = product.Price,
            Description = product.Description,
            CurrentImage = product.ImageName
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = _categoriesService.GetSelectList();
            return View(model);
        }
        var product = await _productServices.GetByName(model.Name);
        if (product != null && product.Id != model.Id)
        {
            ModelState.AddModelError("Name", "There Is Product With Same Name");
            return View(model);
        }
        await _productServices.Edit(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await _productServices.Delete(id);
        return isDeleted ? Ok() : BadRequest();
    }
}
