using ITExpert.BLL.DTO;
using ITExpert.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITExpert.API.Controllers;

[ApiController]
[Route("/category")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet("all")]
    public async Task<IEnumerable<CategoryDto>> All() => await _categoryService.GetAllCategoriesAsync();
}