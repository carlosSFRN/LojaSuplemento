using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaSuplemento.Migrations
{
    public partial class inclusaoproduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoBarras = table.Column<int>(nullable: false),
                    TituloProduto = table.Column<string>(nullable: true),
                    DescricaoProduto = table.Column<string>(nullable: true),
                    PrecoProduto = table.Column<decimal>(nullable: false),
                    DataProdutoValidadeInicio = table.Column<DateTime>(nullable: false),
                    DataProdutoValidadeFim = table.Column<DateTime>(nullable: false),
                    DataProdutoInclusao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
