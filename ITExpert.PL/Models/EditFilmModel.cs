using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using ITExpert.BLL.DTO;

namespace ITExpert.PL.Models;

public class EditFilmModel
{
    public FilmDto Film { get; set; }
    public List<int> Categories { get; set; }
}