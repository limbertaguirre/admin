
ALTER PROCEDURE [dbo].[SP_CARGAR_COMISIONES]
   @id_Ciclo     int
AS

BEGIN TRANSACTION;
BEGIN TRY
   DECLARE @IMPBODY   VARCHAR (500);
   DECLARE @IMPSUBJECT   VARCHAR (500);
 ------------------------------------------------------------------ 
    DECLARE @comisionados as table( TOTALBRUTO  numeric(12, 2), 
									CONTACTOID INT);
	DECLARE @totalMontoVentaDirecta DECIMAL(18,2);
	DECLARE @totalMontoVentaGrupo DECIMAL(18,2);
	DECLARE @totalMontoVentaBonoResidual DECIMAL(18,2);
	DECLARE @totalMontoBruto DECIMAL(18,2);
	DECLARE @totalMontoAplicacion DECIMAL(18,2);
	DECLARE @porcentajeRetencion DECIMAL(18,2);
	DECLARE @MontoTotalRetencion DECIMAL(18,2);
	DECLARE @MontoTotalNeto DECIMAL(18,2);

	DECLARE @idCiclo INT;
	DECLARE @TIPO_PAGO_COMISIONES INT;
	DECLARE @USUARIO_DEFAULT INT;
	DECLARE @ID_COMISION_EXISTENTE INT;
	DECLARE @IDCOMISION_SCOPE INT;
	DECLARE @HABILITADO int;
	DECLARE @ID_ESTADO_PENDIENTE_FACTURACION int;
	DECLARE @TOTALBRUTOItem  numeric(12, 2);
	DECLARE @CONTACTOIDItem int;

	SET @idCiclo=0;
	SET @totalMontoVentaDirecta=0;
	SET @totalMontoVentaGrupo=0;
	SET @totalMontoVentaBonoResidual=0;
	SET @totalMontoBruto=0;
	SET @totalMontoAplicacion=0;

	SET @porcentajeRetencion=15.50;
	SET @MontoTotalRetencion=0;
	SET @MontoTotalNeto=0;
	SET @TIPO_PAGO_COMISIONES=1;
	SET @USUARIO_DEFAULT=1;
	SET @ID_COMISION_EXISTENTE=0;
	SET @IDCOMISION_SCOPE= 0;
	SET @HABILITADO=1;
	SET @ID_ESTADO_PENDIENTE_FACTURACION=1;

	select @idCiclo = C.id_ciclo from BDMultinivel.dbo.CICLO C where C.id_ciclo=80 -- @id_Ciclo parametro
	IF(@idCiclo > 0)
	 BEGIN
		SELECT  @totalMontoVentaDirecta = SUM(dcomision)  FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80')   --COMISION DE VENTA DIRECTA/
		SELECT @totalMontoVentaGrupo = SUM(dcomision) FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80')          --COMISION DE VENTA DE GRUPO/
		SELECT @totalMontoVentaBonoResidual= SUM(dtotalbono) FROM OPENQUERY( [10.2.10.222], 'select * from administracionbonoresidual where lciclo_id = 80') --COMISION DE BONO RESIDUAL/
		SELECT @totalMontoAplicacion =SUM(dtotal)  FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo where lciclo_id = 80')

		SET @totalMontoBruto = @totalMontoVentaDirecta + @totalMontoVentaGrupo +@totalMontoVentaBonoResidual
		SET @MontoTotalRetencion= (@totalMontoBruto * @porcentajeRetencion) / 100;
		SET @MontoTotalNeto = (@totalMontoBruto - @MontoTotalRetencion) - @totalMontoAplicacion;

		select top(1) @ID_COMISION_EXISTENTE=GPCO.id_comision from BDMultinivel.dbo.GP_COMISION GPCO where GPCO.id_ciclo=@idCiclo
		IF(@ID_COMISION_EXISTENTE = 0)
		BEGIN
				insert into BDMultinivel.dbo.GP_COMISION (monto_total_bruto, porcentaje_retencion, monto_total_retencion, monto_total_aplicacion,monto_total_neto, id_ciclo, id_tipo_comision, id_usuario)
				values(
				   @totalMontoBruto,
				   @porcentajeRetencion,
				   @MontoTotalRetencion,
				   @totalMontoAplicacion,
				   @MontoTotalNeto,
				   @idCiclo,
				   @TIPO_PAGO_COMISIONES,
				   @USUARIO_DEFAULT
				);
				SET @IDCOMISION_SCOPE = SCOPE_IDENTITY ();
				--crear estado comision
				INSERT INTO BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I (habilitado, id_comision,id_estado_comision,id_usuario)
				VALUES( @HABILITADO, @IDCOMISION_SCOPE, @ID_ESTADO_PENDIENTE_FACTURACION, @USUARIO_DEFAULT);
								
				    INSERT INTO @comisionados  select SUM(dat.dcomision) as 'TOTALBRUTO', dat.lcontacto_id as 'CONTACTOID' from ( 
						SELECT dcomision, lcontacto_id FROM OPENQUERY( [10.2.10.222], 'select * from administracionventapersonal where lciclo_id = 80') 
						union all
						SELECT dcomision, lcontacto_id FROM OPENQUERY( [10.2.10.222], 'select * from administracionventagrupo where lciclo_id = 80')
						union all
						SELECT dtotalbono, lcontacto_id FROM OPENQUERY( [10.2.10.222], 'select * from administracionbonoresidual where lciclo_id = 80')
					) as dat  
					group by dat.lcontacto_id;

				----------------------------------------------------------------------------------------------------------
					DECLARE COMISION_CURSOR CURSOR FOR 
					Select TOTALBRUTO, CONTACTOID from @comisionados
					OPEN COMISION_CURSOR
					FETCH NEXT FROM COMISION_CURSOR INTO @TOTALBRUTOItem, @CONTACTOIDItem

					WHILE @@FETCH_STATUS = 0  
					BEGIN 
					-------------
					    DECLARE @MontoBruto DECIMAL(18,2);
						DECLARE @porcentajeRetencionComisionFrelancer DECIMAL(18,2);
						DECLARE @MontoRetencion DECIMAL(18,2);
						DECLARE @MontoAplicacion DECIMAL(18,2);
						DECLARE @MontoNeto DECIMAL(18,2);
						DECLARE @FICHA INT;
						DECLARE @IDCOMISIONDETALLE_SCOPE INT;
						DECLARE @ID_ESTADO_NO_FACTURADO INT;
						DECLARE @ID_COMISIONEMPRESA_SELECTED INT;
						DECLARE @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE INT;
						DECLARE @NO_FACTURO INT;

						SET @MontoBruto=0;
						SET @porcentajeRetencionComisionFrelancer=15.50;
						SET @MontoRetencion=0;
						SET @MontoAplicacion=0;
						SET @MontoNeto=0;
						SET @FICHA= 0;
						SET @IDCOMISIONDETALLE_SCOPE=0;
						SET @ID_ESTADO_NO_FACTURADO= 1;
						SET @ID_COMISIONEMPRESA_SELECTED=0;
						SET @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE=1;
						SET @NO_FACTURO=1;

						SET @MontoBruto=@TOTALBRUTOItem;
						SET @MontoRetencion= (@TOTALBRUTOItem * @porcentajeRetencionComisionFrelancer ) / 100;
                        SELECT @MontoAplicacion= CASE WHEN SUM(dtotal) IS NULL THEN 0 ELSE SUM(dtotal) END  FROM OPENQUERY( [10.2.10.222], 'select * from administraciondescuentociclo where lciclo_id = 80') where lcontacto_id=@CONTACTOIDItem
						SET @MontoNeto= (@TOTALBRUTOItem - @MontoRetencion) - @MontoAplicacion;
						--verificar y obtener ficha por lcontacto_id
					    	select @FICHA=id_ficha from BDMultinivel.dbo.FICHA f where f.codigo= @CONTACTOIDItem
						    IF(@FICHA > 0)
							BEGIN
							      
								  insert into BDMultinivel.dbo.GP_COMISION_DETALLE (monto_bruto,porcentaje_retencion, monto_retencion, monto_aplicacion, monto_neto, id_comision, id_ficha,id_usuario)
								  values(
									  @MontoBruto,
									  @porcentajeRetencionComisionFrelancer,
									  @MontoRetencion,
									  @MontoAplicacion,
									  @MontoNeto,
									  @IDCOMISION_SCOPE,
									  @FICHA,
									  @USUARIO_DEFAULT
								  );
								  set @IDCOMISIONDETALLE_SCOPE = SCOPE_IDENTITY ();
								  --agregare estado de comision detalle
									  INSERT INTO BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I(id_comision_detalle, id_estado_comision_detalle, habilitado, id_usuario)
									  VALUES (@IDCOMISIONDETALLE_SCOPE,
											  @ID_ESTADO_NO_FACTURADO,
											  @HABILITADO,
											  @USUARIO_DEFAULT);
                                 --agregar la comision por empresa
								 SELECT @ID_COMISIONEMPRESA_SELECTED= id FROM OPENQUERY( [10.2.10.222], 'select * from rpt_neto_facturacion_ciclo where lciclo_id = 80 ')  where  lcontacto_id =  @CONTACTOIDItem
								 IF(@ID_COMISIONEMPRESA_SELECTED > 0)
								 BEGIN
								    --registrar detalle por empresa
										DECLARE @NETO_SiON DECIMAL(18,2), @NETO_KINTAS DECIMAL(18,2), @NETO_ZURIEL DECIMAL(18,2), @NETO_NICAPOLIS DECIMAL(18,2), @NETO_ASHER  DECIMAL(18,2), 
												@NETO_SHOFAR  DECIMAL(18,2), @NETO_MEXICO  DECIMAL(18,2), @NETO_PRADERAS  DECIMAL(18,2), @NETO_KALOMAI  DECIMAL(18,2),@NETO_VALLE_ANGOSTURA  DECIMAL(18,2),
												@NETO_JAYIL  DECIMAL(18,2), @NETO_NEIZAN_JAYIL  DECIMAL(18,2), @NETO_NEIZAN_ASHER  DECIMAL(18,2), @NETO_ROYAL_PARI  DECIMAL(18,2), @NETO_MENORAH  DECIMAL(18,2), @NETO_AVDEL DECIMAL(18,2);									
										SET @NETO_SiON=0;SET @NETO_KINTAS=0; SET @NETO_ZURIEL=0; SET @NETO_NICAPOLIS=0; SET @NETO_ASHER=0;
										SET @NETO_SHOFAR=0; SET @NETO_MEXICO=0; SET @NETO_PRADERAS=0; SET @NETO_KALOMAI=0; SET @NETO_VALLE_ANGOSTURA=0;
										SET @NETO_JAYIL=0; SET @NETO_NEIZAN_JAYIL=0; SET @NETO_NEIZAN_ASHER=0; SET @NETO_ROYAL_PARI=0; SET @NETO_MENORAH=0; SET @NETO_AVDEL=0;
										
										SELECT @NETO_SiON=neto_sion,@NETO_KINTAS= neto_kintas, @NETO_ZURIEL=neto_zuriel, @NETO_NICAPOLIS=neto_nicapolis, @NETO_ASHER=neto_asher,
										       @NETO_SHOFAR=neto_shofar, @NETO_MEXICO=neto_mexico, @NETO_PRADERAS=neto_praderas, @NETO_KALOMAI= neto_kalomai, @NETO_VALLE_ANGOSTURA=neto_valle_angostura,
										       @NETO_JAYIL=neto_jayil, @NETO_NEIZAN_JAYIL=neto_neizan_jayil, @NETO_NEIZAN_ASHER= neto_neizan_jayil,@NETO_ROYAL_PARI= neto_royal_pari,@NETO_MENORAH= neto_menorah, @NETO_AVDEL=neto_avdel
										FROM OPENQUERY( [10.2.10.222], 'select * from rpt_neto_facturacion_ciclo where lciclo_id = 80 ') where lcontacto_id = @CONTACTOIDItem
										  --comision por empresa calculo
													IF(@NETO_SiON >0)
													BEGIN
														  DECLARE @SION_IDDETALLE_SCOPE INT;
														  SET @SION_IDDETALLE_SCOPE=0;
														  DECLARE @SION_TOTALBRUTO DECIMAL(18,2),@SION_MONTONETO DECIMAL(18,2), @SION_MONTORETENCION DECIMAL(18,2), @SION_RESIDUAL DECIMAL(18,2), @SION_VENTA_GRUPAL DECIMAL(18,2), @SION_VENTA_PERSONAL DECIMAL(18,2);
														  DECLARE @SION_IDEMPRESA INT, @SION_CODIGOEMPRESA INT;
														  SET @SION_IDEMPRESA=1 SET @SION_CODIGOEMPRESA=1;
														  SET @SION_TOTALBRUTO=0; SET @SION_MONTONETO=0;SET @SION_MONTORETENCION=0; SET @SION_RESIDUAL=0; SET @SION_VENTA_GRUPAL=0; SET @SION_VENTA_PERSONAL=0;
											 				----venta grupal
																	DECLARE @ventaGrupal as table(empresa varchar(100),lempresa_id INT, lcontacto_id INT, dcomision numeric(12, 2) );
																	 insert into @ventaGrupal  select gru.empresa, gru.lempresa_id, gru.lcontacto_id, gru.dcomision from (SELECT * FROM OPENQUERY ( [10.2.10.222],'select emp.empresa, emp.lempresa_id, vta.lcontacto_id, vta.dcomision  from administracionventagrupo vta
																		inner join administracioncontrato cto on cto.lcontrato_id = vta.lcontrato_id and vta.lciclo_id = 80
																		inner join administracioncomplejo comp on comp.lcomplejo_id = cto.lcomplejo_id
																		INNER JOIN administracionempresa emp on emp.lempresa_id = comp.lempresa_id GROUP BY emp.lEmpresa_id, vta.lcontacto_id
																		')) as gru 
																	 select @SION_VENTA_GRUPAL = SUM(dcomision)  from @ventaGrupal where  lcontacto_id=@CONTACTOIDItem and lempresa_id=@SION_CODIGOEMPRESA group by lempresa_id, lempresa_id
															         IF(@SION_VENTA_GRUPAL < 0)
																	 BEGIN
																	   SET @SION_VENTA_GRUPAL=0;
																	 END
															 --venta personal..
															   		 DECLARE @ventaPersonal as table(empresa varchar(100),lempresa_id INT, lcontacto_id INT, dcomision numeric(12, 2) );
																	  insert into @ventaPersonal SELECT * FROM OPENQUERY ( [10.2.10.222],'select emp.empresa, emp.lempresa_id, vta.lcontacto_id, vta.dcomision  from administracionventapersonal vta
																		inner join administracioncontrato cto on cto.lcontrato_id = vta.lcontrato_id and vta.lciclo_id = 80
																		inner join administracioncomplejo comp on comp.lcomplejo_id = cto.lcomplejo_id
																		INNER JOIN administracionempresa emp on emp.lempresa_id = comp.lempresa_id
																		') as dat 
																	  select @SION_VENTA_PERSONAL= sum(dcomision)  from  @ventaPersonal  where lcontacto_id=@CONTACTOIDItem   and lempresa_id=@SION_CODIGOEMPRESA  group by lempresa_id, lempresa_id
																	  IF(@SION_VENTA_PERSONAL < 0)
																	  BEGIN
																	    SET @SION_VENTA_PERSONAL=0;
																	  END
													          --residual
																   DECLARE @tableResidual as table(empresa varchar(100),lempresa_id INT, lcontacto_id INT, dmonto numeric(12, 2) );
																	insert into @tableResidual   SELECT * FROM OPENQUERY ( [10.2.10.222],'select empre.empresa, empre.lempresa_id, reci.lcontacto_id, reci.dmonto  from administracionredempresacomplejo reci	
																	 inner join administracioncomplejo comp on comp.lcomplejo_id = reci.lcomplejo_id  and reci.lciclo_id = 80
																	 inner join administracionempresa empre on empre.lempresa_id = comp.lempresa_id  
																	')as resid                                                                
																	select @SION_RESIDUAL= SUM(r.dmonto)  from @tableResidual  r where r.lcontacto_id=@CONTACTOIDItem and lEmpresa_id=@SION_CODIGOEMPRESA  GROUP BY r.lEmpresa_id, r.lcontacto_id
																	IF(@SION_RESIDUAL < 0)
																	BEGIN
																	 SET @SION_RESIDUAL= 0
																	END

															---------------------------		
														   SET @SION_MONTONETO = @NETO_SiON;
														    SET @SION_MONTORETENCION = (@SION_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
														   SET @SION_TOTALBRUTO = @SION_MONTONETO + @SION_MONTORETENCION;
														  
														  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
														  values(
															  @SION_TOTALBRUTO,--monto
															  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
															  '', --path-respaldo vacio
															  '', --nro autirizacion
															  @SION_TOTALBRUTO, --montoa facturar
															  @SION_TOTALBRUTO, --monto total a facturar
															  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
															  @SION_IDEMPRESA, --idempresa =1
															  @USUARIO_DEFAULT, 
															  @SION_VENTA_PERSONAL, --venta personales
															  @SION_VENTA_GRUPAL, --ventas grupales..
															  @SION_RESIDUAL, --residual
															  @SION_MONTORETENCION, -- retencion
															  @SION_MONTONETO, --monto neto
															  @NO_FACTURO --si facturo
														  );
														 SET @SION_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
														 select @SION_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													END
													--IF(@NETO_KINTAS >0)
													--BEGIN
													--	  DECLARE @KINTAS_IDDETALLE_SCOPE INT;
													--	  SET @KINTAS_IDDETALLE_SCOPE=0;
													--	  DECLARE @KINTAS_TOTALBRUTO DECIMAL(18,2),@KINTAS_MONTONETO DECIMAL(18,2), @KINTAS_MONTORETENCION DECIMAL(18,2), @KINTAS_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @KINTAS_IDEMPRESA INT, @KINTAS_CODIGOEMPRESA INT;
													--	  SET @KINTAS_IDEMPRESA=2 SET @KINTAS_CODIGOEMPRESA=2;
													--	  SET @KINTAS_TOTALBRUTO=0; SET @KINTAS_MONTONETO=0;SET @KINTAS_MONTORETENCION=0; SET @KINTAS_RESIDUAL=0;
											 											   
													--	   SET @KINTAS_MONTONETO = @NETO_KINTAS;
													--	   SET @KINTAS_RESIDUAL = (@KINTAS_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @KINTAS_TOTALBRUTO = @KINTAS_MONTONETO + @KINTAS_RESIDUAL;
													--	   SET @KINTAS_MONTORETENCION = @KINTAS_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @KINTAS_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @KINTAS_TOTALBRUTO, --montoa facturar
													--		  @KINTAS_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @KINTAS_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @KINTAS_RESIDUAL, --residual
													--		  @KINTAS_MONTORETENCION, -- retencion
													--		  @KINTAS_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @KINTAS_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @KINTAS_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_ZURIEL >0)
													--BEGIN
													--	  DECLARE @ZURIEL_IDDETALLE_SCOPE INT;
													--	  SET @ZURIEL_IDDETALLE_SCOPE=0;
													--	  DECLARE @ZURIEL_TOTALBRUTO DECIMAL(18,2),@ZURIEL_MONTONETO DECIMAL(18,2), @ZURIEL_MONTORETENCION DECIMAL(18,2), @ZURIEL_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @ZURIEL_IDEMPRESA INT, @ZURIEL_CODIGOEMPRESA INT;
													--	  SET @ZURIEL_IDEMPRESA=3 SET @ZURIEL_CODIGOEMPRESA=3;
													--	  SET @ZURIEL_TOTALBRUTO=0; SET @ZURIEL_MONTONETO=0;SET @ZURIEL_MONTORETENCION=0; SET @ZURIEL_RESIDUAL=0;
											 											   
													--	   SET @ZURIEL_MONTONETO = @NETO_ZURIEL;
													--	   SET @ZURIEL_RESIDUAL = (@ZURIEL_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @ZURIEL_TOTALBRUTO = @ZURIEL_MONTONETO + @ZURIEL_RESIDUAL;
													--	   SET @ZURIEL_MONTORETENCION = @ZURIEL_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @ZURIEL_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @ZURIEL_TOTALBRUTO, --montoa facturar
													--		  @ZURIEL_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @ZURIEL_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @ZURIEL_RESIDUAL, --residual
													--		  @ZURIEL_MONTORETENCION, -- retencion
													--		  @ZURIEL_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @ZURIEL_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @ZURIEL_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_NICAPOLIS >0)
													--BEGIN
													--	  DECLARE @NICAPOLIS_IDDETALLE_SCOPE INT;
													--	  SET @NICAPOLIS_IDDETALLE_SCOPE=0;
													--	  DECLARE @NICAPOLIS_TOTALBRUTO DECIMAL(18,2),@NICAPOLIS_MONTONETO DECIMAL(18,2), @NICAPOLIS_MONTORETENCION DECIMAL(18,2), @NICAPOLIS_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @NICAPOLIS_IDEMPRESA INT, @NICAPOLIS_CODIGOEMPRESA INT;
													--	  SET @NICAPOLIS_IDEMPRESA=4 SET @NICAPOLIS_CODIGOEMPRESA=4;
													--	  SET @NICAPOLIS_TOTALBRUTO=0; SET @NICAPOLIS_MONTONETO=0;SET @NICAPOLIS_MONTORETENCION=0; SET @NICAPOLIS_RESIDUAL=0;
											 											   
													--	   SET @NICAPOLIS_MONTONETO = @NETO_NICAPOLIS;
													--	   SET @NICAPOLIS_RESIDUAL = (@NICAPOLIS_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @NICAPOLIS_TOTALBRUTO = @NICAPOLIS_MONTONETO + @NICAPOLIS_RESIDUAL;
													--	   SET @NICAPOLIS_MONTORETENCION = @NICAPOLIS_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @NICAPOLIS_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @NICAPOLIS_TOTALBRUTO, --montoa facturar
													--		  @NICAPOLIS_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @NICAPOLIS_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @NICAPOLIS_RESIDUAL, --residual
													--		  @NICAPOLIS_MONTORETENCION, -- retencion
													--		  @NICAPOLIS_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @NICAPOLIS_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @NICAPOLIS_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_ASHER >0)
													--BEGIN
													--	  DECLARE @ASHER_IDDETALLE_SCOPE INT;
													--	  SET @ASHER_IDDETALLE_SCOPE=0;
													--	  DECLARE @ASHER_TOTALBRUTO DECIMAL(18,2),@ASHER_MONTONETO DECIMAL(18,2), @ASHER_MONTORETENCION DECIMAL(18,2), @ASHER_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @ASHER_IDEMPRESA INT, @ASHER_CODIGOEMPRESA INT;
													--	  SET @ASHER_IDEMPRESA=5 SET @ASHER_CODIGOEMPRESA=5;
													--	  SET @ASHER_TOTALBRUTO=0; SET @ASHER_MONTONETO=0;SET @ASHER_MONTORETENCION=0; SET @ASHER_RESIDUAL=0;
											 											   
													--	   SET @ASHER_MONTONETO = @NETO_ASHER;
													--	   SET @ASHER_RESIDUAL = (@ASHER_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @ASHER_TOTALBRUTO = @ASHER_MONTONETO + @ASHER_RESIDUAL;
													--	   SET @ASHER_MONTORETENCION = @ASHER_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @ASHER_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @ASHER_TOTALBRUTO, --montoa facturar
													--		  @ASHER_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @ASHER_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @ASHER_RESIDUAL, --residual
													--		  @ASHER_MONTORETENCION, -- retencion
													--		  @ASHER_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @ASHER_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @ASHER_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_SHOFAR >0)
													--BEGIN
													--	  DECLARE @SHOFAR_IDDETALLE_SCOPE INT;
													--	  SET @SHOFAR_IDDETALLE_SCOPE=0;
													--	  DECLARE @SHOFAR_TOTALBRUTO DECIMAL(18,2),@SHOFAR_MONTONETO DECIMAL(18,2), @SHOFAR_MONTORETENCION DECIMAL(18,2), @SHOFAR_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @SHOFAR_IDEMPRESA INT, @SHOFAR_CODIGOEMPRESA INT;
													--	  SET @SHOFAR_IDEMPRESA=6 SET @SHOFAR_CODIGOEMPRESA=6;
													--	  SET @SHOFAR_TOTALBRUTO=0; SET @SHOFAR_MONTONETO=0;SET @SHOFAR_MONTORETENCION=0; SET @SHOFAR_RESIDUAL=0;
											 											   
													--	   SET @SHOFAR_MONTONETO = @NETO_SHOFAR;
													--	   SET @SHOFAR_RESIDUAL = (@SHOFAR_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @SHOFAR_TOTALBRUTO = @SHOFAR_MONTONETO + @SHOFAR_RESIDUAL;
													--	   SET @SHOFAR_MONTORETENCION = @SHOFAR_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @SHOFAR_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @SHOFAR_TOTALBRUTO, --montoa facturar
													--		  @SHOFAR_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @SHOFAR_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @SHOFAR_RESIDUAL, --residual
													--		  @SHOFAR_MONTORETENCION, -- retencion
													--		  @SHOFAR_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @SHOFAR_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @SHOFAR_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_MEXICO >0)
													--BEGIN
													--	  DECLARE @MEXICO_IDDETALLE_SCOPE INT;
													--	  SET @MEXICO_IDDETALLE_SCOPE=0;
													--	  DECLARE @MEXICO_TOTALBRUTO DECIMAL(18,2),@MEXICO_MONTONETO DECIMAL(18,2), @MEXICO_MONTORETENCION DECIMAL(18,2), @MEXICO_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @MEXICO_IDEMPRESA INT, @MEXICO_CODIGOEMPRESA INT;
													--	  SET @MEXICO_IDEMPRESA=8 SET @MEXICO_CODIGOEMPRESA=8;
													--	  SET @MEXICO_TOTALBRUTO=0; SET @MEXICO_MONTONETO=0;SET @MEXICO_MONTORETENCION=0; SET @MEXICO_RESIDUAL=0;
											 											   
													--	   SET @MEXICO_MONTONETO = @NETO_MEXICO;
													--	   SET @MEXICO_RESIDUAL = (@MEXICO_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @MEXICO_TOTALBRUTO = @MEXICO_MONTONETO + @MEXICO_RESIDUAL;
													--	   SET @MEXICO_MONTORETENCION = @MEXICO_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @MEXICO_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @MEXICO_TOTALBRUTO, --montoa facturar
													--		  @MEXICO_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @MEXICO_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @MEXICO_RESIDUAL, --residual
													--		  @MEXICO_MONTORETENCION, -- retencion
													--		  @MEXICO_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @MEXICO_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @MEXICO_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_PRADERAS >0)
													--BEGIN
													--	  DECLARE @PRADERAS_IDDETALLE_SCOPE INT;
													--	  SET @PRADERAS_IDDETALLE_SCOPE=0;
													--	  DECLARE @PRADERAS_TOTALBRUTO DECIMAL(18,2),@PRADERAS_MONTONETO DECIMAL(18,2), @PRADERAS_MONTORETENCION DECIMAL(18,2), @PRADERAS_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @PRADERAS_IDEMPRESA INT, @PRADERAS_CODIGOEMPRESA INT;
													--	  SET @PRADERAS_IDEMPRESA=10 SET @PRADERAS_CODIGOEMPRESA=10;
													--	  SET @PRADERAS_TOTALBRUTO=0; SET @PRADERAS_MONTONETO=0;SET @PRADERAS_MONTORETENCION=0; SET @PRADERAS_RESIDUAL=0;
											 											   
													--	   SET @PRADERAS_MONTONETO = @NETO_PRADERAS;
													--	   SET @PRADERAS_RESIDUAL = (@PRADERAS_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @PRADERAS_TOTALBRUTO = @PRADERAS_MONTONETO + @PRADERAS_RESIDUAL;
													--	   SET @PRADERAS_MONTORETENCION = @PRADERAS_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @PRADERAS_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @PRADERAS_TOTALBRUTO, --montoa facturar
													--		  @PRADERAS_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @PRADERAS_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @PRADERAS_RESIDUAL, --residual
													--		  @PRADERAS_MONTORETENCION, -- retencion
													--		  @PRADERAS_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @PRADERAS_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @PRADERAS_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_KALOMAI >0)
													--BEGIN
													--	  DECLARE @KALOMAI_IDDETALLE_SCOPE INT;
													--	  SET @KALOMAI_IDDETALLE_SCOPE=0;
													--	  DECLARE @KALOMAI_TOTALBRUTO DECIMAL(18,2),@KALOMAI_MONTONETO DECIMAL(18,2), @KALOMAI_MONTORETENCION DECIMAL(18,2), @KALOMAI_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @KALOMAI_IDEMPRESA INT, @KALOMAI_CODIGOEMPRESA INT;
													--	  SET @KALOMAI_IDEMPRESA=12 SET @KALOMAI_CODIGOEMPRESA=12;
													--	  SET @KALOMAI_TOTALBRUTO=0; SET @KALOMAI_MONTONETO=0;SET @KALOMAI_MONTORETENCION=0; SET @KALOMAI_RESIDUAL=0;
											 											   
													--	   SET @KALOMAI_MONTONETO = @NETO_KALOMAI;
													--	   SET @KALOMAI_RESIDUAL = (@KALOMAI_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @KALOMAI_TOTALBRUTO = @KALOMAI_MONTONETO + @KALOMAI_RESIDUAL;
													--	   SET @KALOMAI_MONTORETENCION = @KALOMAI_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @KALOMAI_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @KALOMAI_TOTALBRUTO, --montoa facturar
													--		  @KALOMAI_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @KALOMAI_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @KALOMAI_RESIDUAL, --residual
													--		  @KALOMAI_MONTORETENCION, -- retencion
													--		  @KALOMAI_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @KALOMAI_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @KALOMAI_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_VALLE_ANGOSTURA >0)
													--BEGIN
													--	  DECLARE @ANGOSTURA_IDDETALLE_SCOPE INT;
													--	  SET @ANGOSTURA_IDDETALLE_SCOPE=0;
													--	  DECLARE @ANGOSTURA_TOTALBRUTO DECIMAL(18,2),@ANGOSTURA_MONTONETO DECIMAL(18,2), @ANGOSTURA_MONTORETENCION DECIMAL(18,2), @ANGOSTURA_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @ANGOSTURA_IDEMPRESA INT, @ANGOSTURA_CODIGOEMPRESA INT;
													--	  SET @ANGOSTURA_IDEMPRESA=13 SET @ANGOSTURA_CODIGOEMPRESA=13;
													--	  SET @ANGOSTURA_TOTALBRUTO=0; SET @ANGOSTURA_MONTONETO=0;SET @ANGOSTURA_MONTORETENCION=0; SET @ANGOSTURA_RESIDUAL=0;
											 											   
													--	   SET @ANGOSTURA_MONTONETO = @NETO_VALLE_ANGOSTURA;
													--	   SET @ANGOSTURA_RESIDUAL = (@ANGOSTURA_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @ANGOSTURA_TOTALBRUTO = @ANGOSTURA_MONTONETO + @ANGOSTURA_RESIDUAL;
													--	   SET @ANGOSTURA_MONTORETENCION = @ANGOSTURA_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @ANGOSTURA_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @ANGOSTURA_TOTALBRUTO, --montoa facturar
													--		  @ANGOSTURA_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @ANGOSTURA_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @ANGOSTURA_RESIDUAL, --residual
													--		  @ANGOSTURA_MONTORETENCION, -- retencion
													--		  @ANGOSTURA_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @ANGOSTURA_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @ANGOSTURA_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_JAYIL >0)
													--BEGIN
													--	  DECLARE @JAYIL_IDDETALLE_SCOPE INT;
													--	  SET @JAYIL_IDDETALLE_SCOPE=0;
													--	  DECLARE @JAYIL_TOTALBRUTO DECIMAL(18,2),@JAYIL_MONTONETO DECIMAL(18,2), @JAYIL_MONTORETENCION DECIMAL(18,2), @JAYIL_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @JAYIL_IDEMPRESA INT, @JAYIL_CODIGOEMPRESA INT;
													--	  SET @JAYIL_IDEMPRESA=14 SET @JAYIL_CODIGOEMPRESA=14;
													--	  SET @JAYIL_TOTALBRUTO=0; SET @JAYIL_MONTONETO=0;SET @JAYIL_MONTORETENCION=0; SET @JAYIL_RESIDUAL=0;
											 											   
													--	   SET @JAYIL_MONTONETO = @NETO_JAYIL;
													--	   SET @JAYIL_RESIDUAL = (@JAYIL_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @JAYIL_TOTALBRUTO = @JAYIL_MONTONETO + @JAYIL_RESIDUAL;
													--	   SET @JAYIL_MONTORETENCION = @JAYIL_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @JAYIL_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @JAYIL_TOTALBRUTO, --montoa facturar
													--		  @JAYIL_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @JAYIL_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @JAYIL_RESIDUAL, --residual
													--		  @JAYIL_MONTORETENCION, -- retencion
													--		  @JAYIL_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @JAYIL_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @JAYIL_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_NEIZAN_JAYIL >0)
													--BEGIN
													--	  DECLARE @NEIZANJAYIL_IDDETALLE_SCOPE INT;
													--	  SET @NEIZANJAYIL_IDDETALLE_SCOPE=0;
													--	  DECLARE @NEIZANJAYIL_TOTALBRUTO DECIMAL(18,2),@NEIZANJAYIL_MONTONETO DECIMAL(18,2), @NEIZANJAYIL_MONTORETENCION DECIMAL(18,2), @NEIZANJAYIL_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @NEIZANJAYIL_IDEMPRESA INT, @NEIZANJAYIL_CODIGOEMPRESA INT;
													--	  SET @NEIZANJAYIL_IDEMPRESA=15 SET @NEIZANJAYIL_CODIGOEMPRESA=15;
													--	  SET @NEIZANJAYIL_TOTALBRUTO=0; SET @NEIZANJAYIL_MONTONETO=0;SET @NEIZANJAYIL_MONTORETENCION=0; SET @NEIZANJAYIL_RESIDUAL=0;
											 											   
													--	   SET @NEIZANJAYIL_MONTONETO = @NETO_NEIZAN_JAYIL;
													--	   SET @NEIZANJAYIL_RESIDUAL = (@NEIZANJAYIL_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @NEIZANJAYIL_TOTALBRUTO = @NEIZANJAYIL_MONTONETO + @NEIZANJAYIL_RESIDUAL;
													--	   SET @NEIZANJAYIL_MONTORETENCION = @NEIZANJAYIL_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @NEIZANJAYIL_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @NEIZANJAYIL_TOTALBRUTO, --montoa facturar
													--		  @NEIZANJAYIL_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @NEIZANJAYIL_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @NEIZANJAYIL_RESIDUAL, --residual
													--		  @NEIZANJAYIL_MONTORETENCION, -- retencion
													--		  @NEIZANJAYIL_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @NEIZANJAYIL_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @NEIZANJAYIL_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_NEIZAN_ASHER >0)
													--BEGIN
													--	  DECLARE @NEIZANASHER_IDDETALLE_SCOPE INT;
													--	  SET @NEIZANASHER_IDDETALLE_SCOPE=0;
													--	  DECLARE @NEIZANASHER_TOTALBRUTO DECIMAL(18,2),@NEIZANASHER_MONTONETO DECIMAL(18,2), @NEIZANASHER_MONTORETENCION DECIMAL(18,2), @NEIZANASHER_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @NEIZANASHER_IDEMPRESA INT, @NEIZANASHER_CODIGOEMPRESA INT;
													--	  SET @NEIZANASHER_IDEMPRESA=16 SET @NEIZANASHER_CODIGOEMPRESA=16;
													--	  SET @NEIZANASHER_TOTALBRUTO=0; SET @NEIZANASHER_MONTONETO=0;SET @NEIZANASHER_MONTORETENCION=0; SET @NEIZANASHER_RESIDUAL=0;
											 											   
													--	   SET @NEIZANASHER_MONTONETO = @NETO_NEIZAN_ASHER;
													--	   SET @NEIZANASHER_RESIDUAL = (@NEIZANASHER_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @NEIZANASHER_TOTALBRUTO = @NEIZANASHER_MONTONETO + @NEIZANASHER_RESIDUAL;
													--	   SET @NEIZANASHER_MONTORETENCION = @NEIZANASHER_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @NEIZANASHER_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @NEIZANASHER_TOTALBRUTO, --montoa facturar
													--		  @NEIZANASHER_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @NEIZANASHER_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @NEIZANASHER_RESIDUAL, --residual
													--		  @NEIZANASHER_MONTORETENCION, -- retencion
													--		  @NEIZANASHER_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @NEIZANASHER_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @NEIZANASHER_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_ROYAL_PARI >0)
													--BEGIN
													--	  DECLARE @ROYALPARI_IDDETALLE_SCOPE INT;
													--	  SET @ROYALPARI_IDDETALLE_SCOPE=0;
													--	  DECLARE @ROYALPARI_TOTALBRUTO DECIMAL(18,2),@ROYALPARI_MONTONETO DECIMAL(18,2), @ROYALPARI_MONTORETENCION DECIMAL(18,2), @ROYALPARI_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @ROYALPARI_IDEMPRESA INT, @ROYALPARI_CODIGOEMPRESA INT;
													--	  SET @ROYALPARI_IDEMPRESA=20 SET @ROYALPARI_CODIGOEMPRESA=20;
													--	  SET @ROYALPARI_TOTALBRUTO=0; SET @ROYALPARI_MONTONETO=0;SET @ROYALPARI_MONTORETENCION=0; SET @ROYALPARI_RESIDUAL=0;
											 											   
													--	   SET @ROYALPARI_MONTONETO = @NETO_NEIZAN_ASHER;
													--	   SET @ROYALPARI_RESIDUAL = (@ROYALPARI_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @ROYALPARI_TOTALBRUTO = @ROYALPARI_MONTONETO + @ROYALPARI_RESIDUAL;
													--	   SET @ROYALPARI_MONTORETENCION = @ROYALPARI_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @ROYALPARI_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @ROYALPARI_TOTALBRUTO, --montoa facturar
													--		  @ROYALPARI_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @ROYALPARI_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @ROYALPARI_RESIDUAL, --residual
													--		  @ROYALPARI_MONTORETENCION, -- retencion
													--		  @ROYALPARI_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @ROYALPARI_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @ROYALPARI_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_MENORAH >0)
													--BEGIN
													--	  DECLARE @MENORAH_IDDETALLE_SCOPE INT;
													--	  SET @MENORAH_IDDETALLE_SCOPE=0;
													--	  DECLARE @MENORAH_TOTALBRUTO DECIMAL(18,2),@MENORAH_MONTONETO DECIMAL(18,2), @MENORAH_MONTORETENCION DECIMAL(18,2), @MENORAH_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @MENORAH_IDEMPRESA INT, @MENORAH_CODIGOEMPRESA INT;
													--	  SET @MENORAH_IDEMPRESA=18 SET @MENORAH_CODIGOEMPRESA=18;
													--	  SET @MENORAH_TOTALBRUTO=0; SET @MENORAH_MONTONETO=0;SET @MENORAH_MONTORETENCION=0; SET @MENORAH_RESIDUAL=0;
											 											   
													--	   SET @MENORAH_MONTONETO = @NETO_MENORAH;
													--	   SET @MENORAH_RESIDUAL = (@MENORAH_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @MENORAH_TOTALBRUTO = @MENORAH_MONTONETO + @MENORAH_RESIDUAL;
													--	   SET @MENORAH_MONTORETENCION = @MENORAH_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @MENORAH_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @MENORAH_TOTALBRUTO, --montoa facturar
													--		  @MENORAH_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @MENORAH_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @MENORAH_RESIDUAL, --residual
													--		  @MENORAH_MONTORETENCION, -- retencion
													--		  @MENORAH_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @MENORAH_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @MENORAH_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END
													--IF(@NETO_AVDEL >0)
													--BEGIN
													--	  DECLARE @AVDEL_IDDETALLE_SCOPE INT;
													--	  SET @AVDEL_IDDETALLE_SCOPE=0;
													--	  DECLARE @AVDEL_TOTALBRUTO DECIMAL(18,2),@AVDEL_MONTONETO DECIMAL(18,2), @AVDEL_MONTORETENCION DECIMAL(18,2), @AVDEL_RESIDUAL DECIMAL(18,2);
													--	  DECLARE @AVDEL_IDEMPRESA INT, @AVDEL_CODIGOEMPRESA INT;
													--	  SET @AVDEL_IDEMPRESA=11 SET @AVDEL_CODIGOEMPRESA=12;
													--	  SET @AVDEL_TOTALBRUTO=0; SET @AVDEL_MONTONETO=0;SET @AVDEL_MONTORETENCION=0; SET @AVDEL_RESIDUAL=0;
											 											   
													--	   SET @AVDEL_MONTONETO = @NETO_AVDEL;
													--	   SET @AVDEL_RESIDUAL = (@AVDEL_MONTONETO * @porcentajeRetencionComisionFrelancer) / (100 - @porcentajeRetencionComisionFrelancer);
													--	   SET @AVDEL_TOTALBRUTO = @AVDEL_MONTONETO + @AVDEL_RESIDUAL;
													--	   SET @AVDEL_MONTORETENCION = @AVDEL_RESIDUAL;
													--	  INSERT INTO BDMultinivel.dbo.COMISION_DETALLE_EMPRESA(monto, estado,respaldo_path, nro_autorizacion, monto_a_facturar, monto_total_facturar, id_comision_detalle,id_empresa,id_usuario,ventas_personales,ventas_grupales,residual,retencion,monto_neto,si_facturo)
													--	  values(
													--		  @AVDEL_TOTALBRUTO,--monto
													--		  @ESTADO_COMISION_DETALLE_EMPRESA_PENDIENTE, --estado pendiente 1
													--		  '', --path-respaldo vacio
													--		  '', --nro autirizacion
													--		  @AVDEL_TOTALBRUTO, --montoa facturar
													--		  @AVDEL_TOTALBRUTO, --monto total a facturar
													--		  @IDCOMISIONDETALLE_SCOPE, --idcomisiondetalle
													--		  @AVDEL_IDEMPRESA, --idempresa =1
													--		  @USUARIO_DEFAULT, 
													--		  0, --venta personales
													--		  0, --ventas grupales..
													--		  @AVDEL_RESIDUAL, --residual
													--		  @AVDEL_MONTORETENCION, -- retencion
													--		  @AVDEL_MONTONETO, --monto neto
													--		  @NO_FACTURO --si facturo
													--	  );
													--	 SET @AVDEL_IDDETALLE_SCOPE = SCOPE_IDENTITY ();
													--	 select @AVDEL_IDDETALLE_SCOPE as 'id total solo de empresa sion'
													--END

											--select @ID_COMISIONEMPRESA_SELECTED as 'tiene empresa comisionda por contacto'
								 END
								 ELSE
								 BEGIN
								     
									 insert into BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL(id_ciclo,id_ficha, codigo_cliente, total_monto_bruto, descripcion )
									 values(@idCiclo,@FICHA, @CONTACTOIDItem,@MontoBruto,'no se creo la comision empresa detallada, porque el cliente no existe en rpt_neto_facturacion_ciclo');
									 select @CONTACTOIDItem as 'no tiene x contacto'
								 END

							END
							ELSE
							BEGIN
							      --bitacora registrar la comisiones de frelancer que no se tenga con ese codigo en la base de datos
							      select @FICHA  as 'no exite ficha';
							END

					-------------
					FETCH NEXT FROM COMISION_CURSOR INTO @TOTALBRUTOItem, @CONTACTOIDItem
					END

					DELETE from @comisionados
					CLOSE COMISION_CURSOR
					DEALLOCATE COMISION_CURSOR
				-----------------------------------------------------------------------------------------------------------
				--COMMIT TRANSACTION;
				select 1  as 'se registro correctamente las comisiones';

		END
		ELSE
		BEGIN
		 -- ROLLBACK TRANSACTION;
          SELECT -2 AS 'Existe una Comision con el mismo ciclo Introducido';
		END

     END
	 ELSE
	 BEGIN
	    -- ROLLBACK TRANSACTION;
         SELECT -1 AS 'No exite el ciclo en la tabla';
	 END
  
  --------------------------------------------------------------
    select * from BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL
	SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from rpt_neto_facturacion_ciclo where lciclo_id = 80')
	select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA
 ----------------------------------------------------------------------------------------------------
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
