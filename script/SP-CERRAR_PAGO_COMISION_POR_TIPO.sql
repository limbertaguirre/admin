
CREATE PROCEDURE [dbo].[SP_CERRAR_COMISION_PAGO_POR_TIPO]
     @id_Ciclo     int,
     @id_usuario int,
	 @id_tipo_comision int
AS

BEGIN TRANSACTION;
BEGIN TRY

   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
   DECLARE @ID_USUARIO_LOGUEADO INT;
   DECLARE @ID_CICLO_VAR INT;
   DECLARE @ID_TIPO_COMISION_PAGOCOMISION INT;

   DECLARE @ID_ESTADOCOMISION_CERRADO_FORMA_PAGO_TABLE INT;
   DECLARE @ID_ESTADOCOMISION_PAGO_COMISION_CERRADO_TABLE INT;
   DECLARE  @ESTADO_DESAHABILITADO INT;
   DECLARE @ESTADO_HABILITADO INT;

   SET @ID_USUARIO_LOGUEADO= @id_usuario; 
   SET @ID_CICLO_VAR= @id_Ciclo;
   SET @ID_TIPO_COMISION_PAGOCOMISION=@id_tipo_comision;   
   --//GP_TIPO_COMISION 
   SET @ESTADO_DESAHABILITADO=0;
   SET @ESTADO_HABILITADO=1;


    --select * from BDMultinivel.dbo.GP_COMISION where id_ciclo=83 and id_tipo_comision =1
    --select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I where id_comision=117 and habilitado='true'
		
	COMMIT TRANSACTION;		    
	return 2           

END TRY
BEGIN CATCH
   SELECT ERROR_NUMBER () AS ErrorNumber,
          ERROR_SEVERITY () AS ErrorSeverity,
          ERROR_STATE () AS ErrorState,
          ERROR_PROCEDURE () AS ErrorProcedure,
          ERROR_LINE () AS ErrorLine,
          ERROR_MESSAGE () AS ErrorMessage;

   IF @@TRANCOUNT > 0
      BEGIN
         SET @IMPBODY =
                concat ('SP_CARGAR_COMISIONES ',
                        ' ');
         SET @IMPSUBJECT = 'ALERTA PRODUCCION : no se pudo cargar las comisiones ';
         --EXECUTE msdb.dbo.sp_send_dbmail @profile_name   = 'NotificacionSQL',
         --                                @recipients = 'desarrollo@gruposion.bo; UIT-SION@gruposion.bo',
         --                                @body           = @IMPBODY,
         --                                @subject        = @IMPSUBJECT;
         ROLLBACK TRANSACTION;        
		 return -1
      END
END CATCH;