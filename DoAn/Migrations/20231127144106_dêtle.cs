using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Migrations
{
    /// <inheritdoc />
    public partial class dêtle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gao_LoaiGao",
                table: "Gao");

            migrationBuilder.AddForeignKey(
                name: "FK_Gao_LoaiGao",
                table: "Gao",
                column: "IDLoaiGao",
                principalTable: "LoaiGao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gao_LoaiGao",
                table: "Gao");

            migrationBuilder.AddForeignKey(
                name: "FK_Gao_LoaiGao",
                table: "Gao",
                column: "IDLoaiGao",
                principalTable: "LoaiGao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
