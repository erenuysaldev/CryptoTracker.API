using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Application.ViewModels
{
    public class CoinDetailViewModel
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal MarketCap { get; set; }
        public decimal TotalVolume { get; set; }
    }
}
