using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Migrations
{
    /// <inheritdoc />
    public partial class Migrayion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiGao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenLoaiGao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiGao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuyCachDongGoi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    khoiLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyCachDongGoi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taiKhoan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    matKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loaiTaiKhoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenGao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xuatXu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    giaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dacTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tinhTrang = table.Column<int>(type: "int", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDLoaiGao = table.Column<int>(type: "int", nullable: false),
                    DangBanChay = table.Column<int>(type: "int", nullable: false),
                    conLai = table.Column<int>(type: "int", nullable: false),
                    soSao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gao_LoaiGao",
                        column: x => x.IDLoaiGao,
                        principalTable: "LoaiGao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDTaiKhoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHang_TaiKhoan",
                        column: x => x.IDTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDTaiKhoan = table.Column<int>(type: "int", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHang_TaiKhoan",
                        column: x => x.IDTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DongGoi",
                columns: table => new
                {
                    IDQuyCach = table.Column<int>(type: "int", nullable: false),
                    IDGao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DongGoi", x => new { x.IDQuyCach, x.IDGao });
                    table.ForeignKey(
                        name: "FK_DongGoi_Gao",
                        column: x => x.IDGao,
                        principalTable: "Gao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DongGoi_QuyCach",
                        column: x => x.IDQuyCach,
                        principalTable: "QuyCachDongGoi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhapHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDGao = table.Column<int>(type: "int", nullable: false),
                    soLuongNhap = table.Column<int>(type: "int", nullable: false),
                    ngayNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhapHang_Gao",
                        column: x => x.IDGao,
                        principalTable: "Gao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDGioHang = table.Column<int>(type: "int", nullable: false),
                    IDGao = table.Column<int>(type: "int", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_Gao",
                        column: x => x.IDGao,
                        principalTable: "Gao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_GioHang",
                        column: x => x.IDGioHang,
                        principalTable: "GioHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDGao = table.Column<int>(type: "int", nullable: false),
                    IDKhachHang = table.Column<int>(type: "int", nullable: false),
                    noiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soSao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhGia_Gao",
                        column: x => x.IDGao,
                        principalTable: "Gao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_KhachHang",
                        column: x => x.IDKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDKhachHang = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    TinhThanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang",
                        column: x => x.IDKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDHoaDon = table.Column<int>(type: "int", nullable: false),
                    IDGao = table.Column<int>(type: "int", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_Gao",
                        column: x => x.IDGao,
                        principalTable: "Gao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_HoaDon",
                        column: x => x.IDHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_IDGao",
                table: "ChiTietGioHang",
                column: "IDGao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_IDGioHang",
                table: "ChiTietGioHang",
                column: "IDGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_IDGao",
                table: "ChiTietHoaDon",
                column: "IDGao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_IDHoaDon",
                table: "ChiTietHoaDon",
                column: "IDHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IDGao",
                table: "DanhGia",
                column: "IDGao");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IDKhachHang",
                table: "DanhGia",
                column: "IDKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DongGoi_IDGao",
                table: "DongGoi",
                column: "IDGao");

            migrationBuilder.CreateIndex(
                name: "IX_Gao_IDLoaiGao",
                table: "Gao",
                column: "IDLoaiGao");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_IDTaiKhoan",
                table: "GioHang",
                column: "IDTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IDKhachHang",
                table: "HoaDon",
                column: "IDKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_IDTaiKhoan",
                table: "KhachHang",
                column: "IDTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_NhapHang_IDGao",
                table: "NhapHang",
                column: "IDGao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietGioHang");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "DongGoi");

            migrationBuilder.DropTable(
                name: "NhapHang");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "QuyCachDongGoi");

            migrationBuilder.DropTable(
                name: "Gao");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "LoaiGao");

            migrationBuilder.DropTable(
                name: "TaiKhoan");
        }
    }
}
