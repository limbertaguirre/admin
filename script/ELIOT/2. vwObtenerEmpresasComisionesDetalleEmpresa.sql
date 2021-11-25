USE BDMultinivel;
GO
CREATE view [dbo].[vwObtenerEmpresasComisionesDetalleEmpresa]
as
select c.id_ciclo
, cde.id_empresa
, TRIM(e.nombre) as empresa
, c.id_tipo_comision
, l.id_tipo_pago
, sum(cde.monto_neto) monto_transferir
from LISTADO_FORMAS_PAGO l
inner join GP_COMISION_DETALLE cd on cd.id_comision_detalle = l.id_comisiones_detalle
inner join GP_COMISION c on c.id_comision = cd.id_comision
inner join BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde on cde.id_comision_detalle = cd.id_comision_detalle
inner join BDMultinivel.dbo.EMPRESA e on e.id_empresa = cde.id_empresa
where l.monto_neto <> 0
and l.id_lista_formas_pago not in (select dl.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl where dl.habilitado = 1 and dl.id_estado_listado_forma_pago = 1)
group by c.id_ciclo, cde.id_empresa, e.nombre , c.id_tipo_comision
, l.id_tipo_pago

GO
