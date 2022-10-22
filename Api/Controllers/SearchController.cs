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
    public Data? Get()
    {
        return _data.Data;
    }
}
