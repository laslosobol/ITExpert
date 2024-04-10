using ITExpert.BLL.DTO;
using ITExperts.DAL.Entities;

namespace ITExpert.BLL.Mappers;

public class FilmMapper : GenericMapper<Film, FilmDto>
{
    public override Film Map(FilmDto dtoEntity) => new Film()
    {
        Id = dtoEntity.Id,
        Name = dtoEntity.Name,
        Director = dtoEntity.Director,
        ReleaseDate = dtoEntity.ReleaseDate
    };

    public override FilmDto Map(Film dataEntity)=> new FilmDto()
    {
        Id = dataEntity.Id,
        Name = dataEntity.Name,
        Director = dataEntity.Director,
        ReleaseDate = dataEntity.ReleaseDate
    };
}