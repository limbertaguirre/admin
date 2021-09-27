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
        public virtual DbSet<Banco> Bancoes { get; set; }
        public virtual DbSet<Bitacora> Bitacoras { get; set; }
        public virtual DbSet<BitacoraDetalle> BitacoraDetalles { get; set; }
        public virtual DbSet<Ciclo> Cicloes { get; set; }
        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<ComisionDetalleEmpresa> ComisionDetalleEmpresas { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<EstadoDetalleListadoFormaPago> EstadoDetalleListadoFormaPagoes { get; set; }
        public virtual DbSet<Ficha> Fichas { get; set; }
        public virtual DbSet<FichaIncentivo> FichaIncentivoes { get; set; }
        public virtual DbSet<FichaNivelI> FichaNivelIs { get; set; }
        public virtual DbSet<FichaTipoBajaI> FichaTipoBajaIs { get; set; }
        public virtual DbSet<GpClienteVendedorI> GpClienteVendedorIs { get; set; }
        public virtual DbSet<GpComision> GpComisions { get; set; }
        public virtual DbSet<GpComisionDetalle> GpComisionDetalles { get; set; }
        public virtual DbSet<GpComisionDetalleEstadoI> GpComisionDetalleEstadoIs { get; set; }
        public virtual DbSet<GpComisionEstadoComisionI> GpComisionEstadoComisionIs { get; set; }
        public virtual DbSet<GpDetalleEstadoDetalleListadoFormaPagol> GpDetalleEstadoDetalleListadoFormaPagols { get; set; }
        public virtual DbSet<GpDetalleListadoFormaPago> GpDetalleListadoFormaPagoes { get; set; }
        public virtual DbSet<GpEstadoComision> GpEstadoComisions { get; set; }
        public virtual DbSet<GpEstadoComisionDetalle> GpEstadoComisionDetalles { get; set; }
        public virtual DbSet<GpEstadoProrrateoDetalle> GpEstadoProrrateoDetalles { get; set; }
        public virtual DbSet<GpEstadoProrrateoDetalleIncentivo> GpEstadoProrrateoDetalleIncentivoes { get; set; }
        public virtual DbSet<GpPorrateroDetalleIncentivo> GpPorrateroDetalleIncentivoes { get; set; }
        public virtual DbSet<GpProrrateoDetalle> GpProrrateoDetalles { get; set; }
        public virtual DbSet<GpTipoComision> GpTipoComisions { get; set; }
        public virtual DbSet<Incentivo> Incentivoes { get; set; }
        public virtual DbSet<ListadoFormasPago> ListadoFormasPagoes { get; set; }
        public virtual DbSet<LogDetalleComisionEmpresaFail> LogDetalleComisionEmpresaFails { get; set; }
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
        public virtual DbSet<TipoBaja> TipoBajas { get; set; }
        public virtual DbSet<TipoIncentivo> TipoIncentivoes { get; set; }
        public virtual DbSet<TipoPago> TipoPagoes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VwObtenerCiclo> VwObtenerCiclos { get; set; }
        public virtual DbSet<VwObtenerComisionesDetalleAplicacione> VwObtenerComisionesDetalleAplicaciones { get; set; }
        public virtual DbSet<VwObtenerComisionesDetalleEmpresa> VwObtenerComisionesDetalleEmpresas { get; set; }
        public virtual DbSet<VwObtenerFicha> VwObtenerFichas { get; set; }
        public virtual DbSet<VwObtenerProyectoxProducto> VwObtenerProyectoxProductoes { get; set; }
        public virtual DbSet<VwObtenercomisione> VwObtenercomisiones { get; set; }
        public virtual DbSet<VwObtenercomisionesFormaPago> VwObtenercomisionesFormaPagoes { get; set; }

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
                    .HasName("PK__APLICACI__DE63A1C5785BF4B8");

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
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de actualizacion del registro");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Es el timestamp de creacion del registro");

                entity.Property(e => e.IdBdqishur).HasColumnName("id_bdqishur");

                entity.Property(e => e.IdComisionesDetalle)
                    .HasColumnName("id_comisiones_detalle")
                    .HasComment("llave foranead de la tabla comision detalle donde se tiene toda la comision del cliente frilanzer.");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("id_proyecto")
                    .HasComment("llave foranea que hace referencia al codigo de un proyecto de grupo sion.");

                entity.Property(e => e.IdTipoAplicaciones)
                    .HasColumnName("id_tipo_aplicaciones")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del ultimo usuario que modifico el registro.");

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
                    .HasName("PK__AREA__8A8C837B2E35FC55");

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

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.IdBanco)
                    .HasName("PK__BANCO__70BD16425A561EEB");

                entity.ToTable("BANCO");

                entity.Property(e => e.IdBanco)
                    .ValueGeneratedNever()
                    .HasColumnName("id_banco");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
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

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.IdBitacora)
                    .HasName("PK__BITACORA__7E4268B007BFEFDA");

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
                    .HasName("PK__BITACORA__8597C44BFCF71FE2");

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
                    .HasName("PK__CICLO__A78E2FA306351304");

                entity.ToTable("CICLO");

                entity.Property(e => e.IdCiclo)
                    .ValueGeneratedNever()
                    .HasColumnName("id_ciclo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("PK__CIUDAD__B7DC4CD59BBDFECF");

                entity.ToTable("CIUDAD");

                entity.Property(e => e.IdCiudad)
                    .HasColumnName("id_ciudad")
                    .HasComment("Llave primaria incremental de la tabla CIUDAD.");

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
                    .HasName("PK__COMISION__A81C75CC642A1E1B");

                entity.ToTable("COMISION_DETALLE_EMPRESA");

                entity.Property(e => e.IdComisionDetalleEmpresa)
                    .HasColumnName("id_comision_detalle_empresa")
                    .HasComment("Es la llave primaria de la tabla");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasComment("Es el estado de la tabla activo (1) e inactico (0)");

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

                entity.Property(e => e.IdComisionDetalle).HasColumnName("id_comision_detalle");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

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
                    .HasMaxLength(1)
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
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ventas_personales");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__EMPRESA__4A0B7E2CFB006100");

                entity.ToTable("EMPRESA");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.CodigoCnx).HasColumnName("codigo_cnx");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreBd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_bd");
            });

            modelBuilder.Entity<EstadoDetalleListadoFormaPago>(entity =>
            {
                entity.HasKey(e => e.IdEstadoDetalleListadoFormaPago)
                    .HasName("PK__ESTADO_D__6FD4B26DFE4C3711");

                entity.ToTable("ESTADO_DETALLE_LISTADO_FORMA_PAGO");

                entity.Property(e => e.IdEstadoDetalleListadoFormaPago)
                    .ValueGeneratedNever()
                    .HasColumnName("id_estado_detalle_listado_forma_pago")
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
                    .HasName("PK__FICHA__427B0F8A13F3FB8F");

                entity.ToTable("FICHA");

                entity.Property(e => e.IdFicha).HasColumnName("id_ficha");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.Ci)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ci");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.CodigoCnx)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("codigo_cnx");

                entity.Property(e => e.Comentario)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.CuentaBancaria)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cuenta_bancaria");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FacturaHabilitado).HasColumnName("factura_habilitado");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro");

                entity.Property(e => e.IdBanco).HasColumnName("id_banco");

                entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nit)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nit");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("razon_social");

                entity.Property(e => e.TelFijo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tel_fijo");

                entity.Property(e => e.TelMovil)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tel_movil");

                entity.Property(e => e.TelOficina)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tel_oficina");

                entity.Property(e => e.TieneCuentaBancaria).HasColumnName("tiene_cuenta_bancaria");
            });

            modelBuilder.Entity<FichaIncentivo>(entity =>
            {
                entity.HasKey(e => e.IdFichaIncentivo)
                    .HasName("PK__FICHA_IN__8B56473853F0E6EB");

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
                    .HasName("PK__FICHA_NI__2944BB1A2DEF578E");

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
                    .HasName("PK__FICHA_TI__CD4CE5D404344B5E");

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
                    .HasName("PK__GP_CLIEN__74641BB0E68C5BD8");

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
                    .HasName("PK__GP_COMIS__B25ABED0167E5974");

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
                    .HasName("PK__GP_COMIS__89C1F9943403E821");

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
                    .HasName("PK__GP_COMIS__3C036DC381997D5E");

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
                    .HasName("PK__GP_COMIS__9D60F2EB7D876A5F");

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

            modelBuilder.Entity<GpDetalleEstadoDetalleListadoFormaPagol>(entity =>
            {
                entity.ToTable("GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL");

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

                entity.Property(e => e.IdEstadoDetalleListadoFormaPago)
                    .HasColumnName("id_estado_detalle_listado_forma_pago")
                    .HasComment("llave foranea de la tabla estado del listado de forma de pagos");

                entity.Property(e => e.IdGpDetalleListadoFormaPago)
                    .HasColumnName("id_gp_detalle_listado_forma_pago")
                    .HasComment("llave forare de la tabla detalle lista formad e pago.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");
            });

            modelBuilder.Entity<GpDetalleListadoFormaPago>(entity =>
            {
                entity.HasKey(e => e.IdGpDetalleListadoFormaPago)
                    .HasName("PK__GP_DETAL__6EECFD025E8A5656");

                entity.ToTable("GP_DETALLE_LISTADO_FORMA_PAGO");

                entity.Property(e => e.IdGpDetalleListadoFormaPago)
                    .HasColumnName("id_gp_detalle_listado_forma_pago")
                    .HasComment("Es la llave primaria de la tabla.");

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

                entity.Property(e => e.IdEmpresa)
                    .HasColumnName("id_empresa")
                    .HasComment("Llave foranea que hace referencia a la tabla empresa.");

                entity.Property(e => e.IdListaFormasPago)
                    .HasColumnName("id_lista_formas_pago")
                    .HasComment("Llave foranea d la tabla lista forma de pagos.");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("monto")
                    .HasComment("Es el monto en especifico detallado de un listado de forma de pago.");
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
                    .HasName("PK__GP_ESTAD__A98DDADAFA386B39");

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
                    .HasName("PK__GP_ESTAD__4082A0B6D61B94F4");

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
                    .HasName("PK__GP_PORRA__6CC4685C45B6DA4D");

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
                    .HasName("PK__GP_PRORR__94C19122B10AB02E");

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
                    .HasName("PK__INCENTIV__6035F00CFBD749DD");

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

            modelBuilder.Entity<ListadoFormasPago>(entity =>
            {
                entity.HasKey(e => e.IdListaFormasPago)
                    .HasName("PK__LISTADO___31038ED8BB67B509");

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

            modelBuilder.Entity<LogDetalleComisionEmpresaFail>(entity =>
            {
                entity.HasKey(e => e.IdDetalleComisioEmpresaFail)
                    .HasName("PK__LOG_DETA__60ED7DCFA55D98CE");

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

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo)
                    .HasName("PK__MODULO__B2584DFCCBA5C949");

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
                    .HasName("PK__PAGINA__A2A7C7B64C26E0AE");

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
                    .HasName("PK__PAIS__0941A3A7C2CA03BA");

                entity.ToTable("PAIS");

                entity.Property(e => e.IdPais)
                    .HasColumnName("id_pais")
                    .HasComment("Llave primaria incremental de la tabla PAIS.");

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
                    .HasName("PK__PERMISO__228F224F17877B9F");

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
                    .HasName("PK__PROYECTO__F38AD81D0F538037");

                entity.ToTable("PROYECTO");

                entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");

                entity.Property(e => e.ComplejoidGuardian).HasColumnName("complejoid_guardian");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.ProyectoConexionId).HasColumnName("proyecto_conexion_id");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__ROL__6ABCB5E0E893554E");

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
                    .HasName("PK__ROL_PAGI__657F32AE4909C711");

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
                    .HasName("PK__ROL_PAGI__31BAAF486CCF8CD3");

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
                    .HasName("PK__SUCURSAL__4C75801363F016E6");

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
                    .HasName("PK__TIPO_APL__D56E6A9C0C3B7752");

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
                    .HasName("PK__TIPO_INC__FAEB36E6B0294776");

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

            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.HasKey(e => e.IdTipoPago)
                    .HasName("PK__TIPO_PAG__F7E781E5527E40EB");

                entity.ToTable("TIPO_PAGO");

                entity.Property(e => e.IdTipoPago)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tipo_pago")
                    .HasComment("Es la llave primaria de la tabla");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion mas detallada del nombre de la tabla");

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
                    .HasName("PK__USUARIO__4E3E04AD3FF25A66");

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

            modelBuilder.Entity<UsuariosRole>(entity =>
            {
                entity.HasKey(e => e.IdUsuariosRoles)
                    .HasName("PK__USURIOS___720F812BE54AB333");

                entity.ToTable("USUARIOS_ROLES");

                entity.HasIndex(e => e.IdUsuario, "UQ__USURIOS___4E3E04AC7702B449")
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
                    .HasName("PK__VENTA__459533BFA1808F4E");

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

                entity.Property(e => e.IdEstadoComision).HasColumnName("id_estado_comision");

                entity.Property(e => e.IdFicha).HasColumnName("idFicha");

                entity.Property(e => e.IdListaFormasPago).HasColumnName("id_lista_formas_pago");

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

                entity.Property(e => e.TipoPagoDescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pago_descripcion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
