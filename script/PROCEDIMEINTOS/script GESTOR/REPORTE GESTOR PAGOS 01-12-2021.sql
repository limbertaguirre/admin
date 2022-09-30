
-----------------------------------------------------
--REPORTE  GESTOR DE PAGOS POR SION PAY Y TRANSFERENCIA

SELECT GPCOMI.id_ciclo,
       CI.nombre AS 'ciclo',
       FIC.nombres + ' ' + FIC.apellidos AS 'nombre',
       CIU.nombre AS 'ciudad',
       FIC.ci,
       CASE WHEN TIPAGO.nombre LIKE '%Sion pay%' 
        THEN CU.nro_cuenta collate Modern_Spanish_CI_AS
        ELSE FIC.cuenta_bancaria
      END
        AS 'cuentaBancaria',
       CASE FIC.factura_habilitado
          WHEN 1 THEN 'Factura'
          WHEN 0 THEN 'No factura'
          ELSE ''
       END
          AS 'factura',
       GPDETA.monto_neto AS 'montoNeto',
       GPDETA.fecha_actualizacion AS 'fecha',
       CASE WHEN TIPAGO.nombre IS NULL THEN 'NINGUNO' ELSE TIPAGO.nombre END
          AS 'tipo_pago_descripcion'
  FROM BDMultinivel.dbo.GP_COMISION GPCOMI
       INNER JOIN BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GPESTA  ON GPESTA.id_comision = GPCOMI.id_comision
       INNER JOIN BDMultinivel.dbo.GP_COMISION_DETALLE GPDETA ON GPDETA.id_comision = GPCOMI.id_comision
       INNER JOIN BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha = GPDETA.id_ficha
       LEFT JOIN BDMultinivel.dbo.BANCO BA ON BA.id_banco = FIC.id_banco
       INNER JOIN BDMultinivel.dbo.CICLO CI ON CI.id_ciclo = GPCOMI.id_ciclo
       LEFT JOIN BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I IDESTA ON IDESTA.id_comision_detalle = GPDETA.id_comision_detalle
       LEFT JOIN BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE ESTANA ON ESTANA.id_estado_comision_detalle = IDESTA.id_estado_comision_detalle
       LEFT JOIN BDMultinivel.dbo.LISTADO_FORMAS_PAGO LISTFO ON LISTFO.id_comisiones_detalle = GPDETA.id_comision_detalle
       LEFT JOIN BDMultinivel.dbo.TIPO_PAGO TIPAGO ON TIPAGO.id_tipo_pago = LISTFO.id_tipo_pago
       LEFT JOIN BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL FPAGO  ON FPAGO.id_lista_formas_pago = LISTFO.id_lista_formas_pago
       LEFT JOIN BDMultinivel.dbo.CIUDAD CIU ON CIU.id_ciudad = FIC.id_ciudad
       LEFT JOIN BDPuntosCash.DBO.CUENTA CU ON CU.id_usuario COLLATE Modern_Spanish_CI_AS = FIC.Ci
 WHERE     IDESTA.habilitado = 'true'
       AND GPESTA.habilitado = 'true'
       AND (TIPAGO.id_tipo_pago = 1 or TIPAGO.id_tipo_pago = 2)
       AND FPAGO.habilitado = 1
	   AND FPAGO.id_estado_listado_forma_pago =3
       AND FIC.ci LIKE '%'+@CINIT+'%'

