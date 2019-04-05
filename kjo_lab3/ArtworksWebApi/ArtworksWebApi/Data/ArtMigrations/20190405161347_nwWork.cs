using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworksWebApi.Data.ArtMigrations
{
    public partial class nwWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Artworks_Name",
                schema: "ART",
                table: "Artworks",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Artworks_Name",
                schema: "ART",
                table: "Artworks");
        }
    }
}
