using ITExpert.BLL.DTO;
using ITExpert.BLL.Utils;

namespace ITExpert.BLL.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> GetCategoryByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(CategoryDto dto);
    Task DeleteCategoryAsync(int id);
    Task AddCategoryAsync(CategoryDto dto);
    Task<IEnumerable<CategorySummary>> GetCategoriesSummary();
}