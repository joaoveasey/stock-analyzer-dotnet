using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using StockAnalyzer.WebApi.Models;
using StockAnalyzer.WebApi.Services;
using StockAnalyzer.WebApi.Services.Interfaces;

namespace StockAnalyzer.WebApi.Controllers;

[Route("api/chat")]
[ApiController]
public class AnalyzeController : ControllerBase
{
    private readonly IChatGptService _chatGptService;
    private readonly IDeepSeekService _deepSeekService;

    public AnalyzeController(IChatGptService chatGptService, IDeepSeekService deepSeekService)
    {
        _chatGptService = chatGptService;
        _deepSeekService = deepSeekService;
    }

    [HttpPost()]
    public async Task<ActionResult<string>> Analysys([FromBody] StockAnalysisRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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
           request.Ticket, request.CurrentPrice, request.TwelveMonthChange, request.PriceToEarnings,
           request.PriceToBook, request.DividendYield, request.ReturnOnEquity, request.NetMargin,
           request.EarningsPerShare, request.Sector
        );

        try
        {
            var answer = await _deepSeekService.GetDeepSeekResponse(prompt);
            // var answer = await _chatGptService.GetChatGptResponse(prompt);

            return Ok(answer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao chamar DeepSeek: {ex.Message}");
        }
    }   
}
