using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITExperts.DAL.Entities;

public class Film
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    [Required]
    [StringLength(200)]
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<FilmCategory> FilmCategories { get; set; }
}