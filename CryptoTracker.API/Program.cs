using CryptoTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CryptoTracker.Domain.Entities;
using CryptoTracker.API.Data;
using IdentityAppDbContext = CryptoTracker.API.Data.IdentityAppDbContext;


var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�lar�n� yap�land�r�yoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IdentityAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

// Identity yap�land�rmas�n� ekliyoruz
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityAppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Di�er servisleri buraya ekleyebilirsiniz, �rne�in:
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata ay�klama sayfalar�n� geli�tirme modunda g�ster
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Kimlik do�rulama ve yetkilendirme middleware'lerini ekliyoruz
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Uygulama ba�lat�ld���nda DataSeeder'� �al��t�r�yoruz
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    await DataSeeder.SeedDataAsync(services, userManager);
}

app.Run();
