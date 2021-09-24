using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaSuplemento.Migrations
{
    public partial class inclusaocampodesativado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Produto");

            migrationBuilder.AddColumn<bool>(
                name: "Desativado",
                table: "Produto",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desativado",
                table: "Produto");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Produto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
