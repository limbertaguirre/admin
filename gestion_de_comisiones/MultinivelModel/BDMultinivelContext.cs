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

        public virtual DbSet<AplicacionDetalleProducto> AplicacionDetalleProductoes { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AsignacionEmpresaPago> AsignacionEmpresaPagoes { get; set; }
        public virtual DbSet<AutorizacionComision> AutorizacionComisions { get; set; }
        public virtual DbSet<AutorizacionesArea> AutorizacionesAreas { get; set; }
        public virtual DbSet<Banco> Bancoes { get; set; }
        public virtual DbSet<Bitacora> Bitacoras { get; set; }
        public virtual DbSet<BitacoraDetalle> BitacoraDetalles { get; set; }
        public virtual DbSet<Ciclo> Cicloes { get; set; }
        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<ComisionDetalleEmpresa> ComisionDetalleEmpresas { get; set; }
        public virtual DbSet<ControlUsuario> ControlUsuarios { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<EstadoAutorizacionComision> EstadoAutorizacionComisions { get; set; }
        public virtual DbSet<EstadoListadoFormaPago> EstadoListadoFormaPagoes { get; set; }
        public virtual DbSet<Ficha> Fichas { get; set; }
        public virtual DbSet<FichaIncentivo> FichaIncentivoes { get; set; }
        public virtual DbSet<FichaNivelI> FichaNivelIs { get; set; }
        public virtual DbSet<FichaTipoBajaI> FichaTipoBajaIs { get; set; }
        public virtual DbSet<GpClienteVendedorI> GpClienteVendedorIs { get; set; }
        public virtual DbSet<GpComision> GpComisions { get; set; }
        public virtual DbSet<GpComisionDetalle> GpComisionDetalles { get; set; }
        public virtual DbSet<GpComisionDetalleEstadoI> GpComisionDetalleEstadoIs { get; set; }
        public virtual DbSet<GpComisionEstadoComisionI> GpComisionEstadoComisionIs { get; set; }
        public virtual DbSet<GpDetalleEstadoListadoFormaPagol> GpDetalleEstadoListadoFormaPagols { get; set; }
        public virtual DbSet<GpEstadoComision> GpEstadoComisions { get; set; }
        public virtual DbSet<GpEstadoComisionDetalle> GpEstadoComisionDetalles { get; set; }
        public virtual DbSet<GpEstadoProrrateoDetalle> GpEstadoProrrateoDetalles { get; set; }
        public virtual DbSet<GpEstadoProrrateoDetalleIncentivo> GpEstadoProrrateoDetalleIncentivoes { get; set; }
        public virtual DbSet<GpPorrateroDetalleIncentivo> GpPorrateroDetalleIncentivoes { get; set; }
        public virtual DbSet<GpProrrateoDetalle> GpProrrateoDetalles { get; set; }
        public virtual DbSet<GpTipoComision> GpTipoComisions { get; set; }
        public virtual DbSet<Incentivo> Incentivoes { get; set; }
        public virtual DbSet<IncentivoPagoComision> IncentivoPagoComisions { get; set; }
        public virtual DbSet<ListadoFormasPago> ListadoFormasPagoes { get; set; }
        public virtual DbSet<LogComprobanteBanco> LogComprobanteBancoes { get; set; }
        public virtual DbSet<LogComprobanteContable> LogComprobanteContables { get; set; }
        public virtual DbSet<LogDetalleComisionEmpresaFail> LogDetalleComisionEmpresaFails { get; set; }
        public virtual DbSet<LogPagoComisionTransferenciaPorEmpresa> LogPagoComisionTransferenciaPorEmpresas { get; set; }
        public virtual DbSet<LogPagoMasivoSionPayComisionOEmpresaFail> LogPagoMasivoSionPayComisionOEmpresaFails { get; set; }
        public virtual DbSet<Modulo> Moduloes { get; set; }
        public virtual DbSet<Nivel> Nivels { get; set; }
        public virtual DbSet<Pagina> Paginas { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Permiso> Permisoes { get; set; }
        public virtual DbSet<Proyecto> Proyectoes { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<RolPaginaI> RolPaginaIs { get; set; }
        public virtual DbSet<RolPaginaPermisoI> RolPaginaPermisoIs { get; set; }
        public virtual DbSet<Sucursal> Sucursals { get; set; }
        public virtual DbSet<TipoAplicacione> TipoAplicaciones { get; set; }
        public virtual DbSet<TipoAutorizacion> TipoAutorizacions { get; set; }
        public virtual DbSet<TipoBaja> TipoBajas { get; set; }
        public virtual DbSet<TipoIncentivo> TipoIncentivoes { get; set; }
        public virtual DbSet<TipoIncentivoPago> TipoIncentivoPagoes { get; set; }
        public virtual DbSet<TipoPago> TipoPagoes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioAutorizacion> UsuarioAutorizacions { get; set; }
        public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VwListarAutorizacionesTipo> VwListarAutorizacionesTipoes { get; set; }
        public virtual DbSet<VwObtenerCiclo> VwObtenerCiclos { get; set; }
        public virtual DbSet<VwObtenerCiclosRezagado> VwObtenerCiclosRezagados { get; set; }
        public virtual DbSet<VwObtenerComisionesDetalleAplicacione> VwObtenerComisionesDetalleAplicaciones { get; set; }
        public virtual DbSet<VwObtenerComisionesDetalleEmpresa> VwObtenerComisionesDetalleEmpresas { get; set; }
        public virtual DbSet<VwObtenerEmpresasComisionesDetalleEmpresa> VwObtenerEmpresasComisionesDetalleEmpresas { get; set; }
        public virtual DbSet<VwObtenerFicha> VwObtenerFichas { get; set; }
        public virtual DbSet<VwObtenerInfoExcelFormatoBanco> VwObtenerInfoExcelFormatoBancoes { get; set; }
        public virtual DbSet<VwObtenerProyectoxProducto> VwObtenerProyectoxProductoes { get; set; }
        public virtual DbSet<VwObtenerRezagadosPago> VwObtenerRezagadosPagos { get; set; }
        public virtual DbSet<VwObtenercomisione> VwObtenercomisiones { get; set; }
        public virtual DbSet<VwObtenercomisionesFormaPago> VwObtenercomisionesFormaPagoes { get; set; }
        public virtual DbSet<VwPagosIncentivo> VwPagosIncentivos { get; set; }
        public virtual DbSet<VwTipoAutorizacion> VwTipoAutorizacions { get; set; }
        public virtual DbSet<VwVerificarAutorizacionComision> VwVerificarAutorizacionComisions { get; set; }
        public virtual DbSet<VwVerificarCuentasUsuario> VwVerificarCuentasUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.2.10.15;Database=BDMultinivel; User Id=sa;password=Passw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<AplicacionDetalleProducto>(entity =>
            {
                entity.HasKey(e => e.IdAplicacionDetalleProducto)
                    .HasName("PK__APLICACI__DE63A1C56534C493");

                entity.ToTable("APLICACION_DETALLE_PRODUCTO");

                entity.Property(e => e.IdAplicacionDetalleProducto)
                    .HasColumnName("id_aplicacion_detalle_producto")
                    .HasComment("Llave primaria de la tabla autoincremental.");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasComment("son la cantidad de  cuotas pagadas.");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo_producto")
                    .HasComment("Es el codigo de un producto lote o kalomai  general de gruposion.");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion de la aplicacion de una aplicacion por producto.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdBdqishur)
                    .HasColumnName("id_bdqishur")
                    .HasComment("El id_bdqishur es el ide de la tabla que hace referencia al id primario de la tabla AplicacionesPagos de la bd bdqishur");

                entity.Property(e => e.IdComisionesDetalle)
                    .HasColumnName("id_comisiones_detalle")
                    .HasComment("llave foranead de la tabla comision detalle donde se tiene toda la comision del cliente frilanzer.");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("id_proyecto")
                    .HasComment("llave foranea que hace referencia al codigo de un proyecto de grupo sion.");

                entity.Property(e => e.IdTipoAplicaciones)
                    .HasColumnName("id_tipo_aplicaciones")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasComment("Es el monto de la cuotas pagar total.");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("subtotal")
                    .HasComment("Es monto subtotal de una aplicacion por producto.");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea)
                    .HasName("PK__AREA__8A8C837BCAFB192A");

                entity.ToTable("AREA");

                entity.Property(e => e.IdArea)
                    .HasColumnName("id_area")
                    .HasComment("Llave primaria incremental de la tabla AREA.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del área al cual pertenece el usuario.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del área. Ej. Calidad, Finanzas, Cartera, etc.");
            });

            modelBuilder.Entity<AsignacionEmpresaPago>(entity =>
            {
                entity.HasKey(e => e.IdAsignacionEmpresaPago)
                    .HasName("PK__ASIGNACI__05B4D3C3AFF03538");

                entity.ToTable("ASIGNACION_EMPRESA_PAGO");

                entity.Property(e => e.IdAsignacionEmpresaPago)
                    .HasColumnName("id_asignacion_empresa_pago")
                    .HasComment("Llave primaria incremental de la tabla ASIGNACION_EMPRESA_PAGO.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEmpresa)
                    .HasColumnName("id_empresa")
                    .HasComment("Llave foranea a la tabla EMPRESA.");

                entity.Property(e => e.IdTipoPago)
                    .HasColumnName("id_tipo_pago")
                    .HasComment("Llave foranea a la tabla TIPO DE PAGO.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("Llave foranea a la tabla USUARIO.");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            });

            modelBuilder.Entity<AutorizacionComision>(entity =>
            {
                entity.HasKey(e => e.IdAutorizacionComision)
                    .HasName("PK__AUTORIZA__AF468D671175C8AE");

                entity.ToTable("AUTORIZACION_COMISION");

                entity.Property(e => e.IdAutorizacionComision)
                    .HasColumnName("id_autorizacion_comision")
                    .HasComment("es la llave primaria de la tabla");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdComision)
                    .HasColumnName("id_comision")
                    .HasComment("es la llave foranea de la comision");

                entity.Property(e => e.IdEstadoAutorizacionComision).HasColumnName("id_estado_autorizacion_comision");

                entity.Property(e => e.IdUsuarioAutorizacion)
                    .HasColumnName("id_usuario_autorizacion")
                    .HasComment("es la llave foranea de la tabla id autorizacion usuario.");

                entity.Property(e => e.IdUsuarioModificacion)
                    .HasColumnName("id_usuario_modificacion")
                    .HasComment("Es el i usuario que creo o modifico el registro");
            });

            modelBuilder.Entity<AutorizacionesArea>(entity =>
            {
                entity.HasKey(e => e.IdAutorizacionesArea)
                    .HasName("PK__AUTORIZA__28E9D5F8B33EF508");

                entity.ToTable("AUTORIZACIONES_AREA");

                entity.Property(e => e.IdAutorizacionesArea)
                    .HasColumnName("id_autorizaciones_area")
                    .HasComment("Es es la llave primaria de la tabla autoincremental.");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasComment("Es la cantidad minima o igual que se puede tener para una autorizacion por tipo.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdArea)
                    .HasColumnName("id_area")
                    .HasComment("LLave foranea que hace referencia al id de la tabla area en de trabajo.");

                entity.Property(e => e.IdTipoAutorizacion)
                    .HasColumnName("id_tipo_autorizacion")
                    .HasComment("LLave foranea que hace referencia al id de la tabla tipo autorizacion");

                entity.Property(e => e.IdUsuarioModificacion)
                    .HasColumnName("id_usuario_modificacion")
                    .HasComment("Es la descripcion del registro");
            });

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.IdBanco)
                    .HasName("PK__BANCO__70BD16428F6D8D09");

                entity.ToTable("BANCO");

                entity.Property(e => e.IdBanco)
                    .ValueGeneratedNever()
                    .HasColumnName("id_banco")
                    .HasComment("Llave primaria incremental de la tabla BANCO.");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("codigo")
                    .HasComment("Es el código único que el banco proporciona.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del Banco.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del Banco. Ej.: BANCO NACIONAL DE BOLIVIA, BANCO MERCANTIL SANTA CRUZ, etc.");
            });

            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.IdBitacora)
                    .HasName("PK__BITACORA__7E4268B0E6B753F7");

                entity.ToTable("BITACORA");

                entity.Property(e => e.IdBitacora)
                    .HasColumnName("id_bitacora")
                    .HasComment("Llave primaria incremental de la tabla BITACORA.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdPagina)
                    .HasColumnName("id_pagina")
                    .HasComment("El id_pagina es un identificador que hace referencia al campo id_pagina de la tabla PAGINA.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Ip)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ip")
                    .HasComment("La ip es la dirección ip del usuario que interactúa y realiza acciones en una determinada tabla.");
            });

            modelBuilder.Entity<BitacoraDetalle>(entity =>
            {
                entity.HasKey(e => e.IdBitacoraDetalle)
                    .HasName("PK__BITACORA__8597C44B5004EB02");

                entity.ToTable("BITACORA_DETALLE");

                entity.Property(e => e.IdBitacoraDetalle)
                    .HasColumnName("id_bitacora_detalle")
                    .HasComment("Llave primaria incremental de la tabla BITACORA_DETALLE.");

                entity.Property(e => e.Accion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("accion")
                    .HasComment("Es la acción la cual está realizando un determinado usuario a una tabla.");

                entity.Property(e => e.Campos)
                    .IsUnicode(false)
                    .HasColumnName("campos")
                    .HasComment("Campos almacena los campos(columnas) al cual están creando/modificando de un determinado registro (tupla) de la tabla.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdBitacora)
                    .HasColumnName("id_bitacora")
                    .HasComment("El id_bitacora es un identificador que hace referencia al campo id_bitacora de la tabla BITACORA.");

                entity.Property(e => e.IdTupla)
                    .HasColumnName("id_tupla")
                    .HasComment("La id_tupla es el identificador primary key del registro que se está modificando en una determinada tabla.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Tabla)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tabla")
                    .HasComment("Es la tabla en la cual se está realizando una x acción por un determinado usuario.");
            });

            modelBuilder.Entity<Ciclo>(entity =>
            {
                entity.HasKey(e => e.IdCiclo)
                    .HasName("PK__CICLO__A78E2FA33BBB0EB6");

                entity.ToTable("CICLO");

                entity.Property(e => e.IdCiclo)
                    .ValueGeneratedNever()
                    .HasColumnName("id_ciclo")
                    .HasComment("Llave primaria incremental de la tabla CICLO.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del ciclo.");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fin")
                    .HasComment("Es la fecha que culmina el ciclo.");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio")
                    .HasComment("Es la fecha de inicio del ciclo.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del ciclo. Ej.: Ciclo Marzo 2021, Ciclo Abril 2021, etc.");
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("PK__CIUDAD__B7DC4CD5DD784043");

                entity.ToTable("CIUDAD");

                entity.Property(e => e.IdCiudad)
                    .ValueGeneratedNever()
                    .HasColumnName("id_ciudad")
                    .HasComment("Llave primaria  de la tabla CIUDAD.");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdPais)
                    .HasColumnName("id_pais")
                    .HasComment("El id_pais es un identificador que hace referencia al campo id_pais de la tabla PAIS.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<ComisionDetalleEmpresa>(entity =>
            {
                entity.HasKey(e => e.IdComisionDetalleEmpresa)
                    .HasName("PK__COMISION__A81C75CC81C339FB");

                entity.ToTable("COMISION_DETALLE_EMPRESA");

                entity.Property(e => e.IdComisionDetalleEmpresa)
                    .HasColumnName("id_comision_detalle_empresa")
                    .HasComment("Es la llave primaria de la tabla");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Es el estado que pueda tener la tupla (1 Pendiente, 2 Confirmado, 3 Rechazado/Anulado)");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.FechaPago)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_pago");

                entity.Property(e => e.IdComisionDetalle).HasColumnName("id_comision_detalle");

                entity.Property(e => e.IdComprobanteGenerico)
                    .HasColumnName("id_comprobante_generico")
                    .HasComment("El id_comprobante_generico. puede ser idmovimiento o el idcomprobante de pago");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasComment("es el monto comision por empresa");

                entity.Property(e => e.MontoAFacturar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_a_facturar")
                    .HasComment("El monto a facturar por empresa");

                entity.Property(e => e.MontoNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_neto");

                entity.Property(e => e.MontoTotalFacturar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total_facturar")
                    .HasComment("El monto total a facturar por empresa");

                entity.Property(e => e.NroAutorizacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nro_autorizacion")
                    .HasComment("Es el nro de autorizacion de la factura");

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
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ventas_personales");
            });

            modelBuilder.Entity<ControlUsuario>(entity =>
            {
                entity.HasKey(e => e.IdControlUsuario)
                    .HasName("PK__CONTROL___E299BFDC74E017B7");

                entity.ToTable("CONTROL_USUARIO");

                entity.Property(e => e.IdControlUsuario)
                    .HasColumnName("id_control_usuario")
                    .HasComment("Llave primaria de la tabla.");

                entity.Property(e => e.CantidadIntentos)
                    .HasColumnName("cantidad_intentos")
                    .HasComment("Cantidad de intentos que el usuario fallo al iniciar session");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Estado del usuario.");

                entity.Property(e => e.FechaBloquedo)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_bloquedo")
                    .HasComment("Fecha de bloqueo del usuario");

                entity.Property(e => e.FechaDesbloqueo)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_desbloqueo")
                    .HasComment("Fecha de desbloqueo del usuario.");

                entity.Property(e => e.NetSessionId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("net_session_id")
                    .HasComment("Id del aps.net core.");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("usuario")
                    .HasComment("Usuario que intenta iniciar session (Dominio).");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__EMPRESA__4A0B7E2C6F69FE36");

                entity.ToTable("EMPRESA");

                entity.Property(e => e.IdEmpresa)
                    .HasColumnName("id_empresa")
                    .HasComment("Es la llave primaria y id de la empresa local ");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasComment("Es el codigo de empresa externo de guardian de gruposion");

                entity.Property(e => e.CodigoCnx).HasColumnName("codigo_cnx");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Estado de la empresa activo o inactivo 1 true , 0  false");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Es el nombre de la empresa e inmoboliaria");

                entity.Property(e => e.NombreBd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_bd");
            });

            modelBuilder.Entity<EstadoAutorizacionComision>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ESTADO_AUTORIZACION_COMISION");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdEstadoAutorizacionComision)
                    .HasColumnName("id_estado_autorizacion_comision")
                    .HasComment("es la llave primaria de la tablas");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("Es el i usuario que creo o modifico el registro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("es el estado de una comision autorizada");
            });

            modelBuilder.Entity<EstadoListadoFormaPago>(entity =>
            {
                entity.HasKey(e => e.IdEstadoListadoFormaPago)
                    .HasName("PK__ESTADO_L__3EB39F9E6E321E9B");

                entity.ToTable("ESTADO_LISTADO_FORMA_PAGO");

                entity.Property(e => e.IdEstadoListadoFormaPago)
                    .ValueGeneratedNever()
                    .HasColumnName("id_estado_listado_forma_pago")
                    .HasComment("Es la llave primaria de la tabla.");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la breve descripcion del estado ejemplo: 1: para pagar, 2: error al pagar, 3 pago exitoso ");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");
            });

            modelBuilder.Entity<Ficha>(entity =>
            {
                entity.HasKey(e => e.IdFicha)
                    .HasName("PK__FICHA__427B0F8A4FDE47E3");

                entity.ToTable("FICHA");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("Llave primaria incremental de la tabla FICHA.");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("apellidos")
                    .HasComment("Apellido completo del asesor");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar")
                    .HasComment("El avatar es el path de ubicación de la foto del asesor.");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ci")
                    .HasComment("El carnet de identidad del asesor");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasComment("El código es un identificador que identifica al Asesor y que es generado y asignado por la empresa.");

                entity.Property(e => e.CodigoCnx)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("codigo_cnx");

                entity.Property(e => e.Comentario)
                    .IsUnicode(false)
                    .HasColumnName("comentario")
                    .HasComment("Un breve comentario sobre el asesor.");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("contrasena")
                    .HasComment("La contraseña que el sistema de comisiones le asigna al asesor.");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico")
                    .HasComment("El correo electrónico del asesor");

                entity.Property(e => e.CuentaBancaria)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cuenta_bancaria")
                    .HasComment("Cuenta bancaria del asesor, si no tiene cuenta el valor es null.");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion")
                    .HasComment("La dirección del asesor.");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Es el estado de la tabla 1 es activo , 0 es inactivo.");

                entity.Property(e => e.FacturaHabilitado)
                    .HasColumnName("factura_habilitado")
                    .HasComment("Valor de bit, si es 1 el asesor factura, de lo contrario si es 0 el asesor no factura.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento")
                    .HasComment("La fecha de nacimiento del asesor.");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro")
                    .HasComment("La fecha que la empresa registró al asesor.");

                entity.Property(e => e.IdBanco)
                    .HasColumnName("id_banco")
                    .HasComment("El id_banco es un identificador que hace referencia al campo id_banco de la tabla BANCO.");

                entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");

                entity.Property(e => e.IdTipoPago)
                    .HasColumnName("id_tipo_pago")
                    .HasComment("id tipo pago el FreeLancer si tiene cuenta o cuenta podra tener habilitado o seleccianado un tipo de pago");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nit)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nit")
                    .HasComment("Nit del Asesor, si no tiene el valor es null.");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombres")
                    .HasComment("Nombre completo del asesor");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("razon_social")
                    .HasComment("Razón social del Asesor, si no tiene el valor es null.");

                entity.Property(e => e.TelFijo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tel_fijo")
                    .HasComment("El teléfono fijo del asesor.");

                entity.Property(e => e.TelMovil)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tel_movil")
                    .HasComment("El teléfono móvil del asesor.");

                entity.Property(e => e.TelOficina)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tel_oficina")
                    .HasComment("El teléfono de oficina del asesor.");

                entity.Property(e => e.TieneCuentaBancaria)
                    .HasColumnName("tiene_cuenta_bancaria")
                    .HasComment("Valor de bit, si es 1 el asesor tiene cuenta bancaria, de lo contrario si es 0 no tiene cuenta bancaria.");
            });

            modelBuilder.Entity<FichaIncentivo>(entity =>
            {
                entity.HasKey(e => e.IdFichaIncentivo)
                    .HasName("PK__FICHA_IN__8B564738CF0878CF");

                entity.ToTable("FICHA_INCENTIVO");

                entity.Property(e => e.IdFichaIncentivo)
                    .HasColumnName("id_ficha_incentivo")
                    .HasComment("Este es la llave primaria de la tabla.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("Este el el id de la ficha del frilanzer");

                entity.Property(e => e.IdIncentivo)
                    .HasColumnName("id_incentivo")
                    .HasComment("Este es la llave del incentio que hace referencia a la tabla incentivo.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasComment("Este es el monto ganado para el frilanzer.");
            });

            modelBuilder.Entity<FichaNivelI>(entity =>
            {
                entity.HasKey(e => e.IdFichaNivelI)
                    .HasName("PK__FICHA_NI__2944BB1AF1DEA71B");

                entity.ToTable("FICHA_NIVEL_I");

                entity.Property(e => e.IdFichaNivelI)
                    .HasColumnName("id_ficha_nivel_i")
                    .HasComment("Llave primaria incremental de la tabla FICHA_NIVEL_I.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.");

                entity.Property(e => e.IdNivel)
                    .HasColumnName("id_nivel")
                    .HasComment("El id_nivel es un identificador que hace referencia al campo id_nivel de la tabla NIVEL.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<FichaTipoBajaI>(entity =>
            {
                entity.HasKey(e => e.IdFichaTipoBajaI)
                    .HasName("PK__FICHA_TI__CD4CE5D4A95B58E2");

                entity.ToTable("FICHA_TIPO_BAJA_I");

                entity.Property(e => e.IdFichaTipoBajaI)
                    .HasColumnName("id_ficha_tipo_baja_i")
                    .HasComment("Llave primaria incremental de la tabla FICHA_TIPO_BAJA_I.");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_baja")
                    .HasComment("Fecha en la cuál el Asesor fue dado de baja.");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.");

                entity.Property(e => e.IdTipoBaja)
                    .HasColumnName("id_tipo_baja")
                    .HasComment("El id_tipo_baja es un identificador que hace referencia al campo id_tipo_baja de la tabla TIPO_BAJA.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Motivo)
                    .IsUnicode(false)
                    .HasColumnName("motivo")
                    .HasComment("Descripción del motivo de baja que pueda llegar a tener un Asesor.");
            });

            modelBuilder.Entity<GpClienteVendedorI>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.IdCliente })
                    .HasName("PK__GP_CLIEN__74641BB05A06B841");

                entity.ToTable("GP_CLIENTE_VENDEDOR_I");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.FechaActivacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_activacion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaDesactivacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_desactivacion");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");
            });

            modelBuilder.Entity<GpComision>(entity =>
            {
                entity.HasKey(e => e.IdComision)
                    .HasName("PK__GP_COMIS__B25ABED099BEA923");

                entity.ToTable("GP_COMISION");

                entity.Property(e => e.IdComision)
                    .HasColumnName("id_comision")
                    .HasComment("Llave primaria incremental de la tabla COMISION.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdCiclo)
                    .HasColumnName("id_ciclo")
                    .HasComment("El id_ciclo es un identificador que hace referencia al campo id_ciclo de la tabla CICLO.");

                entity.Property(e => e.IdTipoComision)
                    .HasColumnName("id_tipo_comision")
                    .HasComment("El id_tipo_comision es un identificador que hace referencia al campo id_tipo_comision de la tabla TIPO_COMISION.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.MontoTotalAplicacion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total_aplicacion")
                    .HasComment("El monto total de aplicaciones es la sumatoria de todas las aplicaciones (descuentos) que se le hace al Asesor en el ciclo actual por el concepto de pago de cuotas de sus productos propios o multas que pueda tener.");

                entity.Property(e => e.MontoTotalBruto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total_bruto")
                    .HasComment("Es el monto total bruto que comisionó el Asesor en el ciclo actual.");

                entity.Property(e => e.MontoTotalNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total_neto")
                    .HasComment("El monto total neto es la diferencia del monto total bruto, monto total de retención y monto total de aplicaciones.");

                entity.Property(e => e.MontoTotalRetencion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total_retencion")
                    .HasComment("El monto total de retención es la sumatoria de todas las retenciones que se le hace al Asesor en el ciclo actual.");

                entity.Property(e => e.PorcentajeRetencion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("porcentaje_retencion")
                    .HasComment("Es el porcentaje de retención para impuestos que se realiza al monto total bruto.");
            });

            modelBuilder.Entity<GpComisionDetalle>(entity =>
            {
                entity.HasKey(e => e.IdComisionDetalle)
                    .HasName("PK__GP_COMIS__89C1F994CE23336E");

                entity.ToTable("GP_COMISION_DETALLE");

                entity.Property(e => e.IdComisionDetalle)
                    .HasColumnName("id_comision_detalle")
                    .HasComment("Llave primaria incremental de la tabla GP_COMISION_DETALLE.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdComision)
                    .HasColumnName("id_comision")
                    .HasComment("El id_comision es un identificador que hace referencia al campo id_comision de la tabla COMISION.");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.MontoAplicacion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_aplicacion")
                    .HasComment("El monto de aplicaciones es la sumatoria de todas las aplicaciones (descuentos) que se le hace al Asesor en el detalle del ciclo actual por el concepto de pago de cuotas de sus productos propios o multas que pueda tener.");

                entity.Property(e => e.MontoBruto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_bruto")
                    .HasComment("Es el monto bruto que comisionó el Asesor en el detalle del ciclo actual.");

                entity.Property(e => e.MontoNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_neto")
                    .HasComment("El monto neto es la diferencia del monto bruto, monto de retención y monto de aplicaciones.");

                entity.Property(e => e.MontoRetencion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_retencion")
                    .HasComment("El monto de retención es la sumatoria de todas las retenciones que se le hace al Asesor en el detalle del ciclo actual.");

                entity.Property(e => e.PorcentajeRetencion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("porcentaje_retencion")
                    .HasComment("Es el porcentaje de retención para impuestos que se realiza al monto bruto.");
            });

            modelBuilder.Entity<GpComisionDetalleEstadoI>(entity =>
            {
                entity.HasKey(e => e.IdComisionDetalleEstadoI)
                    .HasName("PK__GP_COMIS__3C036DC37ECCCB59");

                entity.ToTable("GP_COMISION_DETALLE_ESTADO_I");

                entity.Property(e => e.IdComisionDetalleEstadoI)
                    .HasColumnName("id_comision_detalle_estado_i")
                    .HasComment("Llave primaria incremental de la tabla GP_COMISION_DETALLE_ESTADO_I.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdComisionDetalle)
                    .HasColumnName("id_comision_detalle")
                    .HasComment("El id_comision_detalle es un identificador que hace referencia al campo id_comision_detalle de la tabla COMISION_DETALLE.");

                entity.Property(e => e.IdEstadoComisionDetalle)
                    .HasColumnName("id_estado_comision_detalle")
                    .HasComment("El id_estado_comision_detalle es un identificador que hace referencia al campo id_estado_comision_detalle de la tabla GP_ESTADO_COMISION_DETALLE.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<GpComisionEstadoComisionI>(entity =>
            {
                entity.HasKey(e => e.IdComisionEstadoComisionI)
                    .HasName("PK__GP_COMIS__9D60F2EBC8DC1718");

                entity.ToTable("GP_COMISION_ESTADO_COMISION_I");

                entity.Property(e => e.IdComisionEstadoComisionI)
                    .HasColumnName("id_comision_estado_comision_i")
                    .HasComment("Llave primaria incremental de la tabla GP_COMISION_ESTADO_COMISION_I.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdComision)
                    .HasColumnName("id_comision")
                    .HasComment("El id_comision es un identificador que hace referencia al campo id_comision de la tabla COMISION.");

                entity.Property(e => e.IdEstadoComision)
                    .HasColumnName("id_estado_comision")
                    .HasComment("El id_estado_comision es un identificador que hace referencia al campo id_estado_comision de la tabla ESTADO_COMISION.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<GpDetalleEstadoListadoFormaPagol>(entity =>
            {
                entity.ToTable("GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Es la llave primaria de tabla.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Es el estado de la tabla expresado en boleando ejemplo: habilitado = true, inhabilitado = false");

                entity.Property(e => e.IdEstadoListadoFormaPago)
                    .HasColumnName("id_estado_listado_forma_pago")
                    .HasComment("llave foranea de la tabla estado del listado de forma de pagos");

                entity.Property(e => e.IdListaFormasPago)
                    .HasColumnName("id_lista_formas_pago")
                    .HasComment("llave forare de la tabla detalle lista formad e pago.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<GpEstadoComision>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GP_ESTADO_COMISION");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del estado de comisión.");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .HasComment("Nombre del estado de comisión. Ej.: Para facturación, Para carga de datos, Para prorrateo, Para autorización, etc.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdEstadoComision)
                    .HasColumnName("id_estado_comision")
                    .HasComment("Llave primaria incremental de la tabla GP_ESTADO_COMISION.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<GpEstadoComisionDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GP_ESTADO_COMISION_DETALLE");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del estado de la COMISION_DETALLE.");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .HasComment("Nombre del estado que pueda tener la comisión detalle. Ej.: Para forma de pago, Para autorizar, etc.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdEstadoComisionDetalle)
                    .HasColumnName("id_estado_comision_detalle")
                    .HasComment("Llave primaria incremental de la tabla NIVEL.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<GpEstadoProrrateoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdGpEstadoProrrateoDetalle)
                    .HasName("PK__GP_ESTAD__A98DDADA8612B057");

                entity.ToTable("GP_ESTADO_PRORRATEO_DETALLE");

                entity.Property(e => e.IdGpEstadoProrrateoDetalle)
                    .ValueGeneratedNever()
                    .HasColumnName("id_gp_estado_prorrateo_detalle")
                    .HasComment("Es la llave primaria de la tabla.");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la breve descripcion del estado de proratero detalle ");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");
            });

            modelBuilder.Entity<GpEstadoProrrateoDetalleIncentivo>(entity =>
            {
                entity.HasKey(e => e.IdGpEstadoProrrateoDetalleIncentivo)
                    .HasName("PK__GP_ESTAD__4082A0B690C451FE");

                entity.ToTable("GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO");

                entity.Property(e => e.IdGpEstadoProrrateoDetalleIncentivo)
                    .ValueGeneratedNever()
                    .HasColumnName("id_gp_estado_prorrateo_detalle_incentivo")
                    .HasComment("Llave primaria incremental de la tabla.");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripcion breve del estado porrateo detalle incentivo ejemplo: estado pendiente 1, estado procesado 2.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");
            });

            modelBuilder.Entity<GpPorrateroDetalleIncentivo>(entity =>
            {
                entity.HasKey(e => e.IdGpPorrateroDetalleIncentivo)
                    .HasName("PK__GP_PORRA__6CC4685CFDFCD0C3");

                entity.ToTable("GP_PORRATERO_DETALLE_INCENTIVO");

                entity.Property(e => e.IdGpPorrateroDetalleIncentivo)
                    .HasColumnName("id_gp_porratero_detalle_incentivo")
                    .HasComment("Este es la llave primaria de la tabla.");

                entity.Property(e => e.ComprobanteId)
                    .HasColumnName("comprobante_id")
                    .HasComment("Este es el codigo del comprobanto.");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Es el estado de la tabla  activo  y  inactivo.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.Gestion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("gestion")
                    .HasComment("Es el anho  o codigo anhoo con alguna nomesclatura.");

                entity.Property(e => e.IdEmpresaAsume)
                    .HasColumnName("id_empresa_asume")
                    .HasComment("Llave foranea que hace referencia a la empresa que asume el pago.");

                entity.Property(e => e.IdFichaIncentivo)
                    .HasColumnName("id_ficha_incentivo")
                    .HasComment("Llave foranea hace referencia al incentivo de la ficha del frelanzers.");

                entity.Property(e => e.IdGpEstadoProrrateoDetalleIncentivo)
                    .HasColumnName("id_gp_estado_prorrateo_detalle_incentivo")
                    .HasComment("Llave foranea del estados de la tabla ejemplo 1 pendente, 2 procesado.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasComment("Es el monto que la empresa a asumir para darle al frilanzer.");

                entity.Property(e => e.ReciboId)
                    .HasColumnName("recibo_id")
                    .HasComment("Este es el id del recibo por el incentivo.");
            });

            modelBuilder.Entity<GpProrrateoDetalle>(entity =>
            {
                entity.HasKey(e => e.IdGpPorrateoDetalle)
                    .HasName("PK__GP_PRORR__94C19122F9AAC136");

                entity.ToTable("GP_PRORRATEO_DETALLE");

                entity.Property(e => e.IdGpPorrateoDetalle)
                    .HasColumnName("id_gp_porrateo_detalle")
                    .HasComment("Llave primaria de la tabla y auto imncremental.");

                entity.Property(e => e.ComprobanteId)
                    .HasColumnName("comprobante_id")
                    .HasComment("Es el codigo del comprobante del pago.");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion detallada de un prorateo por empresa, empresas que prestaran para el .");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Gestion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("gestion")
                    .HasComment("la gestion es el codigo y anho que se ejecuto el pago.");

                entity.Property(e => e.IdAplicacionDetalleProducto)
                    .HasColumnName("id_aplicacion_detalle_producto")
                    .HasComment("Llave foranea de aplicacion de producto detalle.");

                entity.Property(e => e.IdEmpresaPresta)
                    .HasColumnName("id_empresa_presta")
                    .HasComment("Llave foranea de la empresa que realiza el prestamo.");

                entity.Property(e => e.IdEmpresaRecibe)
                    .HasColumnName("id_empresa_recibe")
                    .HasComment("Llave foranea de la empresa que recibe el pago.");

                entity.Property(e => e.IdGpEstadoProrrateoDetalle)
                    .HasColumnName("id_gp_estado_prorrateo_detalle")
                    .HasComment("Es el estado de la tabla activo 1 e inactivo 0.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasComment("Es el monto que la empresa va a prestar.");

                entity.Property(e => e.ReciboId)
                    .HasColumnName("recibo_id")
                    .HasComment("Es el codigo de recibo del pago.");
            });

            modelBuilder.Entity<GpTipoComision>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GP_TIPO_COMISION");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del tipo de comisión.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdTipoComision)
                    .HasColumnName("id_tipo_comision")
                    .HasComment("Llave primaria incremental de la tabla GP_TIPO_COMISION.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del tipo de comisión. Ej.: Pago Comisiones, Rezagados.");
            });

            modelBuilder.Entity<Incentivo>(entity =>
            {
                entity.HasKey(e => e.IdIncentivo)
                    .HasName("PK__INCENTIV__6035F00C9F4E33E2");

                entity.ToTable("INCENTIVO");

                entity.Property(e => e.IdIncentivo)
                    .HasColumnName("id_incentivo")
                    .HasComment("Es la llave primeria de la tala y auto incremental.");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("codigo")
                    .HasComment("Es el codigo del incentivo por especie que identifica al incentivo.");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion detallada de un incentivo.");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("es el estado de del incentivo 1 activo o  0 inactivo.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdNiveles)
                    .HasColumnName("id_niveles")
                    .HasComment("Este hace referencia a un id niveles de rango ganado o puede ser ninguno, un incentivo puede tener o no un rango de nivel ganado, .");

                entity.Property(e => e.IdTipoIncentivo)
                    .HasColumnName("id_tipo_incentivo")
                    .HasComment("Este es el tipo de incentivo ganado ya sea producto en especie o monetario");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasComment("Es el precio del incentivo ya se si es un especie o dinero");
            });

            modelBuilder.Entity<IncentivoPagoComision>(entity =>
            {
                entity.HasKey(e => e.IdDetalle)
                    .HasName("PK__INCENTIV__4F1332DECF9AEB04");

                entity.ToTable("INCENTIVO_PAGO_COMISION");

                entity.Property(e => e.IdDetalle)
                    .HasColumnName("id_detalle")
                    .HasComment("Llave primaria de la tabla.");

                entity.Property(e => e.IdComisionDetalle)
                    .HasColumnName("id_comision_detalle")
                    .HasComment("llave foranea hacia la tabla GP_COMISION_DETALLE");

                entity.Property(e => e.IdTipoIncentivoPago)
                    .HasColumnName("id_tipo_incentivo_pago")
                    .HasComment("llave foranea hacia la tabla TIPO_INCENTIVO_PAGO");
            });

            modelBuilder.Entity<ListadoFormasPago>(entity =>
            {
                entity.HasKey(e => e.IdListaFormasPago)
                    .HasName("PK__LISTADO___31038ED81C587A11");

                entity.ToTable("LISTADO_FORMAS_PAGO");

                entity.Property(e => e.IdListaFormasPago)
                    .HasColumnName("id_lista_formas_pago")
                    .HasComment("Es el id primaria de la tabla y autoincremental.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdComisionesDetalle)
                    .HasColumnName("id_comisiones_detalle")
                    .HasComment("Llave de referencia de comision de detalle del cliente .");

                entity.Property(e => e.IdTipoPago)
                    .HasColumnName("id_tipo_pago")
                    .HasComment("Llave foranea del tipo pago, 1 sion pay, 2 transferencia, 3 cheche.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.MontoNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_neto")
                    .HasComment("Es el monto neto con el descuento inclido de una forma de producto.");
            });

            modelBuilder.Entity<LogComprobanteBanco>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOG_COMPROBANTE_BANCO");

                entity.Property(e => e.Amount).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.DateRegisterTransaction).HasColumnType("datetime");

                entity.Property(e => e.ErrorDateTime).HasColumnType("datetime");

                entity.Property(e => e.ErrorId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ErrorID");

                entity.Property(e => e.ErrorMessage).IsUnicode(false);

                entity.Property(e => e.ErrorProcedure).IsUnicode(false);

                entity.Property(e => e.Glosa).IsUnicode(false);

                entity.Property(e => e.NameFreelance)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogComprobanteContable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOG_COMPROBANTE_CONTABLE");

                entity.Property(e => e.Amount).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.DateRegisterTransaction).HasColumnType("datetime");

                entity.Property(e => e.ErrorDateTime).HasColumnType("datetime");

                entity.Property(e => e.ErrorId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ErrorID");

                entity.Property(e => e.ErrorMessage).IsUnicode(false);

                entity.Property(e => e.ErrorProcedure).IsUnicode(false);

                entity.Property(e => e.Glosa).IsUnicode(false);

                entity.Property(e => e.NameFreelance)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogDetalleComisionEmpresaFail>(entity =>
            {
                entity.HasKey(e => e.IdDetalleComisioEmpresaFail)
                    .HasName("PK__LOG_DETA__60ED7DCFF821D2A9");

                entity.ToTable("LOG_DETALLE_COMISION_EMPRESA_FAIL");

                entity.Property(e => e.IdDetalleComisioEmpresaFail)
                    .HasColumnName("id_detalle_comisio_empresa_fail")
                    .HasComment("Llave primaria de la tabla autoincremental.");

                entity.Property(e => e.CodigoCliente)
                    .HasColumnName("codigo_cliente")
                    .HasComment("Es el codigo del contacto del guardian, es el cliente");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion del registro");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdCiclo)
                    .HasColumnName("id_ciclo")
                    .HasComment("El idciclo es la llave foranea de comision ciclo");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("Es el id ficha de la tabla comisiones");

                entity.Property(e => e.TotalMontoBruto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_monto_Bruto")
                    .HasComment("Es el monto total bruto de la comision de freelancer");
            });

            modelBuilder.Entity<LogPagoComisionTransferenciaPorEmpresa>(entity =>
            {
                entity.ToTable("LOG_PAGO_COMISION_TRANSFERENCIA_POR_EMPRESA");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Banco)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("banco");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ci");

                entity.Property(e => e.CicloId).HasColumnName("ciclo_id");

                entity.Property(e => e.CodigoRespSp)
                    .HasColumnName("codigo_resp_sp")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ComisionDetalleEmpresaId).HasColumnName("comision_detalle_empresa_id");

                entity.Property(e => e.ComisionDetalleId).HasColumnName("comision_detalle_id");

                entity.Property(e => e.ComisionId).HasColumnName("comision_id");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");

                entity.Property(e => e.EmpresaIdCnx).HasColumnName("empresa_id_cnx");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FichaId).HasColumnName("ficha_id");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre_completo");

                entity.Property(e => e.NombreSp)
                    .IsUnicode(false)
                    .HasColumnName("nombre_sp");

                entity.Property(e => e.NroCuentaBanco)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nro_cuenta_banco");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            });

            modelBuilder.Entity<LogPagoMasivoSionPayComisionOEmpresaFail>(entity =>
            {
                entity.HasKey(e => e.IdSionPayComisioEmpresaFail)
                    .HasName("PK__LOG_PAGO__8A08B05F9733AFC6");

                entity.ToTable("LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL");

                entity.Property(e => e.IdSionPayComisioEmpresaFail)
                    .HasColumnName("id_sion_pay_comisio_empresa_fail")
                    .HasComment("Llave primaria de la tabla autoincremental.");

                entity.Property(e => e.Carnet)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("carnet")
                    .HasComment("Es carnet de identidad del freelancers");

                entity.Property(e => e.CuentaSionPay)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cuenta_sion_pay")
                    .HasComment("Es el nro de cuenta en sion pay del freelancer");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion del registro");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdCiclo)
                    .HasColumnName("id_ciclo")
                    .HasComment("El idciclo es la llave foranea de comision ciclo");

                entity.Property(e => e.IdDetalleComision)
                    .HasColumnName("id_detalle_comision")
                    .HasComment("Es el id de la tabla detalle comision se registrara em caso de no existir su detalle por empresa");

                entity.Property(e => e.IdDetalleComisionEmpresa)
                    .HasColumnName("id_detalle_comision_empresa")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Es el id de la tabla detalle_comision_empresa que se registrara en caso de haya un pago con monto cero por default cero");

                entity.Property(e => e.IdEmpresaCnx).HasColumnName("id_empresa_cnx");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("Es el id ficha de la tabla comisiones");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasDefaultValueSql("((0))")
                    .HasComment("es el monto de la transaccion datos por default cero");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_empresa")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo)
                    .HasName("PK__MODULO__B2584DFCB777C2C9");

                entity.ToTable("MODULO");

                entity.Property(e => e.IdModulo)
                    .HasColumnName("id_modulo")
                    .HasComment("Llave primaria incremental de la tabla MODULO.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.Icono)
                    .IsUnicode(false)
                    .HasColumnName("icono")
                    .HasComment("Path de ubicación en el servidor del ícono asignado al módulo.");

                entity.Property(e => e.IdModuloPadre)
                    .HasColumnName("id_modulo_padre")
                    .HasComment("El id_modulo_padre es un identificador que hace referencia al campo id_modulo de la tabla MODULO. Si está null o 0 entonces es padre, de lo contrario es hijo que hace referencia al id_modulo padre.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del módulo que agrupa un conjunto de submódulos y páginas del sistema.");

                entity.Property(e => e.Orden)
                    .HasColumnName("orden")
                    .HasComment("Número entero que muestra el orden del módulo que se mostrará en el menú.");
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NIVEL");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del tipo de baja.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdNivel)
                    .HasColumnName("id_nivel")
                    .HasComment("Llave primaria incremental de la tabla NIVEL.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del nivel que pueda tener el Asesor. Ej.: Royal Intercontinetal, etc.");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.IdPagina)
                    .HasName("PK__PAGINA__A2A7C7B6F00535C7");

                entity.ToTable("PAGINA");

                entity.Property(e => e.IdPagina)
                    .HasColumnName("id_pagina")
                    .HasComment("Llave primaria incremental de la tabla PAGINA.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.Icono)
                    .IsUnicode(false)
                    .HasColumnName("icono")
                    .HasComment("Path de ubicación en el servidor del ícono asignado a la página.");

                entity.Property(e => e.IdModulo)
                    .HasColumnName("id_modulo")
                    .HasComment("El id_modulo es un identificador que hace referencia al campo id_modulo de la tabla MODULO.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del módulo que agrupa un conjunto de submódulos y páginas del sistema.");

                entity.Property(e => e.Orden)
                    .HasColumnName("orden")
                    .HasComment("Número entero que muestra el orden de la página que se mostrará en el menú.");

                entity.Property(e => e.UrlPagina)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("url_pagina")
                    .HasComment("Url/Path de la página para ser gestionado desde los controladores.");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK__PAIS__0941A3A7A15B5421");

                entity.ToTable("PAIS");

                entity.Property(e => e.IdPais)
                    .ValueGeneratedNever()
                    .HasColumnName("id_pais")
                    .HasComment("Llave primaria  de la tabla PAIS.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del pais. Ej. Bolivia, Brasil, E.E.U.U., etc.");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__PERMISO__228F224F7CC050EE");

                entity.ToTable("PERMISO");

                entity.Property(e => e.IdPermiso)
                    .HasColumnName("id_permiso")
                    .HasComment("Llave primaria incremental de la tabla PERMISO.");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del permiso.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Permiso1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("permiso")
                    .HasComment("Nombre del permiso");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PK__PROYECTO__F38AD81D83E2B0BB");

                entity.ToTable("PROYECTO");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("id_proyecto")
                    .HasComment("Llave primaria de la tabla.");

                entity.Property(e => e.ComplejoidGuardian).HasColumnName("complejoid_guardian");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdEmpresa)
                    .HasColumnName("id_empresa")
                    .HasComment("llave foranea que  hace referencia a la empresa .");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Es el nombre del proyecto o producto.");

                entity.Property(e => e.ProyectoConexionId)
                    .HasColumnName("proyecto_conexion_id")
                    .HasComment("Es el codigo de proyecto en conexion.");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__ROL__6ABCB5E09C593A33");

                entity.ToTable("ROL");

                entity.Property(e => e.IdRol)
                    .HasColumnName("id_rol")
                    .HasComment("Llave primaria incremental de la tabla ROL.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del rol.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del rol. Ej. Gerente, Coordinador, Analista, etc.");
            });

            modelBuilder.Entity<RolPaginaI>(entity =>
            {
                entity.HasKey(e => e.IdRolPaginaI)
                    .HasName("PK__ROL_PAGI__657F32AEF97CB176");

                entity.ToTable("ROL_PAGINA_I");

                entity.Property(e => e.IdRolPaginaI)
                    .HasColumnName("id_rol_pagina_i")
                    .HasComment("Llave primaria incremental de la tabla ROL_PAGINA_I.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdPagina)
                    .HasColumnName("id_pagina")
                    .HasComment("El id_pagina es un identificador que hace referencia al campo id_pagina de la tabla PAGINA.");

                entity.Property(e => e.IdRol)
                    .HasColumnName("id_rol")
                    .HasComment("El id_rol es un identificador que hace referencia al campo id_rol de la tabla ROL.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<RolPaginaPermisoI>(entity =>
            {
                entity.HasKey(e => e.IdRolPaginaPermisoI)
                    .HasName("PK__ROL_PAGI__31BAAF4817FE4209");

                entity.ToTable("ROL_PAGINA_PERMISO_I");

                entity.Property(e => e.IdRolPaginaPermisoI)
                    .HasColumnName("id_rol_pagina_permiso_i")
                    .HasComment("Llave primaria incremental de la tabla ROL_PAGINA_PERMISO_I.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdPermiso)
                    .HasColumnName("id_permiso")
                    .HasComment("El id_permiso es un identificador que hace referencia al campo id_permiso de la tabla PERMISO.");

                entity.Property(e => e.IdRolPagina)
                    .HasColumnName("id_rol_pagina")
                    .HasComment("El id_rol_pagina es un identificador que hace referencia al campo id_rol_pagina de la tabla ROL_PAGINA.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("PK__SUCURSAL__4C758013B435C9B3");

                entity.ToTable("SUCURSAL");

                entity.Property(e => e.IdSucursal)
                    .HasColumnName("id_sucursal")
                    .HasComment("Llave primaria incremental de la tabla SUCURSAL.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve de la Sucursal");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("habilitado")
                    .HasComment("Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).");

                entity.Property(e => e.IdCiudad)
                    .HasColumnName("id_ciudad")
                    .HasComment("El id_ciudad es un identificador que hace referencia al campo id_ciudad de la tabla PAIS.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre de la sucursal. Ej. Ambassador, Cañoto, Kalomai, etc.");
            });

            modelBuilder.Entity<TipoAplicacione>(entity =>
            {
                entity.HasKey(e => e.IdTipoAplicaciones)
                    .HasName("PK__TIPO_APL__D56E6A9CFAAF6BCB");

                entity.ToTable("TIPO_APLICACIONES");

                entity.Property(e => e.IdTipoAplicaciones)
                    .HasColumnName("id_tipo_aplicaciones")
                    .HasComment("Es el id de la tabla es auto incremental.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es el nombre o descripcion del tipo  de descuento");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.GuardianIdCicloDescuentoTipo)
                    .HasColumnName("guardian_id_ciclo_descuento_tipo")
                    .HasComment("Es el codigo tipo que hace referencia al ID de la tabla administraciondescuentociclotipo que esta en el guardian base de datos : grdsion, dato por defecto cero si no pertenece al guardian. ");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.ValidoGuardian)
                    .IsRequired()
                    .HasColumnName("valido_guardian")
                    .HasDefaultValueSql("('false')")
                    .HasComment("Este campo hace referencia si la descripcion es valido solo para guardian = true, false si es diferente a lo de guardian");
            });

            modelBuilder.Entity<TipoAutorizacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoAutorizacion)
                    .HasName("PK__TIPO_AUT__84026FCD802BC2AF");

                entity.ToTable("TIPO_AUTORIZACION");

                entity.Property(e => e.IdTipoAutorizacion)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tipo_autorizacion")
                    .HasComment("Es la llave primaria de la tabla tipo autorizacion");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasComment("Es es la cantidad de usuarios que podran autorizar por tipo de autorizacion.");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Es el estado de la tabla booleano true o false");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdUsuarioModificacion)
                    .HasColumnName("id_usuario_modificacion")
                    .HasComment("Es la descripcion del registro");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Es el breve nombre o descripcion del tipo de autorizacion ");
            });

            modelBuilder.Entity<TipoBaja>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TIPO_BAJA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del tipo de baja.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdTipoBaja)
                    .HasColumnName("id_tipo_baja")
                    .HasComment("Llave primaria incremental de la tabla TIPO_BAJA.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Nombre del tipo de baja que pueda tener el Asesor. Ej.: Cesión de derecho, etc.");
            });

            modelBuilder.Entity<TipoIncentivo>(entity =>
            {
                entity.HasKey(e => e.IdTipoIncentivo)
                    .HasName("PK__TIPO_INC__FAEB36E65F862C76");

                entity.ToTable("TIPO_INCENTIVO");

                entity.Property(e => e.IdTipoIncentivo)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tipo_incentivo")
                    .HasComment("Llave primaria incremental de la tabla.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("nombre es el tipo de incentivo en especie  o en dinero");
            });

            modelBuilder.Entity<TipoIncentivoPago>(entity =>
            {
                entity.HasKey(e => e.IdTipoIncentivo)
                    .HasName("PK__TIPO_INC__FAEB36E6DF580E96");

                entity.ToTable("TIPO_INCENTIVO_PAGO");

                entity.Property(e => e.IdTipoIncentivo)
                    .HasColumnName("id_tipo_incentivo")
                    .HasComment("Llave primaria de la tabla.");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("indica el nombre o descripcion del tipo Incentivo Pago");

                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .HasComment("indica el estado del tipoIncentivo ejm: INACTIVO, ACTIVO");
            });

            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.HasKey(e => e.IdTipoPago)
                    .HasName("PK__TIPO_PAG__F7E781E560879837");

                entity.ToTable("TIPO_PAGO");

                entity.Property(e => e.IdTipoPago)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tipo_pago")
                    .HasComment("Es la llave primaria de la tabla");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion mas detallada del nombre de la tabla");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("icono");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .HasComment("Es el nombre del tipo de pago ejemplo: 1 sion pay, 2 transferencia, 3 checque , 4 ninguno");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__4E3E04AD447C88A5");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("Llave primaria incremental de la tabla USUARIO.");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("apellidos")
                    .HasComment("Apellido completo del usuario");

                entity.Property(e => e.Corporativo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("corporativo")
                    .HasComment("Teléfono comporativo del usuario");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento")
                    .HasComment("Fecha de nacimiento del usuario");

                entity.Property(e => e.IdArea)
                    .HasColumnName("id_area")
                    .HasComment("El id_area es un identificador que hace referencia al campo id_area de la tabla AREA.");

                entity.Property(e => e.IdRol)
                    .HasColumnName("id_rol")
                    .HasComment("El id_rol es un identificador que hace referencia al campo id_rol de la tabla ROL.");

                entity.Property(e => e.IdSucursal)
                    .HasColumnName("id_sucursal")
                    .HasComment("El id_sucursal es un identificador que hace referencia al campo id_sucursal de la tabla SUCURSAL.");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombres")
                    .HasComment("Nombre completo del usuario");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono")
                    .HasComment("Teléfono del usuario");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("usuario")
                    .HasComment("Nombre de usuario extraído del dominio que tiene asignado un trabajador en la empresa");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuario_id")
                    .HasComment("El usuario_id es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<UsuarioAutorizacion>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioAutorizacion)
                    .HasName("PK__USUARIO___59E0A6D5CEBB4E05");

                entity.ToTable("USUARIO_AUTORIZACION");

                entity.Property(e => e.IdUsuarioAutorizacion)
                    .HasColumnName("id_usuario_autorizacion")
                    .HasComment("Es la llave generica de la tabla");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdTipoAutorizacion)
                    .HasColumnName("id_tipo_autorizacion")
                    .HasComment("Es la llave foranea de la tabla tipo de auntorizacin");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("Es el usuario que se le asigno a un tipo de autorizacion");

                entity.Property(e => e.IdUsuarioModificacion).HasColumnName("id_usuario_modificacion");
            });

            modelBuilder.Entity<UsuariosRole>(entity =>
            {
                entity.HasKey(e => e.IdUsuariosRoles)
                    .HasName("PK__USUARIOS__720F812B5F74CCF6");

                entity.ToTable("USUARIOS_ROLES");

                entity.HasIndex(e => e.IdUsuario, "UQ__USUARIOS__4E3E04ACF08C7B12")
                    .IsUnique();

                entity.Property(e => e.IdUsuariosRoles)
                    .HasColumnName("id_usuarios_roles")
                    .HasComment("Llave primaria incremental de la tabla.");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Es el estado del requistro booleano true o false");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualización del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creación del registro");

                entity.Property(e => e.IdRol)
                    .HasColumnName("id_rol")
                    .HasComment("Llave foranea que hace relacion con la tabla Rol");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("Llave foranea que hace referencia a la tabla usuario");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuario_id")
                    .HasComment("El usuario_id es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__VENTA__459533BFC5E7E722");

                entity.ToTable("VENTA");

                entity.Property(e => e.IdVenta)
                    .HasColumnName("id_venta")
                    .HasComment("Es la llave priamria e incremental de la tabla.");

                entity.Property(e => e.ClienteId)
                    .HasColumnName("cliente_id")
                    .HasComment("Llave foranea de la ficha del frelanzer.");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("codigo_producto")
                    .HasComment("Es el codigo del producto .");

                entity.Property(e => e.ComplejoId)
                    .HasColumnName("complejo_id")
                    .HasComment("Es el proyecto id de conexion.");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("descuento")
                    .HasComment("Es el descuento que se aplico sobre la venta.");

                entity.Property(e => e.EsComisionable)
                    .HasColumnName("es_comisionable")
                    .HasComment("Es campo booleando si una venta es comsionable.");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Es el estado de la tabla activo en inactivo.");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_venta")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es la fecha de la venta realizada.");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("Es una llave foranea que hace referencia a la ficha del frilanzer.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

                entity.Property(e => e.Lote)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("lote")
                    .HasComment("El numero de lote del producto.");

                entity.Property(e => e.Manzano)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("manzano")
                    .HasComment("El codigo de manzano del producto.");

                entity.Property(e => e.MontoCuotaInicial)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_cuota_inicial")
                    .HasComment("Es el monto de la cuota inicial expresado en dolares.");

                entity.Property(e => e.MontoNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_neto")
                    .HasComment("Es el monto con los descuentos incluidos.");

                entity.Property(e => e.MontoTotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total")
                    .HasComment("Ese el monto total de la venta realizada.");

                entity.Property(e => e.PorcentajeCuotaInicial)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("porcentaje_cuota_inicial")
                    .HasComment("Es el porcentaje de la cuota inicial de una venta.");

                entity.Property(e => e.PorcentajeDescuento)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("porcentaje_descuento")
                    .HasComment("Es el porcentaje de descuento de  la venta.");

                entity.Property(e => e.ReferidoId)
                    .HasColumnName("referido_id")
                    .HasComment("Es el id de accesor que le vendio un producto.");

                entity.Property(e => e.VentaConexionId)
                    .HasColumnName("venta_conexion_id")
                    .HasComment("Este es el codigo de la venta en conexion.");
            });

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

                entity.Property(e => e.IdComision).HasColumnName("id_comision");

                entity.Property(e => e.IdEstadoComision).HasColumnName("id_estado_comision");

                entity.Property(e => e.IdTipoComision).HasColumnName("id_tipo_comision");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<VwObtenerCiclosRezagado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerCiclosRezagados");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdCiclo).HasColumnName("id_ciclo");

                entity.Property(e => e.IdComision).HasColumnName("id_comision");

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
                    .HasColumnType("decimal(18, 2)")
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

                entity.Property(e => e.IdComision).HasColumnName("id_comision");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdEstadoComision).HasColumnName("id_estado_comision");

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

                entity.Property(e => e.EntidadDestino)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ENTIDAD_DESTINO");

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

                entity.Property(e => e.IdComision).HasColumnName("id_comision");

                entity.Property(e => e.IdComisionDetalleEmpresa).HasColumnName("id_comision_detalle_empresa");

                entity.Property(e => e.IdComisionesDetalle).HasColumnName("id_comisiones_detalle");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdEstadoComisionDetalleEmpresa).HasColumnName("id_estado_comision_detalle_empresa");

                entity.Property(e => e.IdFicha).HasColumnName("id_ficha");

                entity.Property(e => e.IdListaFormasPago).HasColumnName("id_lista_formas_pago");

                entity.Property(e => e.IdTipoPago).HasColumnName("id_tipo_pago");

                entity.Property(e => e.ImporteNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("IMPORTE_NETO");

                entity.Property(e => e.ImportePorEmpresa)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("IMPORTE_POR_EMPRESA");

                entity.Property(e => e.MonedaDestino)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MONEDA_DESTINO");

                entity.Property(e => e.NombreBanco)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDeCliente)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_DE_CLIENTE");

                entity.Property(e => e.NroDeCuenta)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NRO_DE_CUENTA");

                entity.Property(e => e.SucursalDestino)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUCURSAL_DESTINO");
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

            modelBuilder.Entity<VwObtenerRezagadosPago>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwObtenerRezagadosPagos");

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

                entity.Property(e => e.EntidadDestino)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ENTIDAD_DESTINO");

                entity.Property(e => e.EstadoComisionHabilitado).HasColumnName("estado_comision_habilitado");

                entity.Property(e => e.EstadoListadoFormaPagoHabilitado).HasColumnName("estado_listado_forma_pago_habilitado");

                entity.Property(e => e.FechaActualizacionComision)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion_comision");

                entity.Property(e => e.FechaCreacionComision)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion_comision");

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

                entity.Property(e => e.IdComision).HasColumnName("id_comision");

                entity.Property(e => e.IdComisionDetalleEmpresa).HasColumnName("id_comision_detalle_empresa");

                entity.Property(e => e.IdComisionesDetalle).HasColumnName("id_comisiones_detalle");

                entity.Property(e => e.IdDetalleEstadoListadoFormaPago).HasColumnName("id_detalle_estado_listado_forma_pago");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdEstadoComision).HasColumnName("id_estado_comision");

                entity.Property(e => e.IdEstadoComisionDetalleEmpresa).HasColumnName("id_estado_comision_detalle_empresa");

                entity.Property(e => e.IdEstadoListadoFormaPago).HasColumnName("id_estado_listado_forma_pago");

                entity.Property(e => e.IdFicha).HasColumnName("id_ficha");

                entity.Property(e => e.IdListaFormasPago).HasColumnName("id_lista_formas_pago");

                entity.Property(e => e.IdTipoComision).HasColumnName("id_tipo_comision");

                entity.Property(e => e.IdTipoPago).HasColumnName("id_tipo_pago");

                entity.Property(e => e.ImporteNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("IMPORTE_NETO");

                entity.Property(e => e.ImportePorEmpresa)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("IMPORTE_POR_EMPRESA");

                entity.Property(e => e.MonedaDestino)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MONEDA_DESTINO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreBanco)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_BANCO");

                entity.Property(e => e.NombreDeCliente)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_DE_CLIENTE");

                entity.Property(e => e.NroDeCuenta)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NRO_DE_CUENTA");

                entity.Property(e => e.SucursalDestino)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUCURSAL_DESTINO");
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

            modelBuilder.Entity<VwPagosIncentivo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPagosIncentivos");

                entity.Property(e => e.Banco)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("banco");

                entity.Property(e => e.CedulaIdentidad)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cedula_identidad");

                entity.Property(e => e.ComisionPagada)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("comisionPagada");

                entity.Property(e => e.CuentaBanco)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cuenta_banco");

                entity.Property(e => e.CuentaSionPay)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("cuentaSionPay")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.IdCiclo).HasColumnName("idCiclo");

                entity.Property(e => e.IdComision).HasColumnName("id_comision");

                entity.Property(e => e.IdTipoIncentivo).HasColumnName("id_tipo_incentivo");

                entity.Property(e => e.IdTipoIncentivoPago).HasColumnName("id_tipo_incentivo_pago");

                entity.Property(e => e.MontoTotalNeto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto_total_neto");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(511)
                    .IsUnicode(false)
                    .HasColumnName("nombre_completo");

                entity.Property(e => e.TipoIncentivo)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPago)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pago");
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
