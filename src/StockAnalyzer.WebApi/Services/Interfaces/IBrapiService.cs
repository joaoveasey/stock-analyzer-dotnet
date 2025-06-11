using StockAnalyzer.WebApi.Models;

namespace StockAnalyzer.WebApi.Services.Interfaces;

public interface IBrapiService
{
    Task<List<Stocks>> GetAllStocksAsync();
    Task<List<StockAnalysisRequest>> GetStockDetailsAsync(string ticket);
}
