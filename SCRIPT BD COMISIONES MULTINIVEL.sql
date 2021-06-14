
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

create table USURIOS_ROLES
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
EXECUTE sp_addextendedproperty 'MS_Description', 'Llave primaria incremental de la tabla.', 'SCHEMA', 'dbo', 'TABLE', 'USURIOS_ROLES', N'COLUMN', N'id_usuarios_roles'
EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea que hace referencia a la tabla usuario', 'SCHEMA', 'dbo', 'TABLE', 'USURIOS_ROLES', N'COLUMN', N'id_usuario'
EXECUTE sp_addextendedproperty 'MS_Description', 'Llave foranea que hace relacion con la tabla Rol', 'SCHEMA', 'dbo', 'TABLE', 'USURIOS_ROLES', N'COLUMN', N'id_rol'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el estado del requistro booleano true o false', 'SCHEMA', 'dbo', 'TABLE', 'USURIOS_ROLES', N'COLUMN', N'estado'
EXECUTE sp_addextendedproperty 'MS_Description', 'El usuario_id es el id del último usuario que modificó el registro.', 'SCHEMA', 'dbo', 'TABLE', 'USURIOS_ROLES', N'COLUMN', N'usuario_id'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de creación del registro', 'SCHEMA', 'dbo', 'TABLE', 'USURIOS_ROLES', N'COLUMN', N'fecha_creacion'
EXECUTE sp_addextendedproperty 'MS_Description', 'Es el timestamp de actualización del registro', 'SCHEMA', 'dbo', 'TABLE', 'USURIOS_ROLES', N'COLUMN', N'fecha_actualizacion'

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
--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Gestión de Pagos','gestioIcon','1',1,null,1); --padre
--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Gestión de Clientes','gestioIcon','1',1,null,1);--padre

--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Pago de comisiones','gestioIcon','1',1,1,1);--hijo
--insert into MODULO (nombre, icono, orden, habilitado, id_modulo_padre, id_usuario) values('Ficha de cliente','gestioIcon','1',1,2,1);--hijo

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
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('facturacion','/facturacion','facIcon',1,1,3,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Cargar comisiones','/cargar/comisiones','facIcon',1,1,3,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Porrateo','/porrateo','facIcon',3,1,3,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Forma de pago','/forma/pago','facIcon',3,1,3,1);

  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('Cliente','/cliente','facIcon',2,1,4,1);  
  
  

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
    id int not null IDENTITY(1,1),
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
CREATE TABLE APLICACION_DETALLE_PRODUCTO(
  id_aplicacion_detalle_producto int NOT NULL PRIMARY KEY identity,
  cantidad int not null,
  monto decimal(18, 2) NOT NULL,
  descripcion varchar(max),
  subtotal decimal(18, 2) NOT NULL,
  id_proyecto int not null,
  codigo_producto varchar(50) not null,
  id_comisiones_detalle int not null,
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
