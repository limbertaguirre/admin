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

        public virtual DbSet<Administracionciclo> Administracioncicloes { get; set; }
        public virtual DbSet<Administracionciclopresentafactura> Administracionciclopresentafacturas { get; set; }
        public virtual DbSet<Administraciondescuentociclo> Administraciondescuentocicloes { get; set; }
        public virtual DbSet<Administraciondescuentociclodetalle> Administraciondescuentociclodetalles { get; set; }
        public virtual DbSet<EmpresaComplejo> EmpresaComplejoes { get; set; }
        public virtual DbSet<ProyectoConexionSufijo> ProyectoConexionSufijoes { get; set; }

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
            modelBuilder.Entity<Administracionciclo>(entity =>
            {
                entity.HasKey(e => e.LcicloId)
                    .HasName("PRIMARY");

                entity.ToTable("administracionciclo");

                entity.HasIndex(e => e.LcicloId, "lc");

                entity.Property(e => e.LcicloId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("lciclo_id");

                entity.Property(e => e.Cverenweb)
                    .HasMaxLength(1)
                    .HasColumnName("cverenweb")
                    .IsFixedLength(true);

                entity.Property(e => e.Dtfechaadd).HasColumnName("dtfechaadd");

                entity.Property(e => e.Dtfechacierre).HasColumnName("dtfechacierre");

                entity.Property(e => e.Dtfechafin).HasColumnName("dtfechafin");

                entity.Property(e => e.Dtfechainicio).HasColumnName("dtfechainicio");

                entity.Property(e => e.Dtfechamod).HasColumnName("dtfechamod");

                entity.Property(e => e.Dtfechaprecierre1).HasColumnName("dtfechaprecierre1");

                entity.Property(e => e.Dtfechaprecierre2).HasColumnName("dtfechaprecierre2");

                entity.Property(e => e.Estadogestor)
                    .HasMaxLength(1)
                    .HasColumnName("estadogestor")
                    .IsFixedLength(true);

                entity.Property(e => e.Lestado)
                    .HasColumnType("int(11)")
                    .HasColumnName("lestado");

                entity.Property(e => e.Snombre)
                    .HasMaxLength(100)
                    .HasColumnName("snombre");

                entity.Property(e => e.Susuarioadd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuarioadd");

                entity.Property(e => e.Susuariomod)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("susuariomod");
            });

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

                entity.HasOne(d => d.Lciclo)
                    .WithMany(p => p.Administraciondescuentocicloes)
                    .HasForeignKey(d => d.LcicloId)
                    .HasConstraintName("fk_administraciondescuentociclo_lciclo_id");
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

            modelBuilder.Entity<EmpresaComplejo>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresaComplejo, e.EmpresaId, e.ComplejoId })
                    .HasName("PRIMARY");

                entity.ToTable("empresa_complejo");

                entity.HasIndex(e => e.ComplejoId, "fk_provision_complejo_id");

                entity.HasIndex(e => e.EmpresaId, "fk_provision_empresa_id");

                entity.Property(e => e.IdEmpresaComplejo)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_empresa_complejo");

                entity.Property(e => e.EmpresaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("empresa_id");

                entity.Property(e => e.ComplejoId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("complejo_id");

                entity.Property(e => e.Estado)
                    .HasColumnType("bit(1)")
                    .HasColumnName("estado");

                entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion");

                entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_usuario");
            });

            modelBuilder.Entity<ProyectoConexionSufijo>(entity =>
            {
                entity.HasKey(e => e.IdProyectoConexionSufijo)
                    .HasName("PRIMARY");

                entity.ToTable("proyecto_conexion_sufijo");

                entity.Property(e => e.IdProyectoConexionSufijo)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_proyecto_conexion_sufijo");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasColumnName("estado")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion");

                entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");

                entity.Property(e => e.IdEmpresaComplejo)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_empresa_complejo");

                entity.Property(e => e.IdProyectoCnx)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id_proyecto_cnx");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_usuario");

                entity.Property(e => e.Sufijo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("sufijo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
