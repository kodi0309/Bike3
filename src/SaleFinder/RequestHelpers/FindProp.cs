namespace SaleFinder;

public class FindProp
{
    public string FindTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public string OrderBy { get; set; }
    public string FilterBy { get; set; }
}
