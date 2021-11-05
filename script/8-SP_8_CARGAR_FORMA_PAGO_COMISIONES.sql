
ALTER PROCEDURE [dbo].[SP_8EXEC_CARGAR_FORMA_PAGO_COMISIONES]
  @id_Ciclo     int
AS

BEGIN TRY
   BEGIN TRANSACTION;

   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
   DECLARE @VW_COMISIONES_FREELANCERS as table(contacto_id INT, total DECIMAL(18,2), retencion_total DECIMAL(18,2), total_pagar DECIMAL(18,2));


   DECLARE @USUARIO_DEFAULT int;
   DECLARE @ESTADO_COMISION_CERRADO_PRORRATEO int;
   DECLARE @IDCICLO_SELECCIONADO int;
   DECLARE @IDCOMISION_SELECCIONADO int;
   DECLARE @ID_COMISION_INCREMENTAL INT;
   DECLARE @ESTADO_DETALLE_COMISION_NO_PRESENTA_FACTURA int;
   DECLARE @ESTADO_DETALLE_COMISION_SI_FACTURO int;
   DECLARE @ESTADO_HABILITADO int;
   DECLARE @ESTADO_COMISION_DETALLE_PROCESADO int;
   DECLARE @NO_FACTURO INT;
   DECLARE @SI_FACTURO INT;

   SET @USUARIO_DEFAULT=1;
   SET @ESTADO_COMISION_CERRADO_PRORRATEO=8;
   SET @IDCICLO_SELECCIONADO=0;
   sET @IDCOMISION_SELECCIONADO=0;
   SET @ID_COMISION_INCREMENTAL = 0;
   SET @ESTADO_DETALLE_COMISION_NO_PRESENTA_FACTURA=6;
   SET @ESTADO_DETALLE_COMISION_SI_FACTURO= 2;
   SET @ESTADO_HABILITADO = 1;
   SET @ESTADO_COMISION_DETALLE_PROCESADO= 2;
   SET @SI_FACTURO= 1;
   SET @NO_FACTURO= 0;

   DECLARE @CICLO_SELEC int 
   SET @CICLO_SELEC=@id_Ciclo;

	select TOP(1) @IDCICLO_SELECCIONADO = id_ciclo from BDMultinivel.dbo.CICLO
	IF @IDCICLO_SELECCIONADO > 0
	BEGIN

	   select TOP(1) @IDCOMISION_SELECCIONADO=id_comision from BDMultinivel.dbo.GP_COMISION where id_ciclo= @IDCICLO_SELECCIONADO
	   IF @IDCOMISION_SELECCIONADO = 0
	   BEGIN
	       DECLARE @TIPO_PAGO_COMISIONES INT;
	       DECLARE @TOTAL_HEADER DECIMAL(18,2);
		   DECLARE @TOTAL_RETENCION_HEADER DECIMAL(18,2);
		   DECLARE @TOTAL_APLICACION_HEADER DECIMAL(18,2);
		   DECLARE @TOTAL_PAGAR DECIMAL(18,2)
		   DECLARE @HEADER_PORCENTAJE DECIMAL(18,2);
		   SET @TIPO_PAGO_COMISIONES=1;
		   SET @HEADER_PORCENTAJE=0;
		   SET @TOTAL_HEADER =0;set @TOTAL_RETENCION_HEADER = 0;SET @TOTAL_APLICACION_HEADER=0; SET @TOTAL_PAGAR= 0;

	       SELECT @TOTAL_HEADER= SUM(total),@TOTAL_RETENCION_HEADER= SUM(retencion_total) , @TOTAL_APLICACION_HEADER=SUM(total_descuento), @TOTAL_PAGAR = SUM(total_neto)  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=@CICLO_SELEC
		 	SET  @HEADER_PORCENTAJE = @TOTAL_RETENCION_HEADER / @TOTAL_HEADER * 100	
			
		   	insert into BDMultinivel.dbo.GP_COMISION(monto_total_bruto, porcentaje_retencion, monto_total_retencion, monto_total_aplicacion, monto_total_neto, id_ciclo,id_tipo_comision, id_usuario)
			      values(
					@TOTAL_HEADER, --monto_total_bruto, 
					@HEADER_PORCENTAJE, --porcentaje_retencion, 
					@TOTAL_RETENCION_HEADER, --monto_total_retencion, 
					@TOTAL_APLICACION_HEADER, --monto_total_aplicacion, 
					@TOTAL_PAGAR, --monto_total_neto, 
					@CICLO_SELEC, --id_ciclo,
					@TIPO_PAGO_COMISIONES, --id_tipo_comision, 
					@USUARIO_DEFAULT--id_usuario
				  );
			SET @ID_COMISION_INCREMENTAL= SCOPE_IDENTITY();
			----AGREGAR ESTADO
			insert into BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I (habilitado, id_comision, id_estado_comision,id_usuario)
							values (
							 @ESTADO_HABILITADO, -- habilitado, 
							 @ID_COMISION_INCREMENTAL, -- id_comision, 
							 @ESTADO_COMISION_CERRADO_PRORRATEO, -- id_estado_comision,
							 @USUARIO_DEFAULT  -- id_usuario
							);

	        ----------------------------------------------------------------------------------------------------------
		    INSERT INTO @VW_COMISIONES_FREELANCERS	SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar  FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') where ciclo_id=@CICLO_SELEC  group by contacto_id
			
			-------------------------------------------------
			
			DECLARE @CONTACTOITitem INT;
			DECLARE @TOTALitem DECIMAL(18,2);
			DECLARE @RETENCION_TOTALitem DECIMAL(18,2);
			DECLARE @TOTAL_PAGARitem DECIMAL(18,2);	

   			DECLARE CLIENTE_CURSOR CURSOR FOR 
			Select contacto_id ,  total, retencion_total,total_pagar  from @VW_COMISIONES_FREELANCERS
			OPEN CLIENTE_CURSOR
			FETCH NEXT FROM CLIENTE_CURSOR INTO @CONTACTOITitem, @TOTALitem, @RETENCION_TOTALitem, @TOTAL_PAGARitem

			WHILE @@FETCH_STATUS = 0  
			BEGIN 
		    --------------------------------------
			--comision detalle frelancer
			DECLARE @IDFICHA_SELECCIONADO INT;
				SET @IDFICHA_SELECCIONADO=0;
			
			DECLARE @FRELA_TOTAL      DECIMAL(18,2);
			DECLARE @FRELA_RETENCION  DECIMAL(18,2);
			DECLARE @FRELA_TOTALPAGAR DECIMAL(18,2);
			DECLARE @FRELA_PORCENTAJE DECIMAL(18,2);
			 SET @FRELA_TOTAL=0; SET @FRELA_RETENCION=0;SET @FRELA_TOTALPAGAR=0;SET @FRELA_PORCENTAJE= 0;
			

			SELECT top(1)  @FRELA_TOTAL= Sum(total), @FRELA_RETENCION= sum(retencion_total), @FRELA_TOTALPAGAR =sum(total_neto)  FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') where ciclo_id=@CICLO_SELEC  AND contacto_id= @CONTACTOITitem
			SET  @FRELA_PORCENTAJE = @FRELA_RETENCION / @FRELA_TOTAL * 100			   
				  
				 select TOP(1) @IDFICHA_SELECCIONADO = id_ficha from BDMultinivel.dbo.FICHA where codigo= @CONTACTOITitem
				 IF @IDFICHA_SELECCIONADO >0
				 BEGIN
					  --select 'existe el cliente'
					        DECLARE @COMISION_DETALLE_ID_GENERADO INT;
							DECLARE @ESTADO_COMISION_DETALLE_DINAMICO DECIMAL(18,2);
							DECLARE @TOTAL_DESCUENTO_FREELANCER DECIMAL(18,2);
							SET @COMISION_DETALLE_ID_GENERADO =0;
							SET @TOTAL_DESCUENTO_FREELANCER= 0;
							
							--verificamos si facturo
							IF @FRELA_RETENCION > 0
							BEGIN 
							   SET @ESTADO_COMISION_DETALLE_DINAMICO = @ESTADO_DETALLE_COMISION_SI_FACTURO;
							END
						    	ELSE
							BEGIN
							   SET @ESTADO_COMISION_DETALLE_DINAMICO = @ESTADO_DETALLE_COMISION_NO_PRESENTA_FACTURA;
							END 
							SELECT  @TOTAL_DESCUENTO_FREELANCER = total_descuento  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') WHERE ciclo_id = @CICLO_SELEC and contacto_id=@CONTACTOITitem 
							insert into BDMultinivel.dbo.GP_COMISION_DETALLE (monto_bruto, porcentaje_retencion, monto_retencion, monto_aplicacion, monto_neto, id_comision, id_ficha, id_usuario)                 
							values(
								@FRELA_TOTAL, --monto_bruto, 
								@FRELA_PORCENTAJE, --porcentaje_retencion, 
								@FRELA_RETENCION, --monto_retencion, 
								@TOTAL_DESCUENTO_FREELANCER, --monto_aplicacion, 
								@FRELA_TOTALPAGAR, --monto_neto, 
								@ID_COMISION_INCREMENTAL, --id_comision, 
								@IDFICHA_SELECCIONADO, --id_ficha, 
								@USUARIO_DEFAULT --id_usuario
							);
							SET @COMISION_DETALLE_ID_GENERADO = SCOPE_IDENTITY();
							 insert into  BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I (id_comision_detalle, id_estado_comision_detalle, habilitado, id_usuario)
						     values(
								@COMISION_DETALLE_ID_GENERADO ,--id_comision_detalle, 
								@ESTADO_COMISION_DETALLE_DINAMICO,--id_estado_comision_detalle, 
								@ESTADO_HABILITADO,--habilitado, 
								@USUARIO_DEFAULT --id_usuario
								);
							  ----------------------------------------

								--BUSCAR POR CONTACTO
								DECLARE @VW_COMISIONES_EMPRESA as table(contacto_id INT, total DECIMAL(18,2), retencion_total DECIMAL(18,2), total_neto DECIMAL(18,2), empresa_id int, factura_id int   );			
								insert into @VW_COMISIONES_EMPRESA SELECT contacto_id, total, retencion_total, total_neto, empresa_id, factura_id   FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') where ciclo_id=@CICLO_SELEC  AND contacto_id= @CONTACTOITitem
			
								--comisione detalle empresa While
									DECLARE @item_Contacto int;
									DECLARE @item_total DECIMAL(18,2);
									DECLARE @item_retencion DECIMAL(18,2);
									DECLARE @item_total_pagar DECIMAL(18,2);
									DECLARE @item_empresa_guardian int;
									DECLARE @item_facturaId int;
			

									DECLARE COMISION_EMPRESA_CURSOR CURSOR FOR 
									Select  contacto_id, total, retencion_total, total_neto, empresa_id, factura_id  from @VW_COMISIONES_EMPRESA
									OPEN COMISION_EMPRESA_CURSOR
									FETCH NEXT FROM COMISION_EMPRESA_CURSOR INTO @item_Contacto, @item_total, @item_retencion,@item_total_pagar, @item_empresa_guardian, @item_facturaId  

									WHILE @@FETCH_STATUS = 0  
									BEGIN 
									------------------------
				                    --insertar detalle comision empresa
									DECLARE @ID_EMPRESA_GESTOR int
									DECLARE @FACTURA_DINAMICO_DETALLE_STATUS INT;
									SET @ID_EMPRESA_GESTOR =0;

									IF @FRELA_RETENCION > 0
									BEGIN 
										SET @FACTURA_DINAMICO_DETALLE_STATUS = @SI_FACTURO;
									END ELSE BEGIN
										SET @FACTURA_DINAMICO_DETALLE_STATUS = @NO_FACTURO;
									END 

									    select top(1) @ID_EMPRESA_GESTOR=id_empresa from BDMultinivel.dbo.EMPRESA where codigo=@item_empresa_guardian									

									    INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
										values(
											@item_total,--monto
											@ESTADO_COMISION_DETALLE_PROCESADO, --estado pendiente 2
											'', --path-respaldo vacio
											CAST(@item_facturaId as varchar(10)), --nro autirizacion
											@item_total, --montoa facturar
											@item_total, --monto total a facturar
											@COMISION_DETALLE_ID_GENERADO, --idcomisiondetalle
											@ID_EMPRESA_GESTOR, --idempresa 
											@USUARIO_DEFAULT,  --usuarioid
											0, --venta personales
											0, --ventas grupales..
											0, --residual
											@item_retencion, -- retencion
											@item_total_pagar, --monto neto
											@FACTURA_DINAMICO_DETALLE_STATUS --si facturo
										);


									------------------------
									FETCH NEXT FROM COMISION_EMPRESA_CURSOR INTO @item_Contacto, @item_total, @item_retencion,@item_total_pagar, @item_empresa_guardian, @item_facturaId  
									END

									DELETE from @VW_COMISIONES_EMPRESA
									CLOSE COMISION_EMPRESA_CURSOR
									DEALLOCATE COMISION_EMPRESA_CURSOR	
								-------------------------------------
                END
				  ELSE
				BEGIN
							-- select 'no existe el cliente'
							insert into BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL(id_ciclo,id_ficha, codigo_cliente, total_monto_bruto, descripcion )
							values(@CICLO_SELEC,0, @CONTACTOITitem,@TOTAL_PAGARitem,'no se creo la comision del cliente, porque el contacto id no existe en fica');
							
				END
			

				------------------------------------------------
				FETCH NEXT FROM CLIENTE_CURSOR INTO  @CONTACTOITitem, @TOTALitem, @RETENCION_TOTALitem, @TOTAL_PAGARitem
				END

				DELETE from @VW_COMISIONES_FREELANCERS
				CLOSE CLIENTE_CURSOR
				DEALLOCATE CLIENTE_CURSOR	
    
	    --  select 1 as 'exito'
	      COMMIT TRANSACTION;
		END
		 ELSE
		BEGIN
		--  select -2 as 'la comision ciclo  existe'
		  ROLLBACK TRANSACTION;
		END
	END
	  ELSE
	BEGIN
	 -- NO EXISTE EL CICLO
	--	SELECT -1 AS 'NO EXISTE EL CICLO'
		ROLLBACK TRANSACTION;
	END
	-----------------------------------------------	 

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
