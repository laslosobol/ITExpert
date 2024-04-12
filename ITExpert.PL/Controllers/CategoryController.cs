using ITExpert.BLL.DTO;
using ITExpert.BLL.Interfaces;
using ITExpert.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITExpert.PL.Controllers;

public class CategoryController : Controller
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> All() => View(await _categoryService.GetCategoriesSummary());
    [HttpGet]
    public async Task<IEnumerable<CategoryDto>> List() => await _categoryService.GetAllCategoriesAsync();

    [HttpGet]
    public IActionResult Create() => View(new CreateCategoryViewModel());

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category is null)
            return NotFound();
        var result = new CreateCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId
        };
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryViewModel model)
    {
        if (ModelState.IsValid)
        {
            var categoryDto = new CategoryDto() { Name = model.Name, ParentCategoryId = model.ParentCategoryId };
            await _categoryService.AddCategoryAsync(categoryDto);
            return RedirectToAction("All");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CreateCategoryViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategoryAsync(new CategoryDto()
            {
                Id = (int)model.Id!,
                Name = model.Name,
                ParentCategoryId = model.ParentCategoryId
            });
            return RedirectToAction("All");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return RedirectToAction("All");
    }
}