# CurrencyTracker – Döviz Takip Konsol Uygulaması

Bu proje, Türk Lirası (TRY) bazlı döviz kurlarını takip etmek için geliştirilmiş bir C# konsol uygulamasıdır.  
Döviz kurları Frankfurter FREE API üzerinden alınmaktadır.

## Kullanılan Teknolojiler
- C# Konsol Uygulaması
- HttpClient
- async / await
- LINQ
- List

## Kullanılan API
Zorunlu API adresi:
https://api.frankfurter.app/latest?from=TRY

Bu API TRY bazlı güncel döviz kurlarını JSON formatında döndürür.

## Model Sınıfları
Projede aşağıdaki model sınıfları kullanılmıştır.

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

CurrencyResponse sınıfı API’den gelen veriyi almak için,  
Currency sınıfı ise dövizleri hafızada tutmak için kullanılmıştır.

## Uygulamanın Çalışma Mantığı
Uygulama açıldığında API’den döviz verileri alınır.  
Gelen veriler LINQ kullanılarak List<Currency> içine aktarılır.  
Tüm işlemler bu liste üzerinden yapılır.

## Konsol Menü
===== CurrencyTracker =====  
1) Tüm dövizleri listele  
2) Koda göre döviz ara  
3) Belirli bir değerden büyük dövizleri listele  
4) Dövizleri değere göre sırala  
5) İstatistiksel özet göster  
0) Çıkış  

## Menü İşlevleri

1) Tüm Dövizleri Listele  
Tüm dövizler ekrana yazdırılır.  
LINQ Select kullanılmıştır.

2) Koda Göre Döviz Ara  
Girilen döviz koduna göre arama yapılır.  
LINQ Where kullanılmıştır.  
Arama büyük/küçük harf duyarsızdır.

3) Belirli Bir Değerden Büyük Dövizler  
Girilen değerden büyük olan dövizler listelenir.  
LINQ Where kullanılmıştır.

4) Dövizleri Değere Göre Sırala  
Dövizler kur değerine göre sıralanır.  
LINQ OrderBy ve OrderByDescending kullanılmıştır.

5) İstatistiksel Özet  
Dövizler ile ilgili genel bilgiler gösterilir.  
LINQ Count, Max, Min ve Average kullanılmıştır.

Gösterilen bilgiler:
- Toplam döviz sayısı  
- En yüksek kur  
- En düşük kur  
- Ortalama kur  

## Çalıştırma
Proje Visual Studio üzerinden veya dotnet run komutu ile çalıştırılabilir.  
Program çalıştığında menü ekrana gelir ve kullanıcı seçim yapar.
