# FLG - TPIC Entegrasyon Yazılımı

LOGO Tiger ile FAYS TPIC entegrasyonu için geliştirilmiş Windows Forms uygulaması.

## Özellikler

- LOGO Tiger verilerini FAYS TPIC sistemine aktarma
- Stok kartları ve fiş satırları senkronizasyonu
- Ürün grup bilgileri güncelleme
- SPECODE alanları mapping

## Kurulum

### Hazır EXE Dosyası İndirme

1. [Releases](../../releases) sayfasına gidin
2. En son sürümü indirin
3. FLG.exe dosyasını çalıştırın

### Kaynak Koddan Derleme

Gereksinimler:
- Visual Studio 2015 veya üzeri
- .NET Framework 4.7.2
- SQL Server bağlantısı

Derleme adımları:
1. Projeyi klonlayın: `git clone https://github.com/KULLANICI_ADINIZ/FLG_Yeni_Tpic.git`
2. FLG.sln dosyasını Visual Studio ile açın
3. Build > Build Solution

## Otomatik Build

Bu proje GitHub Actions ile otomatik olarak derlenir. Her push işleminde:
- Proje otomatik olarak Windows ortamında derlenir
- Build artifacts olarak saklanır
- Tag push edildiğinde (örn: v1.0.0) otomatik release oluşturulur

## Son Güncellemeler

- UrunGrup3 alanı güncellemesi: Sadece boş olan kayıtlar güncellenir (farklılık kontrolü kaldırıldı)

## Lisans

Bu yazılım proje sahibi tarafından geliştirilmiştir.
