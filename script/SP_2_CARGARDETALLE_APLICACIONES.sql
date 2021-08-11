--SP_2_CARGARDETALLE_APLICACIONES

CREATE PROCEDURE [dbo].[SP_2_CARGARDETALLE_APLICACIONES]
   @id_Ciclo     int
AS

BEGIN TRANSACTION;
BEGIN TRY
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
    DECLARE @LISTA_CI as table(  IDCLIENTE_GUARDIAN INT, CARNET  nvarchar(100));
    DECLARE @IDCLIENTEGUARDIANItem int;
    DECLARE @CARNETItem varchar(100);

	DECLARE @total int;
	SET @total =0;
    -- comisiones
    insert into @LISTA_CI  select Id_Cliente, CI_Cliente from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80  --@id_Ciclo
	group by Id_Cliente, CI_Cliente
	------
	DECLARE CARNET_CURSOR CURSOR FOR 
	Select  IDCLIENTE_GUARDIAN, CARNET from @LISTA_CI
	OPEN CARNET_CURSOR
	FETCH NEXT FROM CARNET_CURSOR INTO @IDCLIENTEGUARDIANItem,  @CARNETItem
	WHILE @@FETCH_STATUS = 0  
	BEGIN 
	-------------
	-------------
	    --select 5
		DECLARE @TABLA_GUARDIAN as table(  Id INT, 
		                                   Id_Empresa int,
										   Id_Venta int,
										   Ciclo int,
										   Id_Cliente int,
										   CI_Cliente varchar(100),
										   Id_Producto varchar(100),
										   Expensa numeric(12, 2),
										   Monto numeric(12, 2),
										   Fecha dateTime,
										   Id_Recibo int,
										   Id_Factura int,
										   Observacion varchar(500),
										   TipoPago int,
										   Intercompania int);

	 insert into @TABLA_GUARDIAN  select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and  CI_Cliente= @CARNETItem ;
      
	    ---------------------------------------------------------
		   DECLARE @IdITEN INT; 
		   DECLARE @IdClienteITEN INT; 
		   DECLARE @CIClienteITEN VARCHAR(100);
		   DECLARE @MontoITEN decimal(12, 2);
		   DECLARE @IdProductoITEN VARCHAR(100);

		    DECLARE @IdEmpresaITEN INT; 
		----------------------------------------------------
			DECLARE DESCUENTO_CURSOR CURSOR FOR 
			Select Id, Id_Cliente, CI_Cliente, Monto, Id_Producto from @TABLA_GUARDIAN
			OPEN DESCUENTO_CURSOR
			FETCH NEXT FROM DESCUENTO_CURSOR INTO @IdITEN, @IdClienteITEN, @CIClienteITEN, @MontoITEN, @IdProductoITEN 

			WHILE @@FETCH_STATUS = 0  
			BEGIN 
			-------------

				select  @IdITEN as 'ingreso'

			-------------
			FETCH NEXT FROM DESCUENTO_CURSOR INTO @IdITEN, @IdClienteITEN, @CIClienteITEN, @MontoITEN, @IdProductoITEN 
			END

			DELETE from @TABLA_GUARDIAN
			CLOSE DESCUENTO_CURSOR
			DEALLOCATE DESCUENTO_CURSOR
		---------------------------------------------------------
	-------------
	-------------
	FETCH NEXT FROM CARNET_CURSOR INTO @IDCLIENTEGUARDIANItem,  @CARNETItem
	END
	DELETE from @LISTA_CI
	CLOSE CARNET_CURSOR
	DEALLOCATE CARNET_CURSOR
	-------
	--select @total as 'totales'

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
