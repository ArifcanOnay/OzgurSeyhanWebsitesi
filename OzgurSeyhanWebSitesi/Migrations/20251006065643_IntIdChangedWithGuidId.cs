using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OzgurSeyhanWebSitesi.Migrations
{
    /// <inheritdoc />
    public partial class IntIdChangedWithGuidId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ogretmenler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ozgecmis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Egitim = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeneyimYili = table.Column<int>(type: "int", nullable: false),
                    ProfilFotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    YouTubeKanalUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogretmenler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IletisimBilgileri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelefonNumarasi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YouTubeKanali = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WhatsAppNumarasi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WebSitesi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OgretmenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "Kurslar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KursAdi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Konular = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    BeklenenSeviye = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    KursSeviyesi = table.Column<int>(type: "int", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    KursSuresiHafta = table.Column<int>(type: "int", nullable: false),
                    HaftalikDersSayisi = table.Column<int>(type: "int", nullable: false),
                    ToplamDersSayisi = table.Column<int>(type: "int", nullable: false),
                    DersGunleri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DersSaatleri = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EskiFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YeniFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndirimdeMi = table.Column<bool>(type: "bit", nullable: false),
                    RenkKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PopulerMi = table.Column<bool>(type: "bit", nullable: false),
                    OgretmenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurslar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kurslar_Ogretmenler_OgretmenId",
                        column: x => x.OgretmenId,
                        principalTable: "Ogretmenler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrnekVideolar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DersYontemi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OgretmenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrnekVideolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrnekVideolar_Ogretmenler_OgretmenId",
                        column: x => x.OgretmenId,
                        principalTable: "Ogretmenler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KursIcerikleri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IcerikBasligi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    KursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "OnceSonralar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OgrenciAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OnceVideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SonraVideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OnceAciklama = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SonraAciklama = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AlinanKurslar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    KursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnceSonralar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnceSonralar_Kurslar_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurslar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paketler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaketAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SinifMevcudu = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WhatsAppGrupLinki = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    KursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paketler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paketler_Kurslar_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurslar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SatinAlmalar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MusteriEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MusteriTelefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OdenenTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OdemeBasarili = table.Column<bool>(type: "bit", nullable: false),
                    OdemeIslemId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SatinAlmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    PaketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatinAlmalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SatinAlmalar_Paketler_PaketId",
                        column: x => x.PaketId,
                        principalTable: "Paketler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IletisimBilgileri_OgretmenId",
                table: "IletisimBilgileri",
                column: "OgretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_KursIcerikleri_KursId",
                table: "KursIcerikleri",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurslar_OgretmenId",
                table: "Kurslar",
                column: "OgretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_OnceSonralar_KursId",
                table: "OnceSonralar",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_OrnekVideolar_OgretmenId",
                table: "OrnekVideolar",
                column: "OgretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_Paketler_KursId",
                table: "Paketler",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_SatinAlmalar_PaketId",
                table: "SatinAlmalar",
                column: "PaketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IletisimBilgileri");

            migrationBuilder.DropTable(
                name: "KursIcerikleri");

            migrationBuilder.DropTable(
                name: "OnceSonralar");

            migrationBuilder.DropTable(
                name: "OrnekVideolar");

            migrationBuilder.DropTable(
                name: "SatinAlmalar");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Paketler");

            migrationBuilder.DropTable(
                name: "Kurslar");

            migrationBuilder.DropTable(
                name: "Ogretmenler");
        }
    }
}
