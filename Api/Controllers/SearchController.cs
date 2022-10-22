using Core;

using Microsoft.AspNetCore.Mvc;

using Servises;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly DataInitialiser _data;

    public SearchController(ILogger<SearchController> logger, DataInitialiser data)
    {
        _logger = logger;
        _data = data;
    }

    [HttpGet()]
    public IActionResult Get(string searchString)
    {
        return Ok(_data.WeightedTrie.Search(searchString)
            .Select(s => new
            {
                s.Key.ShortCut,
                s.Key.Name,
                s.Key.Description,
                Weight = s.Value
            })
            .OrderByDescending(x => x.Weight)
        );
    }
}
