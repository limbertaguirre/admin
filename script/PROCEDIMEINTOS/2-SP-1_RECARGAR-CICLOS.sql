
CREATE PROCEDURE [dbo].[SP_CARGAR_CICLOS]
 
AS


BEGIN TRY

BEGIN TRANSACTION;

   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
   DECLARE @USUARIO_DEFAULT int;
   SET @USUARIO_DEFAULT=1;


   DECLARE @CICLOIDitem INT;
   DECLARE @NOMBREitem VARCHAR (100);
   DECLARE @FECHAINICIOitem datetime;
   DECLARE @FECHAFINitem datetime;
   DECLARE @ESTADOitem INT;
   DECLARE @ListaCiclos as table( lciclo_id  INT, snombre VARCHAR (100), dtfechainicio datetime, dtfechafin datetime, lestado int);

	insert into @ListaCiclos SELECT  * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select lciclo_id,snombre, dtfechainicio, dtfechafin, lestado  from grdsion.administracionciclo');   
	
			DECLARE CICLO_CURSOR CURSOR FOR 
			Select lciclo_id, snombre, dtfechainicio,dtfechafin, lestado from @ListaCiclos
			OPEN CICLO_CURSOR
			FETCH NEXT FROM CICLO_CURSOR INTO @CICLOIDitem, @NOMBREitem, @FECHAINICIOitem,@FECHAFINitem, @ESTADOitem

			WHILE @@FETCH_STATUS = 0  
			BEGIN 
			-------------
					DECLARE @IDCICLOSELECCIONADO INT;
					DECLARE @ESTADO_CICLO int;

					SET @IDCICLOSELECCIONADO=0;
					SET @ESTADO_CICLO=0
					if @ESTADOitem = 0
					BEGIN
					 SET @ESTADO_CICLO=1; --ESTA ACTIVO
					END 
					ELSE
					BEGIN
					   SET @ESTADO_CICLO=0; --INACTIVO
					END

					IF(@CICLOIDitem > 0)
					BEGIN
					SELECT @IDCICLOSELECCIONADO=id_ciclo from BDMultinivel.dbo.CICLO WHERE id_ciclo=@CICLOIDitem
					IF @IDCICLOSELECCIONADO = 0
					BEGIN
							insert into BDMultinivel.dbo.CICLO (id_ciclo, nombre, descripcion, fecha_inicio,fecha_fin, estado,id_usuario)
								values(
								  @CICLOIDitem, 
								  @NOMBREitem,
								  '',
								  @FECHAINICIOitem,
								  @FECHAFINitem,
								  @ESTADO_CICLO,
								  @USUARIO_DEFAULT
								);
							  SELECT @CICLOIDitem AS 'insert'

					END
					ELSE
					BEGIN
							  SELECT @CICLOIDitem AS 'ACTUALIZO'
							  UPDATE BDMultinivel.dbo.CICLO 
							    SET estado = @ESTADO_CICLO
							  WHERE id_ciclo=@CICLOIDitem
					END
					  
					END
					 

			-------------
			FETCH NEXT FROM CICLO_CURSOR INTO @CICLOIDitem, @NOMBREitem, @FECHAINICIOitem,@FECHAFINitem, @ESTADOitem
			END

			DELETE from @ListaCiclos
			CLOSE CICLO_CURSOR
			DEALLOCATE CICLO_CURSOR

			COMMIT TRANSACTION;
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

         SELECT -1 AS 'error catch server';
      END
END CATCH;
