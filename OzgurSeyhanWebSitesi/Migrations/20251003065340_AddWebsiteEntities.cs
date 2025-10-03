using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OzgurSeyhanWebSitesi.Migrations
{
    /// <inheritdoc />
    public partial class AddWebsiteEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Kurslar tablosuna yeni kolonlar ekle
            migrationBuilder.AddColumn<int>(
                name: "KursSuresiHafta",
                table: "Kurslar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaftalikDersSayisi",
                table: "Kurslar",
                type: "int",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "ToplamDersSayisi",
                table: "Kurslar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DersGunleri",
                table: "Kurslar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DersSaatleri",
                table: "Kurslar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "EskiFiyat",
                table: "Kurslar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "YeniFiyat",
                table: "Kurslar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IndirimdeMi",
                table: "Kurslar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RenkKodu",
                table: "Kurslar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "#007bff");

            migrationBuilder.AddColumn<bool>(
                name: "PopulerMi",
                table: "Kurslar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // OnceSonralar tablosuna yeni kolon ekle
            migrationBuilder.AddColumn<string>(
                name: "AlinanKurslar",
                table: "OnceSonralar",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            // Yeni tablolar oluştur
            migrationBuilder.CreateTable(
                name: "IletisimBilgileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelefonNumarasi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YouTubeKanali = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WhatsAppNumarasi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WebSitesi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OgretmenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IletisimBilgileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IletisimBilgileri_Ogretmenler_OgretmenId",
                        column: x => x.OgretmenId,
                        principalTable: "Ogretmenler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KursIcerikleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IcerikBasligi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    KursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursIcerikleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KursIcerikleri_Kurslar_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurslar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // İndeksler oluştur
            migrationBuilder.CreateIndex(
                name: "IX_IletisimBilgileri_OgretmenId",
                table: "IletisimBilgileri",
                column: "OgretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_KursIcerikleri_KursId",
                table: "KursIcerikleri",
                column: "KursId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Yeni tabloları sil
            migrationBuilder.DropTable(
                name: "IletisimBilgileri");

            migrationBuilder.DropTable(
                name: "KursIcerikleri");

            // Kurslar tablosundan yeni kolonları sil
            migrationBuilder.DropColumn(
                name: "KursSuresiHafta",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "HaftalikDersSayisi",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "ToplamDersSayisi",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "DersGunleri",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "DersSaatleri",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "EskiFiyat",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "YeniFiyat",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "IndirimdeMi",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "RenkKodu",
                table: "Kurslar");

            migrationBuilder.DropColumn(
                name: "PopulerMi",
                table: "Kurslar");

            // OnceSonralar tablosundan yeni kolonu sil
            migrationBuilder.DropColumn(
                name: "AlinanKurslar",
                table: "OnceSonralar");
        }
    }
}
