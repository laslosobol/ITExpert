using System.ComponentModel.DataAnnotations.Schema;

namespace ITExperts.DAL.Entities;

public class FilmCategory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int FilmId { get; set; }
    public Film Film { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}