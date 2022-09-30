
CREATE PROCEDURE [dbo].[SP_8EXEC_CARGAR_FORMA_PAGO_COMISIONES]
  @id_Ciclo     int
AS

BEGIN TRY
   BEGIN TRANSACTION;

   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
 DECLARE @VW_COMISIONES_FORMAPAGOS as table( );


   DECLARE @VW_COMISIONES_FORMAPAGOS as table(  scodigo int,
	   contacto_id int, empresa_origen_id int, ciclo_id int,snombrecompleto varchar(100), snombre  varchar(100), dtfechainicio datetime,dtfechafin datetime,
	   total DECIMAL(18,2),total_13 DECIMAL(18,2),total_87 DECIMAL(18,2),retencion_total DECIMAL(18,2),retencion_iue  DECIMAL(18,2),retencion_it DECIMAL(18,2),total_pagar DECIMAL(18,2)
	   );

   DECLARE @USUARIO_DEFAULT int;
   DECLARE @ESTADO_COMISION_CERRADO_PRORRATEO int;
   DECLARE @IDCICLO_SELECCIONADO int;
   DECLARE @IDCOMISION_SELECCIONADO int;
   DECLARE @ID_COMISION_INCREMENTAL INT;
   DECLARE @ESTADO_DETALLE_COMISION_NO_PRESENTA_FACTURA int;
   DECLARE @ESTADO_DETALLE_COMISION_SI_FACTURO int;
   DECLARE @ESTADO_HABILITADO int;

   SET @USUARIO_DEFAULT=1;
   SET @ESTADO_COMISION_CERRADO_PRORRATEO=8;
   SET @IDCICLO_SELECCIONADO=0;
   sET @IDCOMISION_SELECCIONADO=0;
   SET @ID_COMISION_INCREMENTAL = 0;
   SET @ESTADO_DETALLE_COMISION_NO_PRESENTA_FACTURA=6;
   SET @ESTADO_DETALLE_COMISION_SI_FACTURO= 2;
   SET @ESTADO_HABILITADO = 1;


	select TOP(1) @IDCICLO_SELECCIONADO = id_ciclo from BDMultinivel.dbo.CICLO
	IF @IDCICLO_SELECCIONADO > 0
	BEGIN

	   select TOP(1) @IDCOMISION_SELECCIONADO=id_comision from BDMultinivel.dbo.GP_COMISION
	   IF @IDCOMISION_SELECCIONADO = 0
	   BEGIN
	        ----------------------------------------------------------------------------------------------------------
			SELECT contacto_id , SUM(total) as total, SUM(retencion_total) as retension, SUM(total_pagar) as total_pagar  FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') where ciclo_id=85  group by contacto_id
			-------------------------------------------------

		    INSERT INTO  @VW_COMISIONES_FORMAPAGOS SELECT scodigo, lcontacto_id, empresa_origen_id, ciclo_id, snombrecompleto, snombre, dtfechainicio, dtfechafin, total, total_13, total_87, retencion_total, retencion_iue, retencion_it, total_pagar FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') WHERE ciclo_id=@id_Ciclo order by lcontacto_id asc 

			DECLARE @CODIGOitem int;
			DECLARE @CONTACTO_IDitem int;
			DECLARE @IDEMPRESA_GUARDIANitem int;
			DECLARE @CICLO_IDitem int;
			DECLARE @NOMBRE_NOMPLETOitem varchar(100); 
			DECLARE @NOMBREitem varchar(100);
			DECLARE @FECHA_INICIOitem datetime;
			DECLARE @FECHA_FINitem datetime;
			DECLARE @TOTALitem DECIMAL(18,2);
			DECLARE @TOTAL13item DECIMAL(18,2);
			DECLARE @TOTAL87item DECIMAL(18,2);
			DECLARE @RETENCION_TOTALitem DECIMAL(18,2);
			DECLARE @RETENCION_IUEitem  DECIMAL(18,2);
			DECLARE @RETENCION_ITitem DECIMAL(18,2);
			DECLARE @TOTAL_PAGARitem DECIMAL(18,2);

			---------------------------------
			--SUMAR EL TOTAL APLICACION, TOTAL RETENCION, TOTAL BRUTO PARA AGREGAR EN LA CABECERA DE COMISION

		    insert into BDMultinivel.dbo.GP_COMISION(monto_total_bruto, porcentaje_retencion, monto_total_retencion, monto_total_aplicacion, monto_total_neto, id_ciclo,id_tipo_comision, id_usuario)
			      values(
					0, --monto_total_bruto, 
					0, --porcentaje_retencion, 
					0, --monto_total_retencion, 
					0, --monto_total_aplicacion, 
					0, --monto_total_neto, 
					0, --id_ciclo,id_tipo_comision, 
					@USUARIO_DEFAULT--id_usuario
				  );
			SET @ID_COMISION_INCREMENTAL= SCOPE_IDENTITY();
			--AGREGAR ESTADO
			insert into BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I (habilitado, id_comision, id_estado_comision,id_usuario)
							values (
							 @ESTADO_HABILITADO, -- habilitado, 
							 @ID_COMISION_INCREMENTAL, -- id_comision, 
							 @ESTADO_COMISION_CERRADO_PRORRATEO, -- id_estado_comision,
							 @USUARIO_DEFAULT  -- id_usuario
							);


   			DECLARE CLIENTE_CURSOR CURSOR FOR 
			Select scodigo from @VW_COMISIONES_FORMAPAGOS
			OPEN CLIENTE_CURSOR
			FETCH NEXT FROM CLIENTE_CURSOR INTO @CODIGOitem, @CONTACTO_IDitem, @IDEMPRESA_GUARDIANitem, @CICLO_IDitem, @NOMBRE_NOMPLETOitem, @NOMBREitem, @FECHA_INICIOitem, @FECHA_FINitem, @TOTALitem, @TOTAL13item, @TOTAL87item, @RETENCION_TOTALitem, @RETENCION_IUEitem, @RETENCION_ITitem, @TOTAL_PAGARitem

			WHILE @@FETCH_STATUS = 0  
			BEGIN 
			----------------------------------------------
			DECLARE @IDFICHA_SELECCIONADO INT;
			DECLARE @COMISION_DETALLE_ID_GENERADO INT;
			SET @IDFICHA_SELECCIONADO=0;
			SET @COMISION_DETALLE_ID_GENERADO =0;
			

				 select TOP(1) @IDFICHA_SELECCIONADO = id_ficha from BDMultinivel.dbo.FICHA where codigo= @CONTACTO_IDitem
				 insert into BDMultinivel.dbo.GP_COMISION_DETALLE (monto_bruto, porcentaje_retencion, monto_retencion, monto_aplicacion, monto_neto, id_comision, id_ficha, id_usuario)
						values(
							0, --monto_bruto, 
							0, --porcentaje_retencion, 
							0, --monto_retencion, 
							0, --monto_aplicacion, 
							0, --monto_neto, 
							@ID_COMISION_INCREMENTAL, --id_comision, 
							@IDFICHA_SELECCIONADO, --id_ficha, 
							@USUARIO_DEFAULT --id_usuario
						);
		          SET @COMISION_DETALLE_ID_GENERADO = SCOPE_IDENTITY();
				  insert into  BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I (id_comision_detalle, id_estado_comision_detalle, habilitado, id_usuario)
						values(
							@COMISION_DETALLE_ID_GENERADO ,--id_comision_detalle, 
							@ESTADO_DETALLE_COMISION_NO_PRESENTA_FACTURA ,--id_estado_comision_detalle, 
							@ESTADO_HABILITADO,--habilitado, 
							@USUARIO_DEFAULT --id_usuario
							);


			------------------------------------------------
			FETCH NEXT FROM CLIENTE_CURSOR INTO  @CODIGOitem
			END

			DELETE from @VW_COMISIONES_FORMAPAGOS
			CLOSE CLIENTE_CURSOR
			DEALLOCATE CLIENTE_CURSOR	
    

	      --COMMIT TRANSACTION;
		END
		 ELSE
		BEGIN
		  select -2 as 'la comision ciclo  existe'
		END
	END
	  ELSE
	BEGIN
	 -- NO EXISTE EL CICLO
		SELECT -1 AS 'NO EXISTE EL CICLO'
	END
	-----------------------------------------------	 
	select * from  BDMultinivel.dbo.GP_COMISION
	 SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ') WHERE ciclo_id=@id_Ciclo order by lcontacto_id asc 
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
