
ALTER PROCEDURE [dbo].[SP_PAGAR_SION_PAY_COMISIONES_CICLO]
     @id_Ciclo     int,
     @id_usuario int
AS

BEGIN TRANSACTION;
BEGIN TRY
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
 BEGIN  -- Section recargando planilla basica

  DECLARE @COMISIONES as table(id_comision int, idComisionDetalle  int, idFicha INT,nombre varchar(100), ci varchar(100), id_tipo_pago int, tipo_pago varchar(100), ciudad varchar(100), pais varchar(100));

    DECLARE @IDCICLO INT;
	DECLARE @IDUSUARIO INT;
	DECLARE @USUARIO_LOGUADO VARCHAR(100);
	DECLARE @USUARIO_NOMBRE VARCHAR(100);
	DECLARE @DESCRIPCION_CICLO VARCHAR(100);
	DECLARE @PARAM_ID_TIPO_COMISION_PAGO_COMISION INT;
	DECLARE @ID_COMISION_SELECTED INT;
	DECLARE @ID_TIPO_SION_PAY INT;
	DECLARE @ESTADO_COMISION_ACTIVO INT;

	

	SET @IDCICLO=@id_Ciclo
	SET @IDUSUARIO= @id_usuario
	SET @USUARIO_LOGUADO = '';
	SET @USUARIO_NOMBRE='';
	SET @DESCRIPCION_CICLO = '';
	SET @PARAM_ID_TIPO_COMISION_PAGO_COMISION=1; -- //GP_TIPO_COMISION
	SET @ID_COMISION_SELECTED=0;
	SET @ID_TIPO_SION_PAY=1;  --//TIPO_PAGO
	SET @ESTADO_COMISION_ACTIVO=1;

	  SELECT @DESCRIPCION_CICLO= nombre FROM  BDMultinivel.dbo.CICLO WHERE id_ciclo=@IDCICLO
	  SELECT @USUARIO_LOGUADO= usuario, @USUARIO_NOMBRE= nombres + ' '+apellidos FROM BDMultinivel.dbo.USUARIO where id_usuario=@IDUSUARIO
	  select  @ID_COMISION_SELECTED= id_comision from BDMultinivel.dbo.GP_COMISION WHERE id_ciclo=@IDCICLO and id_tipo_comision=@PARAM_ID_TIPO_COMISION_PAGO_COMISION
	  -----------------------
	 
	  ---------------------------------------
	  insert into @COMISIONES  select id_comision, CD.id_comision_detalle,FIC.id_ficha,FIC.nombres +' '+ FIC.apellidos AS 'nombre', FIC.ci,TIPO.id_tipo_pago, TIPO.nombre as 'tipo_pago', CIU.nombre AS 'ciudad', PAI.nombre AS 'pais' from BDMultinivel.dbo.GP_COMISION_DETALLE CD 
	    INNER JOIN BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIFO ON LIFO.id_comisiones_detalle= CD.id_comision_detalle
		INNER JOIN  BDMultinivel.dbo.TIPO_PAGO TIPO ON TIPO.id_tipo_pago= LIFO.id_tipo_pago
		INNER JOIN BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha = CD.id_ficha
		INNER JOIN BDMultinivel.dbo.CIUDAD CIU ON CIU.id_ciudad = FIC.id_ciudad
		INNER JOIN BDMultinivel.dbo.PAIS PAI ON PAI.id_pais= CIU.id_pais
		where CD.id_comision= @ID_COMISION_SELECTED AND LIFO.id_tipo_pago=@ID_TIPO_SION_PAY

	   SET @DESCRIPCION_CICLO = CONCAT('RECARGA - Desde comisiones  - ', @DESCRIPCION_CICLO)

	   ----------------------------------------------------------------------------------------------------------
	   ----------------------------------------------------------------------------------------------------------
	      DECLARE @DE_IDCOMISION_ITEM INT;
		  DECLARE @DE_IDCOMISIONdETALLE_ITEM INT;
		  DECLARE @DE_IDFICHA_ITEM INT;
		  DECLARE @DE_NOMBRE_ITEM VARCHAR(100);
		  DECLARE @DE_CARNET_ITEM VARCHAR(100);
		  DECLARE @DE_IDTIPOPAGO_ITEM INT;
		  DECLARE @DE_TIPOPAGO_ITEM VARCHAR(100);
		  DECLARE @DE_CIUDAD_ITEM VARCHAR(100);
		  DECLARE @DE_PAIS_ITEM VARCHAR(100);

		  declare @NRO_ORDEN INT;
		  SET @NRO_ORDEN= 0;

					DECLARE COMISION_CURSOR CURSOR FOR 
					Select id_comision, idComisionDetalle, idFicha, nombre, ci, id_tipo_pago, tipo_pago,ciudad,pais from @COMISIONES
					OPEN COMISION_CURSOR
					FETCH NEXT FROM COMISION_CURSOR INTO @DE_IDCOMISION_ITEM, @DE_IDCOMISIONdETALLE_ITEM, @DE_IDFICHA_ITEM, @DE_NOMBRE_ITEM, @DE_CARNET_ITEM, @DE_IDTIPOPAGO_ITEM, @DE_TIPOPAGO_ITEM, @DE_CIUDAD_ITEM, @DE_PAIS_ITEM 

					WHILE @@FETCH_STATUS = 0  
					BEGIN 
					------------------------INICIO FORCOMISION
					DECLARE @CANTIDADempresas INT;
					DECLARE @CUENTA_SIONPAY VARCHAR(100);
					SET @CANTIDADempresas=0;
					SET @CUENTA_SIONPAY='';

					DECLARE @COMISIONES_empresas as table(id_comision_detalle_empresa int, monto_neto DECIMAL(18,2), id_empresa INT, id_comprobante_generico INT, id_movimiento INT );
					
					SELECT @CUENTA_SIONPAY=nro_cuenta FROM BDPuntosCash.dbo.CUENTA where id_usuario= @DE_CARNET_ITEM
				    INSERT INTO @COMISIONES_empresas select id_comision_detalle_empresa, monto_neto, id_empresa, id_comprobante_generico, id_movimiento from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle=@DE_IDCOMISIONdETALLE_ITEM

				   SELECT @CANTIDADempresas= COUNT(*) FROM @COMISIONES_empresas
				   IF @CANTIDADempresas > 0
				   BEGIN
							----------------------------------------------------------------------------------------------------------
						    DECLARE @IDCOMISIONEMPRESA_item INT;
							DECLARE @MONTONETO_item DECIMAL(18,2);
							DECLARE @IDEMPRESA_item INT;
							DECLARE @IDCOMPROBANTEGENERICO_item INT;
							DECLARE @IDMOVIMIENTO_item INT;

								DECLARE COMISION_EMPRESA_CURSOR CURSOR FOR 
								Select id_comision_detalle_empresa, monto_neto, id_empresa, id_comprobante_generico, id_movimiento from @COMISIONES_empresas
								OPEN COMISION_EMPRESA_CURSOR
								FETCH NEXT FROM COMISION_EMPRESA_CURSOR INTO @IDCOMISIONEMPRESA_item, @MONTONETO_item, @IDEMPRESA_item, @IDCOMPROBANTEGENERICO_item, @IDMOVIMIENTO_item

								WHILE @@FETCH_STATUS = 0  
								BEGIN 
								------------------------------------------------------
								 IF @IDMOVIMIENTO_item = 0
								 BEGIN
										SET @NRO_ORDEN= @NRO_ORDEN + 1;
											DECLARE @IDEMPRESA_CNX INT;
											DECLARE @NOMBRE_EMPRESA varchar(100);
											SET @IDEMPRESA_CNX=0;
											SET @NOMBRE_EMPRESA= '';
											SELECT @IDEMPRESA_CNX= em.codigo_cnx , @NOMBRE_EMPRESA= em.nombre FROM BDMultinivel.dbo.EMPRESA em where em.id_empresa= @IDEMPRESA_item
											-----------
											--VALIDAR MONTO QUE CERO, CERO PORQUE APLICO EN UN PRODUCTO
											IF @MONTONETO_item > 0
											BEGIN 
												--	-- Registrar tabla temporal
												   INSERT INTO BDPuntosCash.DBO.COMISIONES_XLS (nro, lciclo_id, fecha_regitro, autorizacion, usuario, empresa, id_empresa, nombre_cliente, doc_id, nro_cuenta, monto, fecha_ins, id_comisiones_estado_xls) 
												   VALUES (
														   @NRO_ORDEN, --nro,
														   @IDCICLO, --lciclo_id, 
														   GETDATE(), --fecha_regitro, 
														   @USUARIO_NOMBRE, --autorizacion, 
														   @USUARIO_LOGUADO, --usuario, 
														   @NOMBRE_EMPRESA, --empresa, 
														   @IDEMPRESA_CNX, -- id_empresa, 
														   @DE_NOMBRE_ITEM, --nombre_cliente,
														   @DE_CARNET_ITEM, --doc_id, 
														   @CUENTA_SIONPAY,--nro_cuenta, 
														   @MONTONETO_item, -- monto, 
														   GETDATE(), --fecha_ins, 
														   @ESTADO_COMISION_ACTIVO -- id_comisiones_estado_xls
														   );
											END 
											ELSE
											BEGIN
												 --MONTO CERO, ADD EMPRESA Y NOMBRE EMPRESA
													  insert into BDMultinivel.dbo.LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL( id_ciclo, id_ficha,  carnet, cuenta_sion_pay, id_detalle_comision, id_detalle_comision_empresa, monto,descripcion,id_empresa_cnx, nombre_empresa, fecha_creacion, fecha_actualizacion )
													   values(					       
														  @IDCICLO, -- id_ciclo, 
														  @DE_IDFICHA_ITEM, --id_ficha, 
														  @DE_CARNET_ITEM, --carnet, 
														  @CUENTA_SIONPAY, --cuenta_sion_pay, 
														  @DE_IDCOMISIONdETALLE_ITEM, --id_detalle_comision,
														  @IDCOMISIONEMPRESA_item, --id_detalle_comision_empresa,
														  @MONTONETO_item,--monto,
														  'MONTO CERO', --descripcion,
														  @IDEMPRESA_CNX, --id_empresa_cnx,
														  @NOMBRE_EMPRESA,  --nombre_empresa,
														  GETDATE(),--fecha_creacion,
														  GETDATE()--fecha_actualizacion
														  );
											END

                                  END										

								-------------------------------------------------------
								FETCH NEXT FROM COMISION_EMPRESA_CURSOR INTO @IDCOMISIONEMPRESA_item, @MONTONETO_item, @IDEMPRESA_item, @IDCOMPROBANTEGENERICO_item, @IDMOVIMIENTO_item
								END

								DELETE from @COMISIONES_empresas
								CLOSE COMISION_EMPRESA_CURSOR
								DEALLOCATE COMISION_EMPRESA_CURSOR
							-----------------------------------------------------------------------------------------------------------
					END
					   ELSE
					BEGIN
					--   SELECT @DE_IDCOMISIONdETALLE_ITEM AS 'NO TIENE DETALLE EMPRESA'					  
					   declare @descripcionFail varchar(500);
					   set @descripcionFail= CONCAT('id detalle: ', @DE_IDCOMISIONdETALLE_ITEM ,' esta id comision no tiene detalle por empresa')
					   insert into BDMultinivel.dbo.LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL( id_ciclo, id_ficha,  carnet, cuenta_sion_pay, id_detalle_comision, id_detalle_comision_empresa, monto,descripcion,id_empresa_cnx, nombre_empresa, fecha_creacion, fecha_actualizacion )
							   values(					       
								  @IDCICLO, -- id_ciclo, 
								  @DE_IDFICHA_ITEM, --id_ficha, 
								  @DE_CARNET_ITEM, --carnet, 
								  @CUENTA_SIONPAY, --cuenta_sion_pay, 
								  @DE_IDCOMISIONdETALLE_ITEM, --id_detalle_comision,
								  0, --id_detalle_comision_empresa,
								  0,--monto,
								  @descripcionFail, --descripcion,	
								  0, --id_empresa_cnx,
								  '',  --nombre_empresa,
								  GETDATE(),--fecha_creacion,
								  GETDATE()--fecha_actualizacion
								  );

					END


					----------------------- FIN FORCOMISION
					FETCH NEXT FROM COMISION_CURSOR INTO @DE_IDCOMISION_ITEM, @DE_IDCOMISIONdETALLE_ITEM, @DE_IDFICHA_ITEM, @DE_NOMBRE_ITEM, @DE_CARNET_ITEM, @DE_IDTIPOPAGO_ITEM, @DE_TIPOPAGO_ITEM, @DE_CIUDAD_ITEM, @DE_PAIS_ITEM 
					END

					DELETE from @COMISIONES
					CLOSE COMISION_CURSOR
					DEALLOCATE COMISION_CURSOR
   END
-----------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------
 BEGIN --proceso comision detalle sion pay
		DECLARE @doc_id VARCHAR(MAX), @nro_cuenta VARCHAR(MAX)
			DECLARE @monto DECIMAL(18,2)
			DECLARE @id_comisiones_maestro INT
				DECLARE Cursor_comisiones CURSOR FOR 
				SELECT doc_id, SUM(monto) monto, nro_cuenta FROM BDPuntosCash.DBO.COMISIONES_XLS where id_comisiones_estado_xls = 1 and lciclo_id = @IDCICLO GROUP by doc_id, nro_cuenta
				OPEN Cursor_comisiones FETCH NEXT FROM Cursor_comisiones INTO @doc_id, @monto, @nro_cuenta
				WHILE @@FETCH_STATUS = 0  
				BEGIN  
					-- INSERTAR MAESTRO
					INSERT INTO BDPuntosCash.DBO.COMISIONES_MAESTRO 
						(id_comisiones_tipo_recarga, id_comisiones_estado_maestro_detalle,lciclo_id, doc_id, nro_cuenta, monto)
					VALUES
						(1, 1,@IDCICLO, @doc_id, @nro_cuenta, @monto)
        
					set @id_comisiones_maestro = SCOPE_IDENTITY ();  -- (select IDENT_CURRENT('COMISIONES_MAESTRO'))

					-- INSERTAR DETALLE
					INSERT INTO BDPuntosCash.DBO.COMISIONES_DETALLE
					select @id_comisiones_maestro, id_comisiones_xls, 1,1,@IDCICLO, doc_id, id_empresa, nro_cuenta, monto,0, NULL, GETDATE() from BDPuntosCash.DBO.COMISIONES_XLS  
					where doc_id = @doc_id AND id_comisiones_estado_xls = 1 and lciclo_id = @IDCICLO 

					-- UPDATE XLS ESTADOS
					UPDATE BDPuntosCash.DBO.COMISIONES_XLS SET id_comisiones_estado_xls = 3 WHERE doc_id = @doc_id AND id_comisiones_estado_xls = 1 and lciclo_id = @IDCICLO 
					FETCH NEXT FROM Cursor_comisiones INTO @doc_id, @monto, @nro_cuenta
				END 
				CLOSE Cursor_comisiones  
				DEALLOCATE Cursor_comisiones 
					--select id_comisiones_detalle, doc_id, nro_cuenta, monto, id_empresa 
					--from COMISIONES_DETALLE 
					--where lciclo_id = @IDCICLO and id_comisiones_estado_maestro_detalle = 1 and id_movimiento = 0 ORDER BY doc_id, monto DESC			
--		------------------------------------------------------------------------------------------------------------
END

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		----ejecutar el segundo proceso donde solo recargara de detalle---------------------------------------------------------------------------------------------------------
		--	 DECLARE @DDE_IDCOMISION_ITEM INT;
		--	  DECLARE @2DE_IDCOMISIONdETALLE_ITEM INT;
		--	  DECLARE @2DE_IDFICHA_ITEM INT;
		--	  DECLARE @2DE_NOMBRE_ITEM VARCHAR(100);
		--	  DECLARE @2DE_CARNET_ITEM VARCHAR(100);
		--	  DECLARE @2DE_IDTIPOPAGO_ITEM INT;
		--	  DECLARE @2DE_TIPOPAGO_ITEM VARCHAR(100);
		--	  DECLARE @2DE_CIUDAD_ITEM VARCHAR(100);
		--	  DECLARE @2DE_PAIS_ITEM VARCHAR(100);

			

		--			DECLARE COMISION_CURSOR_TRES CURSOR FOR 
		--			Select id_comision, idComisionDetalle, idFicha, nombre, ci, id_tipo_pago, tipo_pago,ciudad,pais from @COMISIONES
		--			OPEN COMISION_CURSOR_TRES
		--			FETCH NEXT FROM COMISION_CURSOR_TRES INTO @DDE_IDCOMISION_ITEM, @2DE_IDCOMISIONdETALLE_ITEM, @2DE_IDFICHA_ITEM, @2DE_NOMBRE_ITEM, @2DE_CARNET_ITEM, @2DE_IDTIPOPAGO_ITEM, @2DE_TIPOPAGO_ITEM, @2DE_CIUDAD_ITEM, @2DE_PAIS_ITEM 

		--			WHILE @@FETCH_STATUS = 0  
		--			BEGIN 
		--			------------------------INICIO FORCOMISION
		--			DECLARE @2_CANTIDADempresas INT;
		--			DECLARE @2_CUENTA_SIONPAY VARCHAR(100);
		--			SET @2_CANTIDADempresas=0;
		--			SET @2_CUENTA_SIONPAY='';

		--			DECLARE @2_COMISIONES_empresas as table(id_comision_detalle_empresa int, monto_neto DECIMAL(18,2), id_empresa INT, id_comprobante_generico INT, id_movimiento INT );
					
		--			SELECT @2_CUENTA_SIONPAY=nro_cuenta FROM BDPuntosCash.dbo.CUENTA where id_usuario= @2DE_CARNET_ITEM
		--		    INSERT INTO @2_COMISIONES_empresas select id_comision_detalle_empresa, monto_neto, id_empresa, id_comprobante_generico, id_movimiento from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle=@2DE_IDCOMISIONdETALLE_ITEM

		--		   SELECT @2_CANTIDADempresas= COUNT(*) FROM @2_COMISIONES_empresas
		--		   IF @2_CANTIDADempresas > 0
		--		   BEGIN
		--					----------------------------------------------------------------------------------------------------------
		--				    DECLARE @2_IDCOMISIONEMPRESA_item INT;
		--					DECLARE @2_MONTONETO_item DECIMAL(18,2);
		--					DECLARE @2_IDEMPRESA_item INT;
		--					DECLARE @2_IDCOMPROBANTEGENERICO_item INT;
		--					DECLARE @2_IDMOVIMIENTO_item INT;

		--						DECLARE COMISION_2_CURSOR CURSOR FOR 
		--						Select id_comision_detalle_empresa, monto_neto, id_empresa, id_comprobante_generico, id_movimiento from @2_COMISIONES_empresas
		--						OPEN COMISION_2_CURSOR
		--						FETCH NEXT FROM COMISION_2_CURSOR INTO @2_IDCOMISIONEMPRESA_item, @2_MONTONETO_item, @2_IDEMPRESA_item, @2_IDCOMPROBANTEGENERICO_item, @2_IDMOVIMIENTO_item

		--						WHILE @@FETCH_STATUS = 0  
		--						BEGIN 
		--						------------------------------------------------------
		--						 IF @2_IDMOVIMIENTO_item = 0 and @2_MONTONETO_item > 0  --EN MI DETALLE ME ASEGURO 
		--						 BEGIN
										
		--								DECLARE @2_IDEMPRESA_CNX INT;
		--								SET @2_IDEMPRESA_CNX=0;
		--								SELECT @2_IDEMPRESA_CNX= em.codigo_cnx  FROM BDMultinivel.dbo.EMPRESA em where em.id_empresa= @2_IDEMPRESA_item
		--								-----------
		--								--VALIDAR MONTO QUE CERO, CERO PORQUE APLICO EN UN PRODUCTO
		--							    DECLARE @SIONPAY_IDCOMISIONDETALLE INT; DECLARE @SIONPAY_NROCUENTA VARCHAR(100); DECLARE @SIONPAY_MONTO DECIMAL(18,2);
		--								SET @SIONPAY_IDCOMISIONDETALLE=0; SET @SIONPAY_NROCUENTA=''; SET  @SIONPAY_MONTO=0; 	
											
		--									    select @SIONPAY_IDCOMISIONDETALLE= id_comisiones_detalle, @SIONPAY_NROCUENTA= nro_cuenta,@SIONPAY_MONTO= monto from BDPuntosCash.dbo.COMISIONES_DETALLE 
		--										where lciclo_id = @IDCICLO and id_comisiones_estado_maestro_detalle = 1 and id_movimiento = 0 and doc_id=@2DE_CARNET_ITEM and id_empresa=@2_IDEMPRESA_CNX
		--										ORDER BY doc_id, monto DESC
		--							        ---VERIFICAMOS EL DETALLE DE COMISION SI VIENE NULL FUE PROCESADO Y  YA NO EXISTE CON ESTE ESTADO
		--									IF @SIONPAY_IDCOMISIONDETALLE >0
		--									BEGIN
		--										--OBTENER CUENTAID Y SALDO ACTUAL
		--										DECLARE @CUENTAID INT; DECLARE @SALDOACTUAL DECIMAL(18,2); DECLARE @SALDOANTERIOR DECIMAL(18,2);
		--										SET @CUENTAID=0; SET @SALDOACTUAL=0; SET @SALDOANTERIOR=0;
		--										SELECT @CUENTAID= id_cuenta, @SALDOACTUAL= saldo_actual FROM BDPuntosCash.DBO.CUENTA WHERE nro_cuenta=@SIONPAY_NROCUENTA
		--									   IF @CUENTAID > 0 --TIENE CUENTA
		--									   BEGIN
		--									         DECLARE @IDMOVIMIENTO_GENERIC INT;
		--											 DECLARE @IDCOMPROBANTE_NEW_GENERICO INT;
		--											 SET @IDMOVIMIENTO_GENERIC=0;
		--											 SET @IDCOMPROBANTE_NEW_GENERICO=0;

		--									         SET @SALDOANTERIOR= @SALDOACTUAL;
		--									         SET @SALDOACTUAL= @SALDOACTUAL + @SIONPAY_MONTO;
		--										     UPDATE BDPuntosCash.DBO.CUENTA  SET saldo_actual=@SALDOACTUAL WHERE id_cuenta= @CUENTAID;

		--											 INSERT INTO BDPuntosCash.dbo.movimiento(id_cuenta, id_agente, id_tipo_movimiento,monto,saldo_actual,saldo_anterior, fecha, descripcion, id_estado_movimiento)
		--													 VALUES(
		--													 @CUENTAID, -- idCuenta, //19,//
		--													 3,         -- idagente, recarga_sistema_guardian
		--													 1,          --	'id_tipo_movimiento' 1 ingreso
		--													 @SIONPAY_MONTO,  -- monto
		--													 @SALDOACTUAL,     --	saldo_actual
		--													 @SALDOANTERIOR,   --	saldo_anterior
		--													 GETDATE(),	       -- fecha
		--													 @DESCRIPCION_CICLO, --descripcion
		--													 1          --id_estado_movimiento //1// realizado
		--													 )
		--											SET @IDMOVIMIENTO_GENERIC= SCOPE_IDENTITY ();
		--											-- --ACTUALIZAR COMISION DETALLE
		--											update BDPuntosCash.DBO.COMISIONES_DETALLE set id_movimiento= @IDMOVIMIENTO_GENERIC, fecha_movimiento =GETDATE(), id_comisiones_estado_maestro_detalle=1 WHERE id_comisiones_detalle=@SIONPAY_IDCOMISIONDETALLE
		--											-- --GENERAR COMPROBANTE MEDIANTE SP
													   
		--											-- --ACTUALIZAR DETALLE EMPRESA MULTINIVEL: IDMOVIMIENTO, Y COMPROBANT @IDCOMPROBANTE_NEW_GENERICO
		--											update  BDMultinivel.dbo.COMISION_DETALLE_EMPRESA set id_movimiento = @IDMOVIMIENTO_GENERIC, id_comprobante_generico= @IDCOMPROBANTE_NEW_GENERICO, fecha_actualizacion= GETDATE() where id_comision_detalle_empresa= @2_IDCOMISIONEMPRESA_item

		--									     -- SELECT @2_IDCOMISIONEMPRESA_item as 'id detalle detalle', @SIONPAY_IDCOMISIONDETALLE AS 'ID DETALLE CO', @SIONPAY_NROCUENTA AS 'NRO CUENTA', @SALDOACTUAL AS 'MONTO',@SIONPAY_MONTO AS 'MONTO GANADO',  @2DE_CARNET_ITEM AS 'CI', @2_IDEMPRESA_CNX AS 'EMPRESA', @CUENTAID AS 'IDCUENTA'

		--									   END
		--									END																							
										
  --                                END										
		--						-------------------------------------------------------
		--						FETCH NEXT FROM COMISION_2_CURSOR INTO @2_IDCOMISIONEMPRESA_item, @2_MONTONETO_item, @2_IDEMPRESA_item, @2_IDCOMPROBANTEGENERICO_item, @2_IDMOVIMIENTO_item
		--						END

		--						DELETE from @2_COMISIONES_empresas
		--						CLOSE COMISION_2_CURSOR
		--						DEALLOCATE COMISION_2_CURSOR
		--					-----------------------------------------------------------------------------------------------------------
		--			END
		
		--			----------------------- FIN FORCOMISION
		--			FETCH NEXT FROM COMISION_CURSOR_TRES INTO @DDE_IDCOMISION_ITEM, @2DE_IDCOMISIONdETALLE_ITEM, @2DE_IDFICHA_ITEM, @2DE_NOMBRE_ITEM, @2DE_CARNET_ITEM, @2DE_IDTIPOPAGO_ITEM, @2DE_TIPOPAGO_ITEM, @2DE_CIUDAD_ITEM, @2DE_PAIS_ITEM 
		--			END

		--			DELETE from @COMISIONES
		--			CLOSE COMISION_CURSOR_TRES
		--			DEALLOCATE COMISION_CURSOR_TRES
		-----------------------------------------------------------------------------------------------------------


		------------------------------------------------------------------------------------------------------------------------------------------------------
   
	

					COMMIT TRANSACTION;		    
					return 1       
-------------------------------------------------------------------

 ------------------------------------------------------------------
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
