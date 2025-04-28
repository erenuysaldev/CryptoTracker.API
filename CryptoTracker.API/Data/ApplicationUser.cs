using Microsoft.AspNetCore.Identity;

namespace CryptoTracker.API.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Eğer kullanıcıya özel başka özellikler eklemek isterseniz, buraya ekleyebilirsiniz.
        // Örneğin: public string FullName { get; set; }
    }
}