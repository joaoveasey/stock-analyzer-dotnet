namespace StockAnalyzer.WebApi.Services.Interfaces
{
    public interface IDeepSeekService
    {
        Task<string> GetDeepSeekResponse(string prompt);
    }
}
