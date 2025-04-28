using Microsoft.AspNetCore.Identity;

namespace CryptoTracker.Infrastructure.Persistence
{
    public class ApplicationUser : IdentityUser
    {
        // Özelleştirilmiş özellikler ekleyebilirsin
        public string FullName { get; set; }
    }
}
