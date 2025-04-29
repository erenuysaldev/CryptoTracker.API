# CryptoTracker

CryptoTracker, CoinGecko API'sini kullanarak popüler kripto para birimlerinin fiyatlarını takip etmenize olanak tanıyan bir web uygulamasıdır. Kullanıcılar, piyasa değeri en yüksek 10 coin'in fiyatlarını ve diğer detaylarını görüntüleyebilir.

## Özellikler

- **Top 10 Kripto Para:** CoinGecko API'si ile en yüksek piyasa değerine sahip ilk 10 coin'in fiyatlarını ve temel bilgilerini görüntüleme.
- **Kripto Para Fiyatlarını Görüntüleme:** Coin'lerin anlık fiyat bilgilerini, isimlerini ve sembollerini görüntüleme.
- **API Entegrasyonu:** CoinGecko API'si üzerinden anlık veri çekme ve kullanıcıya sunma.
- **Error Handling:** API isteği sırasında oluşabilecek hatalar için kullanıcı dostu hata mesajları ve yanıtlar.

## Teknolojiler

- **ASP.NET Core MVC:** Backend framework olarak kullanıldı.
- **CoinGecko API:** Kripto para fiyat verilerini almak için entegre edilen API.
- **Swagger:** API testleri ve dokümantasyonu için Swagger UI kullanıldı.
- **C# ve .NET Core:** Uygulamanın geliştirilmesinde kullanılan ana programlama dili ve framework.

## Kurulum

### Gereksinimler

- .NET 6.0 veya daha yeni bir sürüm
- Visual Studio 2022 veya benzeri bir IDE
- CoinGecko API anahtarı (API entegrasyonu için gerekli)

### Adımlar

1. **Proje dosyasını klonlayın:**
   ```bash
   git clone https://github.com/username/CryptoTracker.git
   ```

2. **NuGet paketlerini yükleyin:**
   Proje dizininde `dotnet restore` komutunu çalıştırarak gerekli paketleri yükleyin.
   ```bash
   dotnet restore
   ```

3. **API Anahtarını Ayarlayın:**
   CoinGecko API anahtarınızı `appsettings.json` dosyasına ekleyin:
   ```json
   {
     "CoinGecko": {
       "ApiKey": "YOUR_API_KEY"
     }
   }
   ```

4. **Uygulamayı çalıştırın:**
   Aşağıdaki komut ile uygulamayı başlatabilirsiniz:
   ```bash
   dotnet run
   ```

   Uygulama, `https://localhost:7222` veya `http://localhost:5015` adresinden erişilebilir olacaktır.

## API Kullanımı

### Top 10 Kripto Para
GET `/api/coins`
Bu endpoint, piyasa değeri en yüksek 10 kripto parayı döndürür.
### Coin Listesi
GET `api/coins/list`
### Coin Detayları (Detaylı Veri)
GET `/api/coins/{id}`

Bu endpoint, piyasa değeri en yüksek 10 kripto parayı döndürür.

## Katkıda Bulunma

Katkıda bulunmak isterseniz, lütfen şu adımları izleyin:

1. Forklayın ve bir branch oluşturun.
2. Değişikliklerinizi yapın ve test edin.
3. Pull request gönderin.

