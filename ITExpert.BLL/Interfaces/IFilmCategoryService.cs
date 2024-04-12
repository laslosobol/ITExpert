using ITExpert.BLL.DTO;

namespace ITExpert.BLL.Interfaces;

public interface IFilmCategoryService
{
    Task<FilmCategoryDto> GetByIdAsync(int id);
    Task<IReadOnlyCollection<FilmCategoryDto>> GetAllAsync();
    Task UpdateAsync(FilmCategoryDto dto);
    Task DeleteAsync(int id);
    Task AddAsync(FilmCategoryDto dto);
    Task DeleteAllFilmCategoriesByFilmId(int id);
}