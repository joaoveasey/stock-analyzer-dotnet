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
            errors.Add("O campo Cotação Atual deve estar preenhido.");

        // outras validações

        return errors;
    }
}
