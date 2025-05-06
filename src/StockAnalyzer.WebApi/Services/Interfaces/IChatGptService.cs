namespace StockAnalyzer.WebApi.Services.Interfaces
{
    public interface IChatGptService
    {
        Task<string> GetChatGptResponse(string chatId);
    }
}
