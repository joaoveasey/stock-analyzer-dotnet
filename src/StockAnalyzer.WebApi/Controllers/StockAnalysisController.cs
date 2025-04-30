using Microsoft.AspNetCore.Mvc;
using StockAnalyzer.WebApi.Models;

namespace StockAnalyzer.WebApi.Controllers;

[ApiController]
[Route("Controller")]
public class StockAnalysisController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private const string apiUrl = "https://api.openai.com/v1/chat/completions";
    private const string apiKey = "";

    public StockAnalysisController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Analyze([FromBody] StockAnalysisRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string prompt = string.Format(
            "Você é um analista financeiro experiente, com profundo conhecimento em avaliação de empresas, análise fundamentalista e contexto macroeconômico. " +
            "Sua tarefa é elaborar uma análise completa e clara da ação com o ticker {0}, utilizando os dados fornecidos a seguir: " +
            "Preço atual: {1}, Variação nos últimos 12 meses: {2}%, P/L (Preço/Lucro): {3}, P/VP (Preço/Valor Patrimonial): {4}, " +
            "Dividend Yield: {5}%, ROE (Retorno sobre Patrimônio): {6}%, Margem Líquida: {7}%, LPA (Lucro por Ação): {8}, Setor: {9}. " +
            "Baseie-se nesses dados para apresentar uma visão geral sobre a saúde financeira da empresa, seus múltiplos de mercado e atratividade como investimento. " +
            "Considere também: como os indicadores se comparam com os padrões do setor? Os múltiplos sugerem sobrevalorização ou oportunidade de compra? " +
            "Além disso, leve em conta o cenário macroeconômico atual: inflação, taxa de juros, crescimento do PIB e taxa de desemprego, e como esses fatores afetam o setor da empresa. " +
            "Utilize linguagem acessível, mas sem perder a profundidade da análise. Evite jargões técnicos desnecessários e explique os termos financeiros de forma simples. " +
            "Inclua um resumo final com pontos positivos, pontos negativos e uma conclusão com viés (neutro, positivo ou negativo) sobre o investimento na ação.",
            request.Ticker, request.CurrentPrice, request.TwelveMonthChange, request.PriceToEarnings,
            request.PriceToBook, request.DividendYield, request.ReturnOnEquity, request.NetMargin,
            request.EarningsPerShare, request.Sector
        );

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            max_tokens = 800
        };

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        var response = await _httpClient.PostAsJsonAsync(apiUrl, requestBody);
        response.EnsureSuccessStatusCode();

        var chatGptResponse = await response.Content.ReadFromJsonAsync<StockAnalysisResponse>();

        string analysisText = chatGptResponse.Choices[0].Message.Content;

        return Ok(analysisText);
    }
}
