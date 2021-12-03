
alter PROCEDURE [dbo].[SP_CERRAR_COMISION_PAGO_POR_TIPO]
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
   DECLARE @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO INT;
   DECLARE @ID_ESTADO_COMISION_FORMA_DE_PAGO INT;

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
   SET @ID_ESTADO_COMISION_FORMA_DE_PAGO=10
   SET @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO=13;

   DECLARE @IDCOMISION_SELECCIONADO INT;
   DECLARE @IDCOMISION_DETALLE_ESTADO_OLD INT;
   DECLARE @IDESTADOCOMISION_ACTUAL INT;
   SET @IDCOMISION_SELECCIONADO=0;
   SET @IDCOMISION_DETALLE_ESTADO_OLD=0;
   SET @IDESTADOCOMISION_ACTUAL = 0

	   select top(1) @IDCOMISION_SELECCIONADO=co.id_comision, @IDCOMISION_DETALLE_ESTADO_OLD= CE.id_comision_estado_comision_i,@IDESTADOCOMISION_ACTUAL= CE.id_estado_comision  from BDMultinivel.dbo.GP_COMISION co 
	   INNER JOIN BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I CE on co.id_comision = CE.id_comision 
	   where co.id_ciclo=@ID_CICLO_VAR and co.id_tipo_comision =@ID_TIPO_COMISION_PAGOCOMISION and CE.habilitado = @ESTADO_HABILITADO

	   IF @ID_TIPO_COMISION_PAGOCOMISION = 1 --PROCESAN LOS TIPO PAGOS
	   BEGIN	   
	      SELECT 'PROCESAR TIPO PAGO'
		  IF @IDESTADOCOMISION_ACTUAL = @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO --SI SEENCUENTA CERRADO
		  BEGIN 
		      return 1 -- COMISION CERRADA EXISTENTE
			-- SELECT 'COMISION CERRADA EXISTENTE'
		  END
		  IF @IDESTADOCOMISION_ACTUAL = @ID_ESTADO_COMISION_FORMA_DE_PAGO 
		  BEGIN
		       -- ACTUALIZAMOS COMISION A CERRADO			
			   --dehabilitamos el actual estado 10 de forma de pago 
			   UPDATE BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I set habilitado=@ESTADO_DESAHABILITADO   where id_comision_estado_comision_i= @IDCOMISION_DETALLE_ESTADO_OLD
			   --creamos un nuevo estado pago cerrado de comision
			   insert into BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I(habilitado, id_comision,id_estado_comision, id_usuario,fecha_creacion, fecha_actualizacion)
			   values(@ESTADO_HABILITADO, --habilitado, 
			          @IDCOMISION_SELECCIONADO, --id_comision,
					  @ID_ESTADO_COMISION_PAGO_COMISION_CERRADO, --id_estado_comision, 
					  @ID_USUARIO_LOGUEADO, --id_usuario,
					  GETDATE(), -- fecha_creacion, 
					  GETDATE() -- fecha_actualizacion
					  )

            COMMIT TRANSACTION;		
		    RETURN 2
			 --SELECT 'CIERRE EXITOSO'
		  END

	   END
	   IF @ID_TIPO_COMISION_PAGOCOMISION = 2 --PROCESAN LOS TIPO PAGOS REZAGADOS
	   BEGIN	 
	       COMMIT TRANSACTION;		
	       RETURN 4
	      -- SELECT 'PROCESAR LOS RESAGADOS'
	   END

       

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
END CATCH