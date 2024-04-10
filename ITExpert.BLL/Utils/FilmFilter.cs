namespace ITExpert.BLL.Utils;

public class FilmFilter
{
    public List<int> CategoryIds { get; set; }
    public string Director { get; set; }
    public DateTime? ReleaseDateFrom { get; set; }
    public DateTime? ReleaseDateTo { get; set; }
    public string Name { get; set; }
}