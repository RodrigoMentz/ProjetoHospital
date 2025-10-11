using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoHospital.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaRevisoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Revisoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Observacoes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataInicioLimpeza = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataFimLimpeza = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SolicitanteId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExecutanteId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LimpezaId = table.Column<int>(type: "int", nullable: false),
                    LeitoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revisoes_AspNetUsers_ExecutanteId",
                        column: x => x.ExecutanteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Revisoes_AspNetUsers_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Revisoes_Leitos_LeitoId",
                        column: x => x.LeitoId,
                        principalTable: "Leitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Revisoes_Limpezas_LimpezaId",
                        column: x => x.LimpezaId,
                        principalTable: "Limpezas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Revisoes_ExecutanteId",
                table: "Revisoes",
                column: "ExecutanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisoes_LeitoId",
                table: "Revisoes",
                column: "LeitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisoes_LimpezaId",
                table: "Revisoes",
                column: "LimpezaId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisoes_SolicitanteId",
                table: "Revisoes",
                column: "SolicitanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Revisoes");
        }
    }
}
