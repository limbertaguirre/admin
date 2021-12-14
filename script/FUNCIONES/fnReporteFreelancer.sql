SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[fnReporteFreelancer](@IDFICHA int)
returns TABLE
as
return(
select ciclo, tipo_pago, nro_cuenta, cuenta_banco, sum(monto_neto) as monto_neto, STRING_AGG(id_comision_detalle,',') as id_comision_detalle from(
    select distinct
    GPCD.id_comision_detalle,
    CI.nombre as ciclo,
    TIPAGO.nombre as tipo_pago,
    CU.nro_cuenta as nro_cuenta,
    FIC.cuenta_bancaria as cuenta_banco,
    GPCD.monto_neto as monto_neto
from BDMultinivel.dbo.GP_COMISION_DETALLE GPCD
    INNER JOIN BDMultinivel.dbo.GP_COMISION GPC ON GPC.id_comision = GPCD.id_comision
    INNER JOIN BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GCEC on GCEC.id_comision = GPC.id_comision
    INNER JOIN BDMultinivel.dbo.CICLO CI ON CI.id_ciclo = GPC.id_ciclo
    INNER JOIN BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha = GPCD.id_ficha
    LEFT JOIN BDMultinivel.dbo.LISTADO_FORMAS_PAGO LISTFO ON LISTFO.id_comisiones_detalle = GPCD.id_comision_detalle
    LEFT JOIN BDMultinivel.dbo.TIPO_PAGO TIPAGO ON TIPAGO.id_tipo_pago = LISTFO.id_tipo_pago
    LEFT JOIN BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL FPAGO  ON FPAGO.id_lista_formas_pago = LISTFO.id_lista_formas_pago
    LEFT JOIN BDPuntosCash.dbo.CUENTA CU ON CU.id_usuario COLLATE Modern_Spanish_CI_AS = FIC.Ci
 where (TIPAGO.id_tipo_pago = 1 or TIPAGO.id_tipo_pago = 2) 
    AND(FPAGO.id_estado_listado_forma_pago = 3 or GCEC.id_estado_comision IN (9, 13))
    AND GPCD.id_ficha = @IDFICHA
) DAT group by ciclo, tipo_pago, nro_cuenta, cuenta_banco)
GO
