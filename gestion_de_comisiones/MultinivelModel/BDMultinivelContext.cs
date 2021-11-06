using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class BDMultinivelContext : DbContext
    {
        public BDMultinivelContext()
        {
        }

        public BDMultinivelContext(DbContextOptions<BDMultinivelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VwListarAutorizacionesTipo> VwListarAutorizacionesTipoes { get; set; }
        public virtual DbSet<VwObtenerCiclo> VwObtenerCiclos { get; set; }
        public virtual DbSet<VwObtenerComisionesDetalleAplicacione> VwObtenerComisionesDetalleAplicaciones { get; set; }
        public virtual DbSet<VwObtenerComisionesDetalleEmpresa> VwObtenerComisionesDetalleEmpresas { get; set; }
        public virtual DbSet<VwObtenerEmpresasComisionesDetalleEmpresa> VwObtenerEmpresasComisionesDetalleEmpresas { get; set; }
        public virtual DbSet<VwObtenerFicha> VwObtenerFichas { get; set; }
        public virtual DbSet<VwObtenerInfoExcelFormatoBanco> VwObtenerInfoExcelFormatoBancoes { get; set; }
        public virtual DbSet<VwObtenerProyectoxProducto> VwObtenerProyectoxProductoes { get; set; }
        public virtual DbSet<VwObtenercomisione> VwObtenercomisiones { get; set; }
        public virtual DbSet<VwObtenercomisionesFormaPago> VwObtenercomisionesFormaPagoes { get; set; }
        public virtual DbSet<VwTipoAutorizacion> VwTipoAutorizacions { get; set; }
        public virtual DbSet<VwVerificarAutorizacionComision> VwVerificarAutorizacionComisions { get; set; }
        public virtual DbSet<VwVerificarCuentasUsuario> VwVerificarCuentasUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.2.10.20;Database=BDMultinivel; User Id=sa;password=Passw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<VwListarAutorizacionesTipo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_LISTAR_AUTORIZACIONES_TIPO");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.DescripcionArea)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_area");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.IdArea).HasColumnName("id_area");

                entity.Property(e => e.IdTipoAutorizacion).HasColumnName("id_tipo_autorizacion");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioAutorizacion).HasColumnName("id_usuario_autorizacion");

                entity.Property(e => e.NombreTipoAutorizacion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_tipo_autorizacion");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<VwObtenerCiclo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerCiclos");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdCiclo).HasColumnName("id_ciclo");

                entity.Property(e => e.IdEstadoComision).HasColumnName("id_estado_comision");

                entity.Property(e => e.IdTipoComision).HasColumnName("id_tipo_comision");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<VwObtenerComisionesDetalleAplicacione>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerComisionesDetalleAplicaciones");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdAplicacionDetalleProducto).HasColumnName("id_aplicacion_detalle_producto");

                entity.Property(e => e.IdComisionDetalle).HasColumnName("id_comision_detalle");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto");

                entity.Property(e => e.NombreEmpresa)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_empresa");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("subtotal");
            });

            modelBuilder.Entity<VwObtenerComisionesDetalleEmpresa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerComisionesDetalleEmpresa");

                entity.Property(e => e.Empresa)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("empresa");

                entity.Property(e => e.EstadoDetalleEmpresa).HasColumnName("estadoDetalleEmpresa");

                entity.Property(e => e.EstadoEmpresa).HasColumnName("estadoEmpresa");

                entity.Property(e => e.IdComision).HasColumnName("idComision");

                entity.Property(e => e.IdComisionDetalle).HasColumnName("id_comision_detalle");

                entity.Property(e => e.IdComisionDetalleEmpresa).HasColumnName("id_comision_detalle_empresa");

                entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto");

                entity.Property(e => e.MontoAFacturar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_a_facturar");

                entity.Property(e => e.MontoNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_neto");

                entity.Property(e => e.MontoTotalFacturar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total_facturar");

                entity.Property(e => e.NroAutorizacion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("nro_autorizacion");

                entity.Property(e => e.Residual)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("residual");

                entity.Property(e => e.RespaldoPath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("respaldo_path");

                entity.Property(e => e.Retencion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("retencion");

                entity.Property(e => e.SiFacturo).HasColumnName("si_facturo");

                entity.Property(e => e.VentasGrupales)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ventas_grupales");

                entity.Property(e => e.VentasPersonales)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ventas_personales");
            });

            modelBuilder.Entity<VwObtenerEmpresasComisionesDetalleEmpresa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerEmpresasComisionesDetalleEmpresa");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("empresa");

                entity.Property(e => e.IdCiclo).HasColumnName("id_ciclo");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdTipoComision).HasColumnName("id_tipo_comision");

                entity.Property(e => e.IdTipoPago).HasColumnName("id_tipo_pago");

                entity.Property(e => e.MontoTransferir)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("monto_transferir");
            });

            modelBuilder.Entity<VwObtenerFicha>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerFicha");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ci");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.CodigoBanco)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("codigoBanco");

                entity.Property(e => e.CuentaBancaria)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cuentaBancaria");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdBanco).HasColumnName("idBanco");

                entity.Property(e => e.IdFicha).HasColumnName("idFicha");

                entity.Property(e => e.Nivel)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nivel");

                entity.Property(e => e.NombreBanco)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombreBanco");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false)
                    .HasColumnName("nombreCompleto");

                entity.Property(e => e.TieneCuentaBancaria).HasColumnName("tieneCuentaBancaria");
            });

            modelBuilder.Entity<VwObtenerInfoExcelFormatoBanco>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerInfoExcelFormatoBanco");

                entity.Property(e => e.CodigoDeCliente)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_CLIENTE");

                entity.Property(e => e.DocDeIdentidad)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DOC_DE_IDENTIDAD");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("empresa");

                entity.Property(e => e.EntidadDestino).HasColumnName("ENTIDAD_DESTINO");

                entity.Property(e => e.FechaDePago)
                    .HasMaxLength(92)
                    .IsUnicode(false)
                    .HasColumnName("FECHA_DE_PAGO");

                entity.Property(e => e.FormaDePago).HasColumnName("FORMA_DE_PAGO");

                entity.Property(e => e.Glosa)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GLOSA");

                entity.Property(e => e.IdCiclo).HasColumnName("id_ciclo");

                entity.Property(e => e.IdComisionDetalleEmpresa).HasColumnName("id_comision_detalle_empresa");

                entity.Property(e => e.IdComisionesDetalle).HasColumnName("id_comisiones_detalle");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdEstadoComisionDetalleEmpresa).HasColumnName("id_estado_comision_detalle_empresa");

                entity.Property(e => e.IdTipoPago).HasColumnName("id_tipo_pago");

                entity.Property(e => e.ImporteNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("IMPORTE_NETO");

                entity.Property(e => e.ImportePorEmpresa)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("IMPORTE_POR_EMPRESA");

                entity.Property(e => e.MonedaDestino).HasColumnName("MONEDA_DESTINO");

                entity.Property(e => e.NombreDeCliente)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_DE_CLIENTE");

                entity.Property(e => e.NroDeCuenta)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NRO_DE_CUENTA");
            });

            modelBuilder.Entity<VwObtenerProyectoxProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerProyectoxProducto");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreEmpresa");

                entity.Property(e => e.NombreProyecto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreProyecto");

                entity.Property(e => e.Producto)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("producto")
                    .IsFixedLength(true)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<VwObtenercomisione>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenercomisiones");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ci");

                entity.Property(e => e.Ciclo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ciclo");

                entity.Property(e => e.CuentaBancaria)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cuentaBancaria");

                entity.Property(e => e.EstadoDetalleFacturaNombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estadoDetalleFacturaNombre");

                entity.Property(e => e.EstadoFacturoId).HasColumnName("estadoFacturoId");

                entity.Property(e => e.Factura)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("factura");

                entity.Property(e => e.IdBanco).HasColumnName("id_banco");

                entity.Property(e => e.IdCiclo).HasColumnName("id_ciclo");

                entity.Property(e => e.IdComision).HasColumnName("idComision");

                entity.Property(e => e.IdComisionDetalle).HasColumnName("idComisionDetalle");

                entity.Property(e => e.IdEstadoComision).HasColumnName("id_estado_comision");

                entity.Property(e => e.IdFicha).HasColumnName("idFicha");

                entity.Property(e => e.IdTipoComision).HasColumnName("id_tipo_comision");

                entity.Property(e => e.MontoAplicacion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_aplicacion");

                entity.Property(e => e.MontoBruto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("montoBruto");

                entity.Property(e => e.MontoNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("montoNeto");

                entity.Property(e => e.MontoRetencion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_retencion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreBanco)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombreBanco");
            });

            modelBuilder.Entity<VwObtenercomisionesFormaPago>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenercomisionesFormaPago");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ci");

                entity.Property(e => e.Ciclo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ciclo");

                entity.Property(e => e.CuentaBancaria)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cuentaBancaria");

                entity.Property(e => e.EstadoDetalleFacturaNombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estadoDetalleFacturaNombre");

                entity.Property(e => e.EstadoFacturoId).HasColumnName("estadoFacturoId");

                entity.Property(e => e.Factura)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("factura");

                entity.Property(e => e.IdBanco).HasColumnName("id_banco");

                entity.Property(e => e.IdCiclo).HasColumnName("id_ciclo");

                entity.Property(e => e.IdComision).HasColumnName("idComision");

                entity.Property(e => e.IdComisionDetalle).HasColumnName("idComisionDetalle");

                entity.Property(e => e.IdDetalleEstadoFormaPago).HasColumnName("id_detalle_estado_forma_pago");

                entity.Property(e => e.IdEstadoComision).HasColumnName("id_estado_comision");

                entity.Property(e => e.IdEstadoListadoFormaPago).HasColumnName("id_estado_listado_forma_pago");

                entity.Property(e => e.IdFicha).HasColumnName("idFicha");

                entity.Property(e => e.IdListaFormasPago).HasColumnName("id_lista_formas_pago");

                entity.Property(e => e.IdTipoComision).HasColumnName("id_tipo_comision");

                entity.Property(e => e.IdTipoPago).HasColumnName("id_tipo_pago");

                entity.Property(e => e.MontoAplicacion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_aplicacion");

                entity.Property(e => e.MontoBruto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("montoBruto");

                entity.Property(e => e.MontoNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("montoNeto");

                entity.Property(e => e.MontoRetencion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_retencion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreBanco)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombreBanco");

                entity.Property(e => e.PagoDetalleHabilitado).HasColumnName("pago_detalle_habilitado");

                entity.Property(e => e.TipoPagoDescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pago_descripcion");
            });

            modelBuilder.Entity<VwTipoAutorizacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_TIPO_AUTORIZACION");

                entity.Property(e => e.CantidadAprobacionMinimaArea).HasColumnName("cantidad_aprobacion_minima_area");

                entity.Property(e => e.CantidadLimite).HasColumnName("cantidad_limite");

                entity.Property(e => e.IdTipoAutorizacion).HasColumnName("id_tipo_autorizacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<VwVerificarAutorizacionComision>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwVerificarAutorizacionComision");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdAutorizacionComision).HasColumnName("id_autorizacion_comision");

                entity.Property(e => e.IdCiclo).HasColumnName("id_ciclo");

                entity.Property(e => e.IdComision).HasColumnName("id_comision");

                entity.Property(e => e.IdEstadoAutorizacionComision).HasColumnName("id_estado_autorizacion_comision");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioAutorizacion).HasColumnName("id_usuario_autorizacion");
            });

            modelBuilder.Entity<VwVerificarCuentasUsuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwVerificarCuentasUsuario");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ci");

                entity.Property(e => e.EstadoSionPay).HasColumnName("estadoSionPay");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.SionPay)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sionPay");

                entity.Property(e => e.TieneCuentaBancaria).HasColumnName("tiene_cuenta_bancaria");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
