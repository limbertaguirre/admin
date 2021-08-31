
USE BDMultinivel
go
ALTER PROCEDURE [dbo].[SP_REGISTRAR_DESCUENTO_GUARDIAN]
     @usuario_name VARCHAR(100),
	 @codigo_producto VARCHAR(100),
	 @id_proyecto_gestor int,
	 @monto_descuento VARCHAR(100),
	 @id_detalle_comision numeric(12, 2),
	 @id_tipo_aplicaciones int,
	 @descripcion NVARCHAR(MAX)

AS


BEGIN TRY
BEGIN TRANSACTION;
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------   
 --   DECLARE @id_proyecto_gestor int;
	--DECLARE @id_detalle_comision int;
	--DECLARE @monto_descuento numeric(12, 2);
	--DECLARE @descripcion NVARCHAR(MAX);
	--DECLARE @codigo_producto VARCHAR(100);
	--DECLARE @usuario_name VARCHAR(100);

    SET @id_detalle_comision=10472;
	--SET @id_proyecto_gestor=5;
	--SET @monto_descuento=10; 
	--SET @descripcion='venta producto';
	--SET @codigo_producto= 'CGT-PRUEBA-221';
	--SET @usuario_name='user_prueba;'

	--tipo de descuento ciclo
    --cuando es otros lo asume AVDEL

   ---------------------------------------
   DECLARE @id_ciclo int;
   DECLARE @lcontactoId int;
   DECLARE @complejoId int;
   DECLARE @ldescuento_ID int;
   DECLARE @Dtotal numeric(12, 2);
   DECLARE @query_cabecera  NVARCHAR(MAX);

   SET @query_cabecera=N'select * from grdsion.administraciondescuentociclo';
           
               select @id_ciclo = CO.id_ciclo, @lcontactoId= FI.codigo -- CO.id_ciclo,FI.codigo--   
			     from BDMultinivel.dbo.GP_COMISION_DETALLE CODE
			     inner join BDMultinivel.dbo.GP_COMISION CO on CO.id_comision = CODE.id_comision
				 inner join BDMultinivel.dbo.FICHA FI on FI.id_ficha= CODE.id_ficha
			     where CODE.id_comision_detalle=@id_detalle_comision
			  
			   select @complejoId=complejoid_guardian from BDMultinivel.dbo.proyecto where id_proyecto= @id_proyecto_gestor
			   --obtener cabeceera descuento..
			   SELECT  @ldescuento_ID= ldescuentociclo_id, @Dtotal= dtotal FROM OPENQUERY( [10.2.10.222], N'select * from grdsion.administraciondescuentociclo') where lciclo_id=@id_ciclo and lcontacto_id =80182  --@lcontactoId
               IF(@ldescuento_ID > 0)
			   BEGIN
					  SET @Dtotal = @Dtotal + @monto_descuento; 					  
					  UPDATE OPENQUERY([10.2.10.222],'SELECT ldescuentociclo_id, dtotal FROM grdsion.administraciondescuentociclo') SET dtotal= @Dtotal where ldescuentociclo_id=@ldescuento_ID
					  
					    --INSERT OPENQUERY ([10.2.10.222], 'SELECT susuarioadd,dtfechaadd, susuariomod,dtfechamod, ldescuentociclodetalle_id, ldescuentociclotipo_id, lcomplejo_id, smanzano, slote, suv, dmonto, sobservacion   FROM grdsion.administraciondescuentociclodetalle')  
						--VALUES (@usuario_name, --usser add
						--        GETDATE(), --add
						--		@usuario_name, --user mod
						--		GETDATE(), --fechamod
						--		600000, --id table
						--		@ldescuento_ID, 
						--		@complejoId, --complejo guardian
						--		'', --smanzano
						--		'', --slote
						--		@codigo_producto, --producto
						--		@monto_descuento,
						--		@descripcion 
						--       ); 
					 -- SELECT SCOPE_IDENTITY() as 'idautoincremental';

					 COMMIT TRANSACTION;		    
			         return 1012 

			    --      select @id_ciclo as 'ciclo', @lcontactoId as 'contactoid' , @complejoId as 'complejoid', @ldescuento_ID as '@descuento_ID', @Dtotal as '@total'
					  --select @Dtotal as 'total'

			   END
			   ELSE
			   BEGIN
			        --select 10 as ' no tiene descuentos'
				    ROLLBACK TRANSACTION;        
	            	 return -2
			   END			 			       
  COMMIT
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
