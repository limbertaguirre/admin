--------
--guardia usuario para el linkse
--montesion
--
select * from BDMultinivel.dbo.FICHA ORDER BY id_ficha DESC-- where id_ficha=4024  90844
select * from BDMultinivel.dbo.FICHA  where codigo= 90844
SELECT * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit.comision_forma_pago_view ')    where ciclo_id=80  AND contacto_id= 181
SELECT * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.comision_empresa_forma_pago_view ') where  ciclo_id=82  AND contacto_id= 181

select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where  id_empresa=0

SELECT  *   
FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.administracionciclo ')
 
 SELECT * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.administracionciclo')
 	SELECT *  FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.comision_empresa_forma_pago_view ') 


select * from BDMultinivel.dbo.ciclo
select * from BDMultinivel.dbo.rol
--delete  from BDMultinivel.dbo.rol where id_rol in(1,2,3,4,5,6,7,8,9,10,11,12)
select * from BDMultinivel.dbo.MODULO
select * from BDMultinivel.dbo.PERMISO
select * from BDMultinivel.dbo.PAGINA
select * from BDMultinivel.dbo.ROL_PAGINA_I
select * from BDMultinivel.dbo.ROL_PAGINA_PERMISO_I

select * from BDMultinivel.dbo.EMPRESA
select * from BDMultinivel.dbo.ESTADO_LISTADO_FORMA_PAGO

--obtener rol permiso en especifico
select * from BDMultinivel.dbo.ROL_PAGINA_I RPI, BDMultinivel.dbo.ROL_PAGINA_PERMISO_I RPPI
   where RPI.id_rol=15 and RPI.id_pagina=11
    and RPI.id_rol_pagina_i=RPPI.id_rol_pagina 
	and RPPI.id_permiso=10
	----------------------------------------

	select RPPI.id_rol_pagina_permiso_i,RPPI.habilitado, RPPI.id_rol_pagina, RPPI.id_permiso, RPPI.id_usuario from BDMultinivel.dbo.ROL_PAGINA_I RPI, BDMultinivel.dbo.ROL_PAGINA_PERMISO_I RPPI
   where RPI.id_rol=14 --and RPI.id_pagina=15 
    and RPI.id_rol_pagina_i=RPPI.id_rol_pagina 
	and RPPI.id_permiso=10

-----------------------
--id_rol_pagina : 24, permiso 12
---------------------------------
--rol: 14	idpagina:16   -rolidpermi 24 permi 10
--rol: 14   idpagina: 11
--------------------------------------------
--------------------------------------------------------------------------------------------------------------
select * from BDMultinivel.dbo.FICHA
select * from BDMultinivel.dbo.BANCO
select * from BDMultinivel.dbo.FICHA_INCENTIVO
select * from BDMultinivel.dbo.FICHA_NIVEL_I
select * from BDMultinivel.dbo.FICHA_TIPO_BAJA_I  

select * from  BDMultinivel.dbo.USUARIO
--insert into BDMultinivel.dbo.BANCO (nombre,descripcion, codigo, id_usuario) values('BCP','es banco central de peru','12',1);  

--ALTER TABLE BDMultinivel.dbo.FICHA

--ALTER TABLE BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO
--ADD id_tipo_aplicaciones int NOT NULL default 1;

--ALTER TABLE BDMultinivel.dbo.PROYECTO
--ADD complejoid_guardian int NOT NULL default 0;

--ALTER TABLE BDMultinivel.dbo.USUARIO
--ADD id_tipo_pago int NOT NULL default 0;


select * from BDMultinivel.dbo.tipo_baja
select * from BDMultinivel.dbo.FICHA_TIPO_BAJA_I
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja,nombre, descripcion, id_usuario) values('sesion de derecho','es por ...', 1);
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja, nombre, descripcion, id_usuario) values('sabotaje','es por ...', 1);
--insert into BDMultinivel.dbo.tipo_baja(id_tipo_baja, nombre, descripcion, id_usuario) values('incumplimiento','es por ...', 1);

select * from BDMultinivel.dbo.NIVEL
--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(1,'plata 1', 'es el primer rango', 1);
--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(2,'oro 2', 'es el segundo rango', 1);
--insert into  BDMultinivel.dbo.NIVEL(id_nivel ,nombre,descripcion, id_usuario) values(3,'platinun 3', 'es el tercer rango', 1);

select * from BDMultinivel.dbo.FICHA_NIVEL_I
select * from BDMultinivel.dbo.USUARIO

select * from BDMultinivel.dbo.FICHA_TIPO_BAJA_I
--ALTER TABLE BDMultinivel.dbo.FICHA_TIPO_BAJA_I
--  ADD estado bit default 1;

select * from BDMultinivel.dbo.FICHA where ci='3791764'
 ALTER TABLE BDMultinivel.dbo.ficha
ADD id_tipo_pago int NOT NULL default 0;

select * from BDMultinivel.dbo.GP_CLIENTE_VENDEDOR_I

select * from BDMultinivel.dbo.ciclo
insert into BDMultinivel.dbo.ciclo(nombre, descripcion, fecha_inicio, fecha_fin, id_usuario) values('enero 2020', 'en este ciclo hubo un','01-01/2020','30-01-2020',100)
select * from BDMultinivel.dbo.GP_COMISION
select * from BDMultinivel.dbo.GP_TIPO_COMISION

--insert into BDMultinivel.dbo.GP_TIPO_COMISION(id_tipo_comision,nombre, descripcion,id_usuario) values(1,'PAGO COMISIONES', '',1)
--insert into BDMultinivel.dbo.GP_TIPO_COMISION(id_tipo_comision,nombre, descripcion,id_usuario) values(2,'PAGO REZAGADOS', '',1)

select * from BDMultinivel.dbo.GP_COMISION
select * from BDMultinivel.dbo.GP_ESTADO_COMISION
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

select * from BDMultinivel.dbo.FICHA
select * from BDMultinivel.dbo.BANCO

select * from BDMultinivel.dbo.CICLO

select * from BDMultinivel.dbo.GP_COMISION --1015, 1016
select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision=2017
select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA
select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE

select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
select * from BDMultinivel.dbo.GP_ESTADO_COMISION
--- 
-----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
--empresa 
select * from BDMultinivel.dbo.EMPRESA
-- proyectoss...
select * from BDMultinivel.dbo.proyecto

select * from BDMultinivel.dbo.GP_COMISION_DETALLE
select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA
go
--------------------------------
 ---ejmplo detalle user 1 empresa 2,3,4,5
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(50,1,'','',50,50,1,2,1);
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(50,1,'','',50,50,1,3,1);
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(50,1,'','',50,50,1,4,1);
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(50,1,'','',50,50,1,5,1);
---------------------------
---ejemplo 2: detalle id:2
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(75,1,'','',75,75,2,2,1);
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(75,1,'','',75,75,2,3,1);
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(75,1,'','',75,75,2,4,1);
		--insert into BDMultinivel.dbo.COMISION_DETALLE_EMPRESA (monto, estado,respaldo_path,nro_autorizacion,monto_a_facturar, monto_total_facturar, id_comision_detalle, id_empresa, id_usuario)
		--	  values(75,1,'','',75,75,2,5,1);


------------------------
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
		    case FIC.tiene_cuenta_bancaria when 1 then 'True' when 0  then'False' else  'False' END AS 'factura',
			GPDETA.monto_neto AS 'montoNeto',
			'False' As 'facturaDescuento',
			GPCOMI.id_ciclo,
			CI.nombre AS 'ciclo',
			GPESTA.id_estado_comision
	        from BDMultinivel.dbo.GP_COMISION GPCOMI
	        inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GPESTA  ON GPESTA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA ON GPDETA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha= GPDETA.id_ficha
			left join BDMultinivel.dbo.BANCO BA ON BA.id_banco = FIC.id_banco
			inner join BDMultinivel.dbo.CICLO CI ON CI.id_ciclo = GPCOMI.id_ciclo

-----------------------------------------
----------------------------------------
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
		 ComiEmp.estado As 'estadoDetalleEmpresa'
     from BDMultinivel.dbo.GP_COMISION_DETALLE GPDE
			inner join BDMultinivel.dbo.COMISION_DETALLE_EMPRESA ComiEmp on ComiEmp.id_comision_detalle=GPDE.id_comision_detalle
			inner join BDMultinivel.dbo.empresa Emp on Emp.id_empresa=ComiEmp.id_empresa

			----------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------
-- aplicacion por empresa y ciclo
select * from BDQISHUR.dbo.AplicacionesComisionPorEmpresa a
inner join BDComisiones.dbo.CNX_BDCOMISIONES c on c.IDBD = a.Empresa
where a.Ciclo = 81 and 
a.Carnet = '6079319'
--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------

--ventas personales
--vetnas grupales
--recidual
--retencion
--monto neto
-- si factura


select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA

--ALTER TABLE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA  ADD ventas_personales decimal(18,2) default 0 not null;
--ALTER TABLE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA  ADD ventas_grupales decimal(18,2) default 0 not null;
--ALTER TABLE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA  ADD residual decimal(18,2) default 0 not null;
--ALTER TABLE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA  ADD retencion decimal(18,2) default 0 not null;
--ALTER TABLE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA  ADD monto_neto decimal(18,2) default 0 not null;
--ALTER TABLE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA  ADD si_facturo bit default false not null;

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



	---------------------------------


	select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE

	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(1, 'No facturo','no facturo la comision', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(2, 'Si facturo','estado facturado', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(3, 'Para forma de pago','estado  forma de pago', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(4, 'Para autorizar','previo para autorizar', 1 )
	--insert into BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE (id_estado_comision_detalle, estado, descripcion, id_usuario) values(5, 'Resagado','cuando no presenta factura o no tiene una forma de pago', 1 )





   ----------------------------------------------------
   --agregar estado la tabla comision detal 

		
			select * from BDMultinivel.dbo.GP_COMISION_DETALLE
			select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
			--insertando estado para el detalle comsion
			--insert into BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I (id_comision_detalle,id_estado_comision_detalle, habilitado, id_usuario) values(1, 1,1,1) --detalle 1, estado 1 pendiente, habilitador 1, idusuario 1..
			--insert into BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I (id_comision_detalle,id_estado_comision_detalle, habilitado, id_usuario) values(2, 1,1,1) --detalle 1, estado 1 pendiente, habilitador 1, idusuario 1..


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
		    case FIC.tiene_cuenta_bancaria when 1 then 'True' when 0  then'False' else  'False' END AS 'factura',
			GPDETA.monto_neto AS 'montoNeto',
			CASE WHEN IDESTA.id_estado_comision_detalle IS NULL THEN 0 ELSE IDESTA.id_estado_comision_detalle END As 'facturaDescuentoId',
			CASE WHEN ESTANA.estado IS NULL THEN 'No registro estado' ELSE ESTANA.estado END As 'estadoDetalleFacturaNombre',
			GPCOMI.id_ciclo,
			CI.nombre AS 'ciclo',
			GPESTA.id_estado_comision
	        from BDMultinivel.dbo.GP_COMISION GPCOMI
	        inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GPESTA  ON GPESTA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA ON GPDETA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha= GPDETA.id_ficha
			left join BDMultinivel.dbo.BANCO BA ON BA.id_banco = FIC.id_banco
			inner join BDMultinivel.dbo.CICLO CI ON CI.id_ciclo = GPCOMI.id_ciclo
			left join BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I IDESTA ON IDESTA.id_comision_detalle=GPDETA.id_comision_detalle
			left join BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE ESTANA ON ESTANA.id_estado_comision_detalle = IDESTA.id_estado_comision_detalle


----------------------------------------------
 
    select * from  BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I 	
	select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where estado=1 and id_comision_detalle= 2

	select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
    select * from BDMultinivel.dbo.GP_COMISION_DETALLE	


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
			Baja.estado AS 'estadoFicha'
	        from BDMultinivel.dbo.GP_COMISION GPCOMI
	        inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GPESTA  ON GPESTA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA ON GPDETA.id_comision = GPCOMI.id_comision
			inner join BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha= GPDETA.id_ficha
			left join BDMultinivel.dbo.BANCO BA ON BA.id_banco = FIC.id_banco
			inner join BDMultinivel.dbo.CICLO CI ON CI.id_ciclo = GPCOMI.id_ciclo
			left join BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I IDESTA ON IDESTA.id_comision_detalle=GPDETA.id_comision_detalle
			left join BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE ESTANA ON ESTANA.id_estado_comision_detalle = IDESTA.id_estado_comision_detalle

			left join BDMultinivel.dbo.FICHA_TIPO_BAJA_I Baja ON Baja.id_ficha = FIC.id_ficha
			where  Baja.estado= 1


select top(100) * from BDMultinivel.dbo.FICHA


select * from BDMultinivel.dbo.USUARIOS_ROLES

--se cambio el nombre de una tablas 
--EXEC sp_rename 'USURIOS_ROLES', 'USUARIOS_ROLES';  


select * from BDMultinivel.dbo.PROYECTO
select * from BDMultinivel.dbo.EMPRESA


select * from  BDMultinivel.dbo.PROYECTO PRO, BDMultinivel.dbo.EMPRESA EMP 
         where PRO.proyecto_conexion_id= EMP.codigo

		 select PRO.nombre as 'proyecto', 
		 EMP.nombre as 'empresa' 
		 from  BDMultinivel.dbo.PROYECTO PRO
		 left join BDMultinivel.dbo.EMPRESA EMP ON  PRO.proyecto_conexion_id= EMP.codigo


         where PRO.proyecto_conexion_id= EMP.codigo


		 select * from BDMultinivel.dbo.USUARIO
		 select * from BDMultinivel.dbo.USUARIOS_ROLES

------------------------------------------------

select top(100) * from BDMultinivel.dbo.FICHA_NIVEL_I
select top(100) * from BDMultinivel.dbo.NIVEL

---------------------
--create VIEW [dbo].[vwObtenerFicha]
--AS
		 select top(100)
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

		 where ci like '%5362%'

--//-------------------------------------------------------------------------------
--//--------------------------------------------------------------------------------

      --/LISTADO CLIENTE llave l_contacto_id esta llave esta en todas las tablas mencionadas abajo/
			SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto limit 100')

			--COMISION DE VENTA DIRECTA/
			SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80') --lcontratro id
			go
			--COMISION DE VENTA DE GRUPO/
			SELECT SUM(dcomision) FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80')
			go
			--COMISION DE BONO RESIDUAL/
			SELECT SUM(dtotalbono) FROM OPENQUERY( [10.2.10.222], 'select * from administracionbonoresidual where lciclo_id = 80')

				--DESCUENTOS consolidado y por empresa/
				SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo where lciclo_id = 80') where lcontacto_id=23 --- monto total aplicacion
				SELECT SUM(dmonto) FROM OPENQUERY( [10.2.10.222], 'SELECT * FROM administracionredempresacomplejo  where lciclo_id = 80') where lcontacto_id=23 --- ficha por empresa
			------------------------------
			SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from rpt_neto_facturacion_ciclo where lciclo_id = 80')


	-------------------------------------------------------------------------------------
	
	--contacto id    (montoempresa * 15.50 )/ 100 - 15.50 
	-- kalomai ! avdel !        Total comisiones= 164.78
	 --  150    139.24
	 ------------------------------
	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80')

	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontrato limit 150') --lcomplejoid  vente contrato al id contrato join admin complejo, tabla complejo 
		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncomplejo limit 150')	
		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionempresa limit 150')









			SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto limit 100')
			SELECT snombre, '', dtfechainicio, dtfechafin, 100, GETDATE(), GETDATE() FROM OPENQUERY( [10.2.10.222], 'SELECT * FROM  administracionciclo')

			------------------------------
			--administracioncontacto
			--tipobaja: 0,1,2,3
			--pmax


				select id_ficha as 'fichaid' from BDMultinivel.dbo.FICHA f where f.codigo= 44555666
				-------------------------------------

				--el contrato es la venta
	------------------------------------------------------------------------------------------------------------------------------------------------
	--venta grupal
	 SELECT SUM(dcomision) FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80')
	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontrato limit 150')
	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncomplejo limit 150')	
     SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionempresa limit 150')

	 ---venta personal
	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80')  where lcontacto_id= 8
	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontrato limit 150') where lcontacto_id= 8 -- lcontrato_id= 118974
	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncomplejo limit 150')	
     SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionempresa limit 150')

	SELECT empresa.empresa, empresa.lempresa_id, personal.lcontacto_id, sum(dcomision) FROM(  SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80')) as personal
	 inner join  ( SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontrato ')) as contrato on  contrato.lcontrato_id= personal.lcontrato_id
	 inner join (SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncomplejo limit 150') )as complejo on complejo.lcomplejo_id= contrato.lcomplejo_id
	 inner join ( SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionempresa limit 150')) as empresa on empresa.lempresa_id= complejo.lempresa_id
	 group by empresa.empresa, empresa.lempresa_id, personal.lcontacto_id

	  SELECT * FROM OPENQUERY ( [10.2.10.222],'select emp.empresa, emp.lempresa_id, vta.lcontacto_id, SUM(vta.dcomision) AS dcomision  from administracionventapersonal vta
		inner join administracioncontrato cto on cto.lcontrato_id = vta.lcontrato_id and vta.lciclo_id = 80
		inner join administracioncomplejo comp on comp.lcomplejo_id = cto.lcomplejo_id
		INNER JOIN administracionempresa emp on emp.lempresa_id = comp.lempresa_id GROUP BY emp.lEmpresa_id, vta.lcontacto_id
		') 

	---venta personal

	  DECLARE @ventaPersonal as table(empresa varchar(100),lempresa_id INT, lcontacto_id INT, dcomision numeric(12, 2) );
	  insert into @ventaPersonal SELECT * FROM OPENQUERY ( [10.2.10.222],'select emp.empresa, emp.lempresa_id, vta.lcontacto_id, vta.dcomision  from administracionventapersonal vta
		inner join administracioncontrato cto on cto.lcontrato_id = vta.lcontrato_id and vta.lciclo_id = 80
		inner join administracioncomplejo comp on comp.lcomplejo_id = cto.lcomplejo_id
		INNER JOIN administracionempresa emp on emp.lempresa_id = comp.lempresa_id
		') as dat 
	  select sum(dcomision) as montoPersonal from  @ventaPersonal  where lcontacto_id=27   and lempresa_id=2  group by lempresa_id, lempresa_id



	--------------------------------------------------------------------------------------
	--venta  grupal
	 DECLARE @ventaGrupal as table(empresa varchar(100),lempresa_id INT, lcontacto_id INT, dcomision numeric(12, 2) );
	 insert into @ventaGrupal  select gru.empresa, gru.lempresa_id, gru.lcontacto_id, gru.dcomision from (SELECT * FROM OPENQUERY ( [10.2.10.222],'select emp.empresa, emp.lempresa_id, vta.lcontacto_id, vta.dcomision  from administracionventagrupo vta
		inner join administracioncontrato cto on cto.lcontrato_id = vta.lcontrato_id and vta.lciclo_id = 80
		inner join administracioncomplejo comp on comp.lcomplejo_id = cto.lcomplejo_id
		INNER JOIN administracionempresa emp on emp.lempresa_id = comp.lempresa_id GROUP BY emp.lEmpresa_id, vta.lcontacto_id
		')) as gru 
	 select SUM(dcomision) as dcomision from @ventaGrupal where  lcontacto_id=23 and lempresa_id=11 group by lempresa_id, lempresa_id

	-----------------------------
	--monto residual por empresa

	   DECLARE @tableResidual as table(empresa varchar(100),lempresa_id INT, lcontacto_id INT, dmonto numeric(12, 2) );
	    insert into @tableResidual   SELECT * FROM OPENQUERY ( [10.2.10.222],'select empre.empresa, empre.lempresa_id, reci.lcontacto_id, reci.dmonto  from administracionredempresacomplejo reci	
		 inner join administracioncomplejo comp on comp.lcomplejo_id = reci.lcomplejo_id  and reci.lciclo_id = 80
		 inner join administracionempresa empre on empre.lempresa_id = comp.lempresa_id  
		')as resid                                                                
		select SUM(r.dmonto) as dmonto  from @tableResidual  r where r.lcontacto_id=30663 and lEmpresa_id=11  GROUP BY r.lEmpresa_id, r.lcontacto_id

-------------------------------------------
---practica los descuentos que se realizo en diferente empresas
   --select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80

   SELECT * FROM OPENQUERY ( [10.2.10.222],'select * from AplicacionesPagos where Ciclo = 80')
   SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from AplicacionesPagos')

   SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo where lciclo_id = 80') where lcontacto_id=23 

   select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and CI_Cliente='3148996' group by Id_Cliente
   select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 group by CI_Cliente, Id_Cliente

   select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 group by CI_Cliente, Id_Cliente
   select CI_Cliente from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 group by CI_Cliente, Id_Cliente
    select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 order by CI_Cliente

      select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and CI_Cliente='3999408' group by Id_Cliente

   select top(50) * from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO
     
	 select  * from BDMultinivel.dbo.PROYECTO

   select top(50) * from BDMultinivel.dbo.GP_COMISION_DETALLE   
   
   ---prueba de detalle comision
   select * from BDMultinivel.dbo.GP_COMISION_DETALLE --where id_ficha= 24 7474
   --select * from BDMultinivel.dbo.FICHA
   select  * from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO
   ---obtener el idDetalle de ficha..

    select top(1) CODE.id_comision_detalle, FI.id_ficha from BDMultinivel.dbo.CICLO C
    inner join BDMultinivel.dbo.GP_COMISION CO on CO.id_ciclo = C.id_ciclo
	inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= CO.id_comision
	inner join  BDMultinivel.dbo.FICHA FI on FI.id_ficha = CODE.id_ficha
	where C.id_ciclo= 80 and FI.ci='3287520';

	----------------------

	select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 order by CI_Cliente


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
			where IDESTA.habilitado = 'true' and GPESTA.habilitado= 'true' and CI.id_ciclo=80 and  GPESTA.id_estado_comision=2




			select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision_detalle= 7473
			select * from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO where id_comisiones_detalle = 7473

			select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
			select monto_retencion from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision_detalle= 7772
	--------------------
	--descuentos de ishur 
	   select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and CI_Cliente='9594889'
	   select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and CI_Cliente='9594889'
	   --administraciondescuentociclodetalle es de guardian... es por que no se aplican los descuentos manuales... o no se procesas las aplicaciones no procesados..
	   
	   --descuentos guardian	   
	    SELECT top(100) * FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo where lciclo_id=80 ')
		--descuento detalle guardian
		 SELECT top(100) * FROM OPENQUERY( [10.2.10.222], 'select * from Administraciondescuentociclodetalle ')


	   SELECT  *FROM BDComisiones.DBO.CNX_BDCOMISIONES

	   ----------------------------------------------------------------------------------
	   ---albert

	   euse grdsion; /*Aqui unir con la provision y realizar la cabecera del excel(casos de prueba)*/ 
	   select d.* ,co.snombrecompleto,ec.empresa_id,co.lcontacto_id,c.lciclo_id,sum(d.dmonto) total  from administraciondescuentociclodetalle d inner join administraciondescuentociclo c on c.ldescuentociclo_id = d.ldescuentociclo_id inner join administracioncontacto co on co.lcontacto_id = c.lcontacto_id inner join empresa_complejo ec on ec.complejo_id = d.lcomplejo_id where  c.lciclo_id=85 and co.lcontacto_id =95 Group by co.snombrecompleto,ec.empresa_id,co.lcontacto_id,c.lciclo_id;  select ciclo_id, empresa_id, contacto_id, total_pagar  from provision where ciclo_id = 85 and contacto_id = 95;

	   ------------------------------------
	   ----
	      @habilitado_facturar_guardian bit,
   @usuario VARCHAR(100)

	    --aqui actualizar la facturacion en
						  IF(@habilitado_facturar_guardian = 'true')
						  BEGIN
							    BEGIN TRY  
									 --obtener la semana
									 DECLARE @ID_SEMANA INT;
									 SET @ID_SEMANA=0;
									 DECLARE @IDGENERICO INT
									 SET @IDGENERICO=0;

									 SELECT top(1) @ID_SEMANA= lsemana_id FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionsemanaciclo') where lciclo_id= @id_Ciclo
									  IF(@ID_SEMANA > 0)
									  BEGIN
										   SELECT @IDGENERICO = lciclopresentafactura_id FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionciclopresentafactura order by dtfechaadd desc')  where lciclo_id= @id_Ciclo 
										   IF(@IDGENERICO > 0)
										   BEGIN
											SET  @IDGENERICO = @IDGENERICO + 1
									  --      INSERT OPENQUERY ([10.2.10.222], 'select susuarioadd, dtfechaadd, susuariomod,  dtfechamod,lciclopresentafactura_id, lciclo_id, lcontacto_id, lsemana_id  from grdsion.administracionciclopresentafactura')  
											--VALUES (@Usuario, GETDATE(), @Usuario, GETDATE(),@IDGENERICO,@id_Ciclo, @IDCONTACTOGUARDIANItem, @ID_SEMANA); 
										   END
									   
								     END								 							
								END TRY  
								BEGIN CATCH  								       
								  --   insert into BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL(id_ciclo,id_ficha, codigo_cliente, total_monto_bruto, descripcion )
									 --values(@id_Ciclo,@IDFICHAiten, @IDCONTACTOGUARDIANItem,0,'no se pudo registrar la facturacion en el Guardian ');
								END CATCH  
						  END

						  select * from BDMultinivel.dbo.TIPO_APLICACIONES 
						  select * from BDMultinivel.dbo.PROYECTO
						  select * from BDMultinivel.dbo.EMPRESA

  select * from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO order by id_aplicacion_detalle_producto desc
  --------------------------------------------------------------------------------------------------------

  empresa_conexion
  SELECT top(100) * FROM OPENQUERY( [10.2.10.222], 'select * from empresa_complejo ')
  SELECT top(100) * FROM OPENQUERY( [10.2.10.222], 'select * from administracionempresa ')


  select e.snombre,ec.* 
from empresa_complejo ec
inner join administracionempresa e on e.lempresa_id = ec.empresa_id


 
 SELECT  * FROM OPENQUERY( [10.2.10.222], N'select * from grdsion.administraciondescuentociclodetalle ')  where lciclo_id=80 and lcontacto_id =80182

  SELECT  * FROM OPENQUERY( [10.2.10.222], N'select susuarioadd,dtfechaadd, susuariomod, dtfechamod, ldescuentociclodetalle_id, ldescuentociclotipo_id, lcomplejo_id, smanzano, slote, suv, dmonto, sobservacion from grdsion.administraciondescuentociclodetalle ')  

					INSERT OPENQUERY ([10.2.10.222], 'SELECT susuarioadd,dtfechaadd, susuariomod,dtfechamod,ldescuentociclodetalle_id, ldescuentociclotipo_id, lcomplejo_id, smanzano, slote, suv, dmonto, sobservacion   FROM grdsion.administraciondescuentociclodetalle')  
						VALUES ('laguirrePrueba', --usser add
						        GETDATE(), --add
								'laguirrePrueba', --user mod
								GETDATE(), --fechamod
								600000, --id table
								50469,--id cabecera 
								99, --complejo guardian
								'', --smanzano
								'', --slote
								'lprueba-nh-22', --producto
								25,-- monto
								'descripcion producto' 
						       ); 
					 SELECT SCOPE_IDENTITY() as 'idautoincremental';


					 select codigo, 
			       codigo_cnx, 
				   nombres, 
				   apellidos, 
				   ci, 
				   correo_electronico, 
				   fecha_registro,
				   tel_oficina, 
				   tel_movil, 
				   tel_fijo, 
				   direccion,
				   fecha_nacimiento,
				   contrasena,
				   comentario, 
				   avatar, 
				   tiene_cuenta_bancaria, 
				   id_banco,
				   cuenta_bancaria,
				   factura_habilitado,
				   razon_social, 
				   nit,
				   id_usuario,
				   estado, 
				   id_usuario from  BDMultinivel.dbo.FICHA

		     SELECT lcontacto_id, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena,lcuentabanco, lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja    FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto limit 100') order by lcontacto_id asc

			 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto limit 100') order by lcontacto_id asc 

			 SELECT lcontacto_id, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena,lcuentabanco, lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja    FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto limit 200 ') where lpatrocinante_id=-10  

			 --------------------------------------
			---- CARGANDO FICHA - VERIFICAR 
			  --DELETE FROM  BDMultinivel.dbo.FICHA
			  --delete from  BDMultinivel.dbo.FICHA_TIPO_BAJA_I
			  --delete from BDMultinivel.dbo.FICHA_NIVEL_I
			  --delete from BDMultinivel.dbo.GP_CLIENTE_VENDEDOR_I

			  select * from  BDMultinivel.dbo.FICHA
			   select * from  BDMultinivel.dbo.TIPO_BAJA
			  select * from  BDMultinivel.dbo.FICHA_TIPO_BAJA_I

			  select * from BDMultinivel.dbo.NIVEL
			  select * from BDMultinivel.dbo.FICHA_NIVEL_I

			  select * from BDMultinivel.dbo.GP_CLIENTE_VENDEDOR_I

			   --verificar suvendedor
			   select * from  BDMultinivel.dbo.FICHA where codigo=118
			   select * from BDMultinivel.dbo.GP_CLIENTE_VENDEDOR_I where id_cliente=118
			   select * from  BDMultinivel.dbo.FICHA where codigo=119
			  ---se agrego 3 master
			  --- se agrego 105 frelancer q estan en cnx


			  SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto ') where lcuentabanco = '3052125446' !=0 or  ctienecuenta != null
			                                                                              
				SELECT lcontacto_id,dtfechaadd, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena, CASE WHEN lcuentabanco IS NULL THEN 0 ELSE lcuentabanco END as 'lcuentabanco', lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja, lnit    FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto ') order by lcontacto_id asc 																


				---------------------------------------------------------------------------------------------
				-----------------------------------------------------------------------------------------------  , SUM(retencion_total as retension_total, SUM(total_pagar) as total_pagar 
				SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=85 order by empresa_origen_id
				SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retension, SUM(total_pagar) as total_pagar  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=85  group by contacto_id
				 SELECT SUM(total) as total, SUM(retencion_total) as retencion_total, SUM(total_pagar) as total_pagar  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=85
		-------------------------------------------------------------------------------------------------------
		--delete BDMultinivel.dbo.GP_COMISION
		--delete BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
		--delete BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL
		--delete BDMultinivel.dbo.GP_COMISION_DETALLE
		--delete BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
		--delete BDMultinivel.dbo.COMISION_DETALLE_EMPRESA
		

		select * from BDMultinivel.dbo.GP_COMISION  
		select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I

		select * from BDMultinivel.dbo.GP_COMISION_DETALLE  where id_comision= 17
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
		
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_ficha= 64741
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE --estados
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA group by id_comision_detalle
		

		select * from BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL

		
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION

		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionempresa limit 150')
		--guardian descuento por ciclo contacto Id
		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo where lciclo_id = 87') where lcontacto_id=10 
		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=85  AND contacto_id=10 
		SELECT  *   FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo ') where lciclo_id = 85 and lcontacto_id=10 
		----------------------------------------------------------
		---------------------------------------------------------------------
		---obtener venta grupal personal
		 SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal') where lciclo_id=85 
		 SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo')
		 SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from administracionredempresacomplejo')
		 SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from t_ganadores_bonoliderazgo_empresa_pagar')
		 ------------------------------------
		 -- comisiones para forma de pago
	     
		 SELECT top(500) *  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ')
		 SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view') comision_forma_pago_view

		  select * from  BDMultinivel.dbo.TIPO_PAGO
		  -----------------------------------------------------------------------------------
		  --example cabecera comision forma pago

		 --  SELECT @TOTAL_HEADER= SUM(total),@TOTAL_RETENCION_HEADER= SUM(retencion_total) , @TOTAL_PAGAR = SUM(total_pagar)  
		 SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=85 order by total asc
		 SELECT SUM(total), SUM(retencion_total), SUM(total_descuento), SUM(total_neto), (SUM(retencion_total) + SUM(total_descuento) + SUM(total_neto)) as calculado    FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=85  AND contacto_id=10

		 -----cargar comsion po contacto
		 --SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retencion_total, SUM(total_pagar) as total_pagar 
		 --FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=@CICLO_SELEC  group by contacto_id
		 
		 ---LISTA TOTALES POR FRELANCERS
		  SELECT contacto_id, SUM(total) as total,SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar   FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') where  ciclo_id=85  group by contacto_id

		  --OBTENER DESCUENTO POR FRELANCER - PARA GP_COMISION_DETALLE
		  SELECT total_descuento  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') WHERE ciclo_id=87 and contacto_id=8

		  --comsions por empresa
		  --SELECT contacto_id, total, retencion_total, total_pagar, empresa_id, factura_id  

		  select contacto_id, total, retencion_total, total_neto, empresa_id, factura_id   FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') where ciclo_id=85  AND contacto_id= 10

		  ---lista autorizada

		   select * from BDMultinivel.dbo.ESTADO_AUTORIZACION_COMISION
		  select * from BDMultinivel.dbo.TIPO_AUTORIZACION
		   select * from BDMultinivel.dbo.AREA
		   select * from BDMultinivel.dbo.AUTORIZACIONES_AREA
		   select * from BDMultinivel.dbo.USUARIO
		    select * from BDMultinivel.dbo.USUARIO_AUTORIZACION

		--ALTER VIEW [dbo].VW_LISTAR_AUTORIZACIONES_TIPO
          AS
	      select USUA.id_usuario_autorizacion,AREA.id_area,AREA.nombre AS 'descripcion_area', USUA.estado, U.id_usuario, U.nombres, U.apellidos, U.usuario,TA.id_tipo_autorizacion,TA.nombre AS 'nombre_tipo_autorizacion',  USUA.fecha_creacion  FROM BDMultinivel.dbo.USUARIO_AUTORIZACION USUA 
		    INNER JOIN BDMultinivel.dbo.USUARIO U ON USUA.id_usuario= U.id_usuario
			INNER JOIN BDMultinivel.dbo.TIPO_AUTORIZACION TA ON TA.id_tipo_autorizacion= USUA.id_tipo_autorizacion
			INNER JOIN BDMultinivel.dbo.AREA AREA ON AREA.id_area = U.id_area
			where TA.estado='True'

		--VW_TIPO_AUTORIZACION
			 select TP.id_tipo_autorizacion,tp.nombre, TP.cantidad as 'cantidad_limite',AA.cantidad as 'cantidad_aprobacion_minima_area'  from BDMultinivel.dbo.TIPO_AUTORIZACION TP
			 inner join  BDMultinivel.dbo.AUTORIZACIONES_AREA AA on AA.id_tipo_autorizacion = TP.id_tipo_autorizacion
			 where TP.estado='True'

		SELECT * FROM VW_TIPO_AUTORIZACION
		SELECT * FROM VW_LISTAR_AUTORIZACIONES_TIPO


		------------------------------------------------------------------------------
		--id comision = 92 base
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE GCD where id_comision=92

		--update  BDMultinivel.dbo.GP_COMISION_DETALLE 
		--set id_comision=92
		select * from BDMultinivel.dbo.GP_TIPO_COMISION

		select * from BDMultinivel.dbo.GP_COMISION
		select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I 
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION

		select * from BDMultinivel.dbo.GP_COMISION_DETALLE
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLES


		select * from BDMultinivel.dbo.LISTADO_FORMAS_PAGO
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA

			select * from BDMultinivel.dbo.EMPRESA
			select * from BDMultinivel.dbo.usuario


		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from empresa_conexion')

		select * from BDMultinivel.dbo.FICHA where id_ficha= 20735 --contacto 1025
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE  where id_comision_detalle= 12705

		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12692   --cantidad 5
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12694 --NULL
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12695  -- NULL
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12699 --cantidad 4
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12702 -- cantidad 2
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12705 --NULL
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12701 --NULL
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= 12714  -- cantidad 5

		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle IN(12692,12694,12695,12699,12702,12705,12701,12714,12711) --nnull

		

		--------------------------------------------------------------------------------------------------
		--verificar si tiene movimiento el detalle

		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_movimiento > 0
	-----------------------------------------------------------------------------------------------------------------------------------------------------------
	--ELIMINAR LA PLANILLA PREVIA
			select * from BDPuntosCash.DBO.COMISIONES_ESTADO_XLS
			select * from BDPuntosCash.DBO.COMISIONES_XLS where lciclo_id=85
			--DELETE BDPuntosCash.DBO.COMISIONES_XLS where lciclo_id=85

			select * from BDMultinivel.dbo.LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL 
			--delete BDMultinivel.dbo.LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL 
			select * from BDMultinivel.dbo.EMPRESA 

		
----------------------------------------------------------------------------------------------------------------------------------------------------------------
      --ELIMINAR LA PLANTILLA DETALLE CICLO
		   SELECT * FROM BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=85 -- 85
		   --delete BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=85 
		   SELECT * FROM BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=85
		   --delete BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=85
	   ---------------------------------------------------------------------------------------------------------------------------------------------------------
		--- PROCEDIMIENTO ROLANDO EL SERVICE 1, ---------------------------------
		 --ADD BDPuntosCash.DBO.COMISIONES_MAESTRO
		SELECT doc_id, SUM(monto) monto, nro_cuenta FROM BDPuntosCash.DBO.COMISIONES_XLS where id_comisiones_estado_xls = 1 and lciclo_id = 85 GROUP by doc_id, nro_cuenta

		-- BDPuntosCash.DBO.COMISIONES_DETALLE

		SELECT * FROM BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=85 -- 85
		SELECT * FROM BDPuntosCash.DBO.COMISIONES_DETALLE where id_comisiones_maestro=47

		SELECT * FROM BDPuntosCash.DBO.COMISIONES_MAESTRO COM
		INNER JOIN  BDPuntosCash.DBO.COMISIONES_DETALLE CDE on COM.id_comisiones_maestro= CDE.id_comisiones_maestro
		WHERE COM.lciclo_id=85
		--------------------------------------

		  select id_comisiones_detalle, doc_id, nro_cuenta, monto, id_empresa 
    from BDPuntosCash.dbo.COMISIONES_DETALLE 
    where lciclo_id = 85 and id_comisiones_estado_maestro_detalle = 1 and id_movimiento = 0 --and doc_id=4283629 and id_empresa=14
	ORDER BY doc_id, monto DESC
	
	select *  FROM BDPuntosCash.DBO.COMISIONES_ESTADO_MAESTRO_DETALLE
-----------------------------------------------------------------------------------------------------------------------------------------------------------------

  SELECT * FROM BDPuntosCash.DBO.agente
	
	 --id agente recarga 3
		153, 154, 155, 156, 157, 	158, 159, 160, 161 	162, 163, 164, 165, 166,
	-- OBTENER CUENTA
	
	SELECT id_cuenta, saldo_actual FROM BDPuntosCash.DBO.CUENTA WHERE id_cuenta= 27

	--MOVIMIENTO 
	select *   from BDPuntosCash.dbo.movimiento WHERE id_movimiento=12725
	select top(5)id_cuenta, id_agente, id_tipo_movimiento,monto,saldo_actual,saldo_anterior, fecha, descripcion, id_estado_movimiento   from BDPuntosCash.dbo.movimiento 12725

	---ACTUALIZAR DETALLE COMISION PUNTOS CASH

	SELECT * FROM BDPuntosCash.DBO.COMISIONES_DETALLE WHERE id_comisiones_detalle=162

	--ACTUALIZA EL DETALLE PUNTAS CASH
	--update BDPuntosCash.DBO.COMISIONES_DETALLE set id_movimiento= 0, fecha_movimiento =GETDATE(), id_comisiones_estado_maestro_detalle=1 WHERE id_comisiones_detalle=162

	select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle_empresa= 38998
	select id_movimiento, id_comprobante_generico, fecha_actualizacion from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle_empresa= 38998

	---VER LOS MOVIMIENTOS GENERADOS
	SELECT id_cuenta, saldo_actual FROM BDPuntosCash.DBO.CUENTA WHERE id_cuenta= 1371
	select top(200)*   from BDPuntosCash.dbo.movimiento ORDER BY id_movimiento desc


--------------------------
--insertar rol ADMINISTRADOR y permiso manual POR PRIMERA VEZ
    -- ver paginas roles id pagina idRol = 7 page rol
		select * from BDMultinivel.dbo.MODULO
		select * from BDMultinivel.dbo.PAGINA

	  select * from BDMultinivel.dbo.USUARIO

	  select * from BDMultinivel.dbo.PAGINA
	  select * from BDMultinivel.dbo.permiso
	  select * from BDMultinivel.dbo.ROL
	  select * from BDMultinivel.dbo.ROL_PAGINA_I
	  select * from BDMultinivel.dbo.ROL_PAGINA_PERMISO_I

	   select * from BDMultinivel.dbo.USUARIOS_ROLES

	    -- -- CREAR ROL------------------------------------------------------------------------------------------------------------------------
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


				---------------------------------------------------------------

				
		select * from BDMultinivel.dbo.GP_COMISION  
		  select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I

		select * from BDMultinivel.dbo.GP_COMISION_DETALLE  where id_comision= 18
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
		select * from BDMultinivel.dbo.CIUDAD

		select * from BDMultinivel.dbo.GP_COMISION_DETALLE  de
		inner join BDMultinivel.dbo.LISTADO_FORMAS_PAGO LI on de.id_comision_detalle= LI.id_comisiones_detalle
		inner join BDMultinivel.dbo.TIPO_PAGO tipo on LI.id_tipo_pago= tipo.id_tipo_pago
		where id_comision_detalle =2612

		select * from BDMultinivel.dbo.FICHA where id_ficha= 4036
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle in (
		2932,
2933,
2934,
2935,
2936,
2937,
2938,
2940,
2941,
2943,
2964,
2976,
2977)

select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where  id_comision_detalle_empresa not in (
9998,
9999,
10000,
10001,
10002,
10003,
10004,
10005,
10006,
10007,
10008,
10009,
10010,
10011,
10012,
10013,
10014,
10015,
10016,
10017,
10018,
10019,
10025,
10026,
10027,
10028,
10029,
10032,
10033,
10080,
10103,
10104,
10105,
10106,
10107)
-----------------------------------------------------------------------------------------------------------
---consultas ELIO y CHATO 
--acceso por empresa 

select * from BDMultinivel.dbo.vwObtenerInfoExcelFormatoBanco where id_ciclo=87 and id

select * from BDMultinivel.dbo.empresa
select * from BDMultinivel.dbo.vwObtenerInfoExcelFormatoBanco where id_ciclo=88 and id_tipo_pago=2 and id_empresa=1

select i.id_comision_detalle_empresa from dbo.vwObtenerInfoExcelFormatoBanco i where i.id_ciclo = 88 and i.id_empresa = 2 and i.id_tipo_pago = 2 and i.id_estado_comision_detalle_empresa = 2

--

--UPDATE COMISION_DETALLE_EMPRESA SET fecha_pago = null, id_usuario = 1 , estado=1 
--where estado = 2 and
--id_comision_detalle_empresa in ( select i.id_comision_detalle_empresa from dbo.vwObtenerInfoExcelFormatoBanco i where i.id_ciclo = 88 and i.id_empresa = 2 and i.id_tipo_pago = 2 and i.id_estado_comision_detalle_empresa = 2 )

"1255,1515,051515,151515"



select * from BDPuntosCash.DBO.MOVIMIENTO order by id_movimiento desc



DECLARE @Number INT  
  
SET @Number=12345  
  
-- Using CAST function  
  
SELECT CAST(@Number as varchar(10)) as Num1  
  
-- Using CONVERT function  
  
SELECT CONVERT(varchar(10),@Number) as Num2  

ALTER TABLE  BDMultinivel.dbo.COMISION_DETALLE_EMPRESA ALTER COLUMN nro_autorizacion VARCHAR (100) ;
---------------------------------------------------

select * from BDMultinivel.dbo.GP_COMISION where id_ciclo=83 and id_tipo_comision =1
select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I where id_comision=117 and habilitado='true'

select * from BDMultinivel.dbo.GP_ESTADO_COMISION  

select * from BDMultinivel.dbo.GP_COMISION where id_ciclo=83 and id_tipo_comision =1
select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I where id_comision=118 and habilitado='true'

SELECT * FROM BDMultinivel.dbo.vwObtenerCiclos where id_estado_comision=10


SELECT SUM(total), SUM(retencion_total) , SUM(total_descuento), SUM(total_neto)  FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit2.comision_forma_pago_view ') where ciclo_id=89
SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar  FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit2.comision_empresa_forma_pago_view ') where ciclo_id=90  group by contacto_id
insert into @VW_COMISIONES_EMPRESA SELECT contacto_id, total, retencion_total, total_neto, empresa_id, factura_id   FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit2.comision_empresa_forma_pago_view ') where ciclo_id=@CICLO_SELEC  AND contacto_id= @CONTACTOITitem

SELECT  lcontacto_id,dtfechaadd, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena, Isnull(lcuentabanco,0) as 'lcuentabanco', lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja, lnit, ciclo_id   
 FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select co.lcontacto_id, co.dtfechaadd, co.stelefonofijo, co.stelefonomovil, co.scorreoelectronico, co.dtfechanacimiento, co.sdireccion, co.scedulaidentidad, co.lpatrocinante_id, co.lnivel_id, co.snombrecompleto, co.scontrasena, co.lcuentabanco, co.lcodigobanco, co.cbaja, co.dtfechabaja,co.ltipobaja, co.smotivobaja, co.lnit, comi.ciclo_id from grduit2.administracioncontacto co inner join grduit2.comision_forma_pago_view comi on co.lcontacto_id = comi.contacto_id  ') where ciclo_id= 90 order by lcontacto_id asc 

