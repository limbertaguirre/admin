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

	DECLARE @USUARIO_DEFAULT int;
	DECLARE @CANTIDAD_DEFAULT int;

	SET @USUARIO_DEFAULT= 1;
	SET @CANTIDAD_DEFAULT=1;

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

	 insert into @TABLA_GUARDIAN  select * from [BDQISHUR].dbo.AplicacionesPagos where Ciclo = 80 and  CI_Cliente= @CARNETItem    and Id_Cliente=@IDCLIENTEGUARDIANItem --AND Id_Empresa IN (95);
      
	    ---------------------------------------------------------
		   DECLARE @IdITEN INT; 
		   DECLARE @IdClienteITEN INT; 
		   DECLARE @CIClienteITEN VARCHAR(100);
		   DECLARE @MontoITEN decimal(12, 2);
		   DECLARE @IdProductoITEN VARCHAR(100);
		   DECLARE @ObservacionITEN VARCHAR(500);
		   DECLARE @IDEMPRESACNXIten INT;

		    DECLARE @IdEmpresaITEN INT; 
		----------------------------------------------------
			DECLARE DESCUENTO_CURSOR CURSOR FOR 
			Select Id, Id_Cliente, CI_Cliente, Monto, Id_Producto, Observacion, Id_Empresa from @TABLA_GUARDIAN
			OPEN DESCUENTO_CURSOR
			FETCH NEXT FROM DESCUENTO_CURSOR INTO @IdITEN, @IdClienteITEN, @CIClienteITEN, @MontoITEN, @IdProductoITEN, @ObservacionITEN, @IDEMPRESACNXIten 

			WHILE @@FETCH_STATUS = 0  
			BEGIN 
			-------------
			DECLARE @ID_DETALLECOMISION int;
			DECLARE @IDFICHA int;

			--obtiene la idficha, iddetallecomision por ciclo id, actual
			select top(1) @ID_DETALLECOMISION= CODE.id_comision_detalle, @IDFICHA= FI.id_ficha from BDMultinivel.dbo.CICLO C
			inner join BDMultinivel.dbo.GP_COMISION CO on CO.id_ciclo = C.id_ciclo
			inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= CO.id_comision
			inner join  BDMultinivel.dbo.FICHA FI on FI.id_ficha = CODE.id_ficha
			where C.id_ciclo= 80 and FI.ci=@CIClienteITEN
			        --consultarcomplejo = idProyectocnx y empresa a 
			        DECLARE @BDEMPRESA VARCHAR(50), @IDCOMPLEJO INT, @NOMBRECOMPLEJO VARCHAR(MAX) , @SQLQUERY NVARCHAR(MAX), @ParmDefinition NVARCHAR(500);						
	                SET @BDEMPRESA = ''; SET @IDCOMPLEJO = 0; SET @NOMBRECOMPLEJO = '';

			    	SELECT   @BDEMPRESA = RTRIM( NOMBREBD )FROM BDComisiones.DBO.CNX_BDCOMISIONES WHERE IDBD = @IDEMPRESACNXIten
					DECLARE @SRV_DB VARCHAR(MAX)
					IF @IDEMPRESACNXIten = 95 --idneizan
					BEGIN 
						SET @SRV_DB = '[10.2.10.142].[BDConexionnEIZAN]'
					END
					IF @IDEMPRESACNXIten = 82 and @IDEMPRESACNXIten = 81--idshofar
					BEGIN 
						SET @SRV_DB = '[10.2.10.85].[BDConexionSHOFAR]'
					END
					 ELSE
					BEGIN
						SET @SRV_DB = @BDEMPRESA
					END

					SET  @SQLQUERY = N'SELECT @IDALMACEN_AUX =   A.IDALMACEN, @NAMECOMPLEJO = RTRIM(A.DESCRIPCION) FROM '+@SRV_DB+'.DBO.INPRODUCTO P 
						INNER JOIN '+@SRV_DB+'.DBO.INALMACEN A ON A.IDALMACEN = P.IDALMACEN_PROD
						WHERE P.IDPRODUCTO = '''+@IdProductoITEN+''''
					SET @ParmDefinition = N'@IDALMACEN_AUX INT OUTPUT, @NAMECOMPLEJO VARCHAR(MAX) OUTPUT';  
					EXECUTE sp_executesql @SQLQUERY, @ParmDefinition,   @IDALMACEN_AUX=@IDCOMPLEJO OUTPUT, @NAMECOMPLEJO = @NOMBRECOMPLEJO OUTPUT;  

					--SELECT @ID_DETALLECOMISION as 'detalleCOmisionID', @IDCOMPLEJO as 'idcomplejo', @NOMBRECOMPLEJO as 'nombre complejo', @BDEMPRESA as 'bdEmpresa', @IdProductoITEN as 'producto id', @IDEMPRESACNXIten as 'idempresa', @IdITEN

						IF @IDCOMPLEJO > 0
						BEGIN						 
						     --verificamos complejo con la empresa de bdmultinivel
							 DECLARE @IDPROYECTO_BDMULTINIVEL int;
							 SET @IDPROYECTO_BDMULTINIVEL = 0;
							 select top(1) @IDPROYECTO_BDMULTINIVEL = id_proyecto from BDMultinivel.dbo.PROYECTO  where proyecto_conexion_id =  @IDCOMPLEJO
							 IF @IDPROYECTO_BDMULTINIVEL >0
							 BEGIN							     
								  insert into BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO (cantidad, monto, descripcion, subtotal, id_proyecto, codigo_producto, id_comisiones_detalle,id_usuario, id_bdqishur)
								  values(
								      @CANTIDAD_DEFAULT, --default
									   @MontoITEN,--monto
									   @ObservacionITEN,
									   @MontoITEN,--subtotal
									   @IDPROYECTO_BDMULTINIVEL, --idproyecto --complejo cero
									   @IdProductoITEN, --producto en la q aplico
									   @ID_DETALLECOMISION, 
									   @USUARIO_DEFAULT,
									   @IdITEN --[BDQISHUR] aplicacionePago ID
									   ); 
							 END
							 ELSE
							 BEGIN							   
								 -- se inserta en cero el proyecto porque no se encontro el idproyecto conexion en nuestra BDMultinivel, no hay relacion con proyecto
								 insert into BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO (cantidad, monto, descripcion, subtotal, id_proyecto, codigo_producto, id_comisiones_detalle,id_usuario, id_bdqishur)
								 values(
								      @CANTIDAD_DEFAULT, --default
									   @MontoITEN,--monto
									   @ObservacionITEN,
									   @MontoITEN,--subtotal
									   0, --idproyecto --complejo cero
									   @IdProductoITEN, --producto en la q aplico
									   @ID_DETALLECOMISION, 
									   @USUARIO_DEFAULT, 
									   @IdITEN --[BDQISHUR] aplicacionePago ID
									   ); 
							 END							
						END
						ELSE
						BEGIN						   							
								--almacen en cero, no tiene complejo, por defaul se registrara en cero  que hace referencia a sin proyecto 
								insert into BDMultinivel.dbo.APLICACION_DETALLE_PRODUCTO (cantidad, monto, descripcion, subtotal, id_proyecto, codigo_producto, id_comisiones_detalle,id_usuario, id_bdqishur)
								values(
									   @CANTIDAD_DEFAULT, --default
									   @MontoITEN,--monto
									   @ObservacionITEN,
									   @MontoITEN,--subtotal
									   0, --idproyecto --complejo cero
									   @IdProductoITEN, --producto en la q aplico
									   @ID_DETALLECOMISION, 
									   @USUARIO_DEFAULT,
									   @IdITEN --[BDQISHUR] aplicacionePago ID
									  );
						END

			-------------
			FETCH NEXT FROM DESCUENTO_CURSOR INTO @IdITEN, @IdClienteITEN, @CIClienteITEN, @MontoITEN, @IdProductoITEN, @ObservacionITEN , @IDEMPRESACNXIten
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
