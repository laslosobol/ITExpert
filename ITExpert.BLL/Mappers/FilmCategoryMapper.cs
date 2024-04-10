using ITExpert.BLL.DTO;
using ITExperts.DAL.Entities;

namespace ITExpert.BLL.Mappers;

public class FilmCategoryMapper : GenericMapper<FilmCategory, FilmCategoryDto>
{
    public override FilmCategory Map(FilmCategoryDto dtoEntity) => new FilmCategory()
    {
        Id = dtoEntity.Id,
        FilmId = dtoEntity.FilmId,
        CategoryId = dtoEntity.CategoryId
    };

    public override FilmCategoryDto Map(FilmCategory dataEntity) => new FilmCategoryDto()
    {
        Id = dataEntity.Id,
        FilmId = dataEntity.FilmId,
        CategoryId = dataEntity.CategoryId
    };
}