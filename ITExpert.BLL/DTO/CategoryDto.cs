namespace ITExpert.BLL.DTO;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}