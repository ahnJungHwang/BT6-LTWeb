using BTVN6.Helpers;
using BTVN6.Models;
using BTVN6.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTVN6.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _env;

    public ProductController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IWebHostEnvironment env)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    public async Task<IActionResult> Create()
    {
        await SetCategoriesSelectListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            var imageUrl = await ImageHelper.SaveImageAsync(imageFile, _env);
            if (imageUrl != null)
                product.ImageUrl = imageUrl;

            await _productRepository.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        await SetCategoriesSelectListAsync(product.CategoryId);
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();

        await SetCategoriesSelectListAsync(product.CategoryId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imageUrl = await ImageHelper.SaveImageAsync(imageFile, _env);
                if (imageUrl != null)
                    product.ImageUrl = imageUrl;
            }
            else
            {
                var existing = await _productRepository.GetByIdAsync(id);
                product.ImageUrl = existing?.ImageUrl;
            }

            await _productRepository.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        await SetCategoriesSelectListAsync(product.CategoryId);
        return View(product);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task SetCategoriesSelectListAsync(int? selectedId = null)
    {
        var categories = await _categoryRepository.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedId);
    }
}
