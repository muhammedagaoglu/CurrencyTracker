using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // API URL
            string apiUrl = "https://api.frankfurter.app/latest?from=TRY";

            Console.WriteLine("Döviz verileri indiriliyor...");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // API'den veriyi çekiyoruz (async / await)
                    string json = await client.GetStringAsync(apiUrl);
                    
                    // JSON verisini CurrencyResponse sınıfına dönüştürüyoruz
                    // Büyük/küçük harf duyarlılığını kaldırmak için options kullanıyoruz
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    CurrencyResponse response = JsonSerializer.Deserialize<CurrencyResponse>(json, options);

                    if (response == null || response.Rates == null)
                    {
                        Console.WriteLine("Veri alınamadı veya hatalı.");
                        return;
                    }

                    // Dictionary yapısını List<Currency> yapısına çeviriyoruz (LINQ Select)
                    List<Currency> currencies = response.Rates.Select(kvp => new Currency 
                    { 
                        Code = kvp.Key, 
                        Rate = kvp.Value 
                    }).ToList();

                    // Ana Menü Döngüsü
                    while (true)
                    {
                        Console.WriteLine("\n===== CurrencyTracker =====");
                        Console.WriteLine("1. Tüm dövizleri listele");
                        Console.WriteLine("2. Koda göre döviz ara");
                        Console.WriteLine("3. Belirli bir değerden büyük dövizleri listele");
                        Console.WriteLine("4. Dövizleri değere göre sırala");
                        Console.WriteLine("5. İstatistiksel özet göster");
                        Console.WriteLine("0. Çıkış");
                        Console.Write("Seçiminiz: ");

                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                ListAllCurrencies(currencies);
                                break;
                            case "2":
                                SearchByCode(currencies);
                                break;
                            case "3":
                                ListGreaterThan(currencies);
                                break;
                            case "4":
                                SortByValue(currencies);
                                break;
                            case "5":
                                ShowStatistics(currencies);
                                break;
                            case "0":
                                Console.WriteLine("Çıkış yapılıyor...");
                                return;
                            default:
                                Console.WriteLine("Geçersiz seçim!");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }

        // 1. Tüm dövizleri listele
        static void ListAllCurrencies(List<Currency> currencies)
        {
            Console.WriteLine("\n--- Tüm Dövizler ---");
            // LINQ Select kullanımı
            var list = currencies.Select(c => $"{c.Code}: {c.Rate}").ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        // 2. Koda göre döviz ara
        static void SearchByCode(List<Currency> currencies)
        {
            Console.Write("\nDöviz Kodu Girin (örn: USD): ");
            string code = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(code)) return;

            // LINQ Where kullanımı (Büyük/küçük harf duyarsız)
            var result = currencies.Where(c => c.Code.Equals(code.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();

            if (result.Any())
            {
                foreach (var c in result)
                {
                    Console.WriteLine($"Bulunan: {c.Code}: {c.Rate}");
                }
            }
            else
            {
                Console.WriteLine("Döviz bulunamadı.");
            }
        }

        // 3. Belirli bir değerden büyük dövizleri listele
        static void ListGreaterThan(List<Currency> currencies)
        {
            Console.Write("\nMinimum Değer Girin: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal minVal))
            {
                // LINQ Where kullanımı
                var result = currencies.Where(c => c.Rate > minVal).ToList();
                
                Console.WriteLine($"\n{minVal} değerinden büyük kurlar:");
                foreach (var c in result)
                {
                    Console.WriteLine($"{c.Code}: {c.Rate}");
                }
                
                if (!result.Any()) Console.WriteLine("Kriterlere uygun döviz yok.");
            }
            else
            {
                Console.WriteLine("Geçersiz değer girdiniz.");
            }
        }

        // 4. Dövizleri değere göre sırala
        static void SortByValue(List<Currency> currencies)
        {
            Console.WriteLine("\nNasıl sıralansın? (1: Artan, 2: Azalan)");
            string sortChoice = Console.ReadLine();

            List<Currency> sorted;
            if (sortChoice == "1")
            {
                // LINQ OrderBy
                sorted = currencies.OrderBy(c => c.Rate).ToList();
                Console.WriteLine("\n--- Artan Sıralama ---");
            }
            else if (sortChoice == "2")
            {
                // LINQ OrderByDescending
                sorted = currencies.OrderByDescending(c => c.Rate).ToList();
                Console.WriteLine("\n--- Azalan Sıralama ---");
            }
            else
            {
                Console.WriteLine("Geçersiz seçim.");
                return;
            }

            foreach (var c in sorted)
            {
                Console.WriteLine($"{c.Code}: {c.Rate}");
            }
        }

        // 5. İstatistiksel özet göster
        static void ShowStatistics(List<Currency> currencies)
        {
            if (!currencies.Any()) 
            {
                Console.WriteLine("Veri yok.");
                return;
            }

            Console.WriteLine("\n--- İstatistiksel Özet ---");
            
            // LINQ Count
            int count = currencies.Count();
            Console.WriteLine($"Toplam döviz sayısı: {count}");
            
            // LINQ Max
            decimal maxRate = currencies.Max(c => c.Rate);
            Console.WriteLine($"En yüksek kur: {maxRate}");
            
            // LINQ Min
            decimal minRate = currencies.Min(c => c.Rate);
            Console.WriteLine($"En düşük kur: {minRate}");
            
            // LINQ Average
            decimal avgRate = currencies.Average(c => c.Rate);
            Console.WriteLine($"Ortalama kur: {avgRate:F4}");
        }
    }

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
}
