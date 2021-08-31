using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace gestion_de_comisiones.GuardianModels
{
    public partial class grdsionContext : DbContext
    {
        public grdsionContext()
        {
        }

        public grdsionContext(DbContextOptions<grdsionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administracionciclopresentafactura> Administracionciclopresentafacturas { get; set; }
        public virtual DbSet<Administraciondescuentociclo> Administraciondescuentocicloes { get; set; }
        public virtual DbSet<Administraciondescuentociclodetalle> Administraciondescuentociclodetalles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=10.2.10.222;uid=montesion; pwd=CndFZz75u8;database=grdsion; SslMode = none;Command Timeout=3000");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administracionciclopresentafactura>(entity =>
            {
                entity.HasKey(e => e.LciclopresentafacturaId)
                    .HasName("PRIMARY");

                entity.ToTable("administracionciclopresentafactura");

                entity.HasIndex(e => new { e.LcicloId, e.LcontactoId, e.LsemanaId }, "index2")
                    .IsUnique();

                entity.Property(e => e.LciclopresentafacturaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("lciclopresentafactura_id");

                entity.Property(e => e.Dtfechaadd).HasColumnName("dtfechaadd");

                entity.Property(e => e.Dtfechamod).HasColumnName("dtfechamod");

                entity.Property(e => e.LcicloId)
                    .HasColumnType("int(11)")
                    .HasColumnName("lciclo_id");

                entity.Property(e => e.LcontactoId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("lcontacto_id");

                entity.Property(e => e.LsemanaId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("lsemana_id");

                entity.Property(e => e.Susuarioadd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuarioadd");

                entity.Property(e => e.Susuariomod)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuariomod");
            });

            modelBuilder.Entity<Administraciondescuentociclo>(entity =>
            {
                entity.HasKey(e => e.LdescuentocicloId)
                    .HasName("PRIMARY");

                entity.ToTable("administraciondescuentociclo");

                entity.HasIndex(e => e.LcontactoId, "fk_administraciondescuentociclo_lcontacto_id");

                entity.HasIndex(e => new { e.LcicloId, e.LcontactoId, e.LsemanaId }, "uq_lCicloId_ContactoId")
                    .IsUnique();

                entity.Property(e => e.LdescuentocicloId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ldescuentociclo_id");

                entity.Property(e => e.Dtfechaadd).HasColumnName("dtfechaadd");

                entity.Property(e => e.Dtfechamod).HasColumnName("dtfechamod");

                entity.Property(e => e.Dtotal)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("dtotal");

                entity.Property(e => e.LcicloId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("lciclo_id");

                entity.Property(e => e.LcontactoId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("lcontacto_id");

                entity.Property(e => e.LsemanaId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("lsemana_id");

                entity.Property(e => e.Sdetalles).HasColumnName("sdetalles");

                entity.Property(e => e.Susuarioadd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuarioadd");

                entity.Property(e => e.Susuariomod)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuariomod");
            });

            modelBuilder.Entity<Administraciondescuentociclodetalle>(entity =>
            {
                entity.HasKey(e => e.LdescuentociclodetalleId)
                    .HasName("PRIMARY");

                entity.ToTable("administraciondescuentociclodetalle");

                entity.HasIndex(e => e.LcomplejoId, "fk_administraciondescuentociclodetalle_lcomplejo_id");

                entity.HasIndex(e => e.LdescuentocicloId, "fk_administraciondescuentociclodetalle_ldescuentociclo_id");

                entity.HasIndex(e => e.LdescuentociclotipoId, "fk_administraciondescuentociclodetalle_ldescuentociclotipo_id");

                entity.Property(e => e.LdescuentociclodetalleId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ldescuentociclodetalle_id");

                entity.Property(e => e.Dmonto)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("dmonto");

                entity.Property(e => e.Dtfechaadd).HasColumnName("dtfechaadd");

                entity.Property(e => e.Dtfechamod).HasColumnName("dtfechamod");

                entity.Property(e => e.LcomplejoId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("lcomplejo_id");

                entity.Property(e => e.LdescuentocicloId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ldescuentociclo_id");

                entity.Property(e => e.LdescuentociclotipoId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ldescuentociclotipo_id");

                entity.Property(e => e.Slote)
                    .HasMaxLength(100)
                    .HasColumnName("slote");

                entity.Property(e => e.Smanzano)
                    .HasMaxLength(100)
                    .HasColumnName("smanzano");

                entity.Property(e => e.Sobservacion)
                    .HasMaxLength(1000)
                    .HasColumnName("sobservacion");

                entity.Property(e => e.Susuarioadd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuarioadd");

                entity.Property(e => e.Susuariomod)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuariomod");

                entity.Property(e => e.Suv)
                    .HasMaxLength(100)
                    .HasColumnName("suv");

                entity.HasOne(d => d.Ldescuentociclo)
                    .WithMany(p => p.Administraciondescuentociclodetalles)
                    .HasForeignKey(d => d.LdescuentocicloId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_administraciondescuentociclodetalle_ldescuentociclo_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
