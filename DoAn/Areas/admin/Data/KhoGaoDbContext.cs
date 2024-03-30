using DoAn.Areas.admin.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Areas.admin.Data
{
    public class KhoGaoDbContext: DbContext
    {
        public KhoGaoDbContext(DbContextOptions<KhoGaoDbContext> options) : base(options) { }
        public DbSet<ChiTietGioHang> ChiTietGioHang { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<DanhGia> DanhGia { get; set; }
        public DbSet<DongGoi> DongGoi { get; set; }
        public DbSet<Gao> Gao { get; set; }
        public DbSet<GioHang> GioHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<LoaiGao> LoaiGao { get; set; }
        public DbSet<NhapHang> NhapHang { get; set; }
        public DbSet<QuyCachDongGoi> QuyCachDongGoi { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DongGoi>()
                    .ToTable("DongGoi");
            modelBuilder.Entity<DongGoi>(entity =>
            {
                entity.HasKey(e => new { e.IDQuyCach, e.IDGao });

                entity.HasOne(d => d.quyCachDongGoi)
                    .WithMany(b => b.dongGois)
                    .HasForeignKey(d => d.IDQuyCach)
                    .HasConstraintName("FK_DongGoi_QuyCach");
                entity.HasOne(d => d.gao)
                    .WithMany(b => b.dongGois)
                    .HasForeignKey(d => d.IDGao)
                    .HasConstraintName("FK_DongGoi_Gao");
            });
            modelBuilder.Entity<GioHang>()
            .ToTable("GioHang");
            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasOne(d => d.taiKhoan)
                    .WithMany(c => c.gioHangs)
                    .HasForeignKey(d => d.IDTaiKhoan)
                    .HasConstraintName("FK_GioHang_TaiKhoan");
                    
            });
            modelBuilder.Entity<TaiKhoan>()
            .ToTable("TaiKhoan");
            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.Property(d => d.taiKhoan)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(d => d.matKhau)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(x => x.loaiTaiKhoan)
                    .IsRequired();
            });
            modelBuilder.Entity<ChiTietGioHang>()
            .ToTable("ChiTietGioHang");
            modelBuilder.Entity<ChiTietGioHang>(entity =>
            {
                entity.HasOne(d => d.gao)
                    .WithMany(e => e.chiTietGioHangs)
                    .HasForeignKey(entity => entity.IDGao)
                    .HasConstraintName("FK_ChiTietGioHang_Gao");
                entity.HasOne(d => d.gioHang)
                    .WithMany(e => e.chiTietGioHangs)
                    .HasForeignKey(c => c.IDGioHang)
                    .HasConstraintName("FK_ChiTietGioHang_GioHang");
                entity.Property(d => d.soLuong)
                    .IsRequired();
                entity.Property(d => d.khoiLuong)
                    .IsRequired();
                entity.Property(d => d.hinhAnh)
                    .IsRequired();
                    
            });
            modelBuilder.Entity<KhachHang>()
            .ToTable("KhachHang");
            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.Property(d => d.hoTen)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(d => d.email)
                    .IsRequired();
                entity.HasOne(d => d.taiKhoan)
                    .WithMany(e => e.khachHangs)
                    .HasForeignKey(c => c.IDTaiKhoan)
                    .HasConstraintName("FK_KhachHang_TaiKhoan");
            });
            modelBuilder.Entity<DanhGia>()
            .ToTable("DanhGia");
            modelBuilder.Entity<DanhGia>(entity =>
            {
                entity.HasOne(d => d.gao)
                    .WithMany(c => c.danhGias)
                    .HasForeignKey(v => v.IDGao)
                    .HasConstraintName("FK_DanhGia_Gao");
                entity.HasOne(d => d.taiKhoan)
                    .WithMany(e => e.danhGias)
                    .HasForeignKey(f => f.IDTaiKhoan)
                    .HasConstraintName("FK_DanhGia_TaiKhoan");
                entity.Property(d => d.noiDung)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(d =>  d.soSao)
                    .IsRequired();
                entity.Property(d => d.thoiGian)
                    .IsRequired();
            });
            modelBuilder.Entity<Gao>()
            .ToTable("Gao");
            modelBuilder.Entity<Gao>(entity =>
            {
                entity.Property(a => a.tenGao)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(b => b.xuatXu)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(d => d.giaBan)
                    .IsRequired();
                entity.Property(d => d.dacTinh)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(d => d.tinhTrang)
                    .IsRequired();
                entity.Property(d => d.moTa)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(d => d.HinhAnh)
                    .IsRequired()
                    .IsUnicode(true);
                entity.HasOne(d => d.loaiGao)
                    .WithMany(d => d.gaos)
                    .HasForeignKey(d => d.IDLoaiGao)
                    .HasConstraintName("FK_Gao_LoaiGao")
                    .OnDelete(DeleteBehavior.Restrict); // Ngăn chặn xóa nếu có bản ghi liên quan
                entity.Property(d => d.DangBanChay)
                    .IsRequired();
                entity.Property(d => d.conLai)
                    .IsRequired();
                entity.Property(d => d.soSao)
                    .IsRequired();
            });
            modelBuilder.Entity<QuyCachDongGoi>()
            .ToTable("QuyCachDongGoi");
            modelBuilder.Entity<QuyCachDongGoi>(entity =>
            {
                entity.Property(d => d.khoiLuong)
                    .IsRequired();
            });
            modelBuilder.Entity<NhapHang>()
            .ToTable("NhapHang");
            modelBuilder.Entity<NhapHang>(entity =>
            {
                entity.HasOne(d => d.gao)
                    .WithMany(e => e.nhapHangs)
                    .HasForeignKey(c => c.IDGao)
                    .HasConstraintName("FK_NhapHang_Gao");
                entity.Property(d => d.soLuongNhap)
                    .IsRequired();
                entity.Property(d => d.ngayNhap)
                    .IsRequired();
            });
            modelBuilder.Entity<LoaiGao>()
            .ToTable("LoaiGao");
            modelBuilder.Entity<LoaiGao>(entity =>
            {
                entity.Property(d => d.tenLoaiGao)
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(d => d.hinhAnh)
                    .IsRequired()
                    .IsUnicode(true);
                entity.HasMany(x => x.gaos)
                    .WithOne(x => x.loaiGao)
                    .HasForeignKey(x => x.IDLoaiGao)
                    .OnDelete(DeleteBehavior.Restrict);
                //.OnDelete(DeleteBehavior.Restrict); // Ngăn chặn xóa nếu có bản ghi liên quan
            });
            modelBuilder.Entity<ChiTietHoaDon>()
            .ToTable("ChiTietHoaDon");
            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasOne(d => d.hoaDon)
                    .WithMany(d => d.chiTietHoaDons)
                    .HasForeignKey(c => c.IDHoaDon)
                    .HasConstraintName("FK_ChiTietHoaDon_HoaDon");
                entity.Property(d => d.soLuong)
                    .IsRequired();
                entity.Property(e => e.tenGao)
                    .IsRequired();
                entity.Property(f => f.gia)
                    .IsRequired();
                entity.Property(e => e.hinhAnh)
                    .IsRequired();
                entity.Property(d => d.khoiLuong)
                    .IsRequired();
            });
            modelBuilder.Entity<HoaDon>()
            .ToTable("HoaDon");
            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.Property(d => d.Gia)
                    .IsRequired();
                entity.Property(d => d.NgayThanhToan)
                    .IsRequired();
                entity.Property(e => e.hoTen)
                    .IsRequired();
                entity.Property(x => x.eMail)
                    .IsRequired();
                entity.Property(x => x.sDt)
                    .IsRequired();
                entity.HasOne(d => d.khachHang)
                    .WithMany(e => e.hoaDons)
                    .HasForeignKey(c => c.IDKhachHang)
                    .HasConstraintName("FK_HoaDon_KhachHang");
                entity.Property(d => d.TinhThanh)
                    .IsRequired();
                entity.Property(d => d.QuanHuyen)
                    .IsRequired();
                entity.Property(d => d.PhuongXa)
                    .IsRequired();
                entity.Property(d => d.SoNha)
                    .IsRequired();
                entity.Property(d => d.SoNha)
                    .IsRequired();
                entity.Property(d => d.TrangThai)
                    .IsRequired();
            });

        }
    }
}
