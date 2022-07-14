using Microsoft.EntityFrameworkCore.Migrations;

namespace Cursos_API.Migrations
{
    public partial class Adicionadocampostatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Logs",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Logs");
        }
    }
}
