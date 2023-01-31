using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace admin.OperacionModel
{
    public partial class BDOperacionContext : DbContext
    {
        public BDOperacionContext()
        {
        }

        public BDOperacionContext(DbContextOptions<BDOperacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Bitacora> Bitacoras { get; set; }
        public virtual DbSet<BitacoraDetalle> BitacoraDetalles { get; set; }
        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<ControlUsuario> ControlUsuarios { get; set; }
        public virtual DbSet<Modulo> Moduloes { get; set; }
        public virtual DbSet<Pagina> Paginas { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Permiso> Permisoes { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<RolPaginaI> RolPaginaIs { get; set; }
        public virtual DbSet<RolPaginaPermisoI> RolPaginaPermisoIs { get; set; }
        public virtual DbSet<Sucursal> Sucursals { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.2.10.15;Database=BDOperacion; User Id=sa;password=Passw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea)
                    .HasName("PK__AREA__8A8C837BEBCB06B2");

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

            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.IdBitacora)
                    .HasName("PK__BITACORA__7E4268B0B5914D45");

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
                    .HasName("PK__BITACORA__8597C44B2DADC243");

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

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("PK__CIUDAD__B7DC4CD5280E651F");

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

            modelBuilder.Entity<ControlUsuario>(entity =>
            {
                entity.HasKey(e => e.IdControlUsuario)
                    .HasName("PK__CONTROL___E299BFDC0B9FDB69");

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

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo)
                    .HasName("PK__MODULO__B2584DFC574AC7E6");

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

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.IdPagina)
                    .HasName("PK__PAGINA__A2A7C7B62A14FBA1");

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
                    .HasName("PK__PAIS__0941A3A7642E6C64");

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
                    .HasName("PK__PERMISO__228F224F8D404F98");

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

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__ROL__6ABCB5E047BA269C");

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
                    .HasName("PK__ROL_PAGI__657F32AEB3D26234");

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
                    .HasName("PK__ROL_PAGI__31BAAF4854BBBADC");

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
                    .HasName("PK__SUCURSAL__4C758013CA8F18B6");

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

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__4E3E04AD15ABAA7C");

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
                    .HasName("PK__USUARIOS__720F812BABF14EE9");

                entity.ToTable("USUARIOS_ROLES");

                entity.HasIndex(e => e.IdUsuario, "UQ__USUARIOS__4E3E04AC8E31791B")
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
