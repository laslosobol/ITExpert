using System.ComponentModel.DataAnnotations.Schema;

namespace ITExperts.DAL.Entities;

public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}