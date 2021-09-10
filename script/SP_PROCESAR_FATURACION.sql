
ALTER PROCEDURE [dbo].[SP_PROCESAR_FACTURAS_PENDIENTES]
     @id_Ciclo     int,
     @habilitado_facturar_guardian bit,
     @usuario VARCHAR(100)
AS

BEGIN TRANSACTION;
BEGIN TRY
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
  DECLARE @TABLE_COMISIONES as table(id_comision_detalle int,factura_habilitado bit, id_estado_comision_detalle int, contacto_codigo_guardian int );
  DECLARE @idCiclo int;
  DECLARE @idComision int;
  DECLARE @IDCOMISIONDETALLEItem int;
  DECLARE @ESTADODETALLEItem int;
  DECLARE @FACTURAHABILITADOItem bit
  DECLARE @IDCONTACTOGUARDIANItem int

  DECLARE @USUARIO_DEFAULT int;
  DECLARE @NOFACTURO int;
  DECLARE @ESTADO_RESAGADO int;
  DECLARE @HABILITADO int;
  DECLARE @DESHABILITADO int;
  DECLARE @NOPRESENTA_FACTURA int;
  DECLARE @ESTADO_NOFACTURA int;

  SET @idCiclo=0;
  SET @idComision=0;
  SET @NOFACTURO= 1;
  SET @ESTADO_RESAGADO=5;
  SET @USUARIO_DEFAULT= 1;
  SET @HABILITADO= 1;
  SET @DESHABILITADO= 0;
  SET @NOPRESENTA_FACTURA=6;
  SET @ESTADO_NOFACTURA=0;

	select  @idCiclo = C.id_ciclo, 
			@idComision = c.id_comision
	from BDMultinivel.dbo.GP_COMISION C where C.id_ciclo=@id_Ciclo -- @id_Ciclo parametro
	IF(@idComision > 0)
	 BEGIN
	       INSERT INTO @TABLE_COMISIONES   select CD.id_comision_detalle,
		               F.factura_habilitado, CDE.id_estado_comision_detalle, F.codigo as 'contacto_codigo_guardian'
		   from BDMultinivel.dbo.GP_COMISION_DETALLE CD
		   inner join BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I CDE on CDE.id_comision_detalle= CD.id_comision_detalle 
		   inner join BDMultinivel.dbo.FICHA F on F.id_ficha= CD.id_ficha where CD.id_comision=@idComision AND CDE.habilitado = 'true' AND (CDE.id_estado_comision_detalle = 1 or CDE.id_estado_comision_detalle =2 )

		   DECLARE COMISIONDETALLE_CURSOR CURSOR FOR 
			Select id_comision_detalle, id_estado_comision_detalle, factura_habilitado, contacto_codigo_guardian from @TABLE_COMISIONES
			OPEN COMISIONDETALLE_CURSOR
			FETCH NEXT FROM COMISIONDETALLE_CURSOR INTO @IDCOMISIONDETALLEItem, @ESTADODETALLEItem, @FACTURAHABILITADOItem, @IDCONTACTOGUARDIANItem
				WHILE @@FETCH_STATUS = 0  
				BEGIN 
			-------------
		
			    if( @FACTURAHABILITADOItem = 'true')
				BEGIN
				      IF(@ESTADODETALLEItem = @NOFACTURO)
						BEGIN
						    	--a resagado
						    	DECLARE @IDDETALLE_ESTADO int;
						    	SET @IDDETALLE_ESTADO=0;
								select top(1) @IDDETALLE_ESTADO=id_comision_detalle_estado_i from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I DEST where DEST.habilitado = 'true' AND  DEST.id_comision_detalle= @IDCOMISIONDETALLEItem
								IF(@IDDETALLE_ESTADO > 0)
								BEGIN																		
									update   BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I  SET habilitado= @DESHABILITADO where  id_comision_detalle_estado_i= @IDDETALLE_ESTADO --se desahabilita el estad y se crea otro
									insert into BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I (id_comision_detalle, id_estado_comision_detalle, habilitado, id_usuario)
											values (
											@IDCOMISIONDETALLEItem,
											@ESTADO_RESAGADO,
											@HABILITADO,
											@USUARIO_DEFAULT
											);
									update BDMultinivel.dbo.COMISION_DETALLE_EMPRESA set si_facturo=@ESTADO_NOFACTURA where id_comision_detalle=@IDCOMISIONDETALLEItem
								END
						END	
						  ELSE
						BEGIN
						  --aqui como presento factura su retencion lo ponemos en cero, 
								  update BDMultinivel.dbo.GP_COMISION_DETALLE  set monto_retencion= 0 where id_comision_detalle= @IDCOMISIONDETALLEItem
								  IF(@habilitado_facturar_guardian = 'true')
								  BEGIN
										BEGIN TRY  
											-- obtener la semana
											 DECLARE @ID_SEMANA INT;
											 SET @ID_SEMANA=0;
											 DECLARE @IDGENERICO INT
											 SET @IDGENERICO=0;

											 SELECT top(1) @ID_SEMANA= lsemana_id FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionsemanaciclo') where lciclo_id= @id_Ciclo
											  IF(@ID_SEMANA > 0)
											  BEGIN
												   SELECT top(1) @IDGENERICO = lciclopresentafactura_id FROM OPENQUERY( [10.2.10.222], 'select * from grdsion.administracionciclopresentafactura order by dtfechaadd desc')  where lciclo_id= @id_Ciclo 
												   IF(@IDGENERICO > 0)
												   BEGIN
															SET  @IDGENERICO = @IDGENERICO + 1
													        --INSERT OPENQUERY ([10.2.10.222], 'select susuarioadd, dtfechaadd, susuariomod,  dtfechamod,lciclopresentafactura_id, lciclo_id, lcontacto_id, lsemana_id  from grdsion.administracionciclopresentafactura')  
															--VALUES (@Usuario, GETDATE(), @Usuario, GETDATE(),@IDGENERICO,@id_Ciclo, @IDCONTACTOGUARDIANItem, @ID_SEMANA); 
												   END
									   
											 END								 							
										END TRY  
										BEGIN CATCH  								       
										     insert into BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL(id_ciclo,id_ficha, codigo_cliente, total_monto_bruto, descripcion )
											 values(@id_Ciclo,0, @IDCONTACTOGUARDIANItem,0,'no se pudo registrar la facturacion en el Guardian ');
										END CATCH  
								  END

						END
				END
				ELSE
				BEGIN
						--no facturo, no presenta Factura
						DECLARE @IDDETALLEESTADO int;
						SET @IDDETALLEESTADO=0;
						select top(1) @IDDETALLEESTADO=id_comision_detalle_estado_i from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I DEST where DEST.habilitado = 'true' AND  DEST.id_comision_detalle= @IDCOMISIONDETALLEItem
						IF(@IDDETALLEESTADO > 0)
						BEGIN												   
						update   BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I  SET habilitado= @DESHABILITADO where  id_comision_detalle_estado_i= @IDDETALLEESTADO --se desahabilita el estado y se crea otro
						insert into BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I (id_comision_detalle, id_estado_comision_detalle, habilitado, id_usuario)
							values (
							@IDCOMISIONDETALLEItem,
							@NOPRESENTA_FACTURA,
							@HABILITADO,
							@USUARIO_DEFAULT
							);
						END
				END
				
			-------------
				FETCH NEXT FROM COMISIONDETALLE_CURSOR INTO @IDCOMISIONDETALLEItem, @ESTADODETALLEItem, @FACTURAHABILITADOItem, @IDCONTACTOGUARDIANItem
				END
			DELETE from @TABLE_COMISIONES
			CLOSE COMISIONDETALLE_CURSOR
			DEALLOCATE COMISIONDETALLE_CURSOR


			COMMIT TRANSACTION;		    
			 return 1

     END
	 ELSE
	 BEGIN
	     ROLLBACK TRANSACTION;         
		 return -2
	 END
           

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
