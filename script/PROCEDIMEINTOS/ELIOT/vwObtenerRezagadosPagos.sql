SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER VIEW [dbo].[vwObtenerRezagadosPagos]
as
select 
c.id_comision,
c.id_tipo_comision,
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
where cde.monto_neto <> 0
--and c.id_tipo_comision = 2
and cec.id_estado_comision = 9 -- REZAGADOS DE FORMAS PAGOS
--and l.id_lista_formas_pago not in (select dl.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl where dl.habilitado = 1 and dl.id_estado_listado_forma_pago = 1)
group by l.id_comisiones_detalle, cde.id_comision_detalle_empresa, cde.estado, f.codigo_cnx, f.cuenta_bancaria, b.nombre, c.id_ciclo, f.nombres, f.apellidos, f.ci, l.monto_neto, cde.id_empresa,
e.nombre, ci.nombre, l.id_tipo_pago, b.id_banco, b.codigo, ciu.id_pais, ciu.codigo, cde.fecha_pago, l.id_lista_formas_pago, c.id_comision, ci.nombre, c.fecha_creacion, c.fecha_actualizacion,
dl.id_estado_listado_forma_pago, dl.habilitado, c.id_tipo_comision

GO
