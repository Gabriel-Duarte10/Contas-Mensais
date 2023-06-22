using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas_Mensais.Migrations
{
    /// <inheritdoc />
    public partial class LimiteDisponivel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Limite",
                table: "Cartoes",
                newName: "LimiteTotal");

            migrationBuilder.AddColumn<double>(
                name: "LimiteDisponivel",
                table: "Cartoes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimiteDisponivel",
                table: "Cartoes");

            migrationBuilder.RenameColumn(
                name: "LimiteTotal",
                table: "Cartoes",
                newName: "Limite");
        }
    }
}
