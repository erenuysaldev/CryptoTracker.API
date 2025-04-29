using System.Diagnostics;
using CryptoTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using CryptoTracker.Application.ViewModels;
using CryptoTracker.API.Models;

namespace CryptoTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : Controller
    {
        private readonly CoinGeckoService _coinGeckoService;

        public CoinsController(CoinGeckoService coinGeckoService)
        {
            _coinGeckoService = coinGeckoService;
        }

        // API'yi çağırarak 10 en iyi coin'i getirir
        [HttpGet]
        public async Task<IActionResult> GetTopCoins()
        {
            try
            {
                var coins = await _coinGeckoService.GetTopCoinsAsync();
                return Ok(coins);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("list")]
        public async Task<IActionResult> CoinList()
        {
            try
            {
                var coins = await _coinGeckoService.GetTopCoinsAsync();
                var viewModels = coins.Select(c => new CoinViewModel
                {
                    Name = c.Name,
                    Symbol = c.Symbol,
                    CurrentPrice = c.Current_Price
                }).ToList();

                return View(viewModels); // Views/Coins/CoinList.cshtml dosyasını açar
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message); // Hata View'ı
            }
        }
        [HttpGet("index")]  
        public async Task<IActionResult> Index()
        {
            try
            {
                var coins = await _coinGeckoService.GetTopCoinsAsync();
                return View(coins); // Verileri Razor View'e aktar
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                var coin = await _coinGeckoService.GetCoinDetailAsync(id);
                return View("Detail", coin); // Views/Coins/Detail.cshtml
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
