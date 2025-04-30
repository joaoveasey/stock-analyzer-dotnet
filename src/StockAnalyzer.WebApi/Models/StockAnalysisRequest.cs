namespace StockAnalyzer.WebApi.Models;

public class StockAnalysisRequest
{
    public string Ticker { get; set; } 
    public decimal CurrentPrice { get; set; } // cotação atual
    public decimal TwelveMonthChange { get; set; } // variação 12 meses
    public decimal PriceToEarnings { get; set; } // preço/lucro (P/L)
    public decimal PriceToBook { get; set; } // preço/valor patrimonial (P/VP)
    public decimal DividendYield { get; set; }
    public decimal ReturnOnEquity { get; set; }
    public decimal NetMargin { get; set; } // margem líquida
    public decimal EarningsPerShare { get; set; } // lucro por ação (LPA)
    public string Sector { get; set; }
}
