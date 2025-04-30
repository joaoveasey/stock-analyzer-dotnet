namespace StockAnalyzer.WebApi.Models
{
    public class StockAnalysisResponse
    {
        public StockAnalysisChoice[] Choices { get; set; } 
    }

    public class StockAnalysisChoice
    {
        public StockAnalysismessage Message { get; set; }
    }

    public class StockAnalysismessage
    {
        public string Content { get; set; }
    }
}
