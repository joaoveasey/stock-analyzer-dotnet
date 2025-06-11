namespace StockAnalyzer.WebApi.Helpers;

public class SectorTranslator 
{
    private static readonly Dictionary<string, string> _sectorMap = new()
        {
            { "Financial Services", "Serviços Financeiros" },
            { "Energy", "Energia" },
            { "Consumer Goods", "Bens de Consumo" },
            { "Health Care", "Saúde" },
            { "Technology", "Tecnologia" },
            { "Basic Materials", "Materiais Básicos" },
            { "Utilities", "Utilidades Públicas" },
            { "Industrial Goods", "Bens Industriais" },
            { "Communication Services", "Serviços de Comunicação" },
            { "Real Estate", "Imobiliário" },
            { "Consumer Services", "Serviços ao Consumidor" },
            { "Transportation", "Transportes" },
            { "Agriculture", "Agricultura" },
            { "Mining", "Mineração" },
            { "Construction", "Construção" },
            { "Retail", "Varejo" },
            { "Education", "Educação" },
            { "Hospitality", "Hospitalidade" },
            { "Media", "Mídia" },
            { "Chemicals", "Químicos" },
            { "Consumer Defensive", "Consumo Defensivo" },
            { "Industrials", "Industriais" },
            { "Consumer Cyclical", "Consumo Cíclico" }
        };

    public static string Translate(string sector)
    {
        return _sectorMap.TryGetValue(sector, out var translated) ? translated : sector;
    }

    public static List<string> TranslateMany(IEnumerable<string> sectors)
    {
        return sectors.Select(Translate).ToList();
    }
}
