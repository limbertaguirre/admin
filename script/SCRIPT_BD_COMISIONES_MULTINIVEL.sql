
create database BDMultinivel;
go
use BDMultinivel;
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
	 -- insert BDMultinivel.dbo.PAIS select pa.IDPAIS, pa.DESCRIPCION, 1, GETDATE(), GETDATE() from BDComisiones.dbo.PEPAIS pa  
    
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
     -- insert BDMultinivel.dbo.ciudad select c.IDCIUDAD, c.DESCRIPCION, c.IDPAIS, 1, GETDATE(), GETDATE(),'' from BDComisiones.dbo.PECIUDAD c   where IDCIUDAD != 4090

		--update CIUDAD set codigo = 'SCZ' where id_ciudad = 1
		--update CIUDAD set codigo = 'LPZ' where id_ciudad = 2
		--update CIUDAD set codigo = 'CBB' where id_ciudad = 3
		--update CIUDAD set codigo = 'TJA' where id_ciudad = 4
		--update CIUDAD set codigo = 'POT' where id_ciudad = 5
		--update CIUDAD set codigo = 'ORU' where id_ciudad = 6
		--update CIUDAD set codigo = 'SUC' where id_ciudad = 7
		--update CIUDAD set codigo = 'TRI' where id_ciudad = 8
		--update CIUDAD set codigo = 'COB' where id_ciudad = 9
		--update CIUDAD set codigo = 'TJA' where id_ciudad = 10
		--update CIUDAD set codigo = 'LPZ' where id_ciudad = 11
		--update CIUDAD set codigo = 'SCZ' where id_ciudad = 4084
		--update CIUDAD set codigo = 'CBB' where id_ciudad = 4086
		--update CIUDAD set codigo = 'SCZ' where id_ciudad = 10000

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
   --insert into SUCURSAL (nombre,descripcion, habilitado, id_ciudad, id_usuario) values('AMBASADOR','Edificio ubicado en la av san martin 2do y 3re anillo',1,1,100);
   --insert into SUCURSAL (nombre,descripcion, habilitado, id_ciudad, id_usuario) values('CAÑOTO','Edificio ubicado av caÑoto primer anillo',1,1,100);
   --insert into SUCURSAL (nombre,descripcion, habilitado, id_ciudad, id_usuario) values('QUIJARRO','Primer anillo zona los pozos',1,1,100);
   
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
	 -- insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Pagos','gestionPagoIcon',1,1,null,1); --padre
	 -- insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Clientes','gestionClienteIcon',1,1,null,1);--padre
	 -- insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Configuraciones','config',3,1,null,1); --padre

	 --insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Pago de comisiones','pagoComisionesIcon',1,1,1,1);--hijo
	 --insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Ficha de cliente','fichaClientIcon',1,1,2,1);--hijo
	 --insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Gestión de roles','gestionRolesIcon',1,1,3,1);--hijo	 
	 --insert INTO MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) VALUES('Usuarios','gestionClienteIcon',2,1,3,1); --hijo

	 -- insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Reportes','config',2,1,null,1); --padre
	 --INSERT INTO MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) VALUES ('Pagos de comisiones','pagoComisionesIcon',1,1,8,1); --hijo reporte

	 --INSERT INTO MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) VALUES ('Pago de incentivos','pagoComisionesIcon',2,1,1,1); --hijo
  --   INSERT INTO MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) VALUES ('Pago de rezagados', 'pagoComisionesIcon', 3, 1, 1, 2); --hijo
	
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

	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Facturación','/facturacion','facIcon',1,1,4,1);
	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Carga Aplicaciones','/cargar-aplicaciones','facIcon',2,1,4,1);  
	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Prorrateo','/prorrateo','facIcon',3,1,4,1);
	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Forma de pago','/forma/pago','facIcon',4,1,4,1);
	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Pagos','/pagos-gestor','facIcon',5,1,4,1);

	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Cliente','/clientes','facIcon',2,1,5,1);  

	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Roles','/gestion/roles','RolIcon',1,1,6,1);  
	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Asignación de roles','/usuario/asignar-roless','facIcon',1,1,7,1);  
	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Por ciclo','/reporte/ciclos','facIcon',1,1,9,1);  
	 --  insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Por freelancer','/reporte/freelancer','facIcon',2,1,9,1);  

		--insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Carga de planilla','/pagos/incentivos/cargar-planilla','facIcon',1,1,10,1);
		--INSERT INTO PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Forma de pago rezagado', '/forma-pago/rezagados', 'facIcon', 1, 1, 11, 2);  
		--INSERT INTO PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Pagos Rezagados', '/pago/rezagados', 'facIcon', 2, 1, 11, 2);
		--INSERT INTO PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Pagos', '/pago/incentivos/pagar', 'facIcon', 2, 1, 10, 2);

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
create table GP_TIPO_COMISION
(
    id_tipo_comision int not null,
    nombre varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);
go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla GP_TIPO_COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_TIPO_COMISION', N'COLUMN', N'id_tipo_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del tipo de comisión. Ej.: Pago Comisiones, Rezagados.', 'SCHEMA', 'dbo', 'TABLE', 'GP_TIPO_COMISION', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del tipo de comisión.', 'SCHEMA', 'dbo', 'TABLE', 'GP_TIPO_COMISION', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_TIPO_COMISION', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_TIPO_COMISION', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_TIPO_COMISION', N'COLUMN', N'fecha_actualizacion'

go

--insert into BDMultinivel.dbo.GP_TIPO_COMISION(id_tipo_comision,nombre, descripcion,id_usuario) values(1,'PAGO COMISIONES', '',1)
--insert into BDMultinivel.dbo.GP_TIPO_COMISION(id_tipo_comision,nombre, descripcion,id_usuario) values(2,'PAGO REZAGADOS', '',1)
--insert into BDMultinivel.dbo.GP_TIPO_COMISION(id_tipo_comision,nombre, descripcion,id_usuario) values(3,'PAGO INCENTIVOS', '',1)

go
create table CICLO
(
    id_ciclo int not null primary key,
    nombre varchar(255),
    descripcion varchar(255),
    fecha_inicio datetime,
    fecha_fin datetime,
	estado bit,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla CICLO.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'id_ciclo'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del ciclo. Ej.: Ciclo Marzo 2021, Ciclo Abril 2021, etc.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del ciclo.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es la fecha de inicio del ciclo.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_inicio'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es la fecha que culmina el ciclo.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_fin'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_actualizacion'

go

create table GP_COMISION
(
    id_comision int not null primary key IDENTITY,
    monto_total_bruto decimal(18,2),
    porcentaje_retencion decimal(18,2),
    monto_total_retencion decimal(18,2),
    monto_total_aplicacion decimal(18,2),
    monto_total_neto decimal(18,2),
    id_ciclo int,
    id_tipo_comision int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'id_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto total bruto que comisionó el Asesor en el ciclo actual.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'monto_total_bruto'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el porcentaje de retención para impuestos que se realiza al monto total bruto.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'porcentaje_retencion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El monto total de retención es la sumatoria de todas las retenciones que se le hace al Asesor en el ciclo actual.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'monto_total_retencion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El monto total de aplicaciones es la sumatoria de todas las aplicaciones (descuentos) que se le hace al Asesor en el ciclo actual por el concepto de pago de cuotas de sus productos propios o multas que pueda tener.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'monto_total_aplicacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El monto total neto es la diferencia del monto total bruto, monto total de retención y monto total de aplicaciones.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'monto_total_neto'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_ciclo es un identificador que hace referencia al campo id_ciclo de la tabla CICLO.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'id_ciclo'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_tipo_comision es un identificador que hace referencia al campo id_tipo_comision de la tabla TIPO_COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'id_tipo_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION', N'COLUMN', N'fecha_actualizacion'

go

create table GP_ESTADO_COMISION
(
    id_estado_comision int not null,
    estado varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla GP_ESTADO_COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'id_estado_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del estado de comisión. Ej.: Para facturación, Para carga de datos, Para prorrateo, Para autorización, etc.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'estado'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del estado de comisión.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'fecha_actualizacion'
go
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(1, 'PENDIENTE FACTURACION', 'PENDIENTE A FACTURAR',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(2, 'CERRADO FACTURACION', 'CERRADO FACTURACION',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(3, 'ANULADO FACTURACION', 'ANULADO FACTURACION',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(4, 'PENDIENTE APLICACION', 'PENDIENTE APLICACION',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(5, 'PROCESO APLICACION', 'PROCESO APLICACION',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(6, 'CARGADO COMISIONES FINALIZADO', 'CARGADO COMISIONES FINALIZADO',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(7, 'PENDIENTE PORRATERO', 'PENDIENTE PORRATERO',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(8, 'CERRADO PORRATEO', 'CERRADO PORRATEO',1)

insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(9, 'PENDIENTE FORMA DE PAGO', 'Este estado sera listado para los pagos rezagados que volvera a al estado sin formas de pagos  a elegir los rezagados',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(10, 'CERRADO FORMA DE PAGO', 'CERRADO FORMA DE PAGO',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(11, 'PENDIENTE AUTORIZACION', 'PENDIENTE AUTORIZACION',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(12, 'AUTORIZADO', 'AUTORIZADO',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(13, 'PAGO DE COMISION CERRADO', 'PAGO DE COMISION CERRADO',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(14, 'INCENTIVO PENDIENTE', 'INCENTIVO PENDIENTE',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(15, 'INCENTIVO PAGO', 'INCENTIVO PAGO',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(16, 'FORMA PAGO DE COMISION REZAGADO CERRADO ', 'este estado sera listado desde la pagina de pago de rezagados',1)
insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(17, 'PAGO COMISION REZAGADO CERRADO', 'Es cuando el pago de comision rezagado halla sido cerrado y se les pagos por transferencia o sion pay',1)


go
create table GP_COMISION_ESTADO_COMISION_I
(
    id_comision_estado_comision_i int not null primary key IDENTITY,
    habilitado bit,
    id_comision int,
    id_estado_comision int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla GP_COMISION_ESTADO_COMISION_I.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_ESTADO_COMISION_I', N'COLUMN', N'id_comision_estado_comision_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_ESTADO_COMISION_I', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_comision es un identificador que hace referencia al campo id_comision de la tabla COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_ESTADO_COMISION_I', N'COLUMN',N'id_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_estado_comision es un identificador que hace referencia al campo id_estado_comision de la tabla ESTADO_COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_ESTADO_COMISION_I', N'COLUMN', N'id_estado_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_ESTADO_COMISION_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_ESTADO_COMISION_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_ESTADO_COMISION_I', N'COLUMN', N'fecha_actualizacion'

create table BANCO
(
    id_banco int not null primary key,
    nombre varchar(255),
    descripcion varchar(255),
    codigo varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla BANCO.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'id_banco'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del Banco. Ej.: BANCO NACIONAL DE BOLIVIA, BANCO MERCANTIL SANTA CRUZ, etc.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del Banco.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el código único que el banco proporciona.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'codigo'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'fecha_actualizacion'
go
    -- -- obtener bancos de bd comisiones
      --insert into BDMultinivel.dbo.BANCO values( 1,'BANCO NACIONAL DE BOLIVIA','',1001, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 2,'BANCO MERCANTIL SANTA CRUZ','',1003, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 3,'BANCO DE CRÉDITO DE BOLIVIA','',1005, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 4,'BANCO DO BRASIL','',1008, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 5,'BANCO BISA','',1009, 1,GETDATE(), GETDATE() )

	  --insert into BDMultinivel.dbo.BANCO values( 6,'BANCO UNION','',1014, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 7,'BANCO ECONOMICO','',1016, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 8,'BANCO SOLIDARIO','',1017, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 9,'BANCO FIE','',1033, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 10,'BANCO FORTALEZA','',1034, 1,GETDATE(), GETDATE() )

	  --insert into BDMultinivel.dbo.BANCO values( 11,'BANCO FASSIL','',1035, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 12,'BANCO PRODEM S.A','',1036, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 13,'COOPERATIVA JESUS NAZARENO','',3001, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 14,'E-EFECTIVO S.A','',53001, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 15,'BANCO PYME ECOFUTURO S.A.A.','',74002, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 16,'BANCO PYME DE LA COMUNIDAD','',74003, 1,GETDATE(), GETDATE() )
	  --insert into BDMultinivel.dbo.BANCO values( 17,'BANCO GANADERO','',0, 1,GETDATE(), GETDATE() )
go
create table FICHA
(
    id_ficha int not null primary key IDENTITY,
    codigo int not null,
	codigo_cnx varchar(255)not null,
    nombres varchar(255)not null,
    apellidos varchar(255)not null,
    ci varchar(255) not null,
    correo_electronico varchar(255),
    fecha_registro date,
    tel_oficina varchar(255),
    tel_movil varchar(255),
    tel_fijo varchar(255),
    direccion varchar(255),
    fecha_nacimiento date,
    contrasena varchar(255),
    comentario varchar(max),
    avatar varchar(max),
    tiene_cuenta_bancaria bit not null,
    id_banco int not null,
    cuenta_bancaria varchar(255),
    factura_habilitado bit not null,
    razon_social varchar(255),
    nit varchar(255),
	id_ciudad int not null,
	estado int not null,
    id_usuario int not null,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
	id_tipo_pago int NOT NULL default 0,
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla FICHA.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'id_ficha'
EXECUTE sp_addextendedproperty 'MS_Description', 'El código es un identificador que identifica al Asesor y que es generado y asignado por la empresa.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'codigo'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre completo del asesor', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'nombres'
EXECUTE sp_addextendedproperty 'MS_Description', 'Apellido completo del asesor', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'apellidos'
EXECUTE sp_addextendedproperty 'MS_Description', 'El carnet de identidad del asesor', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'ci'
EXECUTE sp_addextendedproperty 'MS_Description', 'El correo electrónico del asesor', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'correo_electronico'
EXECUTE sp_addextendedproperty 'MS_Description', 'La fecha que la empresa registró al asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'fecha_registro'
EXECUTE sp_addextendedproperty 'MS_Description', 'El teléfono de oficina del asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'tel_oficina'
EXECUTE sp_addextendedproperty 'MS_Description', 'El teléfono móvil del asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'tel_movil'
EXECUTE sp_addextendedproperty 'MS_Description', 'El teléfono fijo del asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'tel_fijo'
EXECUTE sp_addextendedproperty 'MS_Description', 'La dirección del asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'direccion'
EXECUTE sp_addextendedproperty 'MS_Description', 'La fecha de nacimiento del asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'fecha_nacimiento'
EXECUTE sp_addextendedproperty 'MS_Description', 'La contraseña que el sistema de comisiones le asigna al asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'contrasena'
EXECUTE sp_addextendedproperty 'MS_Description', 'Un breve comentario sobre el asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'comentario'
EXECUTE sp_addextendedproperty 'MS_Description', 'El avatar es el path de ubicación de la foto del asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'avatar'
EXECUTE sp_addextendedproperty 'MS_Description', 'Valor de bit, si es 1 el asesor tiene cuenta bancaria, de lo contrario si es 0 no tiene cuenta bancaria.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'tiene_cuenta_bancaria'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_banco es un identificador que hace referencia al campo id_banco de la tabla BANCO.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'id_banco'
EXECUTE sp_addextendedproperty 'MS_Description', 'Cuenta bancaria del asesor, si no tiene cuenta el valor es null.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'cuenta_bancaria'
EXECUTE sp_addextendedproperty 'MS_Description', 'Valor de bit, si es 1 el asesor factura, de lo contrario si es 0 el asesor no factura.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'factura_habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'Razón social del Asesor, si no tiene el valor es null.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'razon_social'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nit del Asesor, si no tiene el valor es null.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'nit'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla 1 es activo , 0 es inactivo.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'estado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'fecha_actualizacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'id tipo pago el FreeLancer si tiene cuenta o cuenta podra tener habilitado o seleccianado un tipo de pago', 'SCHEMA', 'dbo', 'TABLE', 'FICHA', N'COLUMN', N'id_tipo_pago'
go

create table GP_CLIENTE_VENDEDOR_I(
    id int not null IDENTITY(1,1),
    id_cliente int not null,
    id_vendedor int not null,
    fecha_activacion datetime,
    fecha_desactivacion datetime,
    activo bit,
    id_usuario int not null,
    fecha_creacion datetime,
    fecha_actualizacion datetime,
	primary key(id,id_cliente)
)

go
create table TIPO_BAJA
(
    id_tipo_baja int not null,
    nombre varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla TIPO_BAJA.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'id_tipo_baja'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del tipo de baja que pueda tener el Asesor. Ej.: Cesión de derecho, etc.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del tipo de baja.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'fecha_actualizacion'
go
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja,nombre, descripcion, id_usuario) values(1,'sesion de derecho','', 1);
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja, nombre, descripcion, id_usuario) values(2,'Suspensión temporal','', 1);
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja, nombre, descripcion, id_usuario) values(3,'Expulsión definitiva','', 1);
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja, nombre, descripcion, id_usuario) values(4,'Otro caso','', 1);

go
create table FICHA_TIPO_BAJA_I
(
    id_ficha_tipo_baja_i int not null primary key IDENTITY,
    motivo varchar(max),
    fecha_baja datetime,
    id_ficha int,
    id_tipo_baja int,
	estado bit default 1,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla FICHA_TIPO_BAJA_I.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_ficha_tipo_baja_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'Descripción del motivo de baja que pueda llegar a tener un Asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'motivo'
EXECUTE sp_addextendedproperty 'MS_Description', 'Fecha en la cuál el Asesor fue dado de baja.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'fecha_baja'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_ficha'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_tipo_baja es un identificador que hace referencia al campo id_tipo_baja de la tabla TIPO_BAJA.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_tipo_baja'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'fecha_actualizacion'

go

create table NIVEL
(
    id_nivel int not null,
    nombre varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla NIVEL.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'id_nivel'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del nivel que pueda tener el Asesor. Ej.: Royal Intercontinetal, etc.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del tipo de baja.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'fecha_actualizacion'
go
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(0,'Freelance', '', 1);	
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(1,'Asesor Comercial', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(2,'LEADER', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(3,'SENIOR', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(4,'SAPPHIRE', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(5,'RUBY', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(6,'EMERALD', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(7,'DIAMOND', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(8,'REGIONAL AMBASSADOR', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(9,'NATIONAL AMBASSADOR', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(10,'INTERNATIONAL AMBASSADOR', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(11,'INTERCONTINENTAL AMBASSADOR', '', 1);
	--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(12,'AMBASSADOR ROYAL', '', 1);

go

create table FICHA_NIVEL_I
(
    id_ficha_nivel_i int not null primary key IDENTITY,
    id_ficha int,
    id_nivel int,
    habilitado bit,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla FICHA_NIVEL_I.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_ficha_nivel_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_ficha'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_nivel es un identificador que hace referencia al campo id_nivel de la tabla NIVEL.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_nivel'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'fecha_actualizacion'

go

create table GP_ESTADO_COMISION_DETALLE
(
    id_estado_comision_detalle int not null,
    estado varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla NIVEL.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION_DETALLE', N'COLUMN', N'id_estado_comision_detalle'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del estado que pueda tener la comisión detalle. Ej.: Para forma de pago, Para autorizar, etc.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION_DETALLE', N'COLUMN', N'estado'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del estado de la COMISION_DETALLE.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION_DETALLE', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION_DETALLE', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION_DETALLE', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION_DETALLE', N'COLUMN', N'fecha_actualizacion'

go

	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(1, 'No facturo','no facturo la comision', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(2, 'Si facturo','estado facturado', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(3, 'Para forma de pago','estado  forma de pago', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(4, 'Para autorizar','previo para autorizar', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(5, 'Resagado','cuando no presenta factura o no tiene una forma de pago', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(6, 'No presenta factura','el freelancer comisiona pero no presenta factura', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(7, 'Incentivo pendiente','El pago de incentivo del FreeLancer se encuentra pendiente', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(8, 'Incentivo pagado','El pago de incentivo del FreeLancer se encuentra pagado', 1 )

go
create table GP_COMISION_DETALLE
(
    id_comision_detalle int not null primary key IDENTITY,
    monto_bruto decimal(18,2),
    porcentaje_retencion decimal(18,2),
    monto_retencion decimal(18,2),
    monto_aplicacion decimal(18,2),
    monto_neto decimal(18,2),
    id_comision int,
    id_ficha int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

go

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla GP_COMISION_DETALLE.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'id_comision_detalle'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto bruto que comisionó el Asesor en el detalle del ciclo actual.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'monto_bruto'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el porcentaje de retención para impuestos que se realiza al monto bruto.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'porcentaje_retencion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El monto de retención es la sumatoria de todas las retenciones que se le hace al Asesor en el detalle del ciclo actual.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN',N'monto_retencion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El monto de aplicaciones es la sumatoria de todas las aplicaciones (descuentos) que se le hace al Asesor en el detalle del ciclo actual por el concepto de pago de cuotas de sus productos propios o multas que pueda tener.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'monto_aplicacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El monto neto es la diferencia del monto bruto, monto de retención y monto de aplicaciones.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'monto_neto'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_comision es un identificador que hace referencia al campo id_comision de la tabla COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'id_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'id_ficha'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE', N'COLUMN', N'fecha_actualizacion'

go

create table GP_COMISION_DETALLE_ESTADO_I
(
    id_comision_detalle_estado_i int not null primary key IDENTITY,
    id_comision_detalle int,
    id_estado_comision_detalle int,
    habilitado bit,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);
go
EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla GP_COMISION_DETALLE_ESTADO_I.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE_ESTADO_I', N'COLUMN', N'id_comision_detalle_estado_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_comision_detalle es un identificador que hace referencia al campo id_comision_detalle de la tabla COMISION_DETALLE.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE_ESTADO_I',N'COLUMN', N'id_comision_detalle'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_estado_comision_detalle es un identificador que hace referencia al campo id_estado_comision_detalle de la tabla GP_ESTADO_COMISION_DETALLE.', 'SCHEMA', 'dbo', 'TABLE','GP_COMISION_DETALLE_ESTADO_I', N'COLUMN', N'id_estado_comision_detalle'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE_ESTADO_I', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE_ESTADO_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE_ESTADO_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_COMISION_DETALLE_ESTADO_I', N'COLUMN', N'fecha_actualizacion'
go

create table COMISION_DETALLE_EMPRESA
(
    id_comision_detalle_empresa int not null primary key IDENTITY,
    monto decimal(18,2) not null,
	estado TINYINT,
	respaldo_path varchar(500),
	nro_autorizacion varchar(100),
	monto_a_facturar decimal(18,2),
	monto_total_facturar decimal(18,2),
	id_comision_detalle int not null,
	id_empresa int not null,
	ventas_personales decimal(18,2) default 0 not null,
	ventas_grupales decimal(18,2) default 0 not null,
	residual decimal(18,2) default 0 not null,
	retencion decimal(18,2) default 0 not null,
	monto_neto decimal(18,2) default 0 not null,
	si_facturo bit default 0 not null,	
	id_comprobante_generico bigint,
	id_movimiento int NOT NULL default 0,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
	fecha_pago datetime,
);
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'id_comision_detalle_empresa'
	EXECUTE sp_addextendedproperty 'MS_Description', 'es el monto comision por empresa', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado que pueda tener la tupla (1 Pendiente, 2 Confirmado, 3 Rechazado/Anulado)', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'estado'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nro de autorizacion de la factura', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'nro_autorizacion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El monto a facturar por empresa', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'monto_a_facturar'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El monto total a facturar por empresa', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'monto_total_facturar'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El id_comprobante_generico. puede ser idmovimiento o el idcomprobante de pago', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'id_comprobante_generico'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'fecha_creacion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'fecha_actualizacion'

go
--individuales
CREATE TABLE GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO(
  id_gp_estado_prorrateo_detalle_incentivo int NOT NULL PRIMARY KEY,
  descripcion varchar(500) NOT NULL,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO', N'COLUMN', N'id_gp_estado_prorrateo_detalle_incentivo'	
	EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripcion breve del estado porrateo detalle incentivo ejemplo: estado pendiente 1, estado procesado 2.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO', N'COLUMN', N'descripcion'
	
	EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO', N'COLUMN', N'fecha_actualizacion'
go
   --insert into GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO (id_gp_estado_prorrateo_detalle_incentivo,descripcion,id_usuario) values(1, 'Pendiente',0)
   --insert into GP_ESTADO_PRORRATEO_DETALLE_INCENTIVO (id_gp_estado_prorrateo_detalle_incentivo,descripcion,id_usuario) values(2, 'Procesado',0)
go
CREATE TABLE TIPO_INCENTIVO(
  id_tipo_incentivo int NOT NULL PRIMARY KEY,
  nombre varchar(50) not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO', N'COLUMN', N'id_tipo_incentivo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'nombre es el tipo de incentivo en especie  o en dinero', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO', N'COLUMN', N'nombre'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO', N'COLUMN', N'fecha_actualizacion'
go
   --insert into TIPO_INCENTIVO (id_tipo_incentivo,nombre,id_usuario) values(1, 'Especie',0)
   --insert into TIPO_INCENTIVO (id_tipo_incentivo,nombre,id_usuario) values(2, 'monetario',0)
go
CREATE TABLE GP_ESTADO_PRORRATEO_DETALLE(
  id_gp_estado_prorrateo_detalle int NOT NULL PRIMARY KEY,
  descripcion varchar(500) NOT NULL,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE', N'COLUMN', N'id_gp_estado_prorrateo_detalle'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la breve descripcion del estado de proratero detalle ', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE', N'COLUMN', N'descripcion'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_PRORRATEO_DETALLE', N'COLUMN', N'fecha_actualizacion'

go
CREATE TABLE ESTADO_LISTADO_FORMA_PAGO( --ESTADO_DETALLE_LISTADO_FORMA_PAGO
  id_estado_listado_forma_pago int NOT NULL PRIMARY KEY,
  descripcion varchar(500) NOT NULL,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_LISTADO_FORMA_PAGO', N'COLUMN', N'id_estado_listado_forma_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la breve descripcion del estado ejemplo: 1: para pagar, 2: error al pagar, 3 pago exitoso ', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_LISTADO_FORMA_PAGO', N'COLUMN', N'descripcion'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_LISTADO_FORMA_PAGO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_LISTADO_FORMA_PAGO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_LISTADO_FORMA_PAGO', N'COLUMN', N'fecha_actualizacion'
go
   --   insert into ESTADO_LISTADO_FORMA_PAGO (id_estado_listado_forma_pago, descripcion,id_usuario) values(1, 'Para pagar',1)
   --   insert into ESTADO_LISTADO_FORMA_PAGO (id_estado_listado_forma_pago, descripcion,id_usuario) values(2, 'Error al pagar',1)
   --   insert into ESTADO_LISTADO_FORMA_PAGO (id_estado_listado_forma_pago, descripcion,id_usuario) values(3, 'Pago exitoso',1)
	  --insert into ESTADO_LISTADO_FORMA_PAGO (id_estado_listado_forma_pago, descripcion,id_usuario) values(4, 'Rechazado en Pagos por Transferencias',1)
go
CREATE TABLE TIPO_PAGO(
  id_tipo_pago int NOT NULL PRIMARY KEY,
  nombre varchar(100) not null,
  descripcion varchar(max),
  id_usuario int not null,
  estado bit NOT NULL default 1,--true
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
  icono varchar(50) NOT NULL,
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'id_tipo_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nombre del tipo de pago ejemplo: 1 sion pay, 2 transferencia, 3 checque , 4 ninguno', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'nombre'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion mas detallada del nombre de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'descripcion'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'fecha_actualizacion'

go
   --insert into TIPO_PAGO (id_tipo_pago,nombre,descripcion,estado,id_usuario,icono) values(1, 'Sion pay','es una billetera movil',1,0,'sionpay')
   --insert into TIPO_PAGO (id_tipo_pago,nombre,descripcion,estado,id_usuario,icono) values(2, 'Transferencia','es una billetera movil',1,0,'transfer')
   
go
CREATE TABLE EMPRESA(
  id_empresa int NOT NULL PRIMARY KEY identity,
  codigo int not null,
  codigo_cnx int not null,
  nombre varchar(100) not null,
  nombre_bd varchar(100) not null,
  estado int not null, --1 o cero 
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria y id de la empresa local ', 'SCHEMA', 'dbo', 'TABLE', 'EMPRESA', N'COLUMN', N'id_empresa'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo de empresa externo de guardian de gruposion', 'SCHEMA', 'dbo', 'TABLE', 'EMPRESA', N'COLUMN', N'codigo'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nombre de la empresa e inmoboliaria', 'SCHEMA', 'dbo', 'TABLE', 'EMPRESA', N'COLUMN', N'nombre'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Estado de la empresa activo o inactivo 1 true , 0  false', 'SCHEMA', 'dbo', 'TABLE', 'EMPRESA', N'COLUMN', N'estado'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'EMPRESA', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'EMPRESA', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'EMPRESA', N'COLUMN', N'fecha_actualizacion'

go
--------------------------------
--------------------------------
--referencia niveles
CREATE TABLE INCENTIVO(
  id_incentivo int NOT NULL PRIMARY KEY identity,
  codigo varchar(100)not null,
  descripcion varchar(max),
  monto decimal(18, 2) NOT NULL,
  estado int not null,
  id_niveles int not null,
  id_tipo_incentivo int not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primeria de la tala y auto incremental.', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'id_incentivo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo del incentivo por especie que identifica al incentivo.', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'codigo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion detallada de un incentivo.', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'descripcion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el precio del incentivo ya se si es un especie o dinero', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'es el estado de del incentivo 1 activo o  0 inactivo.', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'estado'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este hace referencia a un id niveles de rango ganado o puede ser ninguno, un incentivo puede tener o no un rango de nivel ganado, .', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'id_niveles'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este es el tipo de incentivo ganado ya sea producto en especie o monetario', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'id_tipo_incentivo'
	

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO', N'COLUMN', N'fecha_actualizacion'

go
CREATE TABLE  FICHA_INCENTIVO(
  id_ficha_incentivo int NOT NULL PRIMARY KEY identity,
  monto decimal(18, 2) NOT NULL,
  id_ficha int not null,
  id_incentivo int NOT NULL,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
   
    EXECUTE sp_addextendedproperty 'MS_Description', 'Este es la llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_INCENTIVO', N'COLUMN', N'id_ficha_incentivo'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Este es el monto ganado para el frilanzer.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_INCENTIVO', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este el el id de la ficha del frilanzer', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_INCENTIVO', N'COLUMN', N'id_ficha'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este es la llave del incentio que hace referencia a la tabla incentivo.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_INCENTIVO', N'COLUMN', N'id_incentivo'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_INCENTIVO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_INCENTIVO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_INCENTIVO', N'COLUMN', N'fecha_actualizacion'

go
 CREATE TABLE GP_PORRATERO_DETALLE_INCENTIVO(
  id_gp_porratero_detalle_incentivo int NOT NULL PRIMARY KEY identity,
  monto decimal(18, 2) NOT NULL,
  estado int not null,
  id_empresa_asume int not null,
  id_ficha_incentivo int not null,
  id_gp_estado_prorrateo_detalle_incentivo int,
  recibo_id int not null,
  comprobante_id int not null,
  gestion varchar(50) not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
 )
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este es la llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'id_gp_porratero_detalle_incentivo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto que la empresa a asumir para darle al frilanzer.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla  activo  y  inactivo.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'estado'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea que hace referencia a la empresa que asume el pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'id_empresa_asume'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea hace referencia al incentivo de la ficha del frelanzers.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'id_ficha_incentivo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea del estados de la tabla ejemplo 1 pendente, 2 procesado.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'id_gp_estado_prorrateo_detalle_incentivo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este es el id del recibo por el incentivo.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'recibo_id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este es el codigo del comprobanto.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'comprobante_id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el anho  o codigo anhoo con alguna nomesclatura.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'gestion'



    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_PORRATERO_DETALLE_INCENTIVO', N'COLUMN', N'fecha_actualizacion'

go
--referencia a ficha
CREATE TABLE VENTA(
  id_venta int NOT NULL PRIMARY KEY identity,
  codigo_producto varchar(100) not null,
  id_ficha int not null,
  monto_total decimal(18, 2) NOT NULL,
  fecha_venta datetime default CURRENT_TIMESTAMP,
  porcentaje_descuento decimal(18, 2) NOT NULL,
  descuento decimal(18, 2) NOT NULL,
  monto_neto decimal(18, 2) NOT NULL,
  porcentaje_cuota_inicial decimal(18, 2) NOT NULL,
  monto_cuota_inicial decimal(18, 2) NOT NULL,
  cliente_id int not  null,
  referido_id int not null,
  complejo_id int not null,
  es_comisionable bit not null,
  manzano varchar(30)not null,
  lote varchar(30)not null,
  estado int not null,
  venta_conexion_id int not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,  
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave priamria e incremental de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'id_venta'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo del producto .', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'codigo_producto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es una llave foranea que hace referencia a la ficha del frilanzer.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'id_ficha'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Ese el monto total de la venta realizada.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'monto_total'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la fecha de la venta realizada.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'fecha_venta'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el porcentaje de descuento de  la venta.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'porcentaje_descuento'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el descuento que se aplico sobre la venta.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'descuento'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto con los descuentos incluidos.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'monto_neto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el porcentaje de la cuota inicial de una venta.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'porcentaje_cuota_inicial'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto de la cuota inicial expresado en dolares.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'monto_cuota_inicial'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea de la ficha del frelanzer.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'cliente_id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el id de accesor que le vendio un producto.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'referido_id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el proyecto id de conexion.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'complejo_id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es campo booleando si una venta es comsionable.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'es_comisionable'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El codigo de manzano del producto.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'manzano'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El numero de lote del producto.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'lote'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla activo en inactivo.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'estado'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este es el codigo de la venta en conexion.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'venta_conexion_id'
	

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'VENTA', N'COLUMN', N'fecha_actualizacion'

go
--referencia comision_detalle
CREATE TABLE LISTADO_FORMAS_PAGO(
  id_lista_formas_pago int NOT NULL PRIMARY KEY identity,
  monto_neto decimal(18, 2) NOT NULL,
  id_tipo_pago int not null,
  id_comisiones_detalle int not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP, 
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el id primaria de la tabla y autoincremental.', 'SCHEMA', 'dbo', 'TABLE', 'LISTADO_FORMAS_PAGO', N'COLUMN', N'id_lista_formas_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto neto con el descuento inclido de una forma de producto.', 'SCHEMA', 'dbo', 'TABLE', 'LISTADO_FORMAS_PAGO', N'COLUMN', N'monto_neto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea del tipo pago, 1 sion pay, 2 transferencia, 3 cheche.', 'SCHEMA', 'dbo', 'TABLE', 'LISTADO_FORMAS_PAGO', N'COLUMN', N'id_tipo_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave de referencia de comision de detalle del cliente .', 'SCHEMA', 'dbo', 'TABLE', 'LISTADO_FORMAS_PAGO', N'COLUMN', N'id_comisiones_detalle'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'LISTADO_FORMAS_PAGO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LISTADO_FORMAS_PAGO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LISTADO_FORMAS_PAGO', N'COLUMN', N'fecha_actualizacion'

go
--referencia empresa
go
CREATE TABLE GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL( --GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL
  id int NOT NULL PRIMARY KEY identity,
  habilitado bit not null,
  id_lista_formas_pago  int not null,
  id_estado_listado_forma_pago int NOT NULL,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)

go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de tabla.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL', N'COLUMN', N'id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla expresado en boleando ejemplo: habilitado = true, inhabilitado = false', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL', N'COLUMN', N'habilitado'
	EXECUTE sp_addextendedproperty 'MS_Description', 'llave forare de la tabla detalle lista formad e pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL', N'COLUMN', N'id_lista_formas_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranea de la tabla estado del listado de forma de pagos', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL', N'COLUMN', N'id_estado_listado_forma_pago'

   	EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL', N'COLUMN', N'fecha_actualizacion'

go
--referencia empresa
CREATE TABLE PROYECTO(
  id_proyecto int NOT NULL PRIMARY KEY identity,
  nombre varchar(100) not null,
  id_empresa int not null,
  proyecto_conexion_id int not null,
  complejoid_guardian int not null default 0,
  estado bit not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'PROYECTO', N'COLUMN', N'id_proyecto'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nombre del proyecto o producto.', 'SCHEMA', 'dbo', 'TABLE', 'PROYECTO', N'COLUMN', N'nombre'
    EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranea que  hace referencia a la empresa .', 'SCHEMA', 'dbo', 'TABLE', 'PROYECTO', N'COLUMN', N'id_empresa'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo de proyecto en conexion.', 'SCHEMA', 'dbo', 'TABLE', 'PROYECTO', N'COLUMN', N'proyecto_conexion_id'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'PROYECTO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'PROYECTO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'PROYECTO', N'COLUMN', N'fecha_actualizacion'
go


CREATE TABLE TIPO_APLICACIONES(
  id_tipo_aplicaciones int NOT NULL PRIMARY KEY identity,
  guardian_id_ciclo_descuento_tipo int not null default 0,
  descripcion varchar(60),
  valido_guardian bit not null default 'false',
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el id de la tabla es auto incremental.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_APLICACIONES', N'COLUMN', N'id_tipo_aplicaciones'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo tipo que hace referencia al ID de la tabla administraciondescuentociclotipo que esta en el guardian base de datos : grdsion, dato por defecto cero si no pertenece al guardian. ', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_APLICACIONES', N'COLUMN', N'guardian_id_ciclo_descuento_tipo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nombre o descripcion del tipo  de descuento', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_APLICACIONES', N'COLUMN', N'descripcion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Este campo hace referencia si la descripcion es valido solo para guardian = true, false si es diferente a lo de guardian', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_APLICACIONES', N'COLUMN', N'valido_guardian'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_APLICACIONES', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_APLICACIONES', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_APLICACIONES', N'COLUMN', N'fecha_actualizacion'
go
  --insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(0,'sin definir', 'false', 1);
  --insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(2,'Cuota', 'true', 1);
  --insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(3,'A cuenta', 'true', 1);
  --insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(4,'Expensas', 'true', 1);
  --insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(5,'Otros', 'true', 1);

go
CREATE TABLE APLICACION_DETALLE_PRODUCTO(
  id_aplicacion_detalle_producto int NOT NULL PRIMARY KEY identity,
  cantidad int not null,
  monto decimal(18, 2) NOT NULL,
  descripcion varchar(max),
  subtotal decimal(18, 2) NOT NULL,
  id_proyecto int not null,
  codigo_producto varchar(50) not null,
  id_comisiones_detalle int not null,
  id_bdqishur int NOT NULL,
  id_tipo_aplicaciones int NOT NULL default 1,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla autoincremental.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'id_aplicacion_detalle_producto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'son la cantidad de  cuotas pagadas.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'cantidad'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto de la cuotas pagar total.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion de la aplicacion de una aplicacion por producto.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'descripcion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es monto subtotal de una aplicacion por producto.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'subtotal'
	EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranea que hace referencia al codigo de un proyecto de grupo sion.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'id_proyecto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo de un producto lote o kalomai  general de gruposion.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'codigo_producto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranead de la tabla comision detalle donde se tiene toda la comision del cliente frilanzer.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'id_comisiones_detalle'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El id_bdqishur es el ide de la tabla que hace referencia al id primario de la tabla AplicacionesPagos de la bd bdqishur', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'id_bdqishur'
	EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranea de la tabla tipo aplicaciones, dato por defaul 1 sin definir o sin tipo. se agregar un tipo x cuando sea un descuento nuevo', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'id_aplicacion_detalle_producto'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'APLICACION_DETALLE_PRODUCTO', N'COLUMN', N'fecha_actualizacion'

go
CREATE TABLE GP_PRORRATEO_DETALLE(
  id_gp_porrateo_detalle int NOT NULL PRIMARY KEY identity,
  monto decimal(18, 2) NOT NULL,
  descripcion varchar(max),
  id_gp_estado_prorrateo_detalle int NOT NULL,
  id_empresa_presta int NOT NULL,
  id_empresa_recibe int NOT NULL,
  id_aplicacion_detalle_producto int NOT NULL,
  recibo_id int NOT NULL,
  comprobante_id int NOT NULL,
  gestion varchar(50) not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP, 
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla y auto imncremental.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'id_gp_porrateo_detalle'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto que la empresa va a prestar.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion detallada de un prorateo por empresa, empresas que prestaran para el .', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'descripcion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla activo 1 e inactivo 0.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'id_gp_estado_prorrateo_detalle'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea de la empresa que realiza el prestamo.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'id_empresa_presta'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea de la empresa que recibe el pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'id_empresa_recibe'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea de aplicacion de producto detalle.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'id_aplicacion_detalle_producto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo de recibo del pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'recibo_id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo del comprobante del pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'comprobante_id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'la gestion es el codigo y anho que se ejecuto el pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'gestion'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_PRORRATEO_DETALLE', N'COLUMN', N'fecha_actualizacion'

go

CREATE TABLE LOG_DETALLE_COMISION_EMPRESA_FAIL(
  id_detalle_comisio_empresa_fail int NOT NULL PRIMARY KEY identity,
  id_ciclo int not null,
  id_ficha int not null,
  codigo_cliente int not null,
  total_monto_Bruto decimal(18, 2) NOT NULL,
  descripcion varchar(max),
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla autoincremental.', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'id_detalle_comisio_empresa_fail'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El idciclo es la llave foranea de comision ciclo', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'id_ciclo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el id ficha de la tabla comisiones', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'id_ficha'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el codigo del contacto del guardian, es el cliente', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'codigo_cliente'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto total bruto de la comision de freelancer', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'total_monto_Bruto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'descripcion'   
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LOG_DETALLE_COMISION_EMPRESA_FAIL', N'COLUMN', N'fecha_actualizacion'
go
 CREATE TABLE LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL(
  id_sion_pay_comisio_empresa_fail int NOT NULL PRIMARY KEY identity,
  id_ciclo int not null,
  id_ficha int not null, 
  carnet varchar(100) not null, 
  cuenta_sion_pay varchar(100) not null, 
  id_detalle_comision int not null,
  id_detalle_comision_empresa int default 0,
  monto decimal(18,2) default 0,
  descripcion varchar(max),
  id_empresa_cnx INT NOT NULL default 0,
  nombre_empresa varchar(100) NOT NULL default '',
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla autoincremental.', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'id_sion_pay_comisio_empresa_fail'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El idciclo es la llave foranea de comision ciclo', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'id_ciclo'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el id ficha de la tabla comisiones', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'id_ficha'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es carnet de identidad del freelancers', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'carnet'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nro de cuenta en sion pay del freelancer', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'cuenta_sion_pay'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el id de la tabla detalle comision se registrara em caso de no existir su detalle por empresa', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'id_detalle_comision'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el id de la tabla detalle_comision_empresa que se registrara en caso de haya un pago con monto cero por default cero', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'id_detalle_comision_empresa'
	EXECUTE sp_addextendedproperty 'MS_Description', 'es el monto de la transaccion datos por default cero', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'monto'   
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'descripcion'   

	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL', N'COLUMN', N'fecha_actualizacion'

go








CREATE TABLE TIPO_AUTORIZACION(
  id_tipo_autorizacion int NOT NULL PRIMARY KEY,
  nombre varchar(50) not null,
  cantidad int not null,
  estado bit not null default 1,--true
  id_usuario_modificacion int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla tipo autorizacion', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_AUTORIZACION', N'COLUMN', N'id_tipo_autorizacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el breve nombre o descripcion del tipo de autorizacion ', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_AUTORIZACION', N'COLUMN', N'nombre'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es es la cantidad de usuarios que podran autorizar por tipo de autorizacion.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_AUTORIZACION', N'COLUMN', N'cantidad'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla booleano true o false', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_AUTORIZACION', N'COLUMN', N'estado'

	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_AUTORIZACION', N'COLUMN', N'id_usuario_modificacion'   
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_AUTORIZACION', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_AUTORIZACION', N'COLUMN', N'fecha_actualizacion'
go
    insert into BDMultinivel.dbo.TIPO_AUTORIZACION(id_tipo_autorizacion,nombre,cantidad, id_usuario_modificacion)values(1,'SION PAY',3,1);
	insert into BDMultinivel.dbo.TIPO_AUTORIZACION(id_tipo_autorizacion,nombre,cantidad, id_usuario_modificacion)values(2,'TRANSFERENCIA',3,1);
	insert into BDMultinivel.dbo.TIPO_AUTORIZACION(id_tipo_autorizacion,nombre,cantidad, id_usuario_modificacion)values(3,'FORMA DE PAGO',3,1);
    insert into BDMultinivel.dbo.TIPO_AUTORIZACION(id_tipo_autorizacion,nombre,cantidad, id_usuario_modificacion)values(4,'FORMA DE PAGO REZAGADOS', 1, 1);
go
CREATE TABLE AUTORIZACIONES_AREA(
  id_autorizaciones_area int NOT NULL PRIMARY KEY identity,
  id_area int not null,
  id_tipo_autorizacion int NOT NULL,
  cantidad int not null,
  id_usuario_modificacion int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es es la llave primaria de la tabla autoincremental.', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACIONES_AREA', N'COLUMN', N'id_autorizaciones_area'  
	EXECUTE sp_addextendedproperty 'MS_Description', 'LLave foranea que hace referencia al id de la tabla area en de trabajo.', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACIONES_AREA', N'COLUMN', N'id_area'  
	EXECUTE sp_addextendedproperty 'MS_Description', 'LLave foranea que hace referencia al id de la tabla tipo autorizacion', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACIONES_AREA', N'COLUMN', N'id_tipo_autorizacion'  
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la cantidad minima o igual que se puede tener para una autorizacion por tipo.', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACIONES_AREA', N'COLUMN', N'cantidad'  

	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion del registro', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACIONES_AREA', N'COLUMN', N'id_usuario_modificacion'   
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACIONES_AREA', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACIONES_AREA', N'COLUMN', N'fecha_actualizacion'
go
      ----INSERT DE PRUEBA...  BORRAR
      --insert into BDMultinivel.dbo.AUTORIZACIONES_AREA(id_area,id_tipo_autorizacion,cantidad,id_usuario_modificacion)values(1,3,1,1);

go
CREATE TABLE USUARIO_AUTORIZACION(
  id_usuario_autorizacion int NOT NULL PRIMARY KEY identity,
  id_usuario int not null,
  id_tipo_autorizacion int NOT NULL,
  estado bit not null default 1,--true
  id_usuario_modificacion int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave generica de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO_AUTORIZACION', N'COLUMN', N'id_usuario_autorizacion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el usuario que se le asigno a un tipo de autorizacion', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO_AUTORIZACION', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave foranea de la tabla tipo de auntorizacin', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO_AUTORIZACION', N'COLUMN', N'id_tipo_autorizacion'

	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO_AUTORIZACION', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'USUARIO_AUTORIZACION', N'COLUMN', N'fecha_actualizacion'
GO
CREATE TABLE ESTADO_AUTORIZACION_COMISION(
  id_estado_autorizacion_comision int NOT NULL,
  nombre varchar(50) not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'es la llave primaria de la tablas', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_AUTORIZACION_COMISION', N'COLUMN', N'id_estado_autorizacion_comision'
	EXECUTE sp_addextendedproperty 'MS_Description', 'es el estado de una comision autorizada', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_AUTORIZACION_COMISION', N'COLUMN', N'nombre'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el i usuario que creo o modifico el registro', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_AUTORIZACION_COMISION', N'COLUMN', N'id_usuario'

    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_AUTORIZACION_COMISION', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_AUTORIZACION_COMISION', N'COLUMN', N'fecha_actualizacion'
go
   --insert into BDMultinivel.dbo.ESTADO_AUTORIZACION_COMISION(id_estado_autorizacion_comision, nombre, id_usuario)values(0,'Aprobado', 1);
   --insert into BDMultinivel.dbo.ESTADO_AUTORIZACION_COMISION(id_estado_autorizacion_comision, nombre, id_usuario)values(1,'Rechazado', 1);
go

CREATE TABLE AUTORIZACION_COMISION(
  id_autorizacion_comision int NOT NULL PRIMARY KEY identity,
  id_comision int not null,
  id_usuario_autorizacion int NOT NULL,
  id_estado_autorizacion_comision int not null,
  descripcion varchar(max) not null,
  id_usuario_modificacion int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'es la llave primaria de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACION_COMISION', N'COLUMN', N'id_autorizacion_comision'
	EXECUTE sp_addextendedproperty 'MS_Description', 'es la llave foranea de la comision', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACION_COMISION', N'COLUMN', N'id_comision'
	EXECUTE sp_addextendedproperty 'MS_Description', 'es la llave foranea de la tabla id autorizacion usuario.', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACION_COMISION', N'COLUMN', N'id_usuario_autorizacion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el i usuario que creo o modifico el registro', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACION_COMISION', N'COLUMN', N'id_usuario_modificacion'

    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACION_COMISION', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'AUTORIZACION_COMISION', N'COLUMN', N'fecha_actualizacion'
go
/*
    Inserts quemados manual
*/
    -- INSERT INTO BDMultinivel.DBO.AUTORIZACION_COMISION (id_comision, id_usuario_autorizacion, id_estado_autorizacion_comision, descripcion, id_usuario_modificacion) VALUES (1163, 4, 0, '', 1)
    -- INSERT INTO BDMultinivel.DBO.AUTORIZACION_COMISION (id_comision, id_usuario_autorizacion, id_estado_autorizacion_comision, descripcion, id_usuario_modificacion) VALUES (1163, 5, 0, '', 1)
GO
        create table ASIGNACION_EMPRESA_PAGO
        (
            id_asignacion_empresa_pago int not null primary key IDENTITY,
            id_usuario int,
            id_empresa int,
            id_tipo_pago int,
            descripcion VARCHAR(250),
            usuario_id int,
            fecha_creacion datetime default GETDATE(),
            fecha_actualizacion datetime default GETDATE(),
        );

        EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla ASIGNACION_EMPRESA_PAGO.', 'SCHEMA', 'dbo', 'TABLE', 'ASIGNACION_EMPRESA_PAGO', N'COLUMN', N'id_asignacion_empresa_pago'
        EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea a la tabla USUARIO.', 'SCHEMA', 'dbo', 'TABLE', 'ASIGNACION_EMPRESA_PAGO', N'COLUMN', N'id_usuario'
        EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea a la tabla EMPRESA.', 'SCHEMA', 'dbo', 'TABLE', 'ASIGNACION_EMPRESA_PAGO', N'COLUMN', N'id_empresa'
        EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea a la tabla TIPO DE PAGO.', 'SCHEMA', 'dbo', 'TABLE', 'ASIGNACION_EMPRESA_PAGO', N'COLUMN', N'id_tipo_pago'

        EXECUTE sp_addextendedproperty 'MS_Description', 'El usuario_id es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'ASIGNACION_EMPRESA_PAGO', N'COLUMN', N'id_usuario'
        EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'ASIGNACION_EMPRESA_PAGO', N'COLUMN', N'fecha_creacion'
        EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'ASIGNACION_EMPRESA_PAGO', N'COLUMN', N'fecha_actualizacion'
        go

		GO
		CREATE TABLE [dbo].[CONTROL_USUARIO](
			id_control_usuario int primary key IDENTITY(1,1) NOT NULL,
			usuario varchar (255) NULL,
			cantidad_intentos int NULL,
			fecha_bloquedo datetime NULL,
			fecha_desbloqueo datetime NULL,
			net_session_id varchar(255) NULL,
			estado int NOT  NULL,
			)
		EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'CONTROL_USUARIO', N'COLUMN', N'id_control_usuario'
		EXECUTE sp_addextendedproperty 'MS_Description', 'Usuario que intenta iniciar session (Dominio).', 'SCHEMA', 'dbo', 'TABLE', 'CONTROL_USUARIO', N'COLUMN', N'usuario'
		EXECUTE sp_addextendedproperty 'MS_Description', 'Cantidad de intentos que el usuario fallo al iniciar session', 'SCHEMA', 'dbo', 'TABLE', 'CONTROL_USUARIO', N'COLUMN', N'cantidad_intentos'
		EXECUTE sp_addextendedproperty 'MS_Description', 'Fecha de bloqueo del usuario', 'SCHEMA', 'dbo', 'TABLE', 'CONTROL_USUARIO', N'COLUMN', N'fecha_bloquedo'
		EXECUTE sp_addextendedproperty 'MS_Description', 'Fecha de desbloqueo del usuario.', 'SCHEMA', 'dbo', 'TABLE', 'CONTROL_USUARIO', N'COLUMN', N'fecha_desbloqueo'
		EXECUTE sp_addextendedproperty 'MS_Description', 'Id del aps.net core.', 'SCHEMA', 'dbo', 'TABLE', 'CONTROL_USUARIO', N'COLUMN', N'net_session_id'
		EXECUTE sp_addextendedproperty 'MS_Description', 'Estado del usuario.', 'SCHEMA', 'dbo', 'TABLE', 'CONTROL_USUARIO', N'COLUMN', N'estado'
		go
      

-----------------------------------------------------------------------------------INICIO TABLA INCENTIVOS---------------------------------------------------------------
create TABLE BDMultinivel.dbo.TIPO_INCENTIVO_PAGO(
  id_tipo_incentivo int identity(1,1) not null primary key,
  descripcion varchar(300) not null,
  estado varchar(30) 
)


EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO_PAGO', N'COLUMN', N'id_tipo_incentivo'
EXECUTE sp_addextendedproperty 'MS_Description', 'indica el nombre o descripcion del tipo Incentivo Pago', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO_PAGO', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'indica el estado del tipoIncentivo ejm: INACTIVO, ACTIVO', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_INCENTIVO_PAGO', N'COLUMN', N'estado'

CREATE TABLE BDMultinivel.dbo.INCENTIVO_PAGO_COMISION(
    id_detalle int identity (1,1) not null primary key,
    id_tipo_incentivo_pago int not null,
    id_comision_detalle int not null
)


EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO_PAGO_COMISION', N'COLUMN', N'id_detalle'
EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranea hacia la tabla TIPO_INCENTIVO_PAGO', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO_PAGO_COMISION', N'COLUMN', N'id_tipo_incentivo_pago'
EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranea hacia la tabla GP_COMISION_DETALLE', 'SCHEMA', 'dbo', 'TABLE', 'INCENTIVO_PAGO_COMISION', N'COLUMN', N'id_comision_detalle'

GO

insert into BDMultinivel.dbo.TIPO_INCENTIVO_PAGO (
   descripcion
  ,estado
) VALUES (
   'INCENTIVO VENTA MEMBRESIA'  -- descripcion - varchar(300)
  ,'ACTIVO' -- estado - varchar(30)
)


insert into BDMultinivel.dbo.TIPO_INCENTIVO_PAGO (
   descripcion
  ,estado
) VALUES (
   'INCENTIVO VENTA LOTES'  -- descripcion - varchar(300)
  ,'ACTIVO' -- estado - varchar(30)
)
GO
---


-----------------------------------------------------------------------FIN TABLA INCENTIVOS--------------------------------------------------------------------
      
	---------------------------------------------------------------------------------------------------------------------------------------------
	 -- -- CREAR ROL MANUAL ------------------------------------------------------------------------------------------------------------------------
			--insert into BDMultinivel.dbo.ROL( nombre, descripcion, habilitado,id_usuario,fecha_creacion, fecha_actualizacion)
			--values( 'ADMINISTRADOR', 'Usuario que tiene acceso total', 1, 1,GETDATE(), GETDATE())

		  ----RELACIONAR ROL CON PAGINA-----------------------------------------------------------------------------------------------------------
				--insert into BDMultinivel.dbo.ROL_PAGINA_I(habilitado,id_rol, id_pagina, id_usuario, fecha_creacion, fecha_actualizacion)
				--values(1,1,7,1,GETDATE(),GETDATE())
		  --INSERTAR PERMISO VISUALIZAR-----------------------------------------------------
				--insert into BDMultinivel.dbo.ROL_PAGINA_PERMISO_I(habilitado,id_rol_pagina, id_permiso,id_usuario, fecha_creacion,fecha_actualizacion)
				--values(1, 1, 1, 1, GETDATE(), GETDATE())
		  ---- INSERTAR PERMISO CREAR----------------------------------------------------------
				--insert into BDMultinivel.dbo.ROL_PAGINA_PERMISO_I(habilitado,id_rol_pagina, id_permiso,id_usuario, fecha_creacion,fecha_actualizacion)
				--values(1, 1, 2, 1, GETDATE(), GETDATE())
		  --INSERTAR PERMISO ACTUALIZAR------------------------------------------------------
				--insert into BDMultinivel.dbo.ROL_PAGINA_PERMISO_I(habilitado,id_rol_pagina, id_permiso,id_usuario, fecha_creacion,fecha_actualizacion)
				--values(1, 1, 3, 1, GETDATE(), GETDATE())
		  --INSERTAR PERMISO ELIMINAR--------------------------------------------------------
				--insert into BDMultinivel.dbo.ROL_PAGINA_PERMISO_I(habilitado,id_rol_pagina, id_permiso,id_usuario, fecha_creacion,fecha_actualizacion)
				--values(1, 1, 4, 1, GETDATE(), GETDATE())

		 --ASIGNAR ROL A USUARIO
	
			 --insert into BDMultinivel.dbo.USUARIOS_ROLES(id_usuario, id_rol,estado, usuario_id, fecha_creacion, fecha_actualizacion)
			 --values(1, --id_usuario
				--	1, --id_rol
				--	1,-- estado
				--	1,--usuario_id
				--	GETDATE(), 
				--GETDATE()
				--); 
	---------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------------------SE AÑADEN AGENTES EN BDPUNTOSCASH LOS AGENTES DE PAGOS INCENTIVOS, REZAGADOS------------------------------------
insert into BDPuntosCash.dbo.AGENTE VALUES (27, 'Pago de Comisiones por Gestor' ,'PAGO_COMISIONES_GESTOR' , 'c974ba133f12287a0168e272e90e7e34')
insert into BDPuntosCash.dbo.AGENTE VALUES (28, 'Pago de Incentivos por Gestor' ,'PAGO_INCENTIVOS_GESTOR' , 'c974ba133f12287a0168e272e90e7e34')
   