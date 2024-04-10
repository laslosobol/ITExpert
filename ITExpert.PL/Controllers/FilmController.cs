using ITExpert.BLL.DTO;
using ITExpert.BLL.Interfaces;
using ITExpert.BLL.Utils;
using ITExpert.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITExpert.PL.Controllers;

public class FilmController : Controller
{
    private ICategoryService _categoryService;
    private IFilmService _filmService;

    public FilmController(IFilmService filmService, ICategoryService categoryService)
    {
        _filmService = filmService;
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(int id) => View(await _filmService.GetFilmByIdAsync(id));

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
    public IActionResult Create() => View();

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var film = await _filmService.GetFilmByIdAsync(id);
        if (film is null)
            return NotFound();

        return View(film);
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
    public async Task<IActionResult> Edit(FilmDto model)
    {
        if (ModelState.IsValid)
        {
            model.ReleaseDate = DateTime.SpecifyKind(model.ReleaseDate, DateTimeKind.Utc);
            await _filmService.UpdateFilmAsync(model);
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