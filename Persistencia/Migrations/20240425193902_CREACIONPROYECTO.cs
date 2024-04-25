using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class CREACIONPROYECTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    ActividadId = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    UsuarioId1 = table.Column<string>(nullable: true),
                    TipoActividad = table.Column<string>(nullable: true),
                    DescripcionActividad = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.ActividadId);
                    table.ForeignKey(
                        name: "FK_Actividad_AspNetUsers_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    CompraId = table.Column<Guid>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    UsuarioId1 = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.CompraId);
                    table.ForeignKey(
                        name: "FK_Compra_AspNetUsers_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Productoid = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Precio = table.Column<string>(nullable: true),
                    Categoria = table.Column<string>(nullable: true),
                    Imagen = table.Column<byte[]>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Productoid);
                });

            migrationBuilder.CreateTable(
                name: "PronosticoDemanda",
                columns: table => new
                {
                    PronosticoDemandaId = table.Column<Guid>(nullable: false),
                    CantidadPronosticada = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PronosticoDemanda", x => x.PronosticoDemandaId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    ProveedorId = table.Column<Guid>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: true),
                    RUC = table.Column<string>(nullable: true),
                    NumeroCelular = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.ProveedorId);
                    table.ForeignKey(
                        name: "FK_Proveedor_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    VentaId = table.Column<Guid>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    UsuarioId1 = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.VentaId);
                    table.ForeignKey(
                        name: "FK_Venta_AspNetUsers_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    FavoritosId = table.Column<Guid>(nullable: false),
                    ProductoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.FavoritosId);
                    table.ForeignKey(
                        name: "FK_Favoritos_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Productoid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoCompra",
                columns: table => new
                {
                    ProductoId = table.Column<Guid>(nullable: false),
                    CompraId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoCompra", x => new { x.ProductoId, x.CompraId });
                    table.ForeignKey(
                        name: "FK_ProductoCompra_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "CompraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoCompra_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Productoid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    NotificacionId = table.Column<Guid>(nullable: false),
                    PronosticoDemandaId = table.Column<Guid>(nullable: false),
                    SegundosNotificacion = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.NotificacionId);
                    table.ForeignKey(
                        name: "FK_Notificacion_PronosticoDemanda_PronosticoDemandaId",
                        column: x => x.PronosticoDemandaId,
                        principalTable: "PronosticoDemanda",
                        principalColumn: "PronosticoDemandaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoPronosticoDemanda",
                columns: table => new
                {
                    ProductoId = table.Column<Guid>(nullable: false),
                    PronosticoDemandaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoPronosticoDemanda", x => new { x.ProductoId, x.PronosticoDemandaId });
                    table.ForeignKey(
                        name: "FK_ProductoPronosticoDemanda_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Productoid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoPronosticoDemanda_PronosticoDemanda_PronosticoDemandaId",
                        column: x => x.PronosticoDemandaId,
                        principalTable: "PronosticoDemanda",
                        principalColumn: "PronosticoDemandaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoVenta",
                columns: table => new
                {
                    ProductoId = table.Column<Guid>(nullable: false),
                    VentaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoVenta", x => new { x.ProductoId, x.VentaId });
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Productoid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoVenta_Venta_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Venta",
                        principalColumn: "VentaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_UsuarioId1",
                table: "Actividad",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_UsuarioId1",
                table: "Compra",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_ProductoId",
                table: "Favoritos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_PronosticoDemandaId",
                table: "Notificacion",
                column: "PronosticoDemandaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCompra_CompraId",
                table: "ProductoCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoPronosticoDemanda_PronosticoDemandaId",
                table: "ProductoPronosticoDemanda",
                column: "PronosticoDemandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVenta_VentaId",
                table: "ProductoVenta",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_UsuarioId",
                table: "Proveedor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_UsuarioId1",
                table: "Venta",
                column: "UsuarioId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "ProductoCompra");

            migrationBuilder.DropTable(
                name: "ProductoPronosticoDemanda");

            migrationBuilder.DropTable(
                name: "ProductoVenta");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "PronosticoDemanda");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Venta");
        }
    }
}
