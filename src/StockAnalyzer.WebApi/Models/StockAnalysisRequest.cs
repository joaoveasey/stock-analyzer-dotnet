using System.Text.Json.Serialization;

namespace StockAnalyzer.WebApi.Models;

public class StockAnalysisRequest
{
    [JsonPropertyName("symbol")]
    public string Ticket { get; set; }

    [JsonPropertyName("regularMarketPrice")]
    public decimal CurrentPrice { get; set; } // cotação atual

    public decimal TwelveMonthChange { get; set; } // variação 12 meses

    [JsonPropertyName("priceEarnings")]
    public decimal PriceToEarnings { get; set; } // preço/lucro (P/L)

    public decimal PriceToBook { get; set; } // preço/valor patrimonial (P/VP)
    public decimal DividendYield { get; set; }
    public decimal ReturnOnEquity { get; set; }
    public decimal NetMargin { get; set; } // margem líquida

    [JsonPropertyName("earningsPerShare")]
    public decimal EarningsPerShare { get; set; } // lucro por ação (LPA)

    [JsonPropertyName("sector")]
    public string Sector { get; set; }

    [JsonPropertyName("logourl")]
    public string LogoUrl { get; set; }
}
