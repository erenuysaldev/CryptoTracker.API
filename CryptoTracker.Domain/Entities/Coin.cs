using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Domain.Entities
{
    public class Coin
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public decimal PriceUsd { get; set; }

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
