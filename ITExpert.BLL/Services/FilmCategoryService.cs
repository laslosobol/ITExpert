using ITExpert.BLL.DTO;
using ITExpert.BLL.Interfaces;
using ITExpert.BLL.Mappers;
using ITExperts.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITExpert.BLL.Services;

public class FilmCategoryService : IFilmCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private FilmCategoryMapper _filmCategoryMapper;
    
    private FilmCategoryMapper FilmCategoryMapper => _filmCategoryMapper ??= new FilmCategoryMapper();

    public FilmCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<FilmCategoryDto> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.FilmCategoryRepository.GetByIdAsync(id);
        return FilmCategoryMapper.Map(entity);
    }

    public async Task<IReadOnlyCollection<FilmCategoryDto>> GetAllAsync()
    {
        var entities = await _unitOfWork.FilmCategoryRepository.GetAllAsync();
        return entities.Select(_ => FilmCategoryMapper.Map(_)).ToList();
    }

    public async Task UpdateAsync(FilmCategoryDto dto)
    {
        var entity = FilmCategoryMapper.Map(dto);
        _unitOfWork.FilmCategoryRepository.Update(entity);
        
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _unitOfWork.FilmCategoryRepository.GetByIdAsync(id);
        if (entity is null)
            throw new Exception();
        
        _unitOfWork.FilmCategoryRepository.Delete(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task AddAsync(FilmCategoryDto dto)
    {
        var entity = FilmCategoryMapper.Map(dto);
        await _unitOfWork.FilmCategoryRepository.InsertAsync(entity);
        
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAllFilmCategoriesByFilmId(int id)
    {
        var entity = (await _unitOfWork.FilmCategoryRepository.GetAllAsync()).Where(_ => _.FilmId == id).ToList();
        foreach (var filmCategory in entity)
        {
            _unitOfWork.FilmCategoryRepository.Delete(filmCategory);
        }

        await _unitOfWork.CommitAsync();
    }
}