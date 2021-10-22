using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class D : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulosCategorias_Articulos_ArticulosId",
                table: "ArticulosCategorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticulosCategorias",
                table: "ArticulosCategorias");

            migrationBuilder.DropIndex(
                name: "IX_ArticulosCategorias_ArticulosId",
                table: "ArticulosCategorias");

            migrationBuilder.DropColumn(
                name: "ArticuloId",
                table: "ArticulosCategorias");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticulosId",
                table: "ArticulosCategorias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "titulo",
                table: "Articulos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticulosCategorias",
                table: "ArticulosCategorias",
                columns: new[] { "ArticulosId", "CategoriaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulosCategorias_Articulos_ArticulosId",
                table: "ArticulosCategorias",
                column: "ArticulosId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticulosCategorias_Articulos_ArticulosId",
                table: "ArticulosCategorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticulosCategorias",
                table: "ArticulosCategorias");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ArticulosId",
                table: "ArticulosCategorias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ArticuloId",
                table: "ArticulosCategorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "titulo",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticulosCategorias",
                table: "ArticulosCategorias",
                columns: new[] { "ArticuloId", "CategoriaId" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosCategorias_ArticulosId",
                table: "ArticulosCategorias",
                column: "ArticulosId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticulosCategorias_Articulos_ArticulosId",
                table: "ArticulosCategorias",
                column: "ArticulosId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
