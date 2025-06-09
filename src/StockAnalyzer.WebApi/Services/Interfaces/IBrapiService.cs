using StockAnalyzer.WebApi.Models;

namespace StockAnalyzer.WebApi.Services.Interfaces;

public interface IBrapiService
{
    Task<List<StockAnalysisRequest>> GetAllStocksAsync();
    Task<List<StockAnalysisRequest>> GetStockDetailsAsync(string ticket);
}
