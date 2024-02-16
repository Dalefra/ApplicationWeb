using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWeb.Models;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-C038C9D; Database=Store; User=sa; Password=Doas98; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasIndex(e => e.BrandId, "IX_Beers_BrandID");

            entity.Property(e => e.BeerId).HasColumnName("BeerID");
            entity.Property(e => e.Alcohol).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");

            entity.HasOne(d => d.Brand).WithMany(p => p.Beers).HasForeignKey(d => d.BrandId);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.IdLog);

            entity.Property(e => e.IdLog)
                .ValueGeneratedNever()
                .HasColumnName("Id_Log");
            entity.Property(e => e.Accion)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.FechaLog)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Fecha_log");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
