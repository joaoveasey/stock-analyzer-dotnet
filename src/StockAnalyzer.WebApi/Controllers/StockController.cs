using Microsoft.AspNetCore.Mvc;
using StockAnalyzer.WebApi.Services.Interfaces;

namespace StockAnalyzer.WebApi.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StocksController : ControllerBase
    {
        private readonly IBrapiService _brapiService;

        public StocksController(IBrapiService brapiService)
        {
            _brapiService = brapiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _brapiService.GetAllStocksAsync();
            return Ok(stocks);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetStockDetails(string ticket)
        {
            var stocksDetails = await _brapiService.GetStockDetailsAsync(ticket);
            return Ok(stocksDetails);
        }
    }
}
