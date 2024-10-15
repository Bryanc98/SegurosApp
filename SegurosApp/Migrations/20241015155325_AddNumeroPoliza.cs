using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SegurosApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNumeroPoliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroPoliza",
                table: "Cliente_Seguros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroPoliza",
                table: "Cliente_Seguros");
        }
    }
}
