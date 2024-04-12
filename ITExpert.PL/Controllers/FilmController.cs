using ITExpert.BLL.DTO;
using ITExpert.BLL.Interfaces;
using ITExpert.BLL.Utils;
using ITExpert.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITExpert.PL.Controllers;

public class FilmController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IFilmService _filmService;
    private readonly IFilmCategoryService _filmCategoryService;

    public FilmController(IFilmService filmService, ICategoryService categoryService, IFilmCategoryService filmCategoryService)
    {
        _filmService = filmService;
        _categoryService = categoryService;
        _filmCategoryService = filmCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> All(FilmFilter filter = null)
    {
        var films = (await _filmService.GetFilmsByFilter(filter)).ToList();
        var result = new AllFilmsViewModel()
        {
            Films = films,
            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList(),
            SelectedCategories = filter?.CategoryIds ?? new List<int>()
        };

        return View(result);
    }
    
    [HttpGet]
    public IActionResult Create() => View(new CreateFilmViewModel());

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var film = await _filmService.GetFilmByIdAsync(id);
        if (film is null)
            return NotFound();

        var categories = await _filmService.GetFilmCategoriesAsync(film.Id);
        var result = new EditFilmModel()
        {
            Film = film,
            Categories = categories.ToList()
        };
        
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFilmViewModel model)
    {
        if (ModelState.IsValid)
        {
            var filmDto = new FilmDto() { Name = model.Name, Director = model.Director, ReleaseDate = model.ReleaseDate.ToUniversalTime()};
            await _filmService.AddFilmAsync(filmDto);
            return RedirectToAction("All");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditFilmModel model)
    {
        if (ModelState.IsValid)
        {
            var result = new FilmDto()
            {
                Id = model.Film.Id,
                Name = model.Film.Name,
                Director = model.Film.Director,
                ReleaseDate = model.Film.ReleaseDate
            };
            result.ReleaseDate = DateTime.SpecifyKind(result.ReleaseDate, DateTimeKind.Utc);
            await _filmService.UpdateFilmAsync(result);
            await _filmCategoryService.DeleteAllFilmCategoriesByFilmId(result.Id);
            if (!(model?.Categories.Count > 0))
            {
                return RedirectToAction("All");
            }
            foreach (var categoryId in model.Categories)
            {
                await _filmCategoryService.AddAsync(new FilmCategoryDto()
                {
                    FilmId = result.Id,
                    CategoryId = categoryId
                });
            }
            return RedirectToAction("All");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _filmService.DeleteFilmAsync(id);
        return RedirectToAction("All");
    }
}