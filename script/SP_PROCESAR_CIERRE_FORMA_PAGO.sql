
alter PROCEDURE [dbo].[SP_PROCESAR_CERRAR_FORMA_PAGO]
     @id_Ciclo     int,
     @id_usuario int
	 --@id_tipo_comision_pago_comision   int,
	 --@detalleComision_estado_id_si_facturo   int,
	 --@detalleComision_estado_id_no_presenta_factura  int,
	 --@id_tipo_comision_pago_rezagado  int
AS

BEGIN TRANSACTION;
BEGIN TRY
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
   DECLARE @DetallecomisionesSinFormaPagos as table( idComisionDetalle  int, idFicha INT, ci varchar(100));

   DECLARE @ID_USUARIO_LOGUEADO INT;
   DECLARE @TIPO_PAGO_NO_TIENE_FORMA_PAGO INT;
   DECLARE @ID_TIPO_COMISION_PAGOCOMISION INT;
   DECLARE @ID_CICLO_VARI INT;
   DECLARE @ESTADO_COMISION_DETALLE_SI_FACTURA INT;
   DECLARE @ESTADO_COMISION_DETALLE_NO_PRESENTA_FACTURA INT;
   DECLARE @NRO_SIN_FORMA_DE_PAGO INT;
   DECLARE @TIPO_COMISION_PAGO_REZAGADO_TABLE INT;
   DECLARE @ESTADO_DESAHABILITADO INT;
   DECLARE @ESTADO_HABILITADO INT;
   DECLARE @ESTADO_COMISION_CERRADO_FORMA_PAGO_TABLE INT;
   DECLARE @ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE INT;

   SET @ID_USUARIO_LOGUEADO= @id_usuario; 
   SET @ID_TIPO_COMISION_PAGOCOMISION=1;               --//GP_TIPO_COMISION
   set @TIPO_COMISION_PAGO_REZAGADO_TABLE=2;           --//GP_TIPO_COMISION  
   SET @TIPO_PAGO_NO_TIENE_FORMA_PAGO= 0;  --cuando no esta registrado en la forma de pago
   SET @ID_CICLO_VARI=@id_Ciclo; 
   SET @ESTADO_COMISION_DETALLE_SI_FACTURA=2;          --//GP_ESTADO_COMISION_DETALLE
   SET @ESTADO_COMISION_DETALLE_NO_PRESENTA_FACTURA=6; --// GP_ESTADO_COMISION_DETALLE
   SET @ESTADO_DESAHABILITADO=0;
   SET @ESTADO_HABILITADO=1;
   SET @ESTADO_COMISION_CERRADO_FORMA_PAGO_TABLE= 10;  -- //GP_ESTADO_COMISION
   SET @ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE= 9; -- //GP_ESTADO_COMISION

   SET @NRO_SIN_FORMA_DE_PAGO=0

          --estado 2 si facturo 6 no presenta factura
          select @NRO_SIN_FORMA_DE_PAGO=COUNT(idComisionDetalle) from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=@ID_CICLO_VARI and vwF.id_tipo_comision=@ID_TIPO_COMISION_PAGOCOMISION and id_tipo_pago=@TIPO_PAGO_NO_TIENE_FORMA_PAGO
		 INSERT INTO @DetallecomisionesSinFormaPagos  select idComisionDetalle, idFicha, ci from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=@ID_CICLO_VARI and vwF.id_tipo_comision=@ID_TIPO_COMISION_PAGOCOMISION and id_tipo_pago=@TIPO_PAGO_NO_TIENE_FORMA_PAGO
		  IF @NRO_SIN_FORMA_DE_PAGO > 0
		  BEGIN
		      --duplicar ciclo a comision resagada
			  DECLARE @IDCOMISION_SCOPE INT;
			  SET @IDCOMISION_SCOPE = 0;
			  DECLARE @monto_total_bruto DECIMAL(18,2); DECLARE @porcentaje_retencion DECIMAL(18,2); DECLARE @monto_total_retencion DECIMAL(18,2); DECLARE @monto_total_aplicacion DECIMAL(18,2); DECLARE @monto_total_neto DECIMAL(18,2); 
			  select @monto_total_bruto= monto_total_bruto,@porcentaje_retencion=porcentaje_retencion, @monto_total_retencion=monto_total_retencion,@monto_total_aplicacion=monto_total_aplicacion, @monto_total_neto=monto_total_neto  from BDMultinivel.dbo.GP_COMISION co where  co.id_ciclo=@ID_CICLO_VARI and id_tipo_comision=@ID_TIPO_COMISION_PAGOCOMISION
			  ----------------------------------------------------------------------------------
			  --actualizar la comision estado desactiva y agregar a estado cerrado forma de pago
			  declare @id_comision_estado_detalle int; set @id_comision_estado_detalle= 0;
			  declare @id_comision_selec int; set @id_comision_selec=0;

			  select @id_comision_estado_detalle= GCO.id_comision_estado_comision_i, @id_comision_selec= GPCO.id_comision from BDMultinivel.dbo.GP_COMISION GPCO
			  inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GCO on GCO.id_comision = GPCO.id_comision
			  where GPCO.id_ciclo = @ID_CICLO_VARI and GPCO.id_tipo_comision= @ID_TIPO_COMISION_PAGOCOMISION

			    IF @id_comision_estado_detalle > 0
				BEGIN
				    update  BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I 
					set habilitado=@ESTADO_DESAHABILITADO  where id_comision_estado_comision_i= @id_comision_estado_detalle

					INSERT INTO BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I(habilitado,id_comision, id_estado_comision, id_usuario, fecha_creacion, fecha_actualizacion )
					VALUES(
					    @ESTADO_HABILITADO,		--habilitado,
						@id_comision_selec,		--id_comision, 
						@ESTADO_COMISION_CERRADO_FORMA_PAGO_TABLE, --id_estado_comision, 
						@ID_USUARIO_LOGUEADO,	--id_usuario, 
						GETDATE(),				--fecha_creacion,
						GETDATE()				--fecha_actualizacion
					);
				END				
			  ----------------------------------------------------------------------
			  --agregar nueva comision de rezagado
			  insert into BDMultinivel.dbo.GP_COMISION(monto_total_bruto, porcentaje_retencion, monto_total_retencion, monto_total_aplicacion, monto_total_neto,id_ciclo, id_tipo_comision, id_usuario,fecha_creacion, fecha_actualizacion )
				  values(
					@monto_total_bruto,  --monto_total_bruto, 
					@porcentaje_retencion,  --porcentaje_retencion,
					@monto_total_retencion,  --monto_total_retencion,
					@monto_total_aplicacion,  --monto_total_aplicacion,
					@monto_total_neto,  --monto_total_neto,
					@ID_CICLO_VARI,  --id_ciclo, 
					@TIPO_COMISION_PAGO_REZAGADO_TABLE,  --id_tipo_comision, 
					@ID_USUARIO_LOGUEADO,  --id_usuario,
					GETDATE(),  --fecha_creacion,
					GETDATE()  --fecha_actualizacion
				  );
				  SET @IDCOMISION_SCOPE = SCOPE_IDENTITY ();
				  --agregar detalle de estado comision rezagado  en pendiente de forma de pago
					  INSERT INTO BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I(habilitado,id_comision, id_estado_comision, id_usuario, fecha_creacion, fecha_actualizacion )
						VALUES(
							@ESTADO_HABILITADO,		--habilitado,
							@IDCOMISION_SCOPE,		--id_comision, 
							@ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE, --id_estado_comision, 
							@ID_USUARIO_LOGUEADO,	--id_usuario, 
							GETDATE(),				--fecha_creacion,
							GETDATE()				--fecha_actualizacion
						);
              -- -- -- agregar el detalle de y actualizar la comision a resagados 
			  ----------------------------------------------------------------------------------------------------------
			        DECLARE @IDDETALLECOMISIONitem INT
					DECLARE @IDFICHAitem INT
					DECLARE @CIitem  varchar(100)

					DECLARE COMISION_REZAGADO_CURSOR CURSOR FOR 
					Select idComisionDetalle, idFicha, ci from @DetallecomisionesSinFormaPagos
					OPEN COMISION_REZAGADO_CURSOR
					FETCH NEXT FROM COMISION_REZAGADO_CURSOR INTO @IDDETALLECOMISIONitem, @IDFICHAitem, @CIitem
					WHILE @@FETCH_STATUS = 0  
					BEGIN 
					-------------
					DECLARE @ID_SELECCIONADO INT;
					SET @ID_SELECCIONADO=0;
				    select @ID_SELECCIONADO= id_comision_detalle from BDMultinivel.dbo.GP_COMISION_DETALLE GCD where GCD.id_comision_detalle=@IDDETALLECOMISIONitem

					if @ID_SELECCIONADO > 0
					BEGIN
					update  BDMultinivel.dbo.GP_COMISION_DETALLE 
						set id_comision=@IDCOMISION_SCOPE
						where id_comision_detalle=@IDDETALLECOMISIONitem
					END					
					
					-------------
					FETCH NEXT FROM COMISION_REZAGADO_CURSOR INTO @IDDETALLECOMISIONitem, @IDFICHAitem, @CIitem
					END

					DELETE from @DetallecomisionesSinFormaPagos
					CLOSE COMISION_REZAGADO_CURSOR
					DEALLOCATE COMISION_REZAGADO_CURSOR

				-------------------------------------------------------------------------------------------------------------


		  END
		    ELSE
		  BEGIN		  
			     COMMIT TRANSACTION;		    
		         return 1
		  END

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
