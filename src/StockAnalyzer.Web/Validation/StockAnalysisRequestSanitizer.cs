using StockAnalyzer.Web.Models;

namespace StockAnalyzer.Web.Validation;

public class StockAnalysisRequestSanitizer
{
    public static StockAnalysisRequest Sanitize(StockAnalysisRequest input)
    {
        return new StockAnalysisRequest
        {
            Ticket = input.Ticket?.Trim().ToUpperInvariant(),
            CurrentPrice = CleanCurrency(input.CurrentPrice),
            TwelveMonthChange = CleanPercent(input.TwelveMonthChange),
            PriceToEarnings = CleanDecimal(input.PriceToEarnings),
            PriceToBook = CleanDecimal(input.PriceToBook),
            DividendYield = CleanPercent(input.DividendYield),
            ReturnOnEquity = CleanPercent(input.ReturnOnEquity),
            NetMargin = CleanPercent(input.NetMargin),
            EarningsPerShare = CleanDecimal(input.EarningsPerShare),
            Sector = input.Sector?.Trim()
        };
    }

    private static string CleanCurrency(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) 
            return value;

        return value.Replace("R$", "", StringComparison.OrdinalIgnoreCase)
                    .Replace(" ", "")
                    .Replace(".", "")
                    .Replace(",", ".")
                    .Trim();
    }

    private static string CleanPercent(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        return value.Replace("%", "")
                    .Replace(" ", "")
                    .Replace(",", ".")
                    .Trim();
    }

    private static string CleanDecimal(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        return value.Replace(" ", "")
                    .Replace(",", ".")
                    .Trim();
    }
}
