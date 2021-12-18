SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fnReportePorCiclo](@IDCICLO int, @MODE int)
returns table 
as 
return (SELECT nombres, apellidos, ci, nro_cuenta,cuenta_bancaria, nombre as tipo_pago, id_tipo_pago, SUM(monto_neto) as monto_neto, STRING_AGG(id_comision_detalle, ',') as id_comision_detalle 
FROM (
select DISTINCT
        GPDETA.id_comision_detalle,
        FIC.nombres,
        FIC.apellidos,
        FIC.ci,
        GPDETA.monto_neto,
        CU.nro_cuenta,
        FIC.cuenta_bancaria,
        TIPAGO.nombre,
        TIPAGO.id_tipo_pago
    from BDMultinivel.dbo.CICLO CI 
        INNER JOIN BDMultinivel.dbo.GP_COMISION GPCOMI on GPCOMI.id_ciclo = CI.id_ciclo
        INNER JOIN BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA on GPDETA.id_comision = GPCOMI.id_comision
        INNER JOIN BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GCEC on GCEC.id_comision = GPCOMI.id_comision
        INNER JOIN BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha = GPDETA.id_ficha
        LEFT JOIN BDMultinivel.dbo.LISTADO_FORMAS_PAGO LISTFO ON LISTFO.id_comisiones_detalle = GPDETA.id_comision_detalle
        LEFT JOIN BDMultinivel.dbo.TIPO_PAGO TIPAGO ON TIPAGO.id_tipo_pago = LISTFO.id_tipo_pago
        LEFT JOIN BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL FPAGO  ON FPAGO.id_lista_formas_pago = LISTFO.id_lista_formas_pago
        LEFT JOIN BDPuntosCash.dbo.CUENTA CU ON CU.id_usuario COLLATE Modern_Spanish_CI_AS = FIC.Ci
    where (TIPAGO.id_tipo_pago = 1 or TIPAGO.id_tipo_pago = 2) 
        AND 
        (FPAGO.id_estado_listado_forma_pago =3 or 
        GCEC.id_estado_comision IN (9, 13)
        )
        and gcec.habilitado = 1
        AND ((@MODE = 3 and (GPCOMI.id_tipo_comision = 2 or GPCOMI.id_tipo_comision=1)) or (@MODE < 3 and GPCOMI.id_tipo_comision = @MODE))
        AND CI.id_ciclo = @IDCICLO) DAT 
        group by nombres, apellidos, ci, nro_cuenta,cuenta_bancaria, nombre, id_tipo_pago)
GO
