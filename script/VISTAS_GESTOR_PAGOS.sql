
CREATE VIEW [dbo].[vwObtenercomisiones]
AS
     select 
	        GPDETA.id_comision_detalle AS 'idComisionDetalle',
	        GPCOMI.id_comision AS 'idComision', 
			GPCOMI.id_tipo_comision,
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
	     GPDE.id_comision as 'idComision',
	     ComiEmp.id_comision_detalle_empresa,
		 ComiEmp.id_comision_detalle,
	     Emp.nombre AS 'empresa',
		 ComiEmp.monto,
		 ComiEmp.monto_a_facturar,
		 ComiEmp.monto_total_facturar,
		 ComiEmp.respaldo_path,
		 ComiEmp.nro_autorizacion,
		 Emp.id_empresa AS 'idEmpresa',
		 Emp.estado AS 'estadoEmpresa',
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
		E.estado,
		CC.id_tipo_comision
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

 CREATE VIEW [dbo].[vwObtenercomisionesFormaPago]
  AS
        select 
	        GPDETA.id_comision_detalle AS 'idComisionDetalle',
	        GPCOMI.id_comision AS 'idComision', 
			GPCOMI.id_tipo_comision,
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
			GPDETA.monto_aplicacion,
			CASE WHEN LISTFO.id_lista_formas_pago IS NULL THEN 0 ELSE LISTFO.id_lista_formas_pago END As 'id_lista_formas_pago',
			CASE WHEN LISTFO.id_tipo_pago IS NULL THEN 0 ELSE LISTFO.id_tipo_pago END As 'id_tipo_pago',			
			CASE WHEN TIPAGO.nombre IS NULL THEN 'NINGUNO' ELSE TIPAGO.nombre END As 'tipo_pago_descripcion',
			CASE WHEN FPAGO.id IS NULL THEN 0 ELSE FPAGO.id END As 'id_detalle_estado_forma_pago',
			CASE WHEN FPAGO.habilitado IS NULL THEN 'False' ELSE FPAGO.habilitado END As 'pago_detalle_habilitado',		
			CASE WHEN FPAGO.id_estado_listado_forma_pago IS NULL THEN 0 ELSE FPAGO.id_estado_listado_forma_pago END As 'id_estado_listado_forma_pago'			
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
			where IDESTA.habilitado = 'true' and GPESTA.habilitado= 'true'
go

 CREATE VIEW [dbo].[vwVerificarCuentasUsuario]
 AS
	select
	       f.ci,
	       f.nombres,
           f.tiene_cuenta_bancaria, 
		   CASE WHEN u.id_usuario IS NULL THEN 'False' ELSE 'True' END As 'sionPay',
		   CASE WHEN u.id_estado_usuario IS NULL THEN 0 ELSE u.id_estado_usuario  END As 'estadoSionPay'
from  BDMultinivel.dbo.ficha f
left join BDPuntosCash.dbo.USUARIO u on   u.id_usuario COLLATE Latin1_General_CI_AS =   f.ci COLLATE Latin1_General_CI_AS

go

    CREATE VIEW [dbo].VW_LISTAR_AUTORIZACIONES_TIPO
    AS
    select USUA.id_usuario_autorizacion,AREA.id_area,AREA.nombre AS 'descripcion_area', USUA.estado, U.id_usuario, U.nombres, U.apellidos, U.usuario,TA.id_tipo_autorizacion,TA.nombre AS 'nombre_tipo_autorizacion',  USUA.fecha_creacion  FROM BDMultinivel.dbo.USUARIO_AUTORIZACION USUA 
		INNER JOIN BDMultinivel.dbo.USUARIO U ON USUA.id_usuario= U.id_usuario
		INNER JOIN BDMultinivel.dbo.TIPO_AUTORIZACION TA ON TA.id_tipo_autorizacion= USUA.id_tipo_autorizacion
		INNER JOIN BDMultinivel.dbo.AREA AREA ON AREA.id_area = U.id_area
		where TA.estado='True'
go

    CREATE VIEW [dbo].VW_TIPO_AUTORIZACION
    AS
    select TP.id_tipo_autorizacion,tp.nombre, TP.cantidad as 'cantidad_limite',AA.cantidad as 'cantidad_aprobacion_minima_area'  from BDMultinivel.dbo.TIPO_AUTORIZACION TP
			 inner join  BDMultinivel.dbo.AUTORIZACIONES_AREA AA on AA.id_tipo_autorizacion = TP.id_tipo_autorizacion
			 where TP.estado='True'

go

	CREATE VIEW [dbo].[vwVerificarAutorizacionComision]
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
	where UA.estado='True' 

go
   CREATE VIEW [dbo].[vwObtenerInfoExcelFormatoBanco]
	as
	select 
	c.id_ciclo,
	cde.id_empresa,
	TRIM(e.nombre) as empresa,
	l.id_lista_formas_pago,
	l.id_comisiones_detalle,
	cde.id_comision_detalle_empresa,
	cde.estado as id_estado_comision_detalle_empresa,
	f.id_ficha,
	f.codigo_cnx as [CODIGO_DE_CLIENTE],
	b.nombre as NombreBanco,
	f.cuenta_bancaria AS [NRO_DE_CUENTA],
	f.nombres +' '+ f.apellidos as [NOMBRE_DE_CLIENTE],
	f.ci as [DOC_DE_IDENTIDAD],
	sum(cde.monto_neto) as [IMPORTE_POR_EMPRESA],
	l.monto_neto as [IMPORTE_NETO],
	CAST(DATEPART(DAY,  cde.fecha_pago) as VARCHAR) + '/' + CAST(DATEPART(MONTH,  cde.fecha_pago) as VARCHAR) + '/' + CAST(DATEPART(YYYY,  cde.fecha_pago) as VARCHAR) as [FECHA_DE_PAGO],
	-- id_banco = 17 BANCO GANADERO
	case when isnull(b.id_banco, 0) = 17 then 1 else 3 end FORMA_DE_PAGO,
	case when isnull(b.id_banco, 0) = 17 then '' else '2' end MONEDA_DESTINO,
	case when isnull(b.id_banco, 0) = 17 then '' else b.codigo end ENTIDAD_DESTINO,
	-- Sucursal
	case when isnull(b.id_banco, 0) = 17 then '' else case when isnull(ciu.id_pais, -1) = 1 then ciu.codigo else '' end end SUCURSAL_DESTINO,
	ci.nombre as GLOSA,
	l.id_tipo_pago
	from LISTADO_FORMAS_PAGO l
	inner join GP_COMISION_DETALLE cd on cd.id_comision_detalle = l.id_comisiones_detalle
	inner join GP_COMISION c on c.id_comision = cd.id_comision
	inner join GP_COMISION_ESTADO_COMISION_I cec on cec.id_comision = c.id_comision
	inner join BDMultinivel.dbo.CICLO ci on ci.id_ciclo = c.id_ciclo
	inner join BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde on cde.id_comision_detalle = cd.id_comision_detalle
	inner join BDMultinivel.dbo.EMPRESA e on e.id_empresa = cde.id_empresa
	inner join BDMultinivel.dbo.FICHA f on f.id_ficha = cd.id_ficha
	inner join BDMultinivel.dbo.CIUDAD ciu on ciu.id_ciudad = f.id_ciudad
	left join BDMultinivel.dbo.BANCO b on b.id_banco = f.id_banco
	where cde.monto_neto <> 0
	and c.id_tipo_comision = 1
	and cec.id_estado_comision = 10 -- CERRADO FORMA DE PAGO
	and l.id_lista_formas_pago not in (select dl.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl where dl.habilitado = 1 and dl.id_estado_listado_forma_pago = 1)
	group by l.id_lista_formas_pago, l.id_comisiones_detalle, cde.id_comision_detalle_empresa, cde.estado, f.codigo_cnx, b.nombre, f.cuenta_bancaria, c.id_ciclo, f.nombres, f.apellidos, f.ci, l.monto_neto, cde.id_empresa, e.nombre, ci.nombre, l.id_tipo_pago, b.id_banco, b.codigo, ciu.id_pais, ciu.codigo, cde.fecha_pago, f.id_ficha

  GO

    CREATE view [dbo].[vwObtenerEmpresasComisionesDetalleEmpresa]
        as
        select c.id_comision, c.id_ciclo
		, cde.id_empresa
		, TRIM(e.nombre) as empresa
		, c.id_tipo_comision
		, l.id_tipo_pago
		, cec.id_estado_comision
		, sum(cde.monto_neto) monto_transferir
		from LISTADO_FORMAS_PAGO l
		inner join GP_COMISION_DETALLE cd on cd.id_comision_detalle = l.id_comisiones_detalle
		inner join GP_COMISION c on c.id_comision = cd.id_comision
		inner join GP_COMISION_ESTADO_COMISION_I cec on cec.id_comision = c.id_comision
		inner join BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde on cde.id_comision_detalle = cd.id_comision_detalle
		inner join BDMultinivel.dbo.EMPRESA e on e.id_empresa = cde.id_empresa
		where l.monto_neto <> 0
		and l.id_lista_formas_pago not in (select dl.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl where dl.habilitado = 1 and dl.id_estado_listado_forma_pago = 1)
		group by c.id_ciclo, cde.id_empresa, e.nombre , c.id_tipo_comision, l.id_tipo_pago, cec.id_estado_comision, c.id_comision
GO

  CREATE VIEW [dbo].[vwObtenerRezagadosPagos]
	as
	select 
	c.id_comision,
	c.id_tipo_comision,
	cec.id_estado_comision,
	c.id_ciclo,
	ci.nombre,
	cde.id_empresa,
	TRIM(e.nombre) as empresa,
	l.id_lista_formas_pago,
	dl.id_estado_listado_forma_pago,
	dl.habilitado estado_listado_forma_pago_habilitado,
	l.id_comisiones_detalle,
	cde.id_comision_detalle_empresa,
	cde.estado as id_estado_comision_detalle_empresa,
	l.id_tipo_pago,
	c.fecha_creacion fecha_creacion_comision,
	c.fecha_actualizacion fecha_actualizacion_comision,
	f.id_ficha,
	f.codigo_cnx as [CODIGO_DE_CLIENTE],
	f.cuenta_bancaria AS [NRO_DE_CUENTA],
	b.nombre AS NOMBRE_BANCO,
	f.nombres +' '+ f.apellidos as [NOMBRE_DE_CLIENTE],
	f.ci as [DOC_DE_IDENTIDAD],
	sum(cde.monto_neto) as [IMPORTE_POR_EMPRESA],
	l.monto_neto as [IMPORTE_NETO],
	CAST(DATEPART(DAY,  cde.fecha_pago) as VARCHAR) + '/' + CAST(DATEPART(MONTH,  cde.fecha_pago) as VARCHAR) + '/' + CAST(DATEPART(YYYY,  cde.fecha_pago) as VARCHAR) as [FECHA_DE_PAGO],
	-- id_banco = 17 BANCO GANADERO
	case when isnull(b.id_banco, 0) = 17 then 1 else 3 end FORMA_DE_PAGO,
	case when isnull(b.id_banco, 0) = 17 then '' else '2' end MONEDA_DESTINO,
	case when isnull(b.id_banco, 0) = 17 then '' else b.codigo end ENTIDAD_DESTINO,
	-- Sucursal
	case when isnull(b.id_banco, 0) = 17 then '' else case when isnull(ciu.id_pais, -1) = 1 then ciu.codigo else '' end end SUCURSAL_DESTINO,
	ci.nombre as GLOSA
	from LISTADO_FORMAS_PAGO l
	left join BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl on dl.id_lista_formas_pago = l.id_lista_formas_pago
	inner join GP_COMISION_DETALLE cd on cd.id_comision_detalle = l.id_comisiones_detalle
	inner join GP_COMISION c on c.id_comision = cd.id_comision
	inner join GP_COMISION_ESTADO_COMISION_I cec on cec.id_comision = c.id_comision
	inner join BDMultinivel.dbo.CICLO ci on ci.id_ciclo = c.id_ciclo
	inner join BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde on cde.id_comision_detalle = cd.id_comision_detalle
	inner join BDMultinivel.dbo.EMPRESA e on e.id_empresa = cde.id_empresa
	inner join BDMultinivel.dbo.FICHA f on f.id_ficha = cd.id_ficha
	inner join BDMultinivel.dbo.CIUDAD ciu on ciu.id_ciudad = f.id_ciudad
	left join BDMultinivel.dbo.BANCO b on b.id_banco = f.id_banco
	where 
	--cde.monto_neto <> 0
	c.id_tipo_comision = 2 -- TIPO COMISION REZAGADO
	--and cec.id_estado_comision = 9 -- REZAGADOS DE PAGOS
	--and l.id_lista_formas_pago not in (select dl.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl where dl.habilitado = 1 and dl.id_estado_listado_forma_pago = 1)
	group by l.id_comisiones_detalle, cde.id_comision_detalle_empresa, cde.estado, f.codigo_cnx, f.cuenta_bancaria, b.nombre, c.id_ciclo, f.nombres, f.apellidos, f.ci, l.monto_neto, cde.id_empresa,
	e.nombre, ci.nombre, l.id_tipo_pago, b.id_banco, b.codigo, ciu.id_pais, ciu.codigo, cde.fecha_pago, l.id_lista_formas_pago, c.id_comision, ci.nombre, c.fecha_creacion, c.fecha_actualizacion,
	dl.id_estado_listado_forma_pago, dl.habilitado, c.id_tipo_comision, f.id_ficha, cec.id_estado_comision

GO
    CREATE VIEW [dbo].[vwObtenerCiclosRezagados]
	AS
		SELECT
			cc.id_comision,
			C.id_ciclo,
			C.nombre,
			C.descripcion,
			E.id_estado_comision,
			E.estado,
			CC.id_tipo_comision
		FROM BDMultinivel.DBO.CICLO C
			INNER JOIN BDMultinivel.DBO.GP_COMISION CC ON C.id_ciclo = CC.id_ciclo
			INNER JOIN BDMultinivel.DBO.GP_COMISION_DETALLE cd on cd.id_comision = cc.id_comision
			INNER JOIN BDMultinivel.DBO.GP_TIPO_COMISION T ON CC.id_tipo_comision = T.id_tipo_comision
			INNER JOIN BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I CE ON CE.id_comision = CC.id_comision
			INNER JOIN BDMultinivel.DBO.GP_ESTADO_COMISION E ON E.id_estado_comision = CE.id_estado_comision
			INNER JOIN BDMultinivel.DBO.LISTADO_FORMAS_PAGO l ON l.id_comisiones_detalle = cd.id_comision_detalle
		WHERE CE.habilitado='true' and t.id_tipo_comision = 2 -- TipoComisionRezagados
		GROUP BY cc.id_comision,
			C.id_ciclo,
			C.nombre,
			C.descripcion,
			E.id_estado_comision,
			E.estado,
			CC.id_tipo_comision
GO



-------------------------------------------------------------------------------------------------------------------------------------------
USE [BDMultinivel];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
ALTER view [dbo].[vwPagosIncentivos] as
select (f.nombres + ' ' + f.apellidos) as nombre_completo
,c.id_comision AS id_comision
, f.ci as cedula_identidad
,f.cuenta_bancaria as cuenta_banco
, banco.nombre as banco
,cd.monto_neto as monto_total_neto
,ipc.id_tipo_incentivo_pago
,tip.descripcion as  TipoIncentivo
,tp.nombre as tipo_pago
,c.id_ciclo as idCiclo
,tip.id_tipo_incentivo
,(case WHEN (gdel.id_estado_listado_forma_pago = 3)
  THEN 'pagado' 
  ELSE 'no pagado'
END)
 AS comisionPagada,
(case WHEN (cuentaSpay.id_usuario is not null)
  THEN convert(bit, 1)
  ELSE convert(bit, 0)
END) as cuentaSionPay

from GP_COMISION as c
inner join  GP_COMISION_DETALLE  as cd
on c.id_comision = cd.id_comision
inner join GP_COMISION_ESTADO_COMISION_I as ces
on ces.id_comision = c.id_comision
inner join FICHA as f
on cd.id_ficha = f.id_ficha
left join BDPuntosCash.dbo.CUENTA as cuentaSpay
on f.ci collate SQL_Latin1_General_CP1_CI_AS  = cuentaSpay.id_usuario collate SQL_Latin1_General_CP1_CI_AS
inner join GP_COMISION_DETALLE_ESTADO_I as cde
on cd.id_comision_detalle = cde.id_comision_detalle
inner join LISTADO_FORMAS_PAGO as lfp
on cd.id_comision_detalle = lfp.id_comisiones_detalle

left join GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL as gdel
on lfp.id_lista_formas_pago = gdel.id_lista_formas_pago

inner join INCENTIVO_PAGO_COMISION ipc
on cd.id_comision_detalle = ipc.id_comision_detalle
inner join TIPO_INCENTIVO_PAGO as tip
on ipc.id_tipo_incentivo_pago = tip.id_tipo_incentivo
inner join TIPO_PAGO as tp
on lfp.id_tipo_pago = tp.id_tipo_pago
left join BANCO as banco
on f.id_banco = banco.id_banco

where c.id_tipo_comision = 3 
and cde.id_estado_comision_detalle = 7
--and c.id_comision=1193
--and ces.id_estado_comision=14 
and lfp.id_tipo_pago=1
GO