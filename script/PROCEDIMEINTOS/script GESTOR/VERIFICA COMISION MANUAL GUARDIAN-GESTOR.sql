
select * from BDMultinivel.dbo.ciclo
--codigo contactos
select * from BDMultinivel.dbo.FICHA where ci='6582143'  --idficha: 4645
--ci 
--------------------------------------------------------------------------------------------
---------GUARDIAN VERIFICAR COMISION
-- verificar comision frelanzer por ciclo
--codigo 2266 , 2540 , 181 , 95
	SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar  FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit.comision_empresa_forma_pago_view ') 
	where ciclo_id=82  and contacto_id=181  group by contacto_id

--lista freelancer por ciclo empresa
	SELECT *   FROM OPENQUERY(  [SRV-GUARDIAN-TEST], 'select * from grdsion.empresa_conexion ') 
	where ciclo_id=82  AND contacto_id= 181

	SELECT * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.empresa_conexion ')
--------------------------------------------------------------------------
------verificar multinivel LOCAL
select * from BDMultinivel.dbo.FICHA where ci='7814845' --codigo181 ficha = 4023
select * from BDMultinivel.dbo.GP_COMISION where  id_ciclo=82

declare @idficha INT; SET @idficha=4024;
declare @idciclo INT; set @idciclo=82;
declare @idcomision int; SET @idcomision=0;
select @idcomision= id_comision from BDMultinivel.dbo.GP_COMISION where id_tipo_comision=1 and id_ciclo=@idciclo

select * from BDMultinivel.dbo.GP_COMISION CO
         inner join BDMultinivel.dbo.GP_COMISION_DETALLE DE on CO.id_comision=DE.id_comision where CO.id_ciclo=@idciclo and DE.id_comision=@idcomision and id_ficha=@idficha
----verificar por empresa
select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle=7463

select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_empresa=0

------------------------------------------------------------------------------------------------
-- comision por codigo frelancers

	select * from BDMultinivel.dbo.FICHA where id_ficha=4024
	SELECT * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.comision_forma_pago_view ')    where ciclo_id=80  AND contacto_id= 181
	SELECT * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.comision_empresa_forma_pago_view ') where  ciclo_id=82  AND contacto_id= 181
----------------------------------------------------------------------------------------------------
-----migrar solo los contactos comisionados,  las ficha solo por ciclos comisionaddos

	SELECT  lcontacto_id,dtfechaadd, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena, Isnull(lcuentabanco,0) as 'lcuentabanco', lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja, lnit, ciclo_id   
	FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select co.lcontacto_id, co.dtfechaadd, co.stelefonofijo, co.stelefonomovil, co.scorreoelectronico, co.dtfechanacimiento, co.sdireccion, co.scedulaidentidad, co.lpatrocinante_id, co.lnivel_id, co.snombrecompleto, co.scontrasena, co.lcuentabanco, co.lcodigobanco, co.cbaja, co.dtfechabaja,co.ltipobaja, co.smotivobaja, co.lnit, comi.ciclo_id from grduit.administracioncontacto co inner join grduit.comision_forma_pago_view comi on co.lcontacto_id = comi.contacto_id  ') where ciclo_id=80 order by lcontacto_id asc 

	-----------------------------------------------------------------------
	-----verificar el estado de pago detalle

	--idcomisiondetalle :9906
	select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision_detalle=9906
	select * from BDMultinivel.dbo.listado_formas_pago where id_comisiones_detalle=9906 --id 1295
	select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL where id_lista_formas_pago=1295

	select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision_detalle=9910
	select * from BDMultinivel.dbo.listado_formas_pago where id_comisiones_detalle=9910 --id 1295
    select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL where id_lista_formas_pago=1297

	select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision_detalle=10144
	select * from BDMultinivel.dbo.listado_formas_pago where id_lista_formas_pago= 1312
	select * from BDMultinivel.dbo.listado_formas_pago where id_lista_formas_pago= 1313
	 select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL where id_lista_formas_pago in (1312,1313 )

   ---obtener la comision vieja y nueva totales
      DECLARE @ID_COMISION_ACTUAL INT;
	  DECLARE @TOTAL_NETO_COMISION_ACTUAL DECIMAL(18,2);	
	  DECLARE @TOTAL_NETO_COMISION_NUEVO_RECHAZADO DECIMAL(18,2);	
	  DECLARE @ID_COMISION_NEW_RESAGADO INT;

	   select @ID_COMISION_ACTUAL = CO.id_comision, @TOTAL_NETO_COMISION_ACTUAL= CO.monto_total_neto  from BDMultinivel.dbo.GP_COMISION CO
	   inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I COE ON COE.id_comision = CO.id_comision
	   where id_ciclo= 83 and id_tipo_comision=1 and id_estado_comision=8

	   select @ID_COMISION_ACTUAL as 'idcomision actual', @TOTAL_NETO_COMISION_ACTUAL as 'totalneto actual'

	   select COM.monto_total_neto, * from BDMultinivel.dbo.GP_COMISION COM where COM.id_comision=@ID_COMISION_ACTUAL --select update
	   --------------------------------------------------------------------------------------------------------------------------
	   --------------------------------------------------------------------------------------------------------------
	    select * from BDMultinivel.dbo.GP_COMISION where id_ciclo=81
		 select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision= 113

	 --  select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I

	   	select COUNT(vwF.idComisionDetalle)from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=83 and vwF.id_tipo_comision=1 and id_tipo_pago=0 --los resagados forma de pagos
	    select COUNT(idComisionDetalle) from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=83 and vwF.id_tipo_comision=1 and id_tipo_pago != 0 --total los que tienen pagado

	    select sum(vwF.montoNeto) from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=83 and vwF.id_tipo_comision=1 and id_tipo_pago=0 --total los resagados forma de pagos
	    select sum(vwF.montoNeto) from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=83 and vwF.id_tipo_comision=1 and id_tipo_pago != 0 --total los que tienen pagado

		----------------------------------------------------------------------------
		---obtener RESAGADOS SIN FORMA DE PAGO ciclo 80 resultado 97

		 select * from BDMultinivel.dbo.GP_COMISION 

		 SELECT CODE.*, CES.* FROM BDMultinivel.dbo.GP_COMISION Co
		 inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= Co.id_comision
		 inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I CES ON CES.id_comision=Co.id_comision
		 where CES.id_estado_comision=9 and Co.id_tipo_comision = 2

		----------------------------------------------------------------------
		--obtener list comisiones sion pay
		    SELECT CODE.*,LIS.* FROM BDMultinivel.dbo.GP_COMISION Co
			 inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= Co.id_comision
			 inner join BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIS on LIS.id_comisiones_detalle = CODE.id_comision_detalle
			 where LIS.id_tipo_pago=1 and Co.id_tipo_comision = 1
	    --obtener list comisiones detalle empresa sion pay
		    SELECT DEM.* FROM BDMultinivel.dbo.GP_COMISION Co
			 inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= Co.id_comision
			 inner join BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIS on LIS.id_comisiones_detalle = CODE.id_comision_detalle
			 inner join BDMultinivel.dbo.COMISION_DETALLE_EMPRESA DEM on DEM.id_comision_detalle=CODE.id_comision_detalle
			 where LIS.id_tipo_pago=1 and Co.id_tipo_comision = 1
			 

		 ---------------------------------------------------------
		 -----verificar detalle  de comision

		 --tabla base excel
	      select * from BDPuntosCash.DBO.COMISIONES_XLS where lciclo_id=80
		  --maestro
		  SELECT * FROM BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=80
		  --detalle
		  SELECT * FROM BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=80
		  go

		  select * from BDPuntosCash.dbo.movimiento order by fecha_registro desc

		  ----------------
		  ---obtener comision selecionados por sion pay con el GUARDIAN contacto
		   --  FI.nombres,CODE.*,LIS.*
		    select * from 
			( 
				SELECT FI.codigo  , Fi.id_ficha, Fi.nombres +' '+ FI.apellidos as 'nombre completo' FROM BDMultinivel.dbo.GP_COMISION Co
				 inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= Co.id_comision
				 inner join BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIS on LIS.id_comisiones_detalle = CODE.id_comision_detalle
				 inner join BDMultinivel.dbo.FICHA FI on FI.id_ficha= CODE.id_ficha
				 where CO.id_ciclo=80 and LIS.id_tipo_pago=1 and Co.id_tipo_comision = 1
			 )dat
			inner join (
				SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar  
				FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit.comision_empresa_forma_pago_view ') 
				where ciclo_id=80  
				group by contacto_id
			) dat2 on dat.codigo = dat2.contacto_id
		 ---- detalle GUARDIAN
		  ---obtener comision selecionados por sion pay con el guardian contacto
		   --  FI.nombres,CODE.*,LIS.*
		    select * from 
			( 
				SELECT FI.codigo, Fi.id_ficha, Fi.nombres +' '+ FI.apellidos as 'nombre completo'   FROM BDMultinivel.dbo.GP_COMISION Co
				 inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= Co.id_comision
				 inner join BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIS on LIS.id_comisiones_detalle = CODE.id_comision_detalle
				 inner join BDMultinivel.dbo.FICHA FI on FI.id_ficha= CODE.id_ficha
				 where CO.id_ciclo=80 and LIS.id_tipo_pago=1 and Co.id_tipo_comision = 1
			 )dat
			LEFT join (
				SELECT contacto_id ,empresa_id, total_neto
				FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit.comision_empresa_forma_pago_view ') 
				where ciclo_id=80  
			--	group by contacto_id
			) dat2 on dat.codigo = dat2.contacto_id


			select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL 
			select * from BDMultinivel.dbo.ESTADO_LISTADO_FORMA_PAGO

			BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL
     
	        select id_estado_listado_forma_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL where id=1510







		  .
