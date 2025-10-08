using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoHospital.Migrations
{
    /// <inheritdoc />
    public partial class AddColunaIdSolicitanteEIdExecutanteNaManutencao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdExecutante",
                table: "Manutencoes",
                type: "varchar(450)",
                maxLength: 450,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdSolicitante",
                table: "Manutencoes",
                type: "varchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_IdExecutante",
                table: "Manutencoes",
                column: "IdExecutante");

            migrationBuilder.CreateIndex(
                name: "IX_Manutencoes_IdSolicitante",
                table: "Manutencoes",
                column: "IdSolicitante");

            migrationBuilder.AddForeignKey(
                name: "FK_Manutencoes_AspNetUsers_IdExecutante",
                table: "Manutencoes",
                column: "IdExecutante",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Manutencoes_AspNetUsers_IdSolicitante",
                table: "Manutencoes",
                column: "IdSolicitante",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manutencoes_AspNetUsers_IdExecutante",
                table: "Manutencoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Manutencoes_AspNetUsers_IdSolicitante",
                table: "Manutencoes");

            migrationBuilder.DropIndex(
                name: "IX_Manutencoes_IdExecutante",
                table: "Manutencoes");

            migrationBuilder.DropIndex(
                name: "IX_Manutencoes_IdSolicitante",
                table: "Manutencoes");

            migrationBuilder.DropColumn(
                name: "IdExecutante",
                table: "Manutencoes");

            migrationBuilder.DropColumn(
                name: "IdSolicitante",
                table: "Manutencoes");
        }
    }
}
