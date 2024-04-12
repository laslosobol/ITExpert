using ITExpert.BLL.DTO;
using ITExpert.BLL.Utils;

namespace ITExpert.BLL.Interfaces;

public interface IFilmService
{
    Task<FilmDto> GetFilmByIdAsync(int id);
    Task<IReadOnlyCollection<FilmDto>> GetAllFilmsAsync();
    Task UpdateFilmAsync(FilmDto dto);
    Task DeleteFilmAsync(int id);
    Task AddFilmAsync(FilmDto dto);
    Task<IReadOnlyCollection<FilmSummary>> GetFilmsByFilter(FilmFilter filter);
    public Task<IEnumerable<int>> GetFilmCategoriesAsync (int id);
}