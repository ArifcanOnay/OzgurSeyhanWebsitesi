using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OzgurSeyhanWebSitesi.Migrations
{
    /// <inheritdoc />
    public partial class Deneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MezuniyetTarihi",
                table: "OnceSonralar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MezuniyetTarihi",
                table: "OnceSonralar");
        }
    }
}
