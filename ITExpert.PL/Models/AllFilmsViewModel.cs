using ITExpert.BLL.DTO;
using ITExpert.BLL.Utils;

namespace ITExpert.PL.Models;

public class AllFilmsViewModel
{
    public IEnumerable<FilmSummary> Films { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<int> SelectedCategories { get; set; }
}