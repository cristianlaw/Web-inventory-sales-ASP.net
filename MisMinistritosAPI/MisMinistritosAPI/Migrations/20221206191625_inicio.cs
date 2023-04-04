using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MisMinistritosAPI.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteDbModel",
                columns: table => new
                {
                    cedula = table.Column<string>(type: "varchar(12)", nullable: false),
                    nombreCompleto = table.Column<string>(type: "varchar(60)", nullable: false),
                    fechaNacimiento = table.Column<int>(type: "int", nullable: false),
                    telefono = table.Column<string>(type: "varchar(9)", nullable: false),
                    profesion = table.Column<string>(type: "varchar(100)", nullable: false),
                    correoElectronico = table.Column<string>(type: "varchar(100)", nullable: false),
                    comboReferencia = table.Column<string>(type: "varchar(50)", nullable: false),
                    contrasena = table.Column<string>(type: "varchar(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteDbModel", x => x.cedula);
                });

            migrationBuilder.CreateTable(
                name: "FacturaDbModel",
                columns: table => new
                {
                    facturaId = table.Column<string>(type: "varchar(15)", nullable: false),
                    clienteCedula = table.Column<string>(type: "varchar(12)", nullable: false),
                    clienteNombre = table.Column<string>(type: "varchar(60)", nullable: false),
                    montoSubTotal = table.Column<decimal>(type: "decimal", nullable: false),
                    montoIVA = table.Column<decimal>(type: "decimal", nullable: false),
                    montoTotal = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaDbModel", x => x.facturaId);
                });

            migrationBuilder.CreateTable(
                name: "ProveedorDbModel",
                columns: table => new
                {
                    cedula = table.Column<string>(type: "varchar(12)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorDbModel", x => x.cedula);
                });

            migrationBuilder.CreateTable(
                name: "VentaDbModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDatoF = table.Column<string>(type: "varchar(15)", nullable: false),
                    descripcionDatoF = table.Column<string>(type: "varchar(60)", nullable: false),
                    productoPrecio = table.Column<decimal>(type: "decimal", nullable: false),
                    cantidadProducto = table.Column<int>(type: "int", nullable: false),
                    totalProducto = table.Column<decimal>(type: "decimal", nullable: false),
                    FacturaDbModelfacturaId = table.Column<string>(type: "varchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDbModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_VentaDbModel_FacturaDbModel_FacturaDbModelfacturaId",
                        column: x => x.FacturaDbModelfacturaId,
                        principalTable: "FacturaDbModel",
                        principalColumn: "facturaId");
                });

            migrationBuilder.CreateTable(
                name: "ProductoDbModel",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(15)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", nullable: false),
                    ingreso = table.Column<int>(type: "int", nullable: false),
                    proveedorDbModelcedula = table.Column<string>(type: "varchar(12)", nullable: true),
                    proveedor = table.Column<string>(type: "varchar(12)", nullable: false),
                    precioCompra = table.Column<int>(type: "int", nullable: false),
                    utilidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoDbModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductoDbModel_ProveedorDbModel_proveedorDbModelcedula",
                        column: x => x.proveedorDbModelcedula,
                        principalTable: "ProveedorDbModel",
                        principalColumn: "cedula");
                });

            migrationBuilder.CreateTable(
                name: "TourDbModel",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(15)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", nullable: false),
                    verImagen = table.Column<string>(type: "varchar(max)", nullable: false),
                    diasTour = table.Column<string>(type: "varchar(25)", nullable: false),
                    proveedorDbModelcedula = table.Column<string>(type: "varchar(12)", nullable: true),
                    proveedor = table.Column<string>(type: "varchar(12)", nullable: false),
                    precioTour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDbModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_TourDbModel_ProveedorDbModel_proveedorDbModelcedula",
                        column: x => x.proveedorDbModelcedula,
                        principalTable: "ProveedorDbModel",
                        principalColumn: "cedula");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoDbModel_proveedorDbModelcedula",
                table: "ProductoDbModel",
                column: "proveedorDbModelcedula");

            migrationBuilder.CreateIndex(
                name: "IX_TourDbModel_proveedorDbModelcedula",
                table: "TourDbModel",
                column: "proveedorDbModelcedula");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDbModel_FacturaDbModelfacturaId",
                table: "VentaDbModel",
                column: "FacturaDbModelfacturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteDbModel");

            migrationBuilder.DropTable(
                name: "ProductoDbModel");

            migrationBuilder.DropTable(
                name: "TourDbModel");

            migrationBuilder.DropTable(
                name: "VentaDbModel");

            migrationBuilder.DropTable(
                name: "ProveedorDbModel");

            migrationBuilder.DropTable(
                name: "FacturaDbModel");
        }
    }
}
