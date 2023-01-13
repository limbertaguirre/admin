
create database BDOperacion;
go
use BDOperacion;
go

create table ROL
(
    id_rol int not null primary key IDENTITY,
    nombre varchar(255),
    descripcion varchar(255),
    habilitado bit,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla ROL.', 'SCHEMA', 'dbo', 'TABLE', 'ROL', N'COLUMN', N'id_rol'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del rol. Ej. Gerente, Coordinador, Analista, etc.', 'SCHEMA', 'dbo', 'TABLE', 'ROL', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del rol.', 'SCHEMA', 'dbo', 'TABLE', 'ROL', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'ROL', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'ROL', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'ROL', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'ROL', N'COLUMN', N'fecha_actualizacion'

create table AREA
(
    id_area int not null primary key IDENTITY,
    nombre varchar(255),
    descripcion varchar(255),
    habilitado bit,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla AREA.', 'SCHEMA', 'dbo', 'TABLE', 'AREA', N'COLUMN', N'id_area'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del área. Ej. Calidad, Finanzas, Cartera, etc.', 'SCHEMA', 'dbo', 'TABLE', 'AREA', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del área al cual pertenece el usuario.', 'SCHEMA', 'dbo', 'TABLE', 'AREA', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'AREA', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'AREA', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'AREA', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'AREA', N'COLUMN', N'fecha_actualizacion'
go
     insert into AREA (nombre,descripcion, habilitado, id_usuario) values('Unidad Tecnologica','Es el departamento de area de desarrolo e imnovacion',1,100);
	 insert into AREA (nombre,descripcion, habilitado, id_usuario) values('Calidad','en cargado de ',1,100);
	 insert into AREA (nombre,descripcion, habilitado, id_usuario) values('Contabilidad','en cargado de ',1,100);
go
create table PAIS
(
    id_pais int not null primary key,
    nombre varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria  de la tabla PAIS.', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'id_pais'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del pais. Ej. Bolivia, Brasil, E.E.U.U., etc.', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'fecha_actualizacion'
go
     ----- correr insert primera vej, PAIS  Y CIUADAD
	 --insert PAIS select pa.IDPAIS, pa.DESCRIPCION, 1, GETDATE(), GETDATE() from BDComisiones.dbo.PEPAIS pa  
    
go
create table CIUDAD
(
    id_ciudad int not null primary key,
    nombre varchar(255),
    id_pais int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
	codigo varchar(50) default '',
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria  de la tabla CIUDAD.', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'id_ciudad'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_pais es un identificador que hace referencia al campo id_pais de la tabla PAIS.', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'id_pais'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'fecha_actualizacion'
go
      ----- correr insert primera vej, PAIS  Y CIUADAD	 la condicion es porq esa ciudad esta duplicada en 
      insert ciudad select c.IDCIUDAD, c.DESCRIPCION, c.IDPAIS, 1, GETDATE(), GETDATE(),'' from BDComisiones.dbo.PECIUDAD c   where IDCIUDAD != 4090

		update CIUDAD set codigo = 'SCZ' where id_ciudad = 1
		update CIUDAD set codigo = 'LPZ' where id_ciudad = 2
		update CIUDAD set codigo = 'CBB' where id_ciudad = 3
		update CIUDAD set codigo = 'TJA' where id_ciudad = 4
		update CIUDAD set codigo = 'POT' where id_ciudad = 5
		update CIUDAD set codigo = 'ORU' where id_ciudad = 6
		update CIUDAD set codigo = 'SUC' where id_ciudad = 7
		update CIUDAD set codigo = 'TRI' where id_ciudad = 8
		update CIUDAD set codigo = 'COB' where id_ciudad = 9
		update CIUDAD set codigo = 'TJA' where id_ciudad = 10
		update CIUDAD set codigo = 'LPZ' where id_ciudad = 11
		update CIUDAD set codigo = 'SCZ' where id_ciudad = 4084
		update CIUDAD set codigo = 'CBB' where id_ciudad = 4086
		update CIUDAD set codigo = 'SCZ' where id_ciudad = 10000

go
create table SUCURSAL
(
    id_sucursal int not null primary key IDENTITY,
    nombre varchar(255),
    descripcion varchar(255),
    habilitado bit,
    id_ciudad int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla SUCURSAL.', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'id_sucursal'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre de la sucursal. Ej. Ambassador, Cañoto, Kalomai, etc.', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve de la Sucursal', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_ciudad es un identificador que hace referencia al campo id_ciudad de la tabla PAIS.', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'id_ciudad'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'SUCURSAL', N'COLUMN', N'fecha_actualizacion'

go
   insert into SUCURSAL (nombre,descripcion, habilitado, id_ciudad, id_usuario) values('AMBASADOR','Edificio ubicado en la av san martin 2do y 3re anillo',1,1,100);
   insert into SUCURSAL (nombre,descripcion, habilitado, id_ciudad, id_usuario) values('CAÑOTO','Edificio ubicado av caÑoto primer anillo',1,1,100);
   insert into SUCURSAL (nombre,descripcion, habilitado, id_ciudad, id_usuario) values('QUIJARRO','Primer anillo zona los pozos',1,1,100);
   
go
create table USUARIO
(
    id_usuario int not null primary key IDENTITY,
    usuario varchar(255),
    nombres varchar(255),
    apellidos varchar(255),
    telefono varchar(50),
    corporativo varchar(50),
    fecha_nacimiento date,
    id_rol int,
    id_sucursal int,
    id_area int,
    usuario_id int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
	estado bit NOT NULL DEFAULT 1,
);
go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla USUARIO.', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre de usuario extraído del dominio que tiene asignado un trabajador en la empresa', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre completo del usuario', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'nombres'
EXECUTE sp_addextendedproperty 'MS_Description', 'Apellido completo del usuario', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'apellidos'
EXECUTE sp_addextendedproperty 'MS_Description', 'Teléfono del usuario', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'telefono'
EXECUTE sp_addextendedproperty 'MS_Description', 'Teléfono comporativo del usuario', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'corporativo'
EXECUTE sp_addextendedproperty 'MS_Description', 'Fecha de nacimiento del usuario', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'fecha_nacimiento'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_rol es un identificador que hace referencia al campo id_rol de la tabla ROL.', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'id_rol'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_sucursal es un identificador que hace referencia al campo id_sucursal de la tabla SUCURSAL.', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'id_sucursal'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_area es un identificador que hace referencia al campo id_area de la tabla AREA.', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'id_area'
EXECUTE sp_addextendedproperty 'MS_Description', 'El usuario_id es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'usuario_id'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO', N'COLUMN', N'fecha_actualizacion'
go

create table USUARIOS_ROLES
(
    id_usuarios_roles int not null primary key IDENTITY,
    id_usuario int not null unique,
	id_rol int not null,
    estado bit,
    usuario_id int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);
go
EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'USUARIOS_ROLES', N'COLUMN', N'id_usuarios_roles'
EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea que hace referencia a la tabla usuario', 'SCHEMA', 'dbo', 'TABLE', 'USUARIOS_ROLES', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea que hace relacion con la tabla Rol', 'SCHEMA', 'dbo', 'TABLE', 'USUARIOS_ROLES', N'COLUMN', N'id_rol'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado del requistro booleano true o false', 'SCHEMA', 'dbo', 'TABLE', 'USUARIOS_ROLES', N'COLUMN', N'estado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El usuario_id es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'USUARIOS_ROLES', N'COLUMN', N'usuario_id'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'USUARIOS_ROLES', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'USUARIOS_ROLES', N'COLUMN', N'fecha_actualizacion'

go
create table MODULO
(
    id_modulo int not null primary key IDENTITY,
    nombre varchar(255),
    icono varchar(max),
    orden int,
    habilitado bit,
    id_modulo_padre int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);
go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla MODULO.', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'id_modulo'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del módulo que agrupa un conjunto de submódulos y páginas del sistema.', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Path de ubicación en el servidor del ícono asignado al módulo.', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'icono'
EXECUTE sp_addextendedproperty 'MS_Description', 'Número entero que muestra el orden del módulo que se mostrará en el menú.', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'orden'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_modulo_padre es un identificador que hace referencia al campo id_modulo de la tabla MODULO. Si está null o 0 entonces es padre, de lo contrario es hijo que hace referencia al id_modulo padre.', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'id_modulo_padre'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'MODULO', N'COLUMN', N'fecha_actualizacion'
go
        ----Modulo padre
		--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Configuraciones','config',3,1,null,1); 

		--modulo hijo
		--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Gestión de roles','gestionRolesIcon',1,1,1,1);--hijo	 
		--insert INTO MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) VALUES('Usuarios','gestionClienteIcon',2,1,1,1); --hijo

	
go
create table PAGINA
(
    id_pagina int not null primary key IDENTITY,
    nombre varchar(255),
    url_pagina varchar(255),
    icono varchar(max),
    orden int,
    habilitado bit,
    id_modulo int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla PAGINA.', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'id_pagina'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del módulo que agrupa un conjunto de submódulos y páginas del sistema.', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Url/Path de la página para ser gestionado desde los controladores.', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'url_pagina'
EXECUTE sp_addextendedproperty 'MS_Description', 'Path de ubicación en el servidor del ícono asignado a la página.', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'icono'
EXECUTE sp_addextendedproperty 'MS_Description', 'Número entero que muestra el orden de la página que se mostrará en el menú.', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'orden'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_modulo es un identificador que hace referencia al campo id_modulo de la tabla MODULO.', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'id_modulo'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'PAGINA', N'COLUMN', N'fecha_actualizacion'
go
----add modulo antes estos hacen referencia a los id de los modulos hijos

	--insertar PAGINA con el idModulo del Hijo
		--insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Roles','/gestion/roles','RolIcon',1,1,2,1);  
		--insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Asignación de roles','/usuario/asignar-roless','facIcon',1,1,3,1); 
	 

go
create table PERMISO
(
    id_permiso int not null primary key IDENTITY,
    permiso varchar(255),
    descripcion varchar(max),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla PERMISO.', 'SCHEMA', 'dbo', 'TABLE', 'PERMISO', N'COLUMN', N'id_permiso'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del permiso', 'SCHEMA', 'dbo', 'TABLE', 'PERMISO', N'COLUMN', N'permiso'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del permiso.', 'SCHEMA', 'dbo', 'TABLE', 'PERMISO', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'PERMISO', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'PERMISO', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'PERMISO', N'COLUMN', N'fecha_actualizacion'

go
  --insert into PERMISO (permiso,descripcion, id_usuario) values('VISUALIZAR','este te permite visualizar  y acceder',1);
  --insert into PERMISO (permiso,descripcion, id_usuario) values('CREAR','este te permite crear un registro',1);
  --insert into PERMISO (permiso,descripcion, id_usuario) values('ACTUALIZAR','este te permite actualizr un registro',1);
  --insert into PERMISO (permiso,descripcion, id_usuario) values('ELIMINAR','este te permite eliminar un registro',1);
go
create table ROL_PAGINA_I
(
    id_rol_pagina_i int not null primary key IDENTITY,
    habilitado bit,
    id_rol int,
    id_pagina int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla ROL_PAGINA_I.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_I', N'COLUMN', N'id_rol_pagina_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_I', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_rol es un identificador que hace referencia al campo id_rol de la tabla ROL.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_I', N'COLUMN', N'id_rol'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_pagina es un identificador que hace referencia al campo id_pagina de la tabla PAGINA.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_I', N'COLUMN', N'id_pagina'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_I', N'COLUMN', N'fecha_actualizacion'

create table ROL_PAGINA_PERMISO_I
(
    id_rol_pagina_permiso_i int not null primary key IDENTITY,
    habilitado bit,
    id_rol_pagina int,
    id_permiso int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla ROL_PAGINA_PERMISO_I.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_PERMISO_I', N'COLUMN', N'id_rol_pagina_permiso_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_PERMISO_I', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_rol_pagina es un identificador que hace referencia al campo id_rol_pagina de la tabla ROL_PAGINA.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_PERMISO_I', N'COLUMN', N'id_rol_pagina'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_permiso es un identificador que hace referencia al campo id_permiso de la tabla PERMISO.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_PERMISO_I', N'COLUMN', N'id_permiso'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_PERMISO_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_PERMISO_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'ROL_PAGINA_PERMISO_I', N'COLUMN', N'fecha_actualizacion'

create table BITACORA
(
    id_bitacora int not null primary key IDENTITY,
    id_pagina int,     
    ip varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla BITACORA.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA', N'COLUMN', N'id_bitacora'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_pagina es un identificador que hace referencia al campo id_pagina de la tabla PAGINA.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA', N'COLUMN', N'id_pagina'
EXECUTE sp_addextendedproperty 'MS_Description', 'La ip es la dirección ip del usuario que interactúa y realiza acciones en una determinada tabla.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA', N'COLUMN', N'ip'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA', N'COLUMN', N'fecha_actualizacion'

create table BITACORA_DETALLE
(
    id_bitacora_detalle int not null primary key IDENTITY,
    id_bitacora int,
    tabla varchar(255),
    accion varchar(255),
    id_tupla int,
    campos varchar(MAX),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla BITACORA_DETALLE.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'id_bitacora_detalle'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_bitacora es un identificador que hace referencia al campo id_bitacora de la tabla BITACORA.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'id_bitacora'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es la tabla en la cual se está realizando una x acción por un determinado usuario.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'tabla'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es la acción la cual está realizando un determinado usuario a una tabla.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'accion'
EXECUTE sp_addextendedproperty 'MS_Description', 'La id_tupla es el identificador primary key del registro que se está modificando en una determinada tabla.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'id_tupla'
EXECUTE sp_addextendedproperty 'MS_Description', 'Campos almacena los campos(columnas) al cual están creando/modificando de un determinado registro (tupla) de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'campos'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'BITACORA_DETALLE', N'COLUMN', N'fecha_actualizacion'
go
