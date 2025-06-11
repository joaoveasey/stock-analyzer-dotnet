using System.Net.Http.Headers;
using System.Text.Json;
using StockAnalyzer.WebApi.Helpers;
using StockAnalyzer.WebApi.Models;
using StockAnalyzer.WebApi.Services.Interfaces;

namespace StockAnalyzer.WebApi.Services;

public class BrapiService : IBrapiService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public BrapiService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<List<Stocks>> GetAllStocksAsync()
    {
        var token = _config["Brapi:Token"];
        int page = 1, limit = 800;
        var result = new List<Stocks>();

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"quote/list?limit={limit}&page={page}&type=stock");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        using var jsonDoc = JsonDocument.Parse(jsonString);
        var root = jsonDoc.RootElement;

        if (root.TryGetProperty("stocks", out var stocksElement))
        {
            foreach (var stockJson in stocksElement.EnumerateArray())
            {
                var stock = JsonSerializer.Deserialize<Stocks>(stockJson.GetRawText());
                if (stock != null)
                    result.Add(stock);
            }
        }

        return result;
    }

    public async Task<List<StockAnalysisRequest>> GetStockDetailsAsync(string ticket)
    {
        var token = _config["Brapi:Token"];
        var result = new List<StockAnalysisRequest>();

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"quote/{ticket}?modules=summaryProfile");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        using var jsonDoc = JsonDocument.Parse(jsonString);
        var root = jsonDoc.RootElement;

        if (root.TryGetProperty("results", out var resultsElement))
        {
            foreach (var stockJson in resultsElement.EnumerateArray())
            {
                var stock = JsonSerializer.Deserialize<StockAnalysisRequest>(stockJson.GetRawText());
                if (stock != null)
                {
                    if (stock.SummaryProfile == null)
                        stock.SummaryProfile = new SummaryProfile();

                    if (stockJson.TryGetProperty("summaryProfile", out var summaryProfileElem) &&
                        summaryProfileElem.TryGetProperty("sector", out var sectorElem))
                    {
                        stock.SummaryProfile.Sector = SectorTranslator.Translate(sectorElem.GetString());
                    }

                    stock.SummaryProfile.Sector = SectorTranslator.Translate(stock.SummaryProfile.Sector);
                    result.Add(stock);
                }
            }
        }

        return result;
    }
}
