using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.Json;
using System.Text;
using StockAnalyzer.WebApi.Services.Interfaces;

namespace StockAnalyzer.WebApi.Services;

public class ChatGptService : IChatGptService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "https://api.openai.com/v1/chat/completions";
    private readonly string _apiKey;

    public ChatGptService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = "";
    }

    public async Task<string> GetChatGptResponse(string prompt)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
               new { role = "user", content = prompt }
            },
            max_tokens = 200
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(_apiUrl, content);
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro na requisição: {response.StatusCode} - {responseString}");

        using JsonDocument doc = JsonDocument.Parse(responseString);

        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();
    }
}
