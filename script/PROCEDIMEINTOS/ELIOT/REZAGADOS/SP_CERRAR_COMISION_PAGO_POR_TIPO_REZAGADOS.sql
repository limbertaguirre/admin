USE BDMultinivel;
GO
CREATE PROCEDURE [dbo].[SP_CERRAR_COMISION_PAGO_POR_TIPO_REZAGADOS]
  @comision_id int,
  @id_Ciclo int,
  @id_usuario int,
  @id_tipo_comision int,
  @estado_comision_id int
AS
BEGIN TRANSACTION
BEGIN TRY
DECLARE @IMPBODY VARCHAR (500);
DECLARE @IMPSUBJECT VARCHAR (500);
------------------------------------------------------------------ 
DECLARE @ID_USUARIO_LOGUEADO INT;
DECLARE @ID_CICLO_VAR INT;
DECLARE @ID_TIPO_COMISION_PAGOCOMISION INT;
DECLARE @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO INT;
DECLARE @ID_ESTADO_COMISION_FORMA_DE_PAGO INT;
DECLARE @ID_ESTADOCOMISION_CERRADO_FORMA_PAGO_TABLE INT;
DECLARE @ID_ESTADOCOMISION_PAGO_COMISION_CERRADO_TABLE INT;
DECLARE @ESTADO_DESAHABILITADO INT;
DECLARE @ESTADO_HABILITADO INT;
SET @ID_USUARIO_LOGUEADO = @id_usuario;
SET @ID_CICLO_VAR = @id_Ciclo;
SET @ID_TIPO_COMISION_PAGOCOMISION = @id_tipo_comision;
--//GP_TIPO_COMISION 
SET @ESTADO_DESAHABILITADO = 0;
SET @ESTADO_HABILITADO = 1;
SET @ID_ESTADO_COMISION_FORMA_DE_PAGO = @estado_comision_id;
SET @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO = 17;
-- PAGO COMISION REZAGADO CERRADO
DECLARE @IDCOMISION_SELECCIONADO INT;
DECLARE @IDCOMISION_DETALLE_ESTADO_OLD INT;
DECLARE @IDESTADOCOMISION_ACTUAL INT;
DECLARE @ID_ESTADO_COMISION_16_PARA_HABILITAR INT;
SET @IDCOMISION_SELECCIONADO = 0;
SET @IDCOMISION_DETALLE_ESTADO_OLD = 0;
SET @IDESTADOCOMISION_ACTUAL = 0;

  select top(1)
  @IDCOMISION_SELECCIONADO = c.id_comision,
  @IDCOMISION_DETALLE_ESTADO_OLD = cec.id_comision_estado_comision_i,
  @IDESTADOCOMISION_ACTUAL = cec.id_estado_comision
  from BDMultinivel.dbo.GP_COMISION c
  INNER JOIN BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I cec on c.id_comision = cec.id_comision
  where c.id_ciclo = @ID_CICLO_VAR and c.id_tipo_comision = @ID_TIPO_COMISION_PAGOCOMISION and cec.habilitado = @ESTADO_HABILITADO and cec.id_estado_comision = @ID_ESTADO_COMISION_FORMA_DE_PAGO;

  SET @ID_ESTADO_COMISION_16_PARA_HABILITAR = ISNULL(
    (SELECT TOP 1 cec.id_comision_estado_comision_i FROM BDMultinivel.DBO.GP_COMISION C
    INNER JOIN BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I CEC ON C.id_comision = CEC.id_comision
    WHERE C.id_ciclo = @ID_CICLO_VAR AND C.id_tipo_comision = @ID_TIPO_COMISION_PAGOCOMISION AND CEC.habilitado = @ESTADO_DESAHABILITADO), 0);
    
  IF (@ID_TIPO_COMISION_PAGOCOMISION = 2) --PROCESAN LOS TIPO PAGOS
  BEGIN
    IF (@IDESTADOCOMISION_ACTUAL = @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO) --SI SEENCUENTA CERRADO
    BEGIN
      COMMIT TRANSACTION;
      return 1;
      -- COMISION CERRADA EXISTENTE
      -- SELECT 'COMISION CERRADA EXISTENTE'
    END
    IF (@IDESTADOCOMISION_ACTUAL = @ID_ESTADO_COMISION_FORMA_DE_PAGO)
    BEGIN
      -- ACTUALIZAMOS COMISION A CERRADO			
      --dehabilitamos el actual estado 10 de forma de pago 
      UPDATE BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
      SET habilitado = @ESTADO_DESAHABILITADO
      WHERE id_comision_estado_comision_i = @IDCOMISION_DETALLE_ESTADO_OLD;
      --creamos un nuevo estado pago cerrado de comision
      insert into BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
        (
        habilitado,
        id_comision,
        id_estado_comision,
        id_usuario,
        fecha_creacion,
        fecha_actualizacion
        )
      values(
          @ESTADO_HABILITADO,
          @IDCOMISION_SELECCIONADO,
          @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO,        
          @ID_USUARIO_LOGUEADO,
          GETDATE(),
          GETDATE()
      );
      IF(@ID_ESTADO_COMISION_16_PARA_HABILITAR <> 0) 
      BEGIN
        UPDATE BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
        set habilitado = @ESTADO_HABILITADO
        where id_comision_estado_comision_i = @ID_ESTADO_COMISION_16_PARA_HABILITAR;
      END
    COMMIT TRANSACTION;
    RETURN 2;
    --SELECT 'CIERRE EXITOSO'
    END
    COMMIT TRANSACTION;
    RETURN 3;
  END
  IF (@ID_TIPO_COMISION_PAGOCOMISION = 1) --PROCESAN LOS TIPO PAGOS REZAGADOS
  BEGIN
    COMMIT TRANSACTION;
    RETURN 4;
  END
  COMMIT TRANSACTION;
  RETURN 5;
END TRY BEGIN CATCH
SELECT ERROR_NUMBER () AS ErrorNumber,
  ERROR_SEVERITY () AS ErrorSeverity,
  ERROR_STATE () AS ErrorState,
  ERROR_PROCEDURE () AS ErrorProcedure,
  ERROR_LINE () AS ErrorLine,
  ERROR_MESSAGE () AS ErrorMessage;
IF @@TRANCOUNT > 0 BEGIN
  SET @IMPBODY = concat (
    'SP_CARGAR_COMISIONES ',
    ' '
  );
  SET @IMPSUBJECT = 'ALERTA PRODUCCION : no se pudo cargar las comisiones ';
  --EXECUTE msdb.dbo.sp_send_dbmail @profile_name   = 'NotificacionSQL',
  --                                @recipients = 'desarrollo@gruposion.bo; UIT-SION@gruposion.bo',
  --                                @body           = @IMPBODY,
  --                                @subject        = @IMPSUBJECT;
  ROLLBACK TRANSACTION;
  return -1;
END
END CATCH
GO