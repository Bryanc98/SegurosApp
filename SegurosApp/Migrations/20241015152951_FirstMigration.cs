using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SegurosApp.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductoFinancieros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoFinancieros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seguros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente_ProductoFinancieros",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ProductoFinancieroId = table.Column<int>(type: "int", nullable: false),
                    Numeroproducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente_ProductoFinancieros", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cliente_ProductoFinancieros_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_ProductoFinancieros_ProductoFinancieros_ProductoFinancieroId",
                        column: x => x.ProductoFinancieroId,
                        principalTable: "ProductoFinancieros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeguroId = table.Column<int>(type: "int", nullable: false),
                    couta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EdadMin = table.Column<int>(type: "int", nullable: false),
                    EdadMax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planes_Seguros_SeguroId",
                        column: x => x.SeguroId,
                        principalTable: "Seguros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seguro_ProductoFinancieros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeguroId = table.Column<int>(type: "int", nullable: false),
                    ProductoFinancieroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguro_ProductoFinancieros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seguro_ProductoFinancieros_ProductoFinancieros_ProductoFinancieroId",
                        column: x => x.ProductoFinancieroId,
                        principalTable: "ProductoFinancieros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seguro_ProductoFinancieros_Seguros_SeguroId",
                        column: x => x.SeguroId,
                        principalTable: "Seguros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cliente_Seguros",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    SeguroId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoFinancieroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente_Seguros", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cliente_Seguros_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Seguros_Planes_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Seguros_ProductoFinancieros_ProductoFinancieroId",
                        column: x => x.ProductoFinancieroId,
                        principalTable: "ProductoFinancieros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Seguros_Seguros_SeguroId",
                        column: x => x.SeguroId,
                        principalTable: "Seguros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ProductoFinancieros_ClienteId",
                table: "Cliente_ProductoFinancieros",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ProductoFinancieros_ProductoFinancieroId",
                table: "Cliente_ProductoFinancieros",
                column: "ProductoFinancieroId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Seguros_ClienteID",
                table: "Cliente_Seguros",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Seguros_PlanId",
                table: "Cliente_Seguros",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Seguros_ProductoFinancieroId",
                table: "Cliente_Seguros",
                column: "ProductoFinancieroId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Seguros_SeguroId",
                table: "Cliente_Seguros",
                column: "SeguroId");

            migrationBuilder.CreateIndex(
                name: "IX_Planes_SeguroId",
                table: "Planes",
                column: "SeguroId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguro_ProductoFinancieros_ProductoFinancieroId",
                table: "Seguro_ProductoFinancieros",
                column: "ProductoFinancieroId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguro_ProductoFinancieros_SeguroId",
                table: "Seguro_ProductoFinancieros",
                column: "SeguroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente_ProductoFinancieros");

            migrationBuilder.DropTable(
                name: "Cliente_Seguros");

            migrationBuilder.DropTable(
                name: "Seguro_ProductoFinancieros");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "ProductoFinancieros");

            migrationBuilder.DropTable(
                name: "Seguros");
        }
    }
}
