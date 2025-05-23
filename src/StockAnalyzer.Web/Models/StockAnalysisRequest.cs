namespace StockAnalyzer.Web.Models;

public class StockAnalysisRequest
{
    public string Ticket { get; set; }
    public string CurrentPrice { get; set; } // cotação atual
    public string TwelveMonthChange { get; set; } // variação 12 meses
    public string PriceToEarnings { get; set; } // preço/lucro (P/L)
    public string PriceToBook { get; set; } // preço/valor patrimonial (P/VP)
    public string DividendYield { get; set; }
    public string ReturnOnEquity { get; set; }
    public string NetMargin { get; set; } // margem líquida
    public string EarningsPerShare { get; set; } // lucro por ação (LPA)
    public string Sector { get; set; }
}
