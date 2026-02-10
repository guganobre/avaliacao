using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avaliacao.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Segurado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identificador único do segurado")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Nome do segurado"),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false, comment: "CPF do segurado"),
                    Idade = table.Column<int>(type: "int", nullable: false, comment: "Idade do segurado")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segurado", x => x.Id);
                },
                comment: "Tabela de segurados");

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identificador único do veículo")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaModelo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Marca e modelo do veículo"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Valor do veículo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                },
                comment: "Tabela de veículos");

            migrationBuilder.CreateTable(
                name: "Seguro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Identificador único do seguro"),
                    CriadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Data e hora de criação do seguro em UTC"),
                    VeiculoId = table.Column<int>(type: "int", nullable: false, comment: "Identificador do veículo segurado"),
                    SeguradoId = table.Column<int>(type: "int", nullable: false, comment: "Identificador do segurado"),
                    TaxaRisco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Taxa de risco aplicada"),
                    PremioRisco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Prêmio de risco calculado"),
                    PremioPuro = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Prêmio puro calculado"),
                    PremioComercial = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Prêmio comercial final"),
                    ValorSeguro = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Valor total do seguro")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seguro_Segurado_SeguradoId",
                        column: x => x.SeguradoId,
                        principalTable: "Segurado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seguro_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela de seguros");

            migrationBuilder.CreateIndex(
                name: "IX_Seguro_SeguradoId",
                table: "Seguro",
                column: "SeguradoId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguro_VeiculoId",
                table: "Seguro",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seguro");

            migrationBuilder.DropTable(
                name: "Segurado");

            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}