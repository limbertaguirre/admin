USE BDMultinivel;
GO
CREATE PROCEDURE [dbo].[SP_PROCESAR_CERRAR_FORMA_PAGO_REZAGADOS]
    @comision_id int,
    @id_Ciclo int,
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
   DECLARE @ID_TIPO_COMISION_FORMA_PAGO_REZAGADO INT;
   DECLARE @ID_CICLO_VARI INT;
   DECLARE @ESTADO_COMISION_DETALLE_SI_FACTURA INT;
   DECLARE @ESTADO_COMISION_DETALLE_NO_PRESENTA_FACTURA INT;
   DECLARE @NRO_SIN_FORMA_DE_PAGO INT;
   DECLARE @TIPO_COMISION_PAGO_REZAGADO_TABLE INT;
   DECLARE @ESTADO_DESAHABILITADO INT;
   DECLARE @ESTADO_HABILITADO INT;
   DECLARE @ESTADO_COMISION_CERRADO_FORMA_PAGO_REZAGADOS INT;
   DECLARE @ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE INT;
   DECLARE @ESTADO_COMISION_CERRADO_PRORRATEO_FORMA_PAGO_TABLE INT;
   DECLARE @ESTADO_COMISION_CERRADO_PAGO_REZAGADOS INT;

   SET @ID_USUARIO_LOGUEADO= @id_usuario; 
   SET @ID_TIPO_COMISION_FORMA_PAGO_REZAGADO = 2;               --//GP_TIPO_COMISION
   set @TIPO_COMISION_PAGO_REZAGADO_TABLE = 2;           --//GP_TIPO_COMISION  
   SET @TIPO_PAGO_NO_TIENE_FORMA_PAGO = 0;  --cuando no esta registrado en la forma de pago
   SET @ID_CICLO_VARI=@id_Ciclo; 
   SET @ESTADO_COMISION_DETALLE_SI_FACTURA = 2;          --//GP_ESTADO_COMISION_DETALLE
   SET @ESTADO_COMISION_DETALLE_NO_PRESENTA_FACTURA = 6; --// GP_ESTADO_COMISION_DETALLE
   SET @ESTADO_DESAHABILITADO = 0;
   SET @ESTADO_HABILITADO = 1;
   SET @ESTADO_COMISION_CERRADO_FORMA_PAGO_REZAGADOS= 16;  -- //GP_ESTADO_COMISION
   SET @ESTADO_COMISION_CERRADO_PAGO_REZAGADOS = 17;  -- //GP_ESTADO_COMISION
   SET @ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE = 9; -- //GP_ESTADO_COMISION
   SET @ESTADO_COMISION_CERRADO_PRORRATEO_FORMA_PAGO_TABLE=8;

   SET @NRO_SIN_FORMA_DE_PAGO = 0

		DECLARE @CANTIDAD_COMISION_FORMA_PAGO_CERRADO INT = (SELECT COUNT(C.id_comision) FROM BDMultinivel.DBO.GP_COMISION C 
		INNER JOIN BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I CEC ON C.id_comision = CEC.id_comision
		WHERE C.id_ciclo = @id_Ciclo AND CEC.id_estado_comision = @ESTADO_COMISION_CERRADO_FORMA_PAGO_REZAGADOS AND CEC.habilitado = @ESTADO_HABILITADO)

		DECLARE @CANTIDAD_COMISION_PAGOS_CERRADO INT = (SELECT COUNT(C.id_comision) FROM BDMultinivel.DBO.GP_COMISION C 
		INNER JOIN BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I CEC ON C.id_comision = CEC.id_comision
		WHERE C.id_ciclo = @id_Ciclo AND CEC.id_estado_comision = @ESTADO_COMISION_CERRADO_PAGO_REZAGADOS AND CEC.habilitado = @ESTADO_HABILITADO)

		IF(@CANTIDAD_COMISION_FORMA_PAGO_CERRADO > 0 AND @CANTIDAD_COMISION_PAGOS_CERRADO > 0)
		BEGIN	
		 	COMMIT TRANSACTION;		
			RETURN 3
		END
          --estado 2 si facturo 6 no presenta factura
        select @NRO_SIN_FORMA_DE_PAGO=COUNT(idComisionDetalle) from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=@ID_CICLO_VARI and vwF.id_tipo_comision=@ID_TIPO_COMISION_FORMA_PAGO_REZAGADO and id_tipo_pago=@TIPO_PAGO_NO_TIENE_FORMA_PAGO and vwF.idComision = @comision_id

		INSERT INTO @DetallecomisionesSinFormaPagos  select idComisionDetalle, idFicha, ci from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.id_ciclo=@ID_CICLO_VARI and vwF.id_tipo_comision=@ID_TIPO_COMISION_FORMA_PAGO_REZAGADO and id_tipo_pago=@TIPO_PAGO_NO_TIENE_FORMA_PAGO
		  IF @NRO_SIN_FORMA_DE_PAGO > 0
		  BEGIN
		      ---obtener idcomision actual
			      DECLARE @ID_COMISION_ACTUAL INT;
				  DECLARE @TOTAL_NETO_COMISION_ACTUAL DECIMAL(18,2);	
				  DECLARE @TOTAL_NETO_COMISION_NUEVO_RECHAZADO DECIMAL(18,2);	
				  DECLARE @ID_COMISION_NEW_RESAGADO INT;

				   select @ID_COMISION_ACTUAL = CO.id_comision  from BDMultinivel.dbo.GP_COMISION CO
				   inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I COE ON COE.id_comision = CO.id_comision
				   where co.id_ciclo= @ID_CICLO_VARI and co.id_tipo_comision=@ID_TIPO_COMISION_FORMA_PAGO_REZAGADO and COE.id_estado_comision=@ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE and CO.id_comision = @comision_id and COE.habilitado = @ESTADO_HABILITADO

				   --obtener totales
						select @TOTAL_NETO_COMISION_NUEVO_RECHAZADO = sum(vwF.montoNeto) from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where vwF.idComision = @comision_id and vwF.id_ciclo=@ID_CICLO_VARI and vwF.id_estado_comision = @ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE and vwF.id_tipo_comision = @ID_TIPO_COMISION_FORMA_PAGO_REZAGADO and id_tipo_pago = 0 --total los resagados forma de pagos
						select @TOTAL_NETO_COMISION_ACTUAL = sum(vwF.montoNeto) from BDMultinivel.dbo.vwObtenercomisionesFormaPago vwF where  vwF.idComision = @comision_id and vwF.id_ciclo=@ID_CICLO_VARI and vwF.id_estado_comision = @ESTADO_COMISION_PENDIENTE_FORMA_PAGO_TABLE and vwF.id_tipo_comision = @ID_TIPO_COMISION_FORMA_PAGO_REZAGADO and id_tipo_pago != 0 --total los que tienen pagado
                
				   -- actualizar el monto neto de comision actual 				   
					   UPDATE BDMultinivel.dbo.GP_COMISION  
					   SET monto_total_neto =@TOTAL_NETO_COMISION_ACTUAL  where id_comision=@ID_COMISION_ACTUAL 


		      --duplicar ciclo a comision resagada
			  DECLARE @IDCOMISION_SCOPE INT;
			  SET @IDCOMISION_SCOPE = 0;
			  DECLARE @monto_total_bruto DECIMAL(18,2); DECLARE @porcentaje_retencion DECIMAL(18,2); DECLARE @monto_total_retencion DECIMAL(18,2); DECLARE @monto_total_aplicacion DECIMAL(18,2); DECLARE @monto_total_neto DECIMAL(18,2); 
			  select @monto_total_bruto= monto_total_bruto,@porcentaje_retencion=porcentaje_retencion, @monto_total_retencion=monto_total_retencion,@monto_total_aplicacion=monto_total_aplicacion, @monto_total_neto=monto_total_neto  from BDMultinivel.dbo.GP_COMISION co where  co.id_ciclo=@ID_CICLO_VARI and id_tipo_comision=@ID_TIPO_COMISION_FORMA_PAGO_REZAGADO
			  ----------------------------------------------------------------------------------
			  --actualizar la comision estado desactiva y agregar a estado cerrado forma de pago
			  declare @id_comision_estado_detalle int; set @id_comision_estado_detalle= 0;
			  declare @id_comision_selec int; set @id_comision_selec=0;

			  select @id_comision_estado_detalle= GCO.id_comision_estado_comision_i, @id_comision_selec= GPCO.id_comision from BDMultinivel.dbo.GP_COMISION GPCO
			  inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I GCO on GCO.id_comision = GPCO.id_comision
			  where GPCO.id_ciclo = @ID_CICLO_VARI and GPCO.id_tipo_comision= @ID_TIPO_COMISION_FORMA_PAGO_REZAGADO and GPCO.id_comision = @comision_id

			    IF @id_comision_estado_detalle > 0
				BEGIN
				    update  BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I 
					set habilitado=@ESTADO_DESAHABILITADO  where id_comision_estado_comision_i= @id_comision_estado_detalle

					INSERT INTO BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I(habilitado,id_comision, id_estado_comision, id_usuario, fecha_creacion, fecha_actualizacion )
					VALUES(
					    @ESTADO_HABILITADO,		--habilitado,
						@id_comision_selec,		--id_comision, 
						@ESTADO_COMISION_CERRADO_FORMA_PAGO_REZAGADOS, --id_estado_comision, 
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
					@TOTAL_NETO_COMISION_NUEVO_RECHAZADO,  --monto_total_neto, --este es el monto actual rezagado
					@ID_CICLO_VARI,  --id_ciclo, 
					@TIPO_COMISION_PAGO_REZAGADO_TABLE,  --id_tipo_comision, 
					@ID_USUARIO_LOGUEADO,  --id_usuario,
					GETDATE(),  --fecha_creacion,
					GETDATE()  --fecha_actualizacion
				  );
				  SET @IDCOMISION_SCOPE = (select IDENT_CURRENT('GP_COMISION'));
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

GO
