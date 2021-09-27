
CREATE PROCEDURE [dbo].[SP_EXEC_CARGAR_CONTACTOS]
 
AS

BEGIN TRY
   BEGIN TRANSACTION;

   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
   DECLARE @CLIENTES_CNX as table(  lcontacto_id int,dtfechaadd datetime, stelefonofijo varchar(100), stelefonomovil varchar(100),scorreoelectronico varchar(150),dtfechanacimiento datetime,sdireccion varchar(500),scedulaidentidad varchar(100), lpatrocinante_id int, lnivel_id int , snombrecompleto varchar(200), scontrasena varchar(100),lcuentabanco varchar(50), lcodigobanco int , cbaja int, dtfechabaja datetime,ltipobaja int, smotivobaja varchar(500), lnit varchar(50));
   DECLARE @USUARIO_DEFAULT int;
   DECLARE @IDCIUDAD_GESTOR_DEFAULT_SANTACRUZ int;
   DECLARE @ESTADO_FICHA_DEFAULT_HABILITADO int;
   DECLARE @ESTADO_FICHA_DEFAULT_DESHABILITADO int;
   DECLARE @ESTADO_FACTURA_DEHABILITADO int;
   DECLARE @PATROCINADOR_ALTO_RANGO_DEFAULT INT;

   DECLARE @NO_TIENEBAJA_GUARDIAN INT
   

   SET @USUARIO_DEFAULT=1;
   SET @IDCIUDAD_GESTOR_DEFAULT_SANTACRUZ=1;
   SET @ESTADO_FICHA_DEFAULT_HABILITADO=1;
   SET @ESTADO_FICHA_DEFAULT_DESHABILITADO=0;
   SET @ESTADO_FACTURA_DEHABILITADO=0;
   SET @PATROCINADOR_ALTO_RANGO_DEFAULT=-10;
   SET @NO_TIENEBAJA_GUARDIAN=0;


   INSERT INTO  @CLIENTES_CNX SELECT lcontacto_id,dtfechaadd, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena, Isnull(lcuentabanco,0) as 'lcuentabanco', lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja, lnit    FROM OPENQUERY( [10.2.10.222], 'select * from administracioncontacto ') order by lcontacto_id asc 

		DECLARE @CONTACTOIDitem int;
		DECLARE @FECHAREGISTROitem DATETIME;
		DECLARE @TELEFONOFIJOitem VARCHAR(100);
		DECLARE @TELEFONOMOVILitem VARCHAR(100);
		DECLARE @CORREOELECTRONICOitem VARCHAR(100);
		DECLARE @FECHANACIMIENTOitem DATE;
		DECLARE @DIRECCIONitem VARCHAR(100);

		DECLARE @DOCID VARCHAR(100);
		DECLARE @PATROCINADORIDitem INT;
		DECLARE @NIVELIDitem INT;
		DECLARE @NOMBRECOMPLETO VARCHAR(150);
		DECLARE @CONTRASENAitem VARCHAR(150);

		DECLARE @CUENTABANCOitem VARCHAR(100);
		DECLARE @CODIGOBANCOitem INT;
		DECLARE @BAJAItem INT;
		DECLARE @FECHABAJAitem DATE
		DECLARE @TIPOBAJAitem INT;
		DECLARE @MOTIVOBAJAitem VARCHAR(500);
		DECLARE @NITitem VARCHAR(50);


   		DECLARE CLIENTE_CURSOR CURSOR FOR 
		Select lcontacto_id,dtfechaadd, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena,lcuentabanco, lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja, lnit from @CLIENTES_CNX
		OPEN CLIENTE_CURSOR
		FETCH NEXT FROM CLIENTE_CURSOR INTO @CONTACTOIDitem,@FECHAREGISTROitem, @TELEFONOFIJOitem, @TELEFONOMOVILitem, @CORREOELECTRONICOitem, @FECHANACIMIENTOitem, @DIRECCIONitem, @DOCID, @PATROCINADORIDitem, @NIVELIDitem, @NOMBRECOMPLETO, @CONTRASENAitem, @CUENTABANCOitem, @CODIGOBANCOitem, @BAJAItem, @FECHABAJAitem, @TIPOBAJAitem, @MOTIVOBAJAitem, @NITitem

		WHILE @@FETCH_STATUS = 0  
		BEGIN 
		-------------
		DECLARE @IDFICHA_SELECCIONADO INT;
		SET @IDFICHA_SELECCIONADO=0;

		select @IDFICHA_SELECCIONADO= id_ficha from BDMultinivel.dbo.FICHA where codigo=@CONTACTOIDitem
		IF @IDFICHA_SELECCIONADO = 0 
		BEGIN
		-- select 'no existe'
		   DECLARE @ID_FICHA_IDENTITY int
		   DECLARE @ESTADO_FICHA int
		   DECLARE @NOMBRECOMPLETO_VAR VARCHAR(100);

		   SET @ID_FICHA_IDENTITY=0;
		   SET @NOMBRECOMPLETO_VAR= '';
		   SET @ESTADO_FICHA= 0;		 
		   ---obtener CLIENTE cnx

		  -- SELECT  @PATROCINADORIDitem,  @NOMBRECOMPLETO

			   IF @PATROCINADORIDitem = @PATROCINADOR_ALTO_RANGO_DEFAULT  --este es frealncer base que no estan en conexion
			   BEGIN
				  SELECT @PATROCINADORIDitem 'ES UN PATROCINAADOR BASE'
				  -- -- insert al patrocinadores de alto nivel
						 insert into BDMultinivel.dbo.FICHA(codigo, codigo_cnx, nombres, apellidos, ci, correo_electronico, fecha_registro,tel_oficina, tel_movil, tel_fijo, direccion, fecha_nacimiento, contrasena, comentario, avatar, tiene_cuenta_bancaria,id_banco, cuenta_bancaria,factura_habilitado,razon_social, nit,id_ciudad,estado, id_usuario )
							 values(
							   @CONTACTOIDitem, -- codigo, 
							   0 , -- codigo_cnx cero porque no existe en conexion, 
							   @NOMBRECOMPLETO , -- nombres, 
							   '', -- apellidos, 
							   @DOCID, -- ci, 
							   @CORREOELECTRONICOitem, -- correo_electronico, 
							   @FECHAREGISTROitem, -- fecha_registro,
							   '',-- tel_oficina, 
							   @TELEFONOMOVILitem,-- tel_movil, 
							   @TELEFONOFIJOitem,--tel_fijo, 
							   @DIRECCIONitem,--direccion,
							   @FECHANACIMIENTOitem,--fecha_nacimiento,
							   @CONTRASENAitem,--contrasena,
							   '',-- comentario, 
							   '',--avatar, 
							   0,-- tiene_cuenta_bancaria, --CERO IGUAL A FALSE 
							   0,-- id_banco,--POR DEFAUL NO TIENE BANCO
							   @CUENTABANCOitem,--cuenta_bancaria,
							   @ESTADO_FACTURA_DEHABILITADO,--factura_habilitado,
							   '',-- razon_social, 
							   @NITitem, --nit,
							   @IDCIUDAD_GESTOR_DEFAULT_SANTACRUZ, --id_ciudad,
							   1, --estado, --true
							   @USUARIO_DEFAULT --id_usuario
							   );
			   END
				 ELSE
			   BEGIN
					 --SELECT @PATROCINADORIDitem 'FREELANCER'
					  DECLARE @IDCLIENTE_CNX INT;
					  DECLARE @NOMBRE1_CNX VARCHAR(100);
					  DECLARE @NOMBRE2_CNX VARCHAR(100);
					  DECLARE @APPATERNO_CNX VARCHAR(100);
					  DECLARE @APMATERNO_CNX VARCHAR(100);
					  DECLARE @DOCID_CNX VARCHAR(100);
					  DECLARE @IDCIUDAD_CNX INT;

					  DECLARE @ESTADOfiCHA_GESTOR INT;

					  SET @IDCLIENTE_CNX= 0
					  SET @NOMBRE1_CNX='';
					  SET @NOMBRE2_CNX='';
					  SET @APPATERNO_CNX='';
					  SET @APMATERNO_CNX='';
					  SET @DOCID_CNX='';
					  SET @IDCIUDAD_CNX=0;
					  SET @ESTADOfiCHA_GESTOR=0
					  --DEFINIR EL ESTADO
					  if @BAJAItem = @NO_TIENEBAJA_GUARDIAN
					  BEGIN SET @ESTADOfiCHA_GESTOR=@ESTADO_FICHA_DEFAULT_HABILITADO; END   ELSE
					  BEGIN SET @ESTADOfiCHA_GESTOR=@ESTADO_FICHA_DEFAULT_DESHABILITADO; END

						 select top(1) @IDCLIENTE_CNX= IDCLIENTE,@NOMBRE1_CNX= TRIM(NOMBRE1),@NOMBRE2_CNX= TRIM(NOMBRE2),@APPATERNO_CNX= TRIM(APPATERNO),@APMATERNO_CNX= TRIM(APMATERNO),@DOCID_CNX = NUEVO_DOCID ,@IDCIUDAD_CNX = IDCIUDAD_RESIDENCIA   from BDComisiones.[dbo].grlCLIENTE_CCN WHERE NUEVO_DOCID=@DOCID
						 IF @IDCLIENTE_CNX > 0
						 BEGIN
						   -- -- insertar UN CLIENTE Q SI ESTA REGISTRADO
							  select @NOMBRE1_CNX as 'existe en conexion'
							   DECLARE @IDCIUDAD_VARIABLE INT;
							   SET @IDCIUDAD_VARIABLE=0
							   --IF VALIDO EL IDCIUDAD DE CNX YA QUE DEVUELVE VALORES NEGATIVOS, CASO CONTRARIO REGISTRO ID CIUDAD DEFAUL 1 SANTA CRUZ
							   IF @IDCIUDAD_CNX > 0
							   BEGIN SET @IDCIUDAD_VARIABLE=@IDCIUDAD_CNX END ELSE
							   BEGIN  SET @IDCIUDAD_VARIABLE= @IDCIUDAD_GESTOR_DEFAULT_SANTACRUZ END
							   
							   insert into BDMultinivel.dbo.FICHA(codigo, codigo_cnx, nombres, apellidos, ci, correo_electronico, fecha_registro,tel_oficina, tel_movil, tel_fijo, direccion, fecha_nacimiento, contrasena, comentario, avatar, tiene_cuenta_bancaria,id_banco, cuenta_bancaria,factura_habilitado,razon_social, nit,id_ciudad,estado, id_usuario )
								 values(
								   @CONTACTOIDitem, -- codigo, 
								   @IDCLIENTE_CNX , -- codigo_cnx  
								   @NOMBRE1_CNX + ' '+ @NOMBRE2_CNX, -- nombres, 
								   @APPATERNO_CNX +' '+ @APMATERNO_CNX, -- apellidos, 
								   @DOCID, -- ci, 
								   @CORREOELECTRONICOitem, -- correo_electronico, 
								   @FECHAREGISTROitem, -- fecha_registro,
								   '',-- tel_oficina, 
								   @TELEFONOMOVILitem,-- tel_movil, 
								   @TELEFONOFIJOitem,--tel_fijo, 
								   @DIRECCIONitem,--direccion,
								   @FECHANACIMIENTOitem,--fecha_nacimiento,
								   @CONTRASENAitem,--contrasena,
								   '',-- comentario, 
								   '',--avatar, 
								   0,-- tiene_cuenta_bancaria, --CERO IGUAL A FALSE 
								   0,-- id_banco,--POR DEFAUL NO TIENE BANCO
								   @CUENTABANCOitem,--cuenta_bancaria,
								   @ESTADO_FACTURA_DEHABILITADO,--factura_habilitado,
								   '',-- razon_social, 
								   @NITitem, --nit,
								   @IDCIUDAD_VARIABLE, --id_ciudad,
								   @ESTADOfiCHA_GESTOR, --estado, --true
								   @USUARIO_DEFAULT --id_usuario
								   );
								   SET @ID_FICHA_IDENTITY= SCOPE_IDENTITY ();

						 END
						   ELSE
						 BEGIN
						 -- -- insertar si no existe en cnx
							  select 'no existe'
							  	 insert into BDMultinivel.dbo.FICHA(codigo, codigo_cnx, nombres, apellidos, ci, correo_electronico, fecha_registro,tel_oficina, tel_movil, tel_fijo, direccion, fecha_nacimiento, contrasena, comentario, avatar, tiene_cuenta_bancaria,id_banco, cuenta_bancaria,factura_habilitado,razon_social, nit,id_ciudad,estado, id_usuario )
								 values(
								   @CONTACTOIDitem, -- codigo, 
								   0 , -- codigo_cnx cero porque no existe en conexion, 
								   @NOMBRECOMPLETO , -- nombres, 
								   '', -- apellidos, 
								   @DOCID, -- ci, 
								   @CORREOELECTRONICOitem, -- correo_electronico, 
								   @FECHAREGISTROitem, -- fecha_registro,
								   '',-- tel_oficina, 
								   @TELEFONOMOVILitem,-- tel_movil, 
								   @TELEFONOFIJOitem,--tel_fijo, 
								   @DIRECCIONitem,--direccion,
								   @FECHANACIMIENTOitem,--fecha_nacimiento,
								   @CONTRASENAitem,--contrasena,
								   '',-- comentario, 
								   '',--avatar, 
								   0,-- tiene_cuenta_bancaria, --CERO IGUAL A FALSE 
								   0,-- id_banco,--POR DEFAUL NO TIENE BANCO
								   @CUENTABANCOitem,--cuenta_bancaria,
								   @ESTADO_FACTURA_DEHABILITADO,--factura_habilitado,
								   '',-- razon_social, 
								   @NITitem, --nit,
								   @IDCIUDAD_GESTOR_DEFAULT_SANTACRUZ, --id_ciudad
								   @ESTADOfiCHA_GESTOR, --estado, --true
								   @USUARIO_DEFAULT --id_usuario
								   );
								  SET @ID_FICHA_IDENTITY= SCOPE_IDENTITY ();

						 END
						--APLICAR DETALLE DE BAJA EN CASO DE TENER BAJA
						 if @BAJAItem <> @NO_TIENEBAJA_GUARDIAN --no tiene baja
						 BEGIN						          
							  INSERT INTO BDMultinivel.dbo.FICHA_TIPO_BAJA_I(motivo, fecha_baja,id_ficha ,id_tipo_baja, estado, id_usuario )
							  values(
							   @MOTIVOBAJAitem,--motivo, 
							   @FECHABAJAitem,--fecha_baja,
							   @ID_FICHA_IDENTITY,--id_ficha ,
							   @TIPOBAJAitem,--id_tipo_baja,
							   1,--estado, activo
							   @USUARIO_DEFAULT --id_usuario 
							  );
							SELECT 'TIENE BAJA'
						 END
						 --REGISTRAR NIVEL para todos por default, no hay restriccion excepto para los vendedores mastes -10
						 INSERT INTO  BDMultinivel.dbo.FICHA_NIVEL_I (id_ficha, id_nivel, habilitado, id_usuario)
								 values(
									  @ID_FICHA_IDENTITY,--id_ficha, 
									  @NIVELIDitem,--id_nivel, 
									  1,--habilitado, true 
									  @USUARIO_DEFAULT--id_usuario
 								 );
						 --REGISTRO DEL CLIENTE Y VENDEDOR SUS LLAVES SERAN, LOS ID CONTACTO DEL CLIENTE Y DEL VENDEDOR						
						 insert into BDMultinivel.dbo.GP_CLIENTE_VENDEDOR_I(id_cliente, id_vendedor, fecha_activacion, fecha_desactivacion, activo, id_usuario, fecha_creacion, fecha_actualizacion)
								 values(
									   @CONTACTOIDitem,--id_cliente, 
									   @PATROCINADORIDitem,--id_vendedor, 
									   @FECHAREGISTROitem,--fecha_activacion, 
									   null,--fecha_desactivacion, 
									   1,--activo, true default 
									   @USUARIO_DEFAULT,--id_usuario
									   GETDATE(),--fecha_creacion, 
									   GETDATE() --fecha_actualizacion
								   );

			   END		    		
		END
		  ELSE
		BEGIN		
			   IF @PATROCINADORIDitem <> @PATROCINADOR_ALTO_RANGO_DEFAULT -- solo se ignora update al patrocinador base 
			   BEGIN
				  select 'existe'

			   END
		END
		
		-------------
		FETCH NEXT FROM CLIENTE_CURSOR INTO  @CONTACTOIDitem, @FECHAREGISTROitem, @TELEFONOFIJOitem, @TELEFONOMOVILitem, @CORREOELECTRONICOitem, @FECHANACIMIENTOitem, @DIRECCIONitem, @DOCID, @PATROCINADORIDitem, @NIVELIDitem, @NOMBRECOMPLETO, @CONTRASENAitem, @CUENTABANCOitem, @CODIGOBANCOitem, @BAJAItem, @FECHABAJAitem, @TIPOBAJAitem, @MOTIVOBAJAitem, @NITitem
		END

		DELETE from @CLIENTES_CNX
		CLOSE CLIENTE_CURSOR
		DEALLOCATE CLIENTE_CURSOR	


	--COMMIT TRANSACTION;

	-----------------------------------------------	 
	 select * from BDComisiones.[dbo].grlCLIENTE_CCN
	 select * from BDComisiones.[dbo].SUCURSAL_CIUDAD
	 select * from BDComisiones.[dbo].gral_ciudad
	 select * from BDMultinivel.dbo.ficha
	 --apunte quitar el top 100 de obtener cliente
	 --actualizar el insert de bajas y reiniciar contador
	 --actualizar ciudad y reiniciar contador
	 --inserto columna codigo_cnx ficha
	 --actualizaar insert nivel y reiniciar el contador si lleva llave

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
                concat ('SP_CARGAR_CONTACTOS ',
                        ' ');
         SET @IMPSUBJECT = 'ALERTA PRODUCCION : no se pudo cargar LOS CONTACTOS ';
         --EXECUTE msdb.dbo.sp_send_dbmail @profile_name   = 'NotificacionSQL',
         --                                @recipients = 'desarrollo@gruposion.bo; UIT-SION@gruposion.bo',
         --                                @body           = @IMPBODY,
         --                                @subject        = @IMPSUBJECT;
         ROLLBACK TRANSACTION;

         SELECT -1 AS 'error catch server';
      END
END CATCH;
