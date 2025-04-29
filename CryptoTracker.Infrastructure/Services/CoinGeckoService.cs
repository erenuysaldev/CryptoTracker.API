using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using CryptoTracker.Application.ViewModels;
using Microsoft.Extensions.Configuration;
namespace CryptoTracker.Infrastructure.Services
{
    public class CoinGeckoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl = "https://api.coingecko.com/api/v3/coins/markets";

        public CoinGeckoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<CoinDetailViewModel> GetCoinDetailAsync(string id)
        {
            string apiKey = _configuration["CoinGecko:ApiKey"];
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-cg-pro-api-key", apiKey);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CryptoTrackerApp/1.0");

            var response = await _httpClient.GetAsync($"https://api.coingecko.com/api/v3/coins/{id}?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false&sparkline=false");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            return new CoinDetailViewModel
            {
                Id = root.GetProperty("id").GetString(),
                Symbol = root.GetProperty("symbol").GetString(),
                Name = root.GetProperty("name").GetString(),
                Description = root.GetProperty("description").GetProperty("en").GetString(),
                ImageUrl = root.GetProperty("image").GetProperty("large").GetString(),
                CurrentPrice = root.GetProperty("market_data").GetProperty("current_price").GetProperty("usd").GetDecimal(),
                MarketCap = root.GetProperty("market_data").GetProperty("market_cap").GetProperty("usd").GetDecimal(),
                TotalVolume = root.GetProperty("market_data").GetProperty("total_volume").GetProperty("usd").GetDecimal()
            };
        }
        // Top Coins listesi için API çağrısı yapar
        public async Task<List<CoinDto>> GetTopCoinsAsync()
        {
            // API key'in kontrolü
            string apiKey = _configuration["CoinGecko:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new Exception("CoinGecko API key is missing.");
            }

            // HTTP header ayarları
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-cg-pro-api-key", apiKey);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CryptoTrackerApp/1.0");

            // API URL'si
            var url = $"{_baseUrl}?vs_currency=usd&order=market_cap_desc&per_page=10&page=1";

            // API'yi çağır
            var response = await _httpClient.GetAsync(url);

            // Hata kontrolü
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: Unable to retrieve data from CoinGecko API. Status Code: {response.StatusCode}");
            }

            // API yanıtını al
            var content = await response.Content.ReadAsStringAsync();

            // JSON'dan C# objesine dönüşüm
            var coins = JsonSerializer.Deserialize<List<CoinDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Dönen veriyi kontrol et
            if (coins == null)
            {
                throw new Exception("Error: Unable to deserialize the API response.");
            }

            return coins;
        }
    }

    // Coin bilgileri DTO'su
    public class CoinDto
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Current_Price { get; set; }
    }
    
}
    
