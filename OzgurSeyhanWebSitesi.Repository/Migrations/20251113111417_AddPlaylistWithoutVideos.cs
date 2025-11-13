using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OzgurSeyhanWebSitesi.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaylistWithoutVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YoutubeVideos_Playlists_PlaylistId",
                table: "YoutubeVideos");

            migrationBuilder.DropIndex(
                name: "IX_YoutubeVideos_PlaylistId",
                table: "YoutubeVideos");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "YoutubeVideos");

            migrationBuilder.DropColumn(
                name: "LastSyncDate",
                table: "Playlists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "YoutubeVideos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncDate",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideos_PlaylistId",
                table: "YoutubeVideos",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_YoutubeVideos_Playlists_PlaylistId",
                table: "YoutubeVideos",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id");
        }
    }
}
