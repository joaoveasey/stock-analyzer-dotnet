using System.Runtime.CompilerServices;
using StockAnalyzer.Web.Models;

namespace StockAnalyzer.Web.Validation;

public class StockAnalysisRequestValidator
{
    public List<string> Validate(StockAnalysisRequest stock)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(stock.Ticket))
            errors.Add("O campo Ticket deve estar preenchido.");
        else if (stock.Ticket.Length > 6)
            errors.Add("O campo Ticket deve ter no máximo 6 caracteres.");

        if (string.IsNullOrWhiteSpace(stock.CurrentPrice))
            errors.Add("O campo Cotação Atual deve estar preenchido.");
        //else if (int.Parse(stock.CurrentPrice) < 0)
        //    errors.Add("O campo Cotação Atual deve ser um valor positivo.");

        if (string.IsNullOrWhiteSpace(stock.TwelveMonthChange))
            errors.Add("O campo Variação 12 Meses deve estar preenchido.");

        if (string.IsNullOrWhiteSpace(stock.PriceToEarnings))
            errors.Add("O campo Preço/Lucro (P/L) deve estar preenchido.");

        if (string.IsNullOrWhiteSpace(stock.PriceToBook))
            errors.Add("O campo Preço/Valor Patrimonial (P/VP) deve estar preenchido.");

        if (string.IsNullOrWhiteSpace(stock.DividendYield))
            errors.Add("O campo Dividend Yield deve estar preenchido.");

        if (string.IsNullOrWhiteSpace(stock.ReturnOnEquity))
            errors.Add("O campo ROE deve estar preenchido.");

        if (string.IsNullOrWhiteSpace(stock.NetMargin))
            errors.Add("O campo Margem Líquida deve estar preenchido.");

        if (string.IsNullOrWhiteSpace(stock.EarningsPerShare))
            errors.Add("O campo Lucro por Ação (LPA) deve estar preenchido.");

        if (string.IsNullOrWhiteSpace(stock.Sector))
            errors.Add("O campo Setor deve estar preenchido.");

        return errors;
    }
}
