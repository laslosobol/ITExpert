using System.ComponentModel.DataAnnotations.Schema;

namespace ITExperts.DAL.Entities;

public class FilmCategory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int CategoryId { get; set; }
}