# CurrencyTracker - Döviz Takip Konsol Uygulaması

Bu proje, finansal verileri takip etmek amacıyla geliştirilmiş bir C# konsol uygulamasıdır. Frankfurter API üzerinden anlık döviz kurlarını çeker ve kullanıcıya çeşitli sorgulama imkanları sunar.

## Proje Hakkında
Uygulama, **Bilgisayar Programcılığı 2. Sınıf** düzeyindeki gereksinimlere uygun olarak hazırlanmıştır. 
- **Veri Kaynağı:** https://api.frankfurter.app/latest?from=TRY
- **Dil:** C# (.NET Core / .NET 8)
- **Mimari:** Konsol Uygulaması (Tek dosya yapısı)

## Nasıl Çalıştırılır?

Proje dizininde terminali açarak aşağıdaki komutu çalıştırabilirsiniz:

```bash
dotnet run
```

*Not: İnternet bağlantısı gereklidir.*

## Yapılan İşlemler ve Kod Yapısı

Proje geliştirilirken aşağıdaki adımlar izlenmiştir:

1.  **HttpClient Entegrasyonu:** `HttpClient` sınıfı kullanılarak API'ye asenkron (`async/await`) istek atıldı.
2.  **JSON İşleme:** Gelen veri `System.Text.Json` kütüphanesi ile `CurrencyResponse` nesnesine deserialize edildi. Büyük/küçük harf duyarlılığı `PropertyNameCaseInsensitive = true` ile aşıldı.
3.  **Veri Dönüşümü:** `Dictionary` olarak gelen oranlar, LINQ `Select` metodu ile `List<Currency>` listesine çevrildi. Bu sayede veriler üzerinde daha kolay işlem yapılması sağlandı.
4.  **Menü Sistemi:** `while(true)` döngüsü ile kullanıcının seçim yapabileceği bir arayüz oluşturuldu.

## Kullanılan LINQ Metotları

Ödev gereksinimlerine uygun olarak aşağıdaki LINQ metotları aktif bir şekilde kullanılmıştır:

*   **Tüm Dövizleri Listeleme:** `Select` metodu ile veriler string formatına çevrilip listelendi.
*   **Arama:** `Where` metodu ile girilen koda (örn: USD) sahip döviz filtrelendi. `StringComparison.OrdinalIgnoreCase` ile büyük/küçük harf duyarlılığı kaldırıldı.
*   **Filtreleme:** `Where` metodu ile belirli bir değerin üzerindeki kurlar filtrelendi.
*   **Sıralama:** 
    *   `OrderBy`: Küçükten büyüğe sıralama için.
    *   `OrderByDescending`: Büyükten küçüğe sıralama için.
*   **İstatistikler:**
    *   `Count()`: Toplam döviz sayısı.
    *   `Max()`: En yüksek kur değeri.
    *   `Min()`: En düşük kur değeri.
    *   `Average()`: Ortalama kur değeri.

## Teknik Gereksinimler ve Kısıtlamalar

*   ✔ **Hard-coded veri yok:** Tüm veriler canlı olarak API'den çekilmektedir.
*   ✔ **LINQ kullanımı:** Döngülerle manuel arama yapmak yerine LINQ sorguları tercih edilmiştir.
*   ✔ **GUI yok:** Uygulama tamamen konsol üzerinde çalışmaktadır.
*   ✔ **Model Sınıfları:** İstenen `CurrencyResponse` ve `Currency` sınıfları tanımlanmıştır.

## Sınıf Yapısı

```csharp
class CurrencyResponse 
{ 
    public string Base { get; set; } 
    public Dictionary<string, decimal> Rates { get; set; } 
} 

class Currency 
{ 
    public string Code { get; set; } 
    public decimal Rate { get; set; } 
} 
```
