USE BDMultinivel;
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