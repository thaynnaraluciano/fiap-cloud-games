using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicialMySQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Promocoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desconto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocoes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PerfilUsuario = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SenhaHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConfirmadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SenhaSalt = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PromocaoJogos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PromocaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    JogoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocaoJogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromocaoJogos_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocaoJogos_Promocoes_PromocaoId",
                        column: x => x.PromocaoId,
                        principalTable: "Promocoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Biblioteca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdJogo = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdUsuario = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DtAdquirido = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PrecoOriginal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PrecoFinal = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biblioteca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biblioteca_Jogos_IdJogo",
                        column: x => x.IdJogo,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Biblioteca_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Biblioteca_IdJogo",
                table: "Biblioteca",
                column: "IdJogo");

            migrationBuilder.CreateIndex(
                name: "IX_Biblioteca_IdUsuario",
                table: "Biblioteca",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PromocaoJogos_JogoId",
                table: "PromocaoJogos",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_PromocaoJogos_PromocaoId",
                table: "PromocaoJogos",
                column: "PromocaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biblioteca");

            migrationBuilder.DropTable(
                name: "PromocaoJogos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Promocoes");
        }
    }
}
