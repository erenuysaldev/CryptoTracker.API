using CryptoTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CryptoTracker.Domain.Entities;
using CryptoTracker.API.Data;
using IdentityAppDbContext = CryptoTracker.API.Data.IdentityAppDbContext;


var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýlarýný yapýlandýrýyoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IdentityAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

// Identity yapýlandýrmasýný ekliyoruz
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityAppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Diðer servisleri buraya ekleyebilirsiniz, örneðin:
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata ayýklama sayfalarýný geliþtirme modunda göster
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

// Kimlik doðrulama ve yetkilendirme middleware'lerini ekliyoruz
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Uygulama baþlatýldýðýnda DataSeeder'ý çalýþtýrýyoruz
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    await DataSeeder.SeedDataAsync(services, userManager);
}

app.Run();
