-- Playlist tablosuna KategoriBaslik kolonunu ekleme
-- Bu scripti SQL Server Management Studio veya Azure Data Studio ile çalıştırın

USE [OzgurSeyhanWebSitesi];
GO

-- Kolon zaten var mı kontrol et
IF NOT EXISTS (SELECT * FROM sys.columns 
               WHERE object_id = OBJECT_ID(N'[dbo].[Playlists]') 
               AND name = 'KategoriBaslik')
BEGIN
    ALTER TABLE [dbo].[Playlists]
    ADD [KategoriBaslik] NVARCHAR(200) NULL;
    
    PRINT 'KategoriBaslik kolonu başarıyla eklendi.';
END
ELSE
BEGIN
    PRINT 'KategoriBaslik kolonu zaten mevcut.';
END
GO
