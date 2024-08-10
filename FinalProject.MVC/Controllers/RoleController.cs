using FinalProject.MVC.Models;
using FinalProject.MVC.Services.Implementaions;
using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVC.Controllers;

public class RoleController : Controller
{
    #region Fields
    private readonly IRoleService _roleService;
    #endregion

    #region Constructors
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    #endregion

    #region Handle Functions
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _roleService.GetAllAsync());
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View(new CreateRoleFormViewModel());
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateRoleFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _roleService.CreateAsync(model);
        if (!result)
        {
            ModelState.AddModelError("RoleName", "Role is already exists");
            return View(model);
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleService.GetByIdAsync(id);
        return View(new EditRoleFormViewModel { Id = role.Id, RoleName = role.Name });
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(EditRoleFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _roleService.UpdateAsync(model);
        if (!result)
        {
            ModelState.AddModelError("RoleName", "Role is already exists");
            return View(model);
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _roleService.DeleteAsync(id);
        if (!result)
            return NotFound();
        return Ok();
    }
    #endregion
}
