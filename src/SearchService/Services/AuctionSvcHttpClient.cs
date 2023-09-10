using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class AuctionSvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuctionSvcHttpClient(HttpClient http, IConfiguration config)
    {
        _httpClient = http;
        _config = config;
    }

    public async Task<List<Item>> GetItemsForSearchDb()
    {
        var lastUpdated = await DB.Find<Item>()
            .Sort(x => x.Descending(x => x.UpdatedAt))
            .ExecuteFirstAsync();

        return await _httpClient.GetFromJsonAsync<List<Item>>(
            _config["AuctionServiceUrl"] + "/api qauctions?date=" +
            lastUpdated);
    }
}