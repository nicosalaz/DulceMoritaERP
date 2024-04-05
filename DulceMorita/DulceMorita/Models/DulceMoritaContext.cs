using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DulceMorita.Models;

public partial class DulceMoritaContext : DbContext
{
    public DulceMoritaContext()
    {
    }

    public DulceMoritaContext(DbContextOptions<DulceMoritaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoteProduccion> LoteProduccions { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<Operario> Operarios { get; set; }

    public virtual DbSet<OrdenProduccion> OrdenProduccions { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=MPC-GSTH\\SQLEXPRESS; database=dulce_morita; Trusted_Connection=SSPI;MultipleActiveResultSets=true;Trust Server Certificate=true");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoteProduccion>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("PK__lote_pro__9A000486EAFEABC0");

            entity.ToTable("lote_produccion");

            entity.Property(e => e.IdLote).HasColumnName("id_lote");
            entity.Property(e => e.CantidadProduccion).HasColumnName("cantidad_produccion");
            entity.Property(e => e.FechaRegistro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fecha_registro");
            entity.Property(e => e.FkOrden).HasColumnName("fk_orden");

            entity.HasOne(d => d.FkOrdenNavigation).WithMany(p => p.LoteProduccions)
                .HasForeignKey(d => d.FkOrden)
                .HasConstraintName("FK__lote_prod__fk_or__02084FDA");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK__notifica__8270F9A5556737C4");

            entity.ToTable("notificacion");

            entity.Property(e => e.IdNotificacion).HasColumnName("id_notificacion");
            entity.Property(e => e.Buenas).HasColumnName("buenas");
            entity.Property(e => e.FFin)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("f_fin");
            entity.Property(e => e.FInicio)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("f_inicio");
            entity.Property(e => e.FkLote).HasColumnName("fk_lote");
            entity.Property(e => e.FkOpe).HasColumnName("fk_ope");
            entity.Property(e => e.GastosAdicionales).HasColumnName("gastos_adicionales");
            entity.Property(e => e.Malas).HasColumnName("malas");
            entity.Property(e => e.Obseraciones)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("obseraciones");

            entity.HasOne(d => d.FkLoteNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.FkLote)
                .HasConstraintName("FK__notificac__fk_lo__04E4BC85");

            entity.HasOne(d => d.FkOpeNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.FkOpe)
                .HasConstraintName("FK__notificac__fk_op__05D8E0BE");
        });

        modelBuilder.Entity<Operario>(entity =>
        {
            entity.HasKey(e => e.IdOperario).HasName("PK__operario__898003763A66D53A");

            entity.ToTable("operario");

            entity.Property(e => e.IdOperario).HasColumnName("id_operario");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
        });

        modelBuilder.Entity<OrdenProduccion>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__orden_pr__DD5B8F33E12FFE2A");

            entity.ToTable("orden_produccion");

            entity.Property(e => e.IdOrden).HasColumnName("id_orden");
            entity.Property(e => e.FechaCreacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FkProducto).HasColumnName("fk_producto");
            entity.Property(e => e.ProduccionTotal).HasColumnName("produccion_total");

            entity.HasOne(d => d.FkProductoNavigation).WithMany(p => p.OrdenProduccions)
                .HasForeignKey(d => d.FkProducto)
                .HasConstraintName("FK__orden_pro__fk_pr__7F2BE32F");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("pk_persona");

            entity.ToTable("producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
