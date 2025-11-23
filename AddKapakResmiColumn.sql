-- Podcasts tablosuna KapakResmi kolonu ekle
ALTER TABLE Podcasts 
ADD KapakResmi NVARCHAR(500) NULL;

-- Kontrol et
SELECT TOP 1 * FROM Podcasts;
