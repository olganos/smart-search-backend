using Core;

using Microsoft.AspNetCore.Mvc;

using Servises;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchService _searchService;

    public SearchController(ILogger<SearchController> logger, ISearchService searchService)
    {
        _logger = logger;
        _searchService = searchService;
    }

    [HttpGet()]
    public IActionResult Get(string searchString)
    {
        return Ok(_searchService.Execute(searchString));
    }
}
