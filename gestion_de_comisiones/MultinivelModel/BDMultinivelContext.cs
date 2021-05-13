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
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<EstadoDetalleListadoFormaPago> EstadoDetalleListadoFormaPagoes { get; set; }
        public virtual DbSet<Ficha> Fichas { get; set; }
        public virtual DbSet<FichaIncentivo> FichaIncentivoes { get; set; }
        public virtual DbSet<FichaNivelI> FichaNivelIs { get; set; }
        public virtual DbSet<FichaTipoBajaI> FichaTipoBajaIs { get; set; }
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
        public virtual DbSet<TipoBaja> TipoBajas { get; set; }
        public virtual DbSet<TipoIncentivo> TipoIncentivoes { get; set; }
        public virtual DbSet<TipoPago> TipoPagoes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

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

            modelBuilder.Entity<AplicacionDetalleProducto>(entity =>
            {
                entity.HasKey(e => e.IdAplicacionDetalleProducto)
                    .HasName("PK__APLICACI__DE63A1C5E0190487");

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

                entity.Property(e => e.IdComisionesDetalle)
                    .HasColumnName("id_comisiones_detalle")
                    .HasComment("llave foranead de la tabla comision detalle donde se tiene toda la comision del cliente frilanzer.");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("id_proyecto")
                    .HasComment("llave foranea que hace referencia al codigo de un proyecto de grupo sion.");

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
                    .HasName("PK__AREA__8A8C837BD3E0EEA0");

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
                    .HasName("PK__BANCO__70BD16425B2BE58A");

                entity.ToTable("BANCO");

                entity.Property(e => e.IdBanco)
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
                    .HasName("PK__BITACORA__7E4268B04EEBB0CA");

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
                    .HasName("PK__BITACORA__8597C44B166EC625");

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
                    .HasName("PK__CICLO__A78E2FA3D25C237B");

                entity.ToTable("CICLO");

                entity.Property(e => e.IdCiclo)
                    .HasColumnName("id_ciclo")
                    .HasComment("Llave primaria incremental de la tabla CICLO.");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Muestra una descripción breve del ciclo.");

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
                    .HasName("PK__CIUDAD__B7DC4CD58FD9BE15");

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

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__EMPRESA__4A0B7E2CC7A63988");

                entity.ToTable("EMPRESA");

                entity.Property(e => e.IdEmpresa)
                    .HasColumnName("id_empresa")
                    .HasComment("Es la llave primaria y id de la empresa local ");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasComment("Es el codigo de empresa externo de guardian de gruposion");

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
            });

            modelBuilder.Entity<EstadoDetalleListadoFormaPago>(entity =>
            {
                entity.HasKey(e => e.IdEstadoDetalleListadoFormaPago)
                    .HasName("PK__ESTADO_D__6FD4B26D40F4E37C");

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
                    .HasName("PK__FICHA__427B0F8A0C1F8D98");

                entity.ToTable("FICHA");

                entity.Property(e => e.IdFicha)
                    .HasColumnName("id_ficha")
                    .HasComment("Llave primaria incremental de la tabla FICHA.");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("apellidos")
                    .HasComment("Apellido completo del asesor");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar")
                    .HasComment("El avatar es el path de ubicación de la foto del asesor.");

                entity.Property(e => e.Ci)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ci")
                    .HasComment("El carnet de identidad del asesor");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("codigo")
                    .HasComment("El código es un identificador que identifica al Asesor y que es generado y asignado por la empresa.");

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

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasComment("El id_usuario es el id del último usuario que modificó el registro.");

                entity.Property(e => e.Nit)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nit")
                    .HasComment("Nit del Asesor, si no tiene el valor es null.");

                entity.Property(e => e.Nombres)
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
                    .HasName("PK__FICHA_IN__8B5647387087823B");

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
                    .HasName("PK__FICHA_NI__2944BB1A17B4DE59");

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
                    .HasName("PK__FICHA_TI__CD4CE5D4908A6F42");

                entity.ToTable("FICHA_TIPO_BAJA_I");

                entity.Property(e => e.IdFichaTipoBajaI)
                    .HasColumnName("id_ficha_tipo_baja_i")
                    .HasComment("Llave primaria incremental de la tabla FICHA_TIPO_BAJA_I.");

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

            modelBuilder.Entity<GpComision>(entity =>
            {
                entity.HasKey(e => e.IdComision)
                    .HasName("PK__GP_COMIS__B25ABED0DE74A4A6");

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
                    .HasName("PK__GP_COMIS__89C1F994CAF6DCB6");

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
                    .HasName("PK__GP_COMIS__3C036DC36017D585");

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
                    .HasName("PK__GP_COMIS__9D60F2EBD40AAB9B");

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
                    .HasName("PK__GP_DETAL__6EECFD02622B31BD");

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
                    .HasName("PK__GP_ESTAD__A98DDADA2C924B5D");

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
                    .HasName("PK__GP_ESTAD__4082A0B69A9653EF");

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
                    .HasName("PK__GP_PORRA__6CC4685CD4DC904E");

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
                    .HasName("PK__GP_PRORR__94C19122AB4068E6");

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
                    .HasName("PK__INCENTIV__6035F00CED76D9F7");

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
                    .HasName("PK__LISTADO___31038ED87B01AC9F");

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

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo)
                    .HasName("PK__MODULO__B2584DFC4577552F");

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
                    .HasName("PK__PAGINA__A2A7C7B65F4A588F");

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
                    .HasName("PK__PAIS__0941A3A737165C46");

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
                    .HasName("PK__PERMISO__228F224F5887399B");

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
                    .HasName("PK__PROYECTO__F38AD81D27BFC89F");

                entity.ToTable("PROYECTO");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("id_proyecto")
                    .HasComment("Llave primaria de la tabla.");

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
                    .HasName("PK__ROL__6ABCB5E0D6741455");

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
                    .HasName("PK__ROL_PAGI__657F32AEC55BB52B");

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
                    .HasName("PK__ROL_PAGI__31BAAF4868770EE2");

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
                    .HasName("PK__SUCURSAL__4C758013370DBBE5");

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
                    .HasName("PK__TIPO_INC__FAEB36E67B66AF7F");

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
                    .HasName("PK__TIPO_PAG__F7E781E52795BE41");

                entity.ToTable("TIPO_PAGO");

                entity.Property(e => e.IdTipoPago)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tipo_pago")
                    .HasComment("Es la llave primaria de la tabla");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .HasComment("Es la descripcion mas detallada del nombre de la tabla");

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
                    .HasName("PK__USUARIO__4E3E04ADBECEAC8B");

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

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__VENTA__459533BF67FE0033");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
