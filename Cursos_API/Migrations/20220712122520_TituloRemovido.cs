using Microsoft.EntityFrameworkCore.Migrations;

namespace Cursos_API.Migrations
{
    public partial class TituloRemovido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CursoNome",
                table: "Cursos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CursoNome",
                table: "Cursos",
                type: "TEXT",
                nullable: true);
        }
    }
}
