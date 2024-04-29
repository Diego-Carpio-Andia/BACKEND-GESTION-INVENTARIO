using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class EntidadesCreadasFinales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_AspNetUsers_UsuarioId",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Proveedor_UsuarioId",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_ProductoId",
                table: "Favoritos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Proveedor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Proveedor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Favoritos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Favoritos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_UsuarioId1",
                table: "Proveedor",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_ProductoId",
                table: "Favoritos",
                column: "ProductoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UsuarioId",
                table: "Favoritos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_AspNetUsers_UsuarioId",
                table: "Favoritos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_AspNetUsers_UsuarioId1",
                table: "Proveedor",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_AspNetUsers_UsuarioId",
                table: "Favoritos");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_AspNetUsers_UsuarioId1",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Proveedor_UsuarioId1",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_ProductoId",
                table: "Favoritos");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UsuarioId",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Favoritos");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Proveedor",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_UsuarioId",
                table: "Proveedor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_ProductoId",
                table: "Favoritos",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_AspNetUsers_UsuarioId",
                table: "Proveedor",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
