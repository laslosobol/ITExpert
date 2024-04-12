using ITExpert.BLL.DTO;
using ITExpert.BLL.Interfaces;
using ITExpert.BLL.Mappers;
using ITExpert.BLL.Utils;
using ITExperts.DAL.Interfaces;

namespace ITExpert.BLL.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private CategoryMapper _categoryMapper;

    private CategoryMapper CategoryMapper => _categoryMapper ??= new CategoryMapper();

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        return CategoryMapper.Map(entity);
    }

    public async Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync()
    {
        var entities = await _unitOfWork.CategoryRepository.GetAllAsync();
        return entities.Select(_ => CategoryMapper.Map(_)).ToList();
    }

    public async Task UpdateCategoryAsync(CategoryDto dto)
    {
        if (await IsDescendant(dto.Id, dto.ParentCategoryId))
        {
            throw new InvalidOperationException("Circular dependency detected.");
        }
        var entity = CategoryMapper.Map(dto);
        _unitOfWork.CategoryRepository.Update(entity);
        
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (entity is null)
            throw new Exception();
        
        _unitOfWork.CategoryRepository.Delete(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task AddCategoryAsync(CategoryDto dto)
    {
        var entity = CategoryMapper.Map(dto);
        await _unitOfWork.CategoryRepository.InsertAsync(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task<IReadOnlyCollection<CategorySummary>> GetCategoriesSummary()
    {
        var entities = (await _unitOfWork.CategoryRepository.GetAllAsync()).ToList();
        var filmCategory = (await _unitOfWork.FilmCategoryRepository.GetAllAsync()).ToList();
        var result = new List<CategorySummary>();

        foreach (var entity in entities)
        {
            var temp = new CategorySummary
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentCategoryId = entity.ParentCategoryId,
                FilmCount = filmCategory.Count(_ => _.CategoryId == entity.Id)
            };
            temp.Level = 0;
            if (entity.ParentCategoryId is not null)
            {
                var levelCheck = entity;
                while (levelCheck.ParentCategoryId is not null)
                {
                    temp.Level++;
                    levelCheck = entities.FirstOrDefault(_ => _.Id == levelCheck.ParentCategoryId);
                }
            }
            result.Add(temp);
        }

        return result;
    }
    private async Task<bool> IsDescendant(int categoryId, int? potentialParentId)
    {
        if (categoryId == potentialParentId)
        {
            return true;
        }

        var category = await _unitOfWork.CategoryRepository.GetByIdWithoutTrackingAsync(categoryId);
        if (category?.ParentCategoryId == null)
        {
            return false;
        }

        return await IsDescendant(category.ParentCategoryId.Value, potentialParentId);
    }
}