
using FinalProject.MVC.Data;
using FinalProject.MVC.Models;
using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.Settings;
using FinalProject.MVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.MVC.Services.Implementaions;
public class ProductServices : IProductServices
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _imagesPath;

    public ProductServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
    }
    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.Include(p => p.Category).AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Product> GetByName(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
    }
    public async Task<bool> Add(ProductViewModel model)
    {
        var added = false;
        var productWithSameName = await GetByName(model.Name);
        if (productWithSameName != null)
        {
            return added;
        }
        var imageName = await SetImage(model.Image);
        var product = new Product
        {
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
            ImageName = imageName,
            CategoryId = model.CategoryId
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        added = true;
        return added;
    }

    public async Task<Product> Edit(EditProductViewModel model)
    {
        var produt = await GetById(model.Id);
        if (produt is null)
            return null;
        var hasNewCover = model.Image is not null;
        var oldImage = produt.ImageName;
        produt.Name = model.Name;
        produt.CategoryId = model.CategoryId;
        produt.Price = model.Price;
        produt.Description = model.Description;
        if (hasNewCover)
        {
            produt.ImageName = await SetImage(model.Image);
        }
        var effectedRows = await _context.SaveChangesAsync();
        if (effectedRows > 0)
        {
            if (hasNewCover)
            {
                var image = Path.Combine(_imagesPath, oldImage);
                File.Delete(image);
            }
            return produt;
        }
        else
        {
            var image = Path.Combine(_imagesPath, produt.ImageName);
            File.Delete(image);

            return null;
        }

    }

    public async Task<bool> Delete(int id)
    {
        var isDeleted = false;

        var product = await _context.Products.FindAsync(id);

        if (product is null)
            return isDeleted;

        _context.Remove(product);
        var effectedRows = _context.SaveChanges();

        if (effectedRows > 0)
        {
            isDeleted = true;

            var image = Path.Combine(_imagesPath, product.ImageName);
            File.Delete(image);
        }

        return isDeleted;
    }

    private async Task<string> SetImage(IFormFile image)
    {
        var coverName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var path = Path.Combine(_imagesPath, coverName);
        using var fs = File.Create(path);
        await image.CopyToAsync(fs);
        return coverName;
    }
}
