using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int CoinId { get; set; }
        public string UserId { get; set; }
        public Coin Coin { get; set; } = null!;
        public User User { get; set; } = null!;
        
        
    }
}
