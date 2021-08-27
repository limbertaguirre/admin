
CREATE PROCEDURE [dbo].[SP_REGISTRAR_DESCUENTO_GUARDIAN]
     @id_ficha int,
     @usuario_name VARCHAR(100),
	 @codigo_producto VARCHAR(100),
	 @id_proyecto_gestor int,
	 @monto_descuento VARCHAR(100),
	 @id_detalle_comision int,
	 @id_tipo_descuento int

AS

BEGIN TRANSACTION;
BEGIN TRY
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------   
    SET @id_detalle_comision=10472;
	--tipo de descuento ciclo
    --cuando es otros lo asume AVDEL
   ---------------------------------------
            select * from BDMultinivel.dbo.GP_COMISION_DETALLE
			         

            select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and CI_Cliente='9594889'

			COMMIT TRANSACTION;		    
			 return 1        

  --------------------------------------------
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
