
CREATE PROCEDURE [dbo].[SP_EXEC_CARGAR_EMPRESAS]
 
AS


BEGIN TRY

BEGIN TRANSACTION;

   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 
   DECLARE @USUARIO_DEFAULT int;
   SET @USUARIO_DEFAULT=1;
   DECLARE @CICLOIDitem INT;
   DECLARE @NOMBREitem VARCHAR (100);
   DECLARE @FECHAINICIOitem datetime;
   DECLARE @FECHAFINitem datetime;
   DECLARE @ESTADOitem INT;
   DECLARE @listaEmpresa as table( IDBD  INT, NOMBREBD VARCHAR (100), DESCRIPCION VARCHAR (100));

	insert into @listaEmpresa SELECT  IDBD, NOMBREBD, DESCRIPCION from BDComisiones.[dbo].[CNX_BDCOMISIONES] 
	DECLARE @IDEMPRESAitem int;
	DECLARE @NOMBREEMPRESAitem varchar (100);
	DECLARE @NOMBREBDitem varchar (100)

	DECLARE EMPRESA_CURSOR CURSOR FOR 
	Select IDBD, DESCRIPCION, NOMBREBD from @listaEmpresa
	OPEN EMPRESA_CURSOR
	FETCH NEXT FROM EMPRESA_CURSOR INTO @IDEMPRESAitem, @NOMBREEMPRESAitem,@NOMBREBDitem
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
		-------------
			DECLARE @IDEMPRESA_SELECCIONADO int;
			SET @IDEMPRESA_SELECCIONADO=0;				
			select top(1) @IDEMPRESA_SELECCIONADO = id_empresa  FROM BDMultinivel.dbo.EMPRESA where codigo_cnx= @IDEMPRESAitem
			IF @IDEMPRESA_SELECCIONADO = 0
			BEGIN
				--select @IDEMPRESAitem as 'nuevo'
				DECLARE @EMPRESA_GUARDIAN INT;
				SET @EMPRESA_GUARDIAN=0;

				SELECT top(1) @EMPRESA_GUARDIAN=empresa_id FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grdsion.empresa_config ') WHERE empresa_conexion_id= @IDEMPRESAitem

				insert into   BDMultinivel.dbo.EMPRESA (codigo, codigo_cnx, nombre,nombre_bd, estado, id_usuario)
				values(
						@EMPRESA_GUARDIAN,--codigo
						@IDEMPRESAitem,--codigo_cnx
						@NOMBREEMPRESAitem,--nombre
						@NOMBREBDitem,--nombre_bd
						1,--estado true
						@USUARIO_DEFAULT
				);
				SET @IDEMPRESA_SELECCIONADO = SCOPE_IDENTITY ();						
			END
				DECLARE @listaProyectos as table( IDEMPRESA  INT, IDALMACEN  INT, DESCRIPCION VARCHAR (100), HABILITADO bit);
		    	insert into @listaProyectos SELECT IDEMPRESA, IDALMACEN,DESCRIPCION, HABILITADO from  BDComisiones.dbo.vwPROYECTOS_ALL where IDEMPRESA= @IDEMPRESAitem

			      DECLARE @IDPROYECTOitem int;
				  DECLARE @DESCRIPCIONitem VARCHAR(100);
				  DECLARE @HABILITADOitem bit;
		        	DECLARE PROYECTO_CURSOR CURSOR FOR 
					Select IDALMACEN, DESCRIPCION, HABILITADO from @listaProyectos
					OPEN PROYECTO_CURSOR
					FETCH NEXT FROM PROYECTO_CURSOR INTO @IDPROYECTOitem, @DESCRIPCIONitem, @HABILITADOitem
						WHILE @@FETCH_STATUS = 0  
						BEGIN 
						-------------
						DECLARE @PROYECTOseleccionado int;
						set @PROYECTOseleccionado = 0;						
						select @PROYECTOseleccionado = id_empresa from BDMultinivel.dbo.PROYECTO where proyecto_conexion_id=@IDPROYECTOitem
						IF @PROYECTOseleccionado =0
						BEGIN
							 -- select @DESCRIPCIONitem as 'nuevo proyecto'
							  insert into BDMultinivel.dbo.PROYECTO (nombre,id_empresa, proyecto_conexion_id, complejoid_guardian, id_usuario, estado)
							  values(
							        @DESCRIPCIONitem,--nombre
									@IDEMPRESA_SELECCIONADO, --idempresa
									@IDPROYECTOitem, --proyecto_conexion_id									
									@HABILITADOitem,
									@USUARIO_DEFAULT,
									@HABILITADOitem
							  );
						END
						ELSE
						BEGIN
						-- select @NOMBREEMPRESAitem as 'existe proyecto for'
						 update BDMultinivel.dbo.PROYECTO
						       set estado= @HABILITADOitem
							   where id_empresa= @PROYECTOseleccionado
						END

						
						-------------
						FETCH NEXT FROM PROYECTO_CURSOR INTO @IDPROYECTOitem, @DESCRIPCIONitem, @HABILITADOitem
						END
					DELETE from @listaProyectos
					CLOSE PROYECTO_CURSOR
					DEALLOCATE PROYECTO_CURSOR
					  
		-------------
		FETCH NEXT FROM EMPRESA_CURSOR INTO  @IDEMPRESAitem, @NOMBREEMPRESAitem,@NOMBREBDitem
		END
	DELETE from @listaEmpresa
	CLOSE EMPRESA_CURSOR
	DEALLOCATE EMPRESA_CURSOR
	
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
