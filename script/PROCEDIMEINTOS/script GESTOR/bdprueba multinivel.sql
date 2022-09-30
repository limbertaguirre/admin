select * from BDPuntosCash.dbo.movimiento where id_cuenta=1259
select * from BDMultinivel.dbo.ciclo

select * from BDMultinivel.dbo.ROL
select * from BDMultinivel.dbo.ROL_PAGINA_I
select * from BDMultinivel.dbo.ROL_PAGINA_PERMISO_I
select * from BDMultinivel.dbo.permiso

select * from BDMultinivel.dbo.MODULO
select * from BDMultinivel.dbo.PAGINA
select * from BDMultinivel.dbo.permiso
select * from BDMultinivel.dbo.GP_TIPO_COMISION
select * from BDMultinivel.dbo.TIPO_APLICACIONES
 --DBCC CHECKIDENT (TIPO_APLICACIONES, RESEED, 0)

--delete from BDMultinivel.dbo.MODULO
--delete from BDMultinivel.dbo.PAGINA
--delete from BDMultinivel.dbo.permiso
--delete from BDMultinivel.dbo.ROL_PAGINA_I
--delete from BDMultinivel.dbo.ROL_PAGINA_PERMISO_I

  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('facturacion','/facturacion','facIcon',1,1,6,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('ficha de cliente','/cliente','facIcon',2,1,6,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('registro usuario','/usuario','facIcon',3,1,6,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('corizacion','/corizacion','facIcon',4,1,6,1);

  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('comisiones','/comisiones','facIcon',1,1,8,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('porrateo empresa','/empresa','facIcon',2,1,8,1);
  --insert into PAGINA (nombre,url_pagina, icono, orden, habilitado, id_modulo, id_usuario) values('porrateos','/porrateos','facIcon',3,1,8,1);


  
select * from BDMultinivel.dbo.empresa  
select top(50) * from BDMultinivel.dbo.ficha where ci= '5953655'
select  * from BDMultinivel.dbo.ciclo
----------------------------------------------------------------------------------------------
 select * from BDMultinivel.dbo.GP_COMISION
 select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I --detalle estado , 1 pendiente facturacion
-- select * from BDMultinivel.dbo.GP_ESTADO_COMISION

select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision=12 and monto_neto > 0 --323 -- 388
select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I--  where habilitado = 0 -- 1 =id no facturo
select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE

select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA
select * from BDMultinivel.dbo.FICHA  --5469
select * from BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL where id_ciclo=80
select * from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO

 SELECT *  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=86
 SELECT SUM(total), SUM(retencion_total) ,SUM(total_descuento),SUM(total_neto)  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=86

 SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar  FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') where ciclo_id=@CICLO_SELEC  group by contacto_id

-- delete  from BDMultinivel.dbo.GP_COMISION
-- delete from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I

-- delete from BDMultinivel.dbo.GP_COMISION_DETALLE
-- delete from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I

--delete from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA
-- delete from BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL

--delete from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO



--inicializar la comision a pendiente
--update  BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I    set id_estado_comision=1 where id_comision=60

--update  BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I    set habilitado = 'true'

---------------------------------------------------------------------
select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision_detalle =4471
select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle=4471

-----------------------------------------------------

select * from BDMultinivel.dbo.FICHA where ci='3287520'
--update  BDMultinivel.dbo.FICHA
--    set factura_habilitado=1
where ci='5953655'

			SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80')  --COMISION DE VENTA DIRECTA/
			union
			SELECT SUM(dcomision) FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80') --COMISION DE VENTA DE GRUPO/
			union
			SELECT SUM(dtotalbono) FROM OPENQUERY( [10.2.10.222], 'select * from administracionbonoresidual where lciclo_id = 80')----COMISION DE BONO RESIDUAL/

	---------------------------------------------------------------
	-----------------------------------------------------------------
	    --total total frelancer
	      select SUM(dat.dcomision) as 'TOTALBRUTO', dat.lcontacto_id as 'CONTACTOID' from ( 
				SELECT dcomision, lcontacto_id FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80') 
				union all
				--COMISION DE VENTA DE GRUPO/
				SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80')
				union all
				--COMISION DE BONO RESIDUAL/
				SELECT dtotalbono, lcontacto_id FROM OPENQUERY( [10.2.10.222], 'select * from administracionbonoresidual where lciclo_id = 80')
			) as dat  
			group by dat.lcontacto_id

			SELECT neto_sion, neto_kintas, neto_zuriel, neto_nicapolis, neto_asher,
										       neto_shofar, neto_mexico, neto_praderas,  neto_kalomai, neto_valle_angostura,
										       neto_jayil, neto_neizan_jayil,  neto_neizan_jayil, neto_royal_pari, neto_menorah, neto_avdel
										FROM OPENQUERY( [10.2.10.222], 'select * from rpt_neto_facturacion_ciclo where lciclo_id = 80 ')
			SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from rpt_neto_facturacion_ciclo where lciclo_id = 80 ')


			--monto a facturar
			--monto total facturar

			-- monto  => monto neto.
			--resiidual
		   	-- retencion
			-- monto neto
--------------------------------------------------------------------------------------------
			select * from BDMultinivel.dbo.EMPRESA
			--delete from BDMultinivel.dbo.EMPRESA
			---inicializa el index identity a 0
			---DBCC CHECKIDENT (EMPRESA, RESEED, 0)
-------------------------------------------------------------
			select * from BDMultinivel.dbo.GP_COMISION_DETALLE CDE 
			   inner join  BDMultinivel.dbo.COMISION_DETALLE_EMPRESA DEM  on DEM.id_comision_detalle=CDE.id_comision_detalle
			   where CDE.id_comision_detalle=3010
-------------------------------------------

			   SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80') 
						union all
						SELECT dcomision, lcontacto_id FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80')
						union all

				----
						SELECT SUM(dtotalbono) FROM OPENQUERY( [10.2.10.222], 'select * from administracionbonoresidual where lciclo_id = 80')

						
						SELECT * FROM OPENQUERY( [10.2.10.222], 'SELECT * FROM grdsion.administracionredempresacomplejo  where lciclo_id = 80')
				     	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select e.snombre,ec.*  from empresa_complejo ec
                             inner join grdsion.administracionempresa e on e.lempresa_id = ec.empresa_id'


							 SELECT * FROM OPENQUERY( [10.2.10.222], 'SELECT * FROM grdsion.administracioncomplejo  ')


-----------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------

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
      like 


select top(50) * from BDMultinivel.dbo.ficha where ci= '5953655'
update BDMultinivel.dbo.ficha set estado=1   where ci= '5953655'



select * from BDMultinivel.dbo.GP_COMISION_DETALLE
select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I -- 1 =id no facturo
select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE


select * from BDMultinivel.dbo.FICHA where ci='6165841'


select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_ficha=84829
select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where  id_comision_detalle=4773
-- modififcar el montoneto de maestro detalle
--update BDMultinivel.dbo.GP_COMISION_DETALLE set monto_neto=161.75 where id_ficha=84829
--------------------------------------------------
--caso 2--
select * from BDMultinivel.dbo.FICHA where ci='5953655'
select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_ficha=30545
select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where  id_comision_detalle=5372
-- modififcar el montoneto de maestro detalle
--update BDMultinivel.dbo.GP_COMISION_DETALLE set monto_neto=0 where id_ficha=24
--//------------------------------


select si_facturo from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle=6272

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


CREATE VIEW [dbo].[vwObtenerComisionesDetalleAplicaciones]
AS
	 select
	       Ap.id_aplicacion_detalle_producto,
		   GPDE.id_comision_detalle,
	       AP.descripcion,
		   AP.monto,
		   AP.cantidad,
		   AP.subtotal,
		   Ap.id_proyecto,
		   CASE WHEN EMP.id_empresa IS NULL THEN 0 ELSE EMP.id_empresa END as 'id_empresa',		   
		   CASE WHEN EMP.nombre IS NULL THEN 'Sin definir' ELSE EMP.nombre END as 'nombre_empresa'
     from BDMultinivel.dbo.GP_COMISION_DETALLE GPDE
			inner join BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO AP on AP.id_comisiones_detalle=GPDE.id_comision_detalle
			left join BDMultinivel.dbo.PROYECTO PRO on PRO.id_proyecto = AP.id_proyecto
			left join BDMultinivel.dbo.PROYECTO EMP on EMP.id_proyecto = PRO.id_proyecto	
GO
select * from BDMultinivel.dbo.proyecto
select top(50)* from BDComisiones.dbo.vwLOTES_GRL_DOCID GRLOTE


CREATE VIEW [dbo].[vwObtenerProyectoxProducto]
AS

select  PRO.id_proyecto, PRO.nombre as 'nombreProyecto',PRO.id_empresa, GRLOTE.LOTE as 'producto', EMP.nombre as 'nombreEmpresa' from BDComisiones.dbo.vwLOTES_GRL_DOCID GRLOTE
                  inner join BDMultinivel.dbo.proyecto PRO on PRO.proyecto_conexion_id= GRLOTE.IDPROYECTO 
				  inner join BDMultinivel.dbo.EMPRESA EMP on EMP.id_empresa= PRO.id_empresa
				  where GRLOTE.LOTE='PRANO-M2-L47'
 go


 select * from BDMultinivel.dbo.proyecto

 --///------------------------------------------
 ---condicional.. si factura o se quita la factura  actualizar en el guardian..

 SELECT * FROM grdsion.administracionciclopresentafactura;
SELECT * FROM grdsion.administraciondescuentociclodetalle;
SELECT * FROM grdsion.administraciondescuentociclo;

SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionciclopresentafactura order by dtfechaadd desc')  where lciclo_id=80  -- where lcontacto_id= 23
SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administraciondescuentociclodetalle')
SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administraciondescuentociclo')

--agregar semana.. obtener por idciclo
SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionsemanaciclo') where lciclo_id=80

SELECT * FROM OPENQUERY( [10.2.10.222], 'select susuarioadd, dtfechaadd, susuariomod,  dtfechamod, lciclopresentafactura_id, lciclo_id, lcontacto_id, lsemana_id from grdsion.administracionciclopresentafactura')  where lciclo_id=80

------------------------------------------------------------------


--INSERT OPENQUERY ([10.2.10.222], 'select susuarioadd, dtfechaadd, susuariomod,  dtfechamod,lciclopresentafactura_id, lciclo_id, lcontacto_id, lsemana_id  from grdsion.administracionciclopresentafactura')  
--VALUES ('laguiffx', GETDATE(), 'laguirfx', GETDATE(),18503,80, 12, 65) ;  
----------------------------------------------------------------

select * from BDMultinivel.dbo.FICHA where  ci='3287520'

select * from BDMultinivel.dbo.FICHA where codigo=9845
----------------------------------------------------------------------------------------------------------
--- prorrateo guardian tablas
-- select * from prorrateo;
-- select * from detalle_prorrateo;

SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.prorrateo') where lciclo_id=80
----------------------------------------------------------------------------------------------------------------------------------
--tipo de descuento ciclo
--cuando es otros lo asume AVDEL

   SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administraciondescuentociclotipo')     
--------------------------------------------------------------------------------------------------------------------------------------
---agregar DESCUENTO  y verificar DECUENTOs
-- 
  --idtipo 1
  -- SELECT top(50) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracioncomplejo')
  --verificar 
  select top (500) * from BDMultinivel.dbo.FICHA where id_ficha=6394
  select top (500) * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision_detalle =10474
    select top (500) * from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO where id_comisiones_detalle = 10474

  select top (500) * from BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO  order by  fecha_creacion desc
   --estados de la tabla ciclo de guardian
    -- 0 estado activo
	-- 1 estado inactivo

    SELECT  * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionciclo')
	SELECT  * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionciclo')

  SELECT top(5) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administraciondescuentociclo') where lciclo_id=80 and ldescuentociclo_id= 50576
  SELECT top(55) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administraciondescuentociclodetalle') where ldescuentociclo_id=50576
  SELECT top(50) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administraciondescuentociclodetalle') order by dtfechaadd desc-- where ldescuentociclo_id=50576  108051

      --usuarioadd
	  --drfechaadd
	  --susuariomod-ldescuento --identit
	  --lciclo_id
	  --dtotal
	  --sdetalle
	  --lsemana
	   select top (500) * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and CI_Cliente='9594889'
	   --id
	   --id_empresa
	   --if_venta
	   --ciclo
	   --ci_cliente
	   --id_producto
	   --expensa
	   --monto
	   --fecha
	   --id_recibe
	   --id_factura
	   --observacion
	   --tipoPAgo
	   --intercompania 1




	   -- id cabexera = 50576, contacto= 6475, total+ 137.11M
	   --conexion 
	   -- bdproyectosalt
	   select * from BDMultinivel.dbo.PROYECTO
	   select * from BDMultinivel.dbo.EMPRESA

	   --delete BDMultinivel.dbo.PROYECTO
	   --delete BDMultinivel.dbo.EMPRESA

	   --DBCC CHECKIDENT (PROYECTO, RESEED, 0)
	   --DBCC CHECKIDENT (EMPRESA, RESEED, 0)

	


	   select * from BDComisiones.[dbo].[vwPROYECTOS_ALL]

	   select top(10) * from BDComisiones.[dbo].[CNX_BDCOMISIONES]
	   -----------------------------------------------------
	   select * from BDMultinivel.dbo.TIPO_APLICACIONES
	   --GUARDIAN EQUIVALENTE 

	    SELECT top(50) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.empresa_complejo') 
		SELECT top(50) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.proyecto_conexion_sufijo') 
		
		SELECT top(50) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.empresa_asume') 

		CUOTA FEB-2021  VTO. 02/14/2021 - Monto: 110.00 $us. - PDS-B-M1-L48, Descuento por Aporte RPFC - Monto: 14.11 $us.


		SELECT top(50) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.empresa_complejo em inner join grdsion.proyecto_conexion_sufijo pro ') 
		SELECT top(50) * FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.proyecto_conexion_sufijo ') 

		--/---------------------

				 select * from BDComisiones.[dbo].grlCLIENTE_CCN
		 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto') order by lcontacto_id asc --cbaja 1 = boqueado
		 --cbaja
		    --0 normal
			--1 bloqueado
		 --tipo baja
			  -- 0 no esta bloqueado
			  -- 1   Cesión de derecho
			  -- 2   Suspensión temporal
			  -- 3   Expulsión definitiva
			  -- 4   Otro caso

			  lnivel 
		---
		-- presenta factura
		 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionpresentafactura limit 100')

		 select * from BDComisiones.[dbo].grlCLIENTE_CCN ---g administracioncontacto 
		  select * from BDComisiones.[dbo].gral_ciudad
  -------------------------------------------------------------------------------
	select * from BDMultinivel.dbo.FICHA_TIPO_BAJA_I
	select * from BDMultinivel.dbo.TIPO_BAJA
	select * from BDMultinivel.dbo.CIUDAD

	select top(10)* from BDMultinivel.dbo.FICHA where ci='4729472'
	select * from BDMultinivel.dbo.GP_CLIENTE_VENDEDOR_I

   --- empresas equivalente guardian..
    SELECT top(100) * FROM OPENQUERY( [10.2.10.222], 'select * from empresa_conexion ')

  	select * from BDMultinivel.dbo.EMPRESA
	select * from BDMultinivel.dbo.PROYECTO

	@SION_VENTA_PERSONAL,@SION_VENTA_GRUPAL, @SION_RESIDUAL,

	SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from administracionempresa limit 150')

	SELECT  * from BDComisiones.[dbo].[CNX_BDCOMISIONES] 

	SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=87 order by empresa_origen_id

	 --- 6 shofar, 
	 -- 15-neyzan/ jayil srl, 
	 ---18 menorah  --idguardian

	 ------------------------

select * from BDMultinivel.dbo.LISTADO_FORMAS_PAGO
select * from BDMultinivel.dbo.TIPO_PAGO

select * from BDMultinivel.dbo.GP_DETALLE_LISTADO_FORMA_PAGO
select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_DETALLE_LISTADO_FORMA_PAGOL
select * from BDMultinivel.dbo.ESTADO_DETALLE_LISTADO_FORMA_PAGO

select * from BDMultinivel.dbo.AUTORIZACION_COMISION

--ALTER TABLE BDMultinivel.dbo.TIPO_PAGO
--ADD estado bit NOT NULL default 0;
--ALTER TABLE BDMultinivel.dbo.TIPO_PAGO
--ADD icono varchar(50) NOT NULL default '';

--CREATE VIEW [dbo].[vwVerificarCuentasUsuario]
 AS
	select
	       f.ci,
	       f.nombres,
           f.tiene_cuenta_bancaria, 
		   CASE WHEN u.id_usuario IS NULL THEN 'False' ELSE 'True' END As 'sionPay',
		   CASE WHEN u.id_estado_usuario IS NULL THEN 0 ELSE u.id_estado_usuario  END As 'estadoSionPay'
from  BDMultinivel.dbo.ficha f
left join BDPuntosCash.dbo.USUARIO u on   u.id_usuario COLLATE Latin1_General_CI_AS =   f.ci COLLATE Latin1_General_CI_AS

select * from BDPuntosCash.dbo.USUARIO
where  f.ci= '3905604' and f.tiene_cuenta_bancaria=1


--CREATE VIEW [dbo].[vwVerificarAutorizacionComision]
AS
      select  UA.id_usuario_autorizacion,
	          UA.id_usuario,
			  CASE WHEN AUC.id_autorizacion_comision IS NULL THEN 0 ELSE AUC.id_autorizacion_comision  END As 'id_autorizacion_comision',
			  CASE WHEN CO.id_ciclo IS NULL THEN 0 ELSE CO.id_ciclo  END As 'id_ciclo',
			  CASE WHEN CO.id_comision IS NULL THEN 0 ELSE CO.id_comision  END As 'id_comision',
			  AUC.id_estado_autorizacion_comision,
			  AUC.descripcion
		from BDMultinivel.dbo.USUARIO_AUTORIZACION UA
        LEFT JOIN BDMultinivel.dbo.AUTORIZACION_COMISION AUC on AUC.id_usuario_autorizacion=UA.id_usuario_autorizacion
		LEFT JOIN BDMultinivel.dbo.GP_COMISION CO ON Co.id_comision = AUC.id_comision
		where UA.estado='True' --and AUC.id_estado_autorizacion_comision=0 and UA.id_usuario=1 and CO.id_ciclo= 85




---tablas de autorizacion
select * from BDMultinivel.dbo.USUARIO
select * from BDMultinivel.dbo.USUARIO_AUTORIZACION UA
select * from BDMultinivel.dbo.AUTORIZACION_COMISION

select * from BDMultinivel.dbo.GP_COMISION
select * from BDMultinivel.dbo.TIPO_AUTORIZACION
select * from BDMultinivel.dbo.USUARIO

select * from BDMultinivel.dbo.AREA

select * from BDMultinivel.dbo.AUTORIZACIONES_AREA

--usuarios en gestor de comisiones
    --1 aguirre
	--2 mgnunez
	--3 ehumerez
	--4 rcarrasco
	--5 srios
	--6 rgonzalesm
	--7 jaflores
	--8 fverdi


	----------------------------------------------------------------------
	-- verificar la comision

	select * FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') 
	select *  FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ')



	    select * from BDMultinivel.dbo.LISTADO_FORMAS_PAGO where id_comisiones_detalle=12693 ---79
		select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL where id_lista_formas_pago=125

			select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL
			--delete BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL  --where id_lista_formas_pago=
		-------------------------------------------------
		---VERIFICAR METODO DE PAGO SION PAY
			select id_comision, CD.id_comision_detalle,FIC.id_ficha,FIC.nombres +' '+ FIC.apellidos AS 'nombre', FIC.ci,TIPO.id_tipo_pago, TIPO.nombre as 'tipo_pago', CIU.nombre AS 'ciudad', PAI.nombre AS 'pais', CD.monto_neto from BDMultinivel.dbo.GP_COMISION_DETALLE CD 
						INNER JOIN BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIFO ON LIFO.id_comisiones_detalle= CD.id_comision_detalle
						INNER JOIN  BDMultinivel.dbo.TIPO_PAGO TIPO ON TIPO.id_tipo_pago= LIFO.id_tipo_pago
						INNER JOIN BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha = CD.id_ficha
						left JOIN BDMultinivel.dbo.CIUDAD CIU ON CIU.id_ciudad = FIC.id_ciudad
						left JOIN BDMultinivel.dbo.PAIS PAI ON PAI.id_pais= CIU.id_pais
						where CD.id_comision= 92 AND LIFO.id_tipo_pago=1

--monto cero
SELECT * FROM  BDMultinivel.dbo.COMISION_DETALLE_EMPRESA WHERE id_comision_detalle = 12702
--
SELECT * FROM  BDMultinivel.dbo.COMISION_DETALLE_EMPRESA WHERE id_comision_detalle in (12693 )

 -- traer pais y ciuadad

 select * FROM OPENQUERY( [10.2.10.222], 'select * from pepais ')

 ----------------------------------------------------------------------------------
-- -- REPORTE de pago de comisiones por freelancer, de forma mensual , SELECT

			select 
				GPCOMI.id_ciclo,
				CI.nombre AS 'ciclo',
			   -- GPDETA.id_comision_detalle AS 'idComisionDetalle',
			   -- GPCOMI.id_comision AS 'idComision', 
				--GPCOMI.id_tipo_comision,
				--GPDETA.id_ficha AS 'idFicha',
				FIC.nombres +' '+ FIC.apellidos as 'nombre',
				CIU.nombre as 'ciudad',
				FIC.ci,
				CU.nro_cuenta,
				FIC.cuenta_bancaria AS 'cuentaBancaria',
				--FIC.id_banco,
				--BA.nombre AS 'nombreBanco',
				--GPDETA.monto_bruto AS 'montoBruto' ,
				case FIC.factura_habilitado when 1 then 'True' when 0  then'False' else  'False' END AS 'factura',
				GPDETA.monto_neto AS 'montoNeto',
				GPDETA.fecha_actualizacion as 'fecha',
				--CASE WHEN IDESTA.id_estado_comision_detalle IS NULL THEN 0 ELSE IDESTA.id_estado_comision_detalle END As 'estadoFacturoId',
				--CASE WHEN ESTANA.estado IS NULL THEN 'No registro estado' ELSE ESTANA.estado END As 'estadoDetalleFacturaNombre',			
				--GPESTA.id_estado_comision,
				--GPDETA.monto_retencion,
				--GPDETA.monto_aplicacion,
				--CASE WHEN LISTFO.id_lista_formas_pago IS NULL THEN 0 ELSE LISTFO.id_lista_formas_pago END As 'id_lista_formas_pago',
				--CASE WHEN LISTFO.id_tipo_pago IS NULL THEN 0 ELSE LISTFO.id_tipo_pago END As 'id_tipo_pago',			
				CASE WHEN TIPAGO.nombre IS NULL THEN 'NINGUNO' ELSE TIPAGO.nombre END As 'tipo_pago_descripcion'
				--CASE WHEN FPAGO.id IS NULL THEN 0 ELSE FPAGO.id END As 'id_detalle_estado_forma_pago',
				--CASE WHEN FPAGO.habilitado IS NULL THEN 'False' ELSE FPAGO.habilitado END As 'pago_detalle_habilitado',		
				--CASE WHEN FPAGO.id_estado_listado_forma_pago IS NULL THEN 0 ELSE FPAGO.id_estado_listado_forma_pago END As 'id_estado_listado_forma_pago'			
				from BDMultinivel.dbo.GP_COMISION GPCOMI
				inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GPESTA  ON GPESTA.id_comision = GPCOMI.id_comision
				inner join BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA ON GPDETA.id_comision = GPCOMI.id_comision
				inner join BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha= GPDETA.id_ficha
				left join BDMultinivel.dbo.BANCO BA ON BA.id_banco = FIC.id_banco
				inner join BDMultinivel.dbo.CICLO CI ON CI.id_ciclo = GPCOMI.id_ciclo
				left join BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I IDESTA ON IDESTA.id_comision_detalle=GPDETA.id_comision_detalle
				left join BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE ESTANA ON ESTANA.id_estado_comision_detalle = IDESTA.id_estado_comision_detalle
				left join BDMultinivel.dbo.LISTADO_FORMAS_PAGO LISTFO ON  LISTFO.id_comisiones_detalle = GPDETA.id_comision_detalle
				left join BDMultinivel.dbo.TIPO_PAGO TIPAGO ON TIPAGO.id_tipo_pago= LISTFO.id_tipo_pago
				LEFT join BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL FPAGO ON FPAGO.id_lista_formas_pago = LISTFO.id_lista_formas_pago
				LEFT join BDMultinivel.dbo.CIUDAD CIU ON CIU.id_ciudad = FIC.id_ciudad
				left join BDPuntosCash.DBO.CUENTA CU ON CU.id_usuario collate Modern_Spanish_CI_AS =FIC.Ci
				where IDESTA.habilitado = 'true' and GPESTA.habilitado= 'true' and TIPAGO.id_tipo_pago=1  and  FPAGO.habilitado= 1
	go

	select *, nro_cuenta FROM BDPuntosCash.DBO.CUENTA where id_usuario='5361144'

	CU.nro_cuenta
	Modern_Spanish_CI_AS

	-------------------------------------------------------------------------------
	-------------------------------------------------------------------------------
	--migrar ficha solo por ciclos comisionaddos

	SELECT  lcontacto_id,dtfechaadd, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena, Isnull(lcuentabanco,0) as 'lcuentabanco', lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja, lnit, ciclo_id   
	FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select co.lcontacto_id, co.dtfechaadd, co.stelefonofijo, co.stelefonomovil, co.scorreoelectronico, co.dtfechanacimiento, co.sdireccion, co.scedulaidentidad, co.lpatrocinante_id, co.lnivel_id, co.snombrecompleto, co.scontrasena, co.lcuentabanco, co.lcodigobanco, co.cbaja, co.dtfechabaja,co.ltipobaja, co.smotivobaja, co.lnit, comi.ciclo_id from grduit.administracioncontacto co inner join grduit.comision_forma_pago_view comi on co.lcontacto_id = comi.contacto_id  ') where ciclo_id=80 order by lcontacto_id asc 

	select * from BDMultinivel.dbo.vwObtenercomisionesFormaPago where id_ciclo=83 and id_tipo_comision=1 and id_tipo_pago=2 and id_estado_listado_forma_pago=3

	---------------------------------------------------------

	---LA PLANTILLA DETALLE CICLO
		SELECT * FROM BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=85 -- 85
		--delete BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=85 
		SELECT * FROM BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=85
		--delete BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=85

    