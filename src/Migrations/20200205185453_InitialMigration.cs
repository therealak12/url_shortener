using Microsoft.EntityFrameworkCore.Migrations;

namespace src.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "url_response",
                columns: table => new
                {
                    ShortUrl = table.Column<string>(nullable: false),
                    LongUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_url_response", x => x.ShortUrl);
                });

            migrationBuilder.CreateIndex(
                name: "IX_url_response_ShortUrl",
                table: "url_response",
                column: "ShortUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "url_response");
        }
    }
}
