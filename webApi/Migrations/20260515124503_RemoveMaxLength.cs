using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaxLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_empresa_cliente_ClienteID",
                table: "empresa");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteID",
                table: "empresa",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_empresa_cliente_ClienteID",
                table: "empresa",
                column: "ClienteID",
                principalTable: "cliente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_empresa_cliente_ClienteID",
                table: "empresa");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteID",
                table: "empresa",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_empresa_cliente_ClienteID",
                table: "empresa",
                column: "ClienteID",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
