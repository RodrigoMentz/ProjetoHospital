using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoHospital.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteTabelaSetores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDelete",
                table: "Setores",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDelete",
                table: "Setores");
        }
    }
}
