
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
    --insert into AREA (nombre,descripcion, habilitado, id_usuario) values('Unidad Tecnologica','Es el departamento de area de desarrolo e imnovacion',1,100);
go
create table PAIS
(
    id_pais int not null primary key IDENTITY,
    nombre varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla PAIS.', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'id_pais'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del pais. Ej. Bolivia, Brasil, E.E.U.U., etc.', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'PAIS', N'COLUMN', N'fecha_actualizacion'
go
 --insert into PAIS (nombre, id_usuario) values('BOLIVIA',100)
 --insert into PAIS (nombre, id_usuario) values('BRASIL',100)
go
create table CIUDAD
(
    id_ciudad int not null primary key IDENTITY,
    nombre varchar(255),
    id_pais int,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla CIUDAD.', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'id_ciudad'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_pais es un identificador que hace referencia al campo id_pais de la tabla PAIS.', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'id_pais'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'CIUDAD', N'COLUMN', N'fecha_actualizacion'
go
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('SANTA CRUZ',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('LA PAZ',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('COCHABANBA',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('TARIJA',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('BENI',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('PANDO',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('ORURO',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('POTOSI',1,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('SUCRE',1,100)

  --insert into CIUDAD (nombre, id_pais, id_usuario) values('sao paulo',2,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('rio de janeiro',2,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('pananbi',2,100)
  --insert into CIUDAD (nombre, id_pais, id_usuario) values('mato groso do sul',2,100)
  
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
--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Pagos','gestionPagoIcon','1',1,null,1); --padre
----insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Clientes','gestionClienteIcon','1',1,null,1);--padre
--  insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Configuraciones','config','3',1,null,1); --padre

--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Pago de comisiones','pagoComisionesIcon','1',1,1,1);--hijo
----insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Ficha de cliente','fichaClientIcon','1',1,2,1);--hijo
-- insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Gestión de roles','gestionRolesIcon','1',1,5,1);--hijo
-- INSERT INTO MODULO VALUES('Usuarios','gestionClienteIcon',2,1,5,1,GETDATE(),GETDATE());
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
--add modulo antes estos hacen referencia a los id de los modulos hijos
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Facturación','/facturacion','facIcon',1,1,3,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Carga Aplicaciones','/cargar-aplicaciones','facIcon',2,1,3,1);  
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Forma de pago','/forma/pago','facIcon',3,1,3,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Prorrateo','/prorrateo','facIcon',4,1,3,1);

  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Cliente','/clientes','facIcon',2,1,4,1);  

   --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Roles','/gestion/roles','RolIcon',1,1,6,1);  
   --INSERT INTO PAGINA VALUES('Asignación de roles','/usuario/asignar-roles','facIcon',1,1,@@Identity,1,GETDATE(),GETDATE());
  
  

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

go
create table CICLO
(
    id_ciclo int not null primary key IDENTITY,
    nombre varchar(255),
    descripcion varchar(255),
    fecha_inicio datetime,
    fecha_fin datetime,
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla CICLO.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'id_ciclo'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del ciclo. Ej.: Ciclo Marzo 2021, Ciclo Abril 2021, etc.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del ciclo.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es la fecha de inicio del ciclo.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_inicio'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es la fecha que culmina el ciclo.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_fin'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'CICLO', N'COLUMN', N'fecha_actualizacion'

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

create table GP_ESTADO_COMISION
(
    id_estado_comision int not null,
    estado varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla GP_ESTADO_COMISION.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'id_estado_comision'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del estado de comisión. Ej.: Para facturación, Para carga de datos, Para prorrateo, Para autorización, etc.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'estado'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del estado de comisión.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_ESTADO_COMISION', N'COLUMN', N'fecha_actualizacion'
go
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(1, 'PENDIENTE FACTURACION', 'PENDIENTE A FACTURAR',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(2, 'CERRADO FACTURACION', 'CERRADO FACTURACION',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(3, 'ANULADO FACTURACION', 'ANULADO FACTURACION',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(4, 'PENDIENTE APLICACION', 'PENDIENTE APLICACION',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(5, 'PROCESO APLICACION', 'PROCESO APLICACION',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(6, 'CARGADO COMISIONES FINALIZADO', 'CARGADO COMISIONES FINALIZADO',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(7, 'PENDIENTE PORRATERO', 'PENDIENTE PORRATERO',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(8, 'CERRADO PORRATEO', 'CERRADO PORRATEO',1)

--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(9, 'PENDIENTE FORMA DE PAGO', 'PENDIENTE FORMA DE PAGO',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(10, 'CERRADO FORMA DE PAGO', 'CERRADO FORMA DE PAGO',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(11, 'PENDIENTE AUTORIZACION', 'PENDIENTE AUTORIZACION',1)
--insert into BDMultinivel.dbo.GP_ESTADO_COMISION(id_estado_comision, estado,descripcion,id_usuario)values(12, 'AUTORIZADO', 'AUTORIZADO',1)

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

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla BANCO.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'id_banco'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del Banco. Ej.: BANCO NACIONAL DE BOLIVIA, BANCO MERCANTIL SANTA CRUZ, etc.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del Banco.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el código único que el banco proporciona.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'codigo'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'BANCO', N'COLUMN', N'fecha_actualizacion'
go
    --insert into BDMultinivel.dbo.BANCO (nombre,descripcion, codigo, id_usuario) values('BCP','es banco central de peru','12',1);  
go
create table FICHA
(
    id_ficha int not null primary key IDENTITY,
    codigo varchar(255)not null,
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
);

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
go
/*
    id_cliente hace referencia al id ficha del cliente
    id_vendedor hace referencia al id ficha del vendedor
*/
create table GP_CLIENTE_VENDEDOR_I(
    id int not null primary key IDENTITY(1,1),
    id_cliente int not null,
    id_vendedor int not null,
    fecha_activacion datetime,
    fecha_desactivacion datetime,
    activo bit,
    id_usuario int not null,
    fecha_creacion datetime,
    fecha_actualizacion datetime
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

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla TIPO_BAJA.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'id_tipo_baja'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del tipo de baja que pueda tener el Asesor. Ej.: Cesión de derecho, etc.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del tipo de baja.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_BAJA', N'COLUMN', N'fecha_actualizacion'
go
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja,nombre, descripcion, id_usuario) values(1,'sesion de derecho','es por ...', 1);
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja, nombre, descripcion, id_usuario) values(2,'sabotaje','es por ...', 1);
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja, nombre, descripcion, id_usuario) values(3,'incumplimiento','es por ...', 1);
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

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla FICHA_TIPO_BAJA_I.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_ficha_tipo_baja_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'Descripción del motivo de baja que pueda llegar a tener un Asesor.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'motivo'
EXECUTE sp_addextendedproperty 'MS_Description', 'Fecha en la cuál el Asesor fue dado de baja.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'fecha_baja'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_ficha'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_tipo_baja es un identificador que hace referencia al campo id_tipo_baja de la tabla TIPO_BAJA.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_tipo_baja'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_TIPO_BAJA_I', N'COLUMN', N'fecha_actualizacion'

create table NIVEL
(
    id_nivel int not null,
    nombre varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla NIVEL.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'id_nivel'
EXECUTE sp_addextendedproperty 'MS_Description', 'Nombre del nivel que pueda tener el Asesor. Ej.: Royal Intercontinetal, etc.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'nombre'
EXECUTE sp_addextendedproperty 'MS_Description', 'Muestra una descripción breve del tipo de baja.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'descripcion'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'NIVEL', N'COLUMN', N'fecha_actualizacion'
go
--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(1,'plata 1', 'es el prueba primer rango', 1);
--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(2,'oro 2', 'es el prueba segundo rango', 1);
--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(3,'platinun 3', 'es el prueba tercer rango', 1);
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

EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla FICHA_NIVEL_I.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_ficha_nivel_i'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_ficha es un identificador que hace referencia al campo id_ficha de la tabla FICHA.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_ficha'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_nivel es un identificador que hace referencia al campo id_nivel de la tabla NIVEL.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_nivel'
EXECUTE sp_addextendedproperty 'MS_Description', 'Si el registro actual está habilitado el valor será 1 (True), de lo contrario 0 (False).', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'habilitado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'FICHA_NIVEL_I', N'COLUMN', N'fecha_actualizacion'

create table GP_ESTADO_COMISION_DETALLE
(
    id_estado_comision_detalle int not null,
    estado varchar(255),
    descripcion varchar(255),
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);

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
	estado bit not null,
	respaldo_path varchar(500),
	nro_autorizacion varchar,
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
    id_usuario int,
    fecha_creacion datetime default GETDATE(),
    fecha_actualizacion datetime default GETDATE(),
);
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'id_comision_detalle_empresa'
	EXECUTE sp_addextendedproperty 'MS_Description', 'es el monto comision por empresa', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla activo (1) e inactico (0)', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'estado'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nro de autorizacion de la factura', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'nro_autorizacion'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El monto a facturar por empresa', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'monto_a_facturar'
	EXECUTE sp_addextendedproperty 'MS_Description', 'El monto total a facturar por empresa', 'SCHEMA', 'dbo', 'TABLE', 'COMISION_DETALLE_EMPRESA', N'COLUMN', N'monto_total_facturar'
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
CREATE TABLE ESTADO_DETALLE_LISTADO_FORMA_PAGO(
  id_estado_detalle_listado_forma_pago int NOT NULL PRIMARY KEY,
  descripcion varchar(500) NOT NULL,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'id_estado_detalle_listado_forma_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la breve descripcion del estado ejemplo: 1: para pagar, 2: error al pagar, 3 pago exitoso ', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'descripcion'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'ESTADO_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'fecha_actualizacion'
go
   --insert into ESTADO_DETALLE_LISTADO_FORMA_PAGO (id_estado_detalle_listado_forma_pago,descripcion,id_usuario) values(1, 'para pago',0)

go
CREATE TABLE TIPO_PAGO(
  id_tipo_pago int NOT NULL PRIMARY KEY,
  nombre varchar(100) not null,
  descripcion varchar(max),
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'id_tipo_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el nombre del tipo de pago ejemplo: 1 sion pay, 2 transferencia, 3 checque , 4 ninguno', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'nombre'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es la descripcion mas detallada del nombre de la tabla', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'descripcion'

    EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del ultimo usuario que modifico el registro.', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualizacion del registro', 'SCHEMA', 'dbo', 'TABLE', 'TIPO_PAGO', N'COLUMN', N'fecha_actualizacion'

go
   --insert into TIPO_PAGO (id_tipo_pago,nombre,descripcion,id_usuario) values(1, 'Sion pay','es una billetera movil',0)
   --insert into TIPO_PAGO (id_tipo_pago,nombre,descripcion,id_usuario) values(2, 'Transferencia','es una billetera movil',0)
   --insert into TIPO_PAGO (id_tipo_pago,nombre,descripcion,id_usuario) values(3, 'Cheque','es una billetera movil',0)
   --insert into TIPO_PAGO (id_tipo_pago,nombre,descripcion,id_usuario) values(4, 'Ninguno','es una billetera movil',0)
go
CREATE TABLE EMPRESA(
  id_empresa int NOT NULL PRIMARY KEY identity,
  codigo int not null,
  codigo_cnx int not null,
  nombre varchar(100) not null,
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

--insert into BDMultinivel.dbo.EMPRESA(codigo, nombre, estado,id_usuario)values(10,'ZURIEL',1, 1);
--insert into BDMultinivel.dbo.EMPRESA(codigo, nombre, estado,id_usuario)values(11,'AVDEL',1, 1);
--insert into BDMultinivel.dbo.EMPRESA(codigo, nombre, estado,id_usuario)values(12,'CCNORTE',1, 1);
--insert into BDMultinivel.dbo.EMPRESA(codigo, nombre, estado,id_usuario)values(13,'JAYIL',1, 1);

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


CREATE TABLE GP_DETALLE_LISTADO_FORMA_PAGO(
  id_gp_detalle_listado_forma_pago  int NOT NULL PRIMARY KEY identity,
  monto decimal(18, 2) NOT NULL,
  id_lista_formas_pago int NOT NULL,
  id_empresa int not null,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)
go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'id_gp_detalle_listado_forma_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el monto en especifico detallado de un listado de forma de pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'monto'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea d la tabla lista forma de pagos.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'id_lista_formas_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea que hace referencia a la tabla empresa.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'id_empresa'

	EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_LISTADO_FORMA_PAGO', N'COLUMN', N'fecha_actualizacion'

go
CREATE TABLE GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL(
  id int NOT NULL PRIMARY KEY identity,
  habilitado bit not null,
  id_gp_detalle_listado_forma_pago  int not null,
  id_estado_detalle_listado_forma_pago int NOT NULL,
  id_usuario int not null,
  fecha_creacion datetime default CURRENT_TIMESTAMP,
  fecha_actualizacion datetime default CURRENT_TIMESTAMP,
)

go
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es la llave primaria de tabla.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL', N'COLUMN', N'id'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado de la tabla expresado en boleando ejemplo: habilitado = true, inhabilitado = false', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL', N'COLUMN', N'habilitado'
	EXECUTE sp_addextendedproperty 'MS_Description', 'llave forare de la tabla detalle lista formad e pago.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL', N'COLUMN', N'id_gp_detalle_listado_forma_pago'
	EXECUTE sp_addextendedproperty 'MS_Description', 'llave foranea de la tabla estado del listado de forma de pagos', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL', N'COLUMN', N'id_estado_detalle_listado_forma_pago'

   	EXECUTE sp_addextendedproperty 'MS_Description', 'El id_usuario es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL', N'COLUMN', N'id_usuario'
	EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL', N'COLUMN', N'fecha_creacion'
    EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL', N'COLUMN', N'fecha_actualizacion'

go
--referencia empresa
CREATE TABLE PROYECTO(
  id_proyecto int NOT NULL PRIMARY KEY identity,
  nombre varchar(100) not null,
  id_empresa int not null,
  proyecto_conexion_id int not null,
  complejoid_guardian int not null default 0,
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

--insert into BDMultinivel.dbo.PROYECTO(nombre,id_empresa,proyecto_conexion_id, id_usuario)values('KALOMAI', 2,100,1);
--insert into BDMultinivel.dbo.PROYECTO(nombre,id_empresa,proyecto_conexion_id, id_usuario)values('LA FLORESTA DEL NORTE', 3,100,1);
--insert into BDMultinivel.dbo.PROYECTO(nombre,id_empresa,proyecto_conexion_id, id_usuario)values('COMPLEJO CAMPESTRE URUBO', 3,100,1);
--insert into BDMultinivel.dbo.PROYECTO(nombre,id_empresa,proyecto_conexion_id, id_usuario)values('LA ARBOLEA DEL ESTE', 3,100,1);

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
  insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(0,'sin definir', 'false', 1);
  insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(2,'Cuota', 'true', 1);
  insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(3,'A cuenta', 'true', 1);
  insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(4,'Expensas', 'true', 1);
  insert into BDMultinivel.dbo.TIPO_APLICACIONES( guardian_id_ciclo_descuento_tipo, descripcion, valido_guardian, id_usuario)values(5,'Otros', 'true', 1);

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
ALTER VIEW [dbo].[vwObtenercomisiones]
AS
     select 
	        GPDETA.id_comision_detalle AS 'idComisionDetalle',
	        GPCOMI.id_comision AS 'idComision', 
			GPDETA.id_ficha AS 'idFicha',
			FIC.nombres +' '+ FIC.apellidos as 'nombre',
			FIC.ci,
			FIC.cuenta_bancaria AS 'cuentaBancaria',
			FIC.id_banco,
			BA.nombre AS 'nombreBanco',
			GPDETA.monto_bruto AS 'montoBruto' ,
		    case FIC.factura_habilitado when 1 then 'True' when 0  then'False' else  'False' END AS 'factura',
			GPDETA.monto_neto AS 'montoNeto',
			CASE WHEN IDESTA.id_estado_comision_detalle IS NULL THEN 0 ELSE IDESTA.id_estado_comision_detalle END As 'estadoFacturoId',
			CASE WHEN ESTANA.estado IS NULL THEN 'No registro estado' ELSE ESTANA.estado END As 'estadoDetalleFacturaNombre',
			GPCOMI.id_ciclo,
			CI.nombre AS 'ciclo',
			GPESTA.id_estado_comision,
			GPDETA.monto_retencion,
			GPDETA.monto_aplicacion
	        from BDMultinivel.dbo.GP_COMISION GPCOMI
	        inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GPESTA  ON GPESTA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA ON GPDETA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha= GPDETA.id_ficha
			left join BDMultinivel.dbo.BANCO BA ON BA.id_banco = FIC.id_banco
			inner join BDMultinivel.dbo.CICLO CI ON CI.id_ciclo = GPCOMI.id_ciclo
			left join BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I IDESTA ON IDESTA.id_comision_detalle=GPDETA.id_comision_detalle
			left join BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE ESTANA ON ESTANA.id_estado_comision_detalle = IDESTA.id_estado_comision_detalle
			where IDESTA.habilitado = 'true' and GPESTA.habilitado= 'true'
GO

CREATE VIEW [dbo].[vwObtenerComisionesDetalleEmpresa]
AS
	 select 
	     ComiEmp.id_comision_detalle_empresa,
		 ComiEmp.id_comision_detalle,
	     Emp.nombre AS 'empresa',
		 ComiEmp.monto,
		 ComiEmp.monto_a_facturar,
		 ComiEmp.monto_total_facturar,
		 ComiEmp.respaldo_path,
		 ComiEmp.nro_autorizacion,
		 Emp.id_empresa AS 'idEmpresa',
		 ComiEmp.estado As 'estadoDetalleEmpresa',
		 ComiEmp.ventas_personales,
		 ComiEmp.ventas_grupales,
		 ComiEmp.residual,
		 ComiEmp.retencion,
		 ComiEmp.monto_neto,		 
		 ComiEmp.si_facturo
     from BDMultinivel.dbo.GP_COMISION_DETALLE GPDE
			inner join BDMultinivel.dbo.COMISION_DETALLE_EMPRESA ComiEmp on ComiEmp.id_comision_detalle=GPDE.id_comision_detalle
			inner join BDMultinivel.dbo.empresa Emp on Emp.id_empresa=ComiEmp.id_empresa
			
GO

CREATE VIEW [dbo].[vwObtenerCiclos]
AS
	SELECT
		C.id_ciclo,
		C.nombre,
		C.descripcion,
		E.id_estado_comision,
		E.estado
	FROM BDMultinivel.DBO.CICLO C
		INNER JOIN BDMultinivel.DBO.GP_COMISION CC ON C.id_ciclo = CC.id_ciclo
		INNER JOIN BDMultinivel.DBO.GP_TIPO_COMISION T ON CC.id_tipo_comision = T.id_tipo_comision
		INNER JOIN BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I CE ON CE.id_comision = CC.id_comision
		INNER JOIN BDMultinivel.DBO.GP_ESTADO_COMISION E ON E.id_estado_comision = CE.id_estado_comision
	WHERE CE.habilitado='true'
go
CREATE VIEW [dbo].[vwObtenerFicha]
AS
		 select 
		  F.id_ficha as 'idFicha',
		  F.codigo,
		  F.nombres + ' '+F.apellidos as 'nombreCompleto',
		  F.ci,
		  F.tiene_cuenta_bancaria as 'tieneCuentaBancaria',
		  CASE WHEN  F.id_banco IS NULL THEN 0 ELSE F.id_banco END As 'idBanco',	  
		  CASE WHEN  B.nombre IS NULL THEN '' ELSE B.nombre END As 'nombreBanco',
		  CASE WHEN   B.codigo IS NULL THEN '' ELSE  B.codigo END As 'codigoBanco',
		   CASE WHEN   F.cuenta_bancaria IS NULL THEN '' ELSE  F.cuenta_bancaria END As 'cuentaBancaria',
		  F.estado,
		  F.avatar,	
		 CASE WHEN  NI.nombre IS NULL THEN 'Asesor Comercial' ELSE NI.nombre END As 'nivel'
		 from BDMultinivel.dbo.FICHA F
		 left join BDMultinivel.dbo.BANCO B ON B.id_banco = F.id_banco
		 left join BDMultinivel.dbo.FICHA_NIVEL_I FNIV ON FNIV.id_ficha=F.id_ficha 
		 inner join BDMultinivel.dbo.NIVEL NI ON NI.id_nivel=FNIV.id_nivel

GO
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

create VIEW [dbo].[vwObtenerComisionesDetalleAplicaciones]
AS
	 select
	       Ap.id_aplicacion_detalle_producto,
		   GPDE.id_comision_detalle,
	       AP.descripcion,
		   AP.monto,
		   AP.cantidad,
		   AP.subtotal,
		   AP.id_proyecto,
		   CASE WHEN EMP.id_empresa IS NULL THEN 0 ELSE EMP.id_empresa END as 'id_empresa',		   
		   CASE WHEN EMP.nombre IS NULL THEN '-' ELSE EMP.nombre END as 'nombre_empresa',
		   CASE WHEN AP.codigo_producto='' THEN '-' ELSE AP.codigo_producto END as 'codigo_producto'	   
     from BDMultinivel.dbo.GP_COMISION_DETALLE GPDE
			inner join BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO AP on AP.id_comisiones_detalle=GPDE.id_comision_detalle
			left join BDMultinivel.dbo.PROYECTO PRO on PRO.id_proyecto = AP.id_proyecto
			left join BDMultinivel.dbo.PROYECTO EMP on EMP.id_proyecto = PRO.id_proyecto	
go

CREATE VIEW [dbo].[vwObtenerProyectoxProducto]
AS

   select  PRO.id_proyecto, PRO.nombre as 'nombreProyecto',PRO.id_empresa, GRLOTE.LOTE as 'producto', EMP.nombre as 'nombreEmpresa' from BDComisiones.dbo.vwLOTES_GRL_DOCID GRLOTE
                  inner join BDMultinivel.dbo.proyecto PRO on PRO.proyecto_conexion_id= GRLOTE.IDPROYECTO 
				  inner join BDMultinivel.dbo.EMPRESA EMP on EMP.id_empresa= PRO.id_empresa
 go