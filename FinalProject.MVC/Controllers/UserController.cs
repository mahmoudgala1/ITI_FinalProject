using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVC.Controllers;

public class UserController : Controller
{

    #region Fields
    private readonly IUserService _userService;
    private readonly IDepartmentService _departmentService;
    private readonly IRoleService _roleService;
    #endregion

    #region Constructors
    public UserController(IUserService userService, IDepartmentService departmentService, IRoleService roleService)
    {
        _userService = userService;
        _departmentService = departmentService;
        _roleService = roleService;
    }
    #endregion

    #region Handle Functions
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _userService.GetAllAsync());
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new CreateUserFormViewModel
        {
            Departments = _departmentService.GetSelectList().Result,
            Roles = _roleService.GetSelectList().Result,
        };
        return View(model);
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateUserFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model = new CreateUserFormViewModel
            {
                Departments = _departmentService.GetSelectList().Result,
                Roles = _roleService.GetSelectList().Result,
            };
            return View(model);
        }
        var userWithSameEmail = await _userService.FindByEmailAsync(model.Email);
        if (userWithSameEmail != null)
        {
            model = new CreateUserFormViewModel
            {
                Departments = _departmentService.GetSelectList().Result,
                Roles = _roleService.GetSelectList().Result,
            };
            ModelState.AddModelError("Email", "Email is already exists");
            return View(model);
        }
        await _userService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userService.FindByIdAsync(id);
        if (user is null)
            return NotFound();
        var model = new EditUserFormViewModel
        {
            Id = user.Id,
            FirstName = user.FName,
            LastName = user.LName,
            Email = user.Email,
            Phone = user.Phone,
            DepartmentId = user.DepartmentId,
            SelectedRoles = _userService.GetRolesAsync(user).Result.Select(ur => ur.RoleId).ToList(),
            Departments = _departmentService.GetSelectList().Result,
            Roles = _roleService.GetSelectList().Result,
            CurrentImage = user.Image
        };
        return View(model);
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(EditUserFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model = new EditUserFormViewModel
            {
                Departments = _departmentService.GetSelectList().Result,
                Roles = _roleService.GetSelectList().Result,
            };
            return View(model);
        }
        var userWithSameEmail = await _userService.FindByEmailAsync(model.Email);
        if (userWithSameEmail != null && userWithSameEmail.Id != model.Id)
        {
            model = new EditUserFormViewModel
            {
                Departments = _departmentService.GetSelectList().Result,
                Roles = _roleService.GetSelectList().Result,
            };
            ModelState.AddModelError("Email", "Email is already exists");
            return View(model);
        }
        await _userService.UpdateAsync(model);
        if (model.SelectedRoles == null)
        {
            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction(nameof(Index));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _userService.DeleteAsync(id);
        if (!result)
            return NotFound();
        return Ok();
    }
    #endregion

}
