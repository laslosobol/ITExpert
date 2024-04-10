using ITExpert.BLL.DTO;

namespace ITExpert.BLL.Utils;

public class CategorySummary
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public int Level { get; set; }
    public int FilmCount { get; set; }
    public List<FilmDto> Films { get; set; }
}