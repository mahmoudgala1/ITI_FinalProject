
using FinalProject.MVC.Models;
using FinalProject.MVC.ViewModels;

namespace FinalProject.MVC.Services.Interfaces;
public interface IProductServices
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int id);
    Task<Product> GetByName(string name);
    Task<bool> Add(ProductViewModel model);
    Task<Product> Edit(EditProductViewModel model);
    Task<bool> Delete(int id);
}

