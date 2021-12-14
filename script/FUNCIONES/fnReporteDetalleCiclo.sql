SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnReporteDetalleCiclo](@IDCOMISIONDETALLE VARCHAR(MAX))
 RETURNS TABLE 
 AS
 RETURN(
 SELECT
        CDE.id_comision_detalle_empresa,
        CDE.monto_neto,
        EMP.nombre as nombre_empresa,
        GTC.nombre as tipo_comision,
        GTC.id_tipo_comision
    FROM BDMultinivel.dbo.COMISION_DETALLE_EMPRESA CDE
        INNER JOIN BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA on GPDETA.id_comision_detalle = CDE.id_comision_detalle
        INNER JOIN BDMultinivel.dbo.GP_COMISION GPCOMI on GPCOMI.id_comision = GPDETA.id_comision
        INNER JOIN BDMultinivel.dbo.GP_TIPO_COMISION GTC on GTC.id_tipo_comision = GPCOMI.id_tipo_comision
        INNER JOIN BDMultinivel.dbo.EMPRESA EMP on EMP.id_empresa = CDE.id_empresa
    where CDE.id_comision_detalle IN (SELECT value from STRING_SPLIT(@IDCOMISIONDETALLE, ',')))
GO
