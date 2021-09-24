using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaSuplemento.Migrations
{
    public partial class inclusaocategoriavinculadaaoproduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "Produto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdCategoria",
                table: "Produto",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_IdCategoria",
                table: "Produto",
                column: "IdCategoria",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_IdCategoria",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_IdCategoria",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "Produto");
        }
    }
}
