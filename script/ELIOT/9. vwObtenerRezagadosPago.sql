USE [BDMultinivel];
GO
SET  ANSI_NULLS ON;
GO
SET  QUOTED_IDENTIFIER ON;
GO

ALTER VIEW [dbo].[vwObtenerRezagadosPagos]
AS
   SELECT c.id_comision,
          c.id_ciclo,
          ci.nombre,
          cde.id_empresa,
          TRIM (e.nombre) AS empresa,
          l.id_lista_formas_pago,
          dl.id_estado_listado_forma_pago,
          dl.habilitado estado_listado_forma_pago_habilitado,
          l.id_comisiones_detalle,
          cde.id_comision_detalle_empresa,
          cde.estado AS id_estado_comision_detalle_empresa,
          f.codigo_cnx AS [CODIGO_DE_CLIENTE],
          f.cuenta_bancaria AS [NRO_DE_CUENTA],
          b.nombre AS NOMBRE_BANCO,
          f.nombres + ' ' + f.apellidos AS [NOMBRE_DE_CLIENTE],
          f.ci AS [DOC_DE_IDENTIDAD],
          sum (cde.monto_neto) AS [IMPORTE_POR_EMPRESA],
          l.monto_neto AS [IMPORTE_NETO],
            CAST (DATEPART (DAY, cde.fecha_pago) AS VARCHAR)
          + '/'
          + CAST (DATEPART (MONTH, cde.fecha_pago) AS VARCHAR)
          + '/'
          + CAST (DATEPART (YYYY, cde.fecha_pago) AS VARCHAR)
             AS [FECHA_DE_PAGO],
          -- id_banco = 17 BANCO GANADERO
          CASE WHEN isnull (b.id_banco, 0) = 17 THEN 1 ELSE 3 END
             FORMA_DE_PAGO,
          CASE WHEN isnull (b.id_banco, 0) = 17 THEN '' ELSE '2' END
             MONEDA_DESTINO,
          CASE WHEN isnull (b.id_banco, 0) = 17 THEN '' ELSE b.codigo END
             ENTIDAD_DESTINO,
          -- Sucursal
          CASE
             WHEN isnull (b.id_banco, 0) = 17
             THEN
                ''
             ELSE
                CASE
                   WHEN isnull (ciu.id_pais, -1) = 1 THEN ciu.codigo
                   ELSE ''
                END
          END
             SUCURSAL_DESTINO,
          ci.nombre AS GLOSA,
          l.id_tipo_pago,
          c.fecha_creacion fecha_creacion_comision,
          c.fecha_actualizacion fecha_actualizacion_comision
     FROM LISTADO_FORMAS_PAGO l
          INNER JOIN
          BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl
             ON dl.id_lista_formas_pago = l.id_lista_formas_pago
          INNER JOIN GP_COMISION_DETALLE cd
             ON cd.id_comision_detalle = l.id_comisiones_detalle
          INNER JOIN GP_COMISION c ON c.id_comision = cd.id_comision
          INNER JOIN GP_COMISION_ESTADO_COMISION_I cec
             ON cec.id_comision = c.id_comision
          INNER JOIN BDMultinivel.dbo.CICLO ci ON ci.id_ciclo = c.id_ciclo
          INNER JOIN BDMultinivel.dbo.COMISION_DETALLE_EMPRESA cde
             ON cde.id_comision_detalle = cd.id_comision_detalle
          INNER JOIN BDMultinivel.dbo.EMPRESA e
             ON e.id_empresa = cde.id_empresa
          INNER JOIN BDMultinivel.dbo.FICHA f ON f.id_ficha = cd.id_ficha
          INNER JOIN BDMultinivel.dbo.CIUDAD ciu
             ON ciu.id_ciudad = f.id_ciudad
          LEFT JOIN BDMultinivel.dbo.BANCO b ON b.id_banco = f.id_banco
    WHERE     cde.monto_neto <> 0
          AND c.id_tipo_comision = 2
          AND cec.id_estado_comision = 11                -- REZAGADOS DE PAGOS
   --and l.id_lista_formas_pago not in (select dl.id_lista_formas_pago from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL dl where dl.habilitado = 1 and dl.id_estado_listado_forma_pago = 1)
   GROUP BY l.id_comisiones_detalle,
            cde.id_comision_detalle_empresa,
            cde.estado,
            f.codigo_cnx,
            f.cuenta_bancaria,
            b.nombre,
            c.id_ciclo,
            f.nombres,
            f.apellidos,
            f.ci,
            l.monto_neto,
            cde.id_empresa,
            e.nombre,
            ci.nombre,
            l.id_tipo_pago,
            b.id_banco,
            b.codigo,
            ciu.id_pais,
            ciu.codigo,
            cde.fecha_pago,
            l.id_lista_formas_pago,
            c.id_comision,
            ci.nombre,
            c.fecha_creacion,
            c.fecha_actualizacion,
            dl.id_estado_listado_forma_pago,
            dl.habilitado
GO