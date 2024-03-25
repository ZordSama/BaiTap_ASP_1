using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TruongABC_PhamQuangSang.Models;

public partial class TruongAbcContext : DbContext
{
    public TruongAbcContext(DbContextOptions<TruongAbcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<GiangVien> GiangViens { get; set; }

    public virtual DbSet<GioiThieu> GioiThieus { get; set; }

    public virtual DbSet<LichCt> LichCts { get; set; }

    public virtual DbSet<LienKet> LienKets { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhomTin> NhomTins { get; set; }

    public virtual DbSet<TinTuc> TinTucs { get; set; }

    public virtual DbSet<VanBanPq> VanBanPqs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DIEM");

            entity.Property(e => e.AnhDiem).HasMaxLength(1);
            entity.Property(e => e.IdLop).HasColumnName("ID_LOP");
            entity.Property(e => e.IdMon).HasColumnName("ID_MON");

            entity.HasOne(d => d.IdLopNavigation).WithMany()
                .HasForeignKey(d => d.IdLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__ID_LOP__48CFD27E");

            entity.HasOne(d => d.IdMonNavigation).WithMany()
                .HasForeignKey(d => d.IdMon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__ID_MON__49C3F6B7");
        });

        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.HasKey(e => e.IdGv).HasName("PK__GIANG_VI__8B62CF1286DCE981");

            entity.ToTable("GIANG_VIEN");

            entity.Property(e => e.IdGv).HasColumnName("ID_GV");
            entity.Property(e => e.DienThoai).HasMaxLength(20);
            entity.Property(e => e.TenGv)
                .HasMaxLength(255)
                .HasColumnName("TenGV");
        });

        modelBuilder.Entity<GioiThieu>(entity =>
        {
            entity.HasKey(e => e.IdGt).HasName("PK__GIOI_THI__8B62CF10E07EC103");

            entity.ToTable("GIOI_THIEU");

            entity.Property(e => e.IdGt).HasColumnName("ID_GT");
            entity.Property(e => e.NoiDungGt)
                .HasColumnType("ntext")
                .HasColumnName("NoiDungGT");
            entity.Property(e => e.TenGt)
                .HasMaxLength(255)
                .HasColumnName("TenGT");
        });

        modelBuilder.Entity<LichCt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LICH_CT__3214EC279ADD6ACB");

            entity.ToTable("LICH_CT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiaDiem).HasMaxLength(255);
            entity.Property(e => e.NoiDung).HasMaxLength(500);
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");
        });

        modelBuilder.Entity<LienKet>(entity =>
        {
            entity.HasKey(e => e.IdLk).HasName("PK__LIEN_KET__8B62F4AB70105233");

            entity.ToTable("LIEN_KET");

            entity.Property(e => e.IdLk).HasColumnName("ID_LK");
            entity.Property(e => e.Anh).HasMaxLength(500);
            entity.Property(e => e.Link).HasMaxLength(500);
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.IdLop).HasName("PK__LOP__2DBE37947D9FDC90");

            entity.ToTable("LOP");

            entity.Property(e => e.IdLop).HasColumnName("ID_LOP");
            entity.Property(e => e.TenLop).HasMaxLength(255);
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.IdMon).HasName("PK__MON_HOC__276DB7291769F7A2");

            entity.ToTable("MON_HOC");

            entity.Property(e => e.IdMon).HasColumnName("ID_MON");
            entity.Property(e => e.TenMon).HasMaxLength(255);
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NGUOI_DUNG");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("ID");
            entity.Property(e => e.Passwd).HasMaxLength(255);
            entity.Property(e => e.Quyen).HasComment("");
            entity.Property(e => e.TenNgDung).HasMaxLength(255);
        });

        modelBuilder.Entity<NhomTin>(entity =>
        {
            entity.HasKey(e => e.IdNhomTin).HasName("PK__NHOM_TIN__3DE82897278A69AF");

            entity.ToTable("NHOM_TIN");

            entity.Property(e => e.IdNhomTin).HasColumnName("ID_NhomTin");
            entity.Property(e => e.TenNhomTin).HasMaxLength(255);
        });

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasKey(e => e.IdTin).HasName("PK__TIN_TUC__27BF62CC19B93B16");

            entity.ToTable("TIN_TUC");

            entity.Property(e => e.IdTin).HasColumnName("ID_TIN");
            entity.Property(e => e.Anh).HasMaxLength(500);
            entity.Property(e => e.NoiDung).HasColumnType("ntext");
            entity.Property(e => e.TenTin).HasMaxLength(255);
            entity.Property(e => e.TomTat).HasColumnType("ntext");

            entity.HasOne(d => d.NhomTinNavigation).WithMany(p => p.TinTucs)
                .HasForeignKey(d => d.NhomTin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TIN_TUC__NhomTin__47DBAE45");
        });

        modelBuilder.Entity<VanBanPq>(entity =>
        {
            entity.HasKey(e => e.MaVb).HasName("PK__VAN_BAN___53E1D4C9B94257DF");

            entity.ToTable("VAN_BAN_PQ");

            entity.Property(e => e.MaVb)
                .HasMaxLength(50)
                .HasColumnName("MA_VB");
            entity.Property(e => e.Link).HasMaxLength(500);
            entity.Property(e => e.TenVb)
                .HasMaxLength(255)
                .HasColumnName("TenVB");
            entity.Property(e => e.TomTat).HasColumnType("ntext");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
