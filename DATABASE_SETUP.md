# Database Migration Instructions

Veritabanı migration'larını oluşturmak ve uygulamak için aşağıdaki komutları çalıştırın:

## 1. İlk Migration'ı Oluşturma
```bash
dotnet ef migrations add InitialCreate
```

## 2. Veritabanını Güncelleme
```bash
dotnet ef database update
```

## 3. Yeni Migration Oluşturma (gelecekte değişiklik olduğunda)
```bash
dotnet ef migrations add [MigrationName]
dotnet ef database update
```

## Connection String Bilgileri
- Server: CAN\SQLEXPRESS01
- Database: OzgurSeyhanWebSitesi
- Username: sa
- Password: 1

## Oluşturulacak Tablolar ve İlişkiler:

### Tablolar:
1. **Ogretmenler** - Öğretmen bilgileri
2. **Kurslar** - Kurs bilgileri  
3. **OrnekVideolar** - Örnek video bilgileri
4. **Paketler** - Paket bilgileri
5. **OnceSonralar** - Önce/Sonra karşılaştırma bilgileri
6. **SatinAlmalar** - Satın alma işlemleri

### İlişkiler:
- **Ogretmen → Kurs**: One-to-Many (Bir öğretmenin birden fazla kursu olabilir)
- **Ogretmen → OrnekVideo**: One-to-Many (Bir öğretmenin birden fazla örnek videosu olabilir)
- **Kurs → Paket**: One-to-Many (Bir kursun birden fazla paketi olabilir)
- **Kurs → OnceSonra**: One-to-Many (Bir kursun birden fazla önce/sonra örneği olabilir)
- **Paket → SatinAlma**: One-to-Many (Bir paketin birden fazla satın alma işlemi olabilir)

## Test Endpoint'leri:
Proje çalıştıktan sonra aşağıdaki endpoint'leri test edebilirsiniz:

- GET /api/Ogretmen - Tüm öğretmenleri listele
- GET /api/Ogretmen/{id} - Belirli bir öğretmeni getir
- POST /api/Ogretmen - Yeni öğretmen ekle
- PUT /api/Ogretmen/{id} - Öğretmen güncelle
- DELETE /api/Ogretmen/{id} - Öğretmen sil
