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
    public async Task<IActionResult> Index(int id) => View(await _categoryService.GetCategoryByIdAsync(id));
    
    [HttpGet]
    public async Task<IActionResult> All() => View(await _categoryService.GetCategoriesSummary());

    [HttpGet]
    public IActionResult Create() => View();

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category is null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryViewModel model)
    {
        if (ModelState.IsValid)
        {
            var categoryDto = new CategoryDto() { Name = model.Name, ParentCategoryId = model.ParentCategoryId };
            // if (!(await ValidateCategory(categoryDto)))
            //     return StatusCode(StatusCodes.Status304NotModified);
            await _categoryService.AddCategoryAsync(categoryDto);
            return RedirectToAction("All");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto model)
    {
        if (ModelState.IsValid)
        {
            // if (!(await ValidateCategory(model)))
            //     return StatusCode(StatusCodes.Status304NotModified);
            await _categoryService.UpdateCategoryAsync(model);
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

    // private async Task<bool> ValidateCategory(CategoryDto dto)
    // {
    //     if (dto.ParentCategoryId is not null && dto.ParentCategoryId != 0)
    //     {
    //         var categories = (await _categoryService.GetAllCategoriesAsync()).ToList();
    //         var children = categories.Where(_ => _.ParentCategoryId == dto.Id);
    //         
    //     }
    //     return true;
    // }
}