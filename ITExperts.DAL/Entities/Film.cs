using System.ComponentModel.DataAnnotations.Schema;

namespace ITExperts.DAL.Entities;

public class Film
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }

}