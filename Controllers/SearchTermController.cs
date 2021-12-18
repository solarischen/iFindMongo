using iFindMongo.Models;
using iFindMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace iFindMongo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchTermController
{
    private readonly SearchTermService _searchTermService;

    private Dictionary<string, string> marketString = new Dictionary<string, string>()
        {
            { "us", "Amazon.com" },
            { "uk", "Amazon.co.uk" },
            { "de", "Amazon.de" },
            { "fr", "Amazon.fr" },
            { "es", "Amazon.es" },
            { "it", "Amazon.it" },
        };

    public SearchTermController(SearchTermService searchTermService) =>
        _searchTermService = searchTermService;

    [HttpGet("/count")]
    public async Task<long> GetDocumentsCount() =>
        await _searchTermService.GetDocumentsCountAsync();

    [HttpGet("/{market}")]
    public async Task<List<SearchTerm>> GetAll(string market) =>
        await _searchTermService.GetAllAsync(marketString[market]);


    // 精确查询
    [HttpGet("/{market}/exact/{name}")]
    public async Task<SearchTerm?> GetExactSearchTerm(string market, string name)
    {
        return marketString.ContainsKey(market) ?
            await _searchTermService.GetExactSearchTermAsync(marketString[market], name) :
            null;
    }

    // 模糊查询
    [HttpGet("/{market}/board/{name}")]
    public async Task<List<SearchTerm>?> GetBoardSearchTerm(string market, string name) => 
        await _searchTermService.GetBoardSearchTermAsync(marketString[market], name);


    // asin 查询
    [HttpGet("/{market}/asin/{asin}")]
    public async Task<List<SearchTerm>?> GetSearchTermWithASINShallow(string market, string asin) =>
        await _searchTermService.GetSearchTermWithASINShallowAsync(marketString[market], asin);

    // deep query
    
}

