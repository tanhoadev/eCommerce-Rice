using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_KhachHang",
                table: "DanhGia");

            migrationBuilder.RenameColumn(
                name: "IDKhachHang",
                table: "DanhGia",
                newName: "IDTaiKhoan");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGia_IDKhachHang",
                table: "DanhGia",
                newName: "IX_DanhGia_IDTaiKhoan");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_TaiKhoan",
                table: "DanhGia",
                column: "IDTaiKhoan",
                principalTable: "TaiKhoan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_TaiKhoan",
                table: "DanhGia");

            migrationBuilder.RenameColumn(
                name: "IDTaiKhoan",
                table: "DanhGia",
                newName: "IDKhachHang");

            migrationBuilder.RenameIndex(
                name: "IX_DanhGia_IDTaiKhoan",
                table: "DanhGia",
                newName: "IX_DanhGia_IDKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_KhachHang",
                table: "DanhGia",
                column: "IDKhachHang",
                principalTable: "KhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
