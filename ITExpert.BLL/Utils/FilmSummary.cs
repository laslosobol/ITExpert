using ITExpert.BLL.DTO;

namespace ITExpert.BLL.Utils;

public class FilmSummary
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<CategoryDto> Categories { get; set; }
}