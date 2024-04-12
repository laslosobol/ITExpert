using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITExperts.DAL.Entities;

public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }

    public ICollection<Category> Subcategories { get; set; }
    public ICollection<FilmCategory> FilmCategories { get; set; }
}