
DECLARE @totalMontoVentaDirecta DECIMAL(18,2);
DECLARE @totalMontoVentaGrupo DECIMAL(18,2);
DECLARE @totalMontoVentaBonoResidual DECIMAL(18,2)
DECLARE @totalMontoBruto DECIMAL(18,2)
DECLARE @totalMontoAplicacion DECIMAL(18,2)
DECLARE @porcentajeRetencion DECIMAL(18,2)
DECLARE @MontoTotalRetencion DECIMAL(18,2)

DECLARE @idCiclo INT

SET @totalMontoVentaDirecta=0;
SET @totalMontoVentaGrupo=0;
SET @totalMontoVentaBonoResidual=0;
SET @totalMontoBruto=0;
SET @totalMontoAplicacion=0;

SET @porcentajeRetencion=15;
SET @MontoTotalRetencion=0;

	SELECT  @totalMontoVentaDirecta = SUM(dcomision)  FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80')--COMISION DE VENTA DIRECTA/
	SELECT @totalMontoVentaGrupo = SUM(dcomision) FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80') --COMISION DE VENTA DE GRUPO/
	SELECT @totalMontoVentaBonoResidual= SUM(dtotalbono) FROM OPENQUERY( [10.2.10.222], 'select * from administracionbonoresidual where lciclo_id = 80')----COMISION DE BONO RESIDUAL/
	SELECT @totalMontoAplicacion =SUM(dtotal)  FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo where lciclo_id = 80')

	SET @totalMontoBruto = @totalMontoVentaDirecta + @totalMontoVentaGrupo +@totalMontoVentaBonoResidual
	SET @MontoTotalRetencion= (@totalMontoBruto * @porcentajeRetencion) / 100;

	select  @totalMontoBruto as 'montoTotalBruto', 
	       @totalMontoAplicacion as 'montoTotalAplicacion',
		   @porcentajeRetencion as 'porcentajeRetencion',
		   @MontoTotalRetencion as 'monyoTotalRetencion'