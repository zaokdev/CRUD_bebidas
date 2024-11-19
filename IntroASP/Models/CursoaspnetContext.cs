using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace IntroASP.Models;

public partial class CursoaspnetContext : DbContext
{
    public CursoaspnetContext()
    {
    }

    public CursoaspnetContext(DbContextOptions<CursoaspnetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=1234;database=cursoaspnet", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.0.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => e.Beerid).HasName("PRIMARY");

            entity.ToTable("beer");

            entity.HasIndex(e => e.Brandid, "Fk_beer_brand");

            entity.Property(e => e.Beerid).HasColumnName("beerid");
            entity.Property(e => e.Brandid).HasColumnName("brandid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Beers)
                .HasForeignKey(d => d.Brandid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_beer_brand");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.brandid).HasName("PRIMARY");

            entity.ToTable("brand");

            entity.Property(e => e.brandid).HasColumnName("brandid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
