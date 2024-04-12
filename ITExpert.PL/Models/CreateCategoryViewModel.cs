namespace ITExpert.PL.Models;

public class CreateCategoryViewModel
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}