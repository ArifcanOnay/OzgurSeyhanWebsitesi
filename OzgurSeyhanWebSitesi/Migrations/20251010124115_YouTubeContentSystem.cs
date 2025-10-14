using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OzgurSeyhanWebSitesi.Migrations
{
    /// <inheritdoc />
    public partial class YouTubeContentSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YouTubeKonulari",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KonuAdi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PlaylistUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VideoSayisi = table.Column<int>(type: "int", nullable: false),
                    RenkKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeKonulari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciKonuIzlemeler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    YouTubeKonuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KonuTamamlandiMi = table.Column<bool>(type: "bit", nullable: false),
                    KazanilanPuan = table.Column<int>(type: "int", nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TamamlanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestAktifMi = table.Column<bool>(type: "bit", nullable: false),
                    TestTamamlandiMi = table.Column<bool>(type: "bit", nullable: false),
                    TestTamamlanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciKonuIzlemeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KullaniciKonuIzlemeler_YouTubeKonulari_YouTubeKonuId",
                        column: x => x.YouTubeKonuId,
                        principalTable: "YouTubeKonulari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciKonuIzlemeler_KullaniciId_YouTubeKonuId",
                table: "KullaniciKonuIzlemeler",
                columns: new[] { "KullaniciId", "YouTubeKonuId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciKonuIzlemeler_YouTubeKonuId",
                table: "KullaniciKonuIzlemeler",
                column: "YouTubeKonuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciKonuIzlemeler");

            migrationBuilder.DropTable(
                name: "YouTubeKonulari");
        }
    }
}
