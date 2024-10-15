using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SegurosApp.Migrations
{
    /// <inheritdoc />
    public partial class FixClienteID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_ProductoFinancieros_Clientes_ClienteId",
                table: "Cliente_ProductoFinancieros");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Seguros_Clientes_ClienteID",
                table: "Cliente_Seguros");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Seguros_Seguros_SeguroId",
                table: "Cliente_Seguros");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_ProductoFinancieros_Clientes_ClienteId",
                table: "Cliente_ProductoFinancieros",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Seguros_Clientes_ClienteID",
                table: "Cliente_Seguros",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Seguros_Seguros_SeguroId",
                table: "Cliente_Seguros",
                column: "SeguroId",
                principalTable: "Seguros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_ProductoFinancieros_Clientes_ClienteId",
                table: "Cliente_ProductoFinancieros");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Seguros_Clientes_ClienteID",
                table: "Cliente_Seguros");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Seguros_Seguros_SeguroId",
                table: "Cliente_Seguros");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_ProductoFinancieros_Clientes_ClienteId",
                table: "Cliente_ProductoFinancieros",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Seguros_Clientes_ClienteID",
                table: "Cliente_Seguros",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Seguros_Seguros_SeguroId",
                table: "Cliente_Seguros",
                column: "SeguroId",
                principalTable: "Seguros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
