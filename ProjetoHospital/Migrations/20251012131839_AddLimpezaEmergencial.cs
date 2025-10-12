using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoHospital.Migrations
{
    /// <inheritdoc />
    public partial class AddLimpezaEmergencial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Limpezas",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdSolicitante",
                table: "Limpezas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Limpezas_IdSolicitante",
                table: "Limpezas",
                column: "IdSolicitante");

            migrationBuilder.AddForeignKey(
                name: "FK_Limpezas_AspNetUsers_IdSolicitante",
                table: "Limpezas",
                column: "IdSolicitante",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Limpezas_AspNetUsers_IdSolicitante",
                table: "Limpezas");

            migrationBuilder.DropIndex(
                name: "IX_Limpezas_IdSolicitante",
                table: "Limpezas");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Limpezas");

            migrationBuilder.DropColumn(
                name: "IdSolicitante",
                table: "Limpezas");
        }
    }
}
