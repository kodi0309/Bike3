using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace SaleFinder;

[ApiController]
[Route("api/find")]
public class SaleFinder : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> FindItems([FromQuery] FindProp findProp)
    {
        var query = DB.PagedSearch<Item, Item>();

        query.Sort(x => x.Ascending(a => a.Brand));

        if (!string.IsNullOrEmpty(findProp.FindTerm))
        {
            query.Match(Search.Full, findProp.FindTerm).SortByTextScore();
        }

        query = findProp.OrderBy switch
        {
            "brand" => query.Sort(x => x.Ascending(a => a.Brand)).Sort(x => x.Ascending(a => a.Model)),
            "model" => query.Sort(x => x.Ascending(a => a.Model)),
            _ => query.Sort(x => x.Ascending(a => a.Brand))
        };

        query = findProp.FilterBy switch
        {
            "new" => query.Match(x => x.Year > 2023),
            "all" => query.Match(x => x.Year > 0),
            _ => query.Match(x => x.Year <= 2024)
        };

        query.PageNumber(findProp.PageNumber);
        query.PageSize(findProp.PageSize);

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pages = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}