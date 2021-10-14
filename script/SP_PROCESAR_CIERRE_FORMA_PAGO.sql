
alter PROCEDURE [dbo].[SP_PROCESAR_CERRAR_FORMA_PAGO]
     @id_Ciclo     int,
     @id_usuario VARCHAR(100)
AS

BEGIN TRANSACTION;
BEGIN TRY
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
 
          --estado 2 si facturo 6 no presenta factura
        --  select * from BDMultinivel.dbo.vwObtenercomisionesFormaPago where id_ciclo=85


			COMMIT TRANSACTION;		    
			 return 1

 
           

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
