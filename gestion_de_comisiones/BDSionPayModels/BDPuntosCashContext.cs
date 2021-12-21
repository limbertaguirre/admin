using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace gestion_de_comisiones.BDSionPayModels
{
    public partial class BDPuntosCashContext : DbContext
    {
        public BDPuntosCashContext()
        {
        }

        public BDPuntosCashContext(DbContextOptions<BDPuntosCashContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.2.10.15;Database=BDPuntosCash; User Id=sa;password=Passw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__CUENTA__C7E28685E02E35E6");

                entity.ToTable("CUENTA");

                entity.HasIndex(e => e.NroCuenta, "UQ__CUENTA__63902210C5A7EBDC")
                    .IsUnique();

                entity.Property(e => e.IdCuenta)
                    .HasColumnName("id_cuenta")
                    .HasComment("Identificador de la cuenta");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))")
                    .IsFixedLength(true)
                    .HasComment("Estado de la cuenta");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de creacion de registro");

                entity.Property(e => e.IdUsuario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id_usuario");

                entity.Property(e => e.NroCuenta)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nro_cuenta")
                    .HasComment("Codigo de cuenta del usuario");

                entity.Property(e => e.SaldoActual)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("saldo_actual")
                    .HasComment("Saldo actual de puntos");

                entity.Property(e => e.SaldoNoTransferible)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("saldo_no_transferible")
                    .HasComment("Saldo de puntos no transferibles");

                entity.Property(e => e.SaldoTransferibleDealer)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("saldo_transferible_dealer")
                    .HasComment("Monto el cual puede transferir el dealer.");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__4E3E04ADA550EF02");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id_usuario")
                    .HasComment("Identificador del usuario");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de creacion del registro");

                entity.Property(e => e.IdEstadoUsuario)
                    .HasColumnName("id_estado_usuario")
                    .HasComment("Foreign Key de la tabla ESTADO_USUARIO");

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuario")
                    .HasComment("Carnet de identidad del usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
