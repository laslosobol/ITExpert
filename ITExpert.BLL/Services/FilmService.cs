using ITExpert.BLL.DTO;
using ITExpert.BLL.Interfaces;
using ITExpert.BLL.Mappers;
using ITExpert.BLL.Utils;
using ITExperts.DAL.Interfaces;

namespace ITExpert.BLL.Services;

public class FilmService : IFilmService
{
    private readonly IUnitOfWork _unitOfWork;
    private FilmMapper _filmMapper;
    private CategoryMapper _categoryMapper;

    private FilmMapper FilmMapper => _filmMapper ??= new FilmMapper();
    private CategoryMapper CategoryMapper => _categoryMapper ??= new CategoryMapper();

    public FilmService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<FilmDto> GetFilmByIdAsync(int id)
    {
        var entity = await _unitOfWork.FilmRepository.GetByIdAsync(id);
        return FilmMapper.Map(entity);
    }

    public async Task<IReadOnlyCollection<FilmDto>> GetAllFilmsAsync()
    {
        var entities = await _unitOfWork.FilmRepository.GetAllAsync();
        return entities.Select(_ => FilmMapper.Map(_)).ToList();
    }
    public async Task<IEnumerable<int>> GetFilmCategoriesAsync(int id)
    {
        var filmCategories= (await _unitOfWork.FilmCategoryRepository.GetAllAsync()).ToList();
        var temp = filmCategories.Where(_ => _.FilmId == id).Select(_ => _.CategoryId);
        return temp;
    }

    public async Task UpdateFilmAsync(FilmDto dto)
    {
        var entity = FilmMapper.Map(dto);
        _unitOfWork.FilmRepository.Update(entity);
        
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteFilmAsync(int id)
    {
        var entity = await _unitOfWork.FilmRepository.GetByIdAsync(id);
        if (entity is null)
            throw new Exception();
        
        _unitOfWork.FilmRepository.Delete(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task AddFilmAsync(FilmDto dto)
    {
        var entity = FilmMapper.Map(dto);
        await _unitOfWork.FilmRepository.InsertAsync(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyCollection<FilmSummary>> GetFilmsByFilter(FilmFilter filter)
    {

        var entities = (await _unitOfWork.FilmRepository.GetAllAsync()).ToList();
        
        if (filter is not null)
        {
            if (!string.IsNullOrEmpty(filter.Director))
                entities = entities.Where(_ => _.Director == filter.Director).ToList();
            if (filter.ReleaseDateFrom is not null)
                entities = entities.Where(_ => _.ReleaseDate >= filter.ReleaseDateFrom).ToList();
            if (filter.ReleaseDateTo is not null)
                entities = entities.Where(_ => _.ReleaseDate <= filter.ReleaseDateTo).ToList();
            if (!string.IsNullOrEmpty(filter.Name))
                entities = entities.Where(_ => _.Name.Contains(filter.Name)).ToList();
        }

        var filmCategories = (await _unitOfWork.FilmCategoryRepository.GetAllAsync()).ToList();
        var categories = (await _unitOfWork.CategoryRepository.GetAllAsync()).ToList();

        var result = new List<FilmSummary>();

        if (entities.Count == 0)
            return result;

        foreach (var entity in entities)
        {
            var categoryIds = filmCategories.Where(_ => _.FilmId == entity.Id).ToList();
            var entityCategories = categories.Where(_ => categoryIds.Select(_ => _.CategoryId).ToList().Contains(_.Id)).ToList();

            var toAdd = new FilmSummary()
            {
                Id = entity.Id,
                Name = entity.Name,
                Director = entity.Director,
                ReleaseDate = entity.ReleaseDate,
                Categories = new List<CategoryDto>()
            };
            toAdd.Categories = entityCategories.Select(_ => CategoryMapper.Map(_)).ToList();
            result.Add(toAdd);

        }
        
        return result;
    }
}