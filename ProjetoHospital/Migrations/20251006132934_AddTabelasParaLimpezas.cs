using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoHospital.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelasParaLimpezas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Limpezas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInicioLimpeza = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataFimLimpeza = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LeitoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoLimpeza = table.Column<int>(type: "int", nullable: false),
                    TirarLixo = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparVasoSanitario = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparBox = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    RevisarMofo = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparPia = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparCama = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparMesaCabeceira = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparLixeira = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_TirarLixo = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_LimparVasoSanitario = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_LimparBox = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_RevisarMofo = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_LimparPia = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_LimparCama = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparEscadaCama = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_LimparMesaCabeceira = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparArmario = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    RecolherRoupaSuja = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    RevisarPapelToalhaEHigienico = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparDispensers = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimpezaTerminal_LimparLixeira = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparTeto = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparParedes = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    LimparChao = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limpezas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Limpezas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Limpezas_Leitos_LeitoId",
                        column: x => x.LeitoId,
                        principalTable: "Leitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Limpezas_LeitoId",
                table: "Limpezas",
                column: "LeitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Limpezas_UsuarioId",
                table: "Limpezas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Limpezas");
        }
    }
}
