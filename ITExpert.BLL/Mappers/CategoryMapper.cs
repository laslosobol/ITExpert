using ITExpert.BLL.DTO;
using ITExperts.DAL.Entities;

namespace ITExpert.BLL.Mappers;

public class CategoryMapper : GenericMapper<Category, CategoryDto>
{
    public override Category Map(CategoryDto dtoEntity) => new Category()
    {
        Id = dtoEntity.Id,
        Name = dtoEntity.Name,
        ParentCategoryId = dtoEntity.ParentCategoryId
    };

    public override CategoryDto Map(Category dataEntity)=> new CategoryDto()
    {
        Id = dataEntity.Id,
        Name = dataEntity.Name,
        ParentCategoryId = dataEntity.ParentCategoryId
    };
}