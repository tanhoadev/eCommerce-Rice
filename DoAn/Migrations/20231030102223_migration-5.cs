using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Migrations
{
    /// <inheritdoc />
    public partial class migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDon_Gao",
                table: "ChiTietHoaDon");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDon_IDGao",
                table: "ChiTietHoaDon");

            migrationBuilder.RenameColumn(
                name: "IDGao",
                table: "ChiTietHoaDon",
                newName: "tenGao");

            migrationBuilder.AddColumn<decimal>(
                name: "gia",
                table: "ChiTietHoaDon",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "hinhAnh",
                table: "ChiTietHoaDon",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "khoiLuong",
                table: "ChiTietHoaDon",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gia",
                table: "ChiTietHoaDon");

            migrationBuilder.DropColumn(
                name: "hinhAnh",
                table: "ChiTietHoaDon");

            migrationBuilder.DropColumn(
                name: "khoiLuong",
                table: "ChiTietHoaDon");

            migrationBuilder.RenameColumn(
                name: "tenGao",
                table: "ChiTietHoaDon",
                newName: "IDGao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_IDGao",
                table: "ChiTietHoaDon",
                column: "IDGao");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDon_Gao",
                table: "ChiTietHoaDon",
                column: "IDGao",
                principalTable: "Gao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
