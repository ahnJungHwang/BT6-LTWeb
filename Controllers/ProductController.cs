using BTVN6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BTVN6.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index(int? categoryId)
    {
        var categories = await _categoryRepository.GetAllAsync();
        ViewBag.Categories = categories;
        ViewBag.SelectedCategoryId = categoryId;

        var products = categoryId.HasValue
            ? await _productRepository.GetByCategoryIdAsync(categoryId.Value)
            : await _productRepository.GetAllAsync();

        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }
}
