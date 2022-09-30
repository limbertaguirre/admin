------------------------------------------------------------
--- grdt
-----ver las comisiones en produccion
      select * from BDQISHUR.dbo.AplicacionesComisionado c where c.Ciclo = 81 and Lcontacto_id=92643
-----------------------------------------------------------------
--REINICIAR LAS TABLAS COMISIONES FORMA DE PAGO
 --   delete BDMultinivel.dbo.GP_COMISION
	--delete BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
	--delete BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL
	--delete BDMultinivel.dbo.GP_COMISION_DETALLE
	--delete  BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
	--delete BDMultinivel.dbo.COMISION_DETALLE_EMPRESA

	--delete BDMultinivel.dbo.INCENTIVO_PAGO_COMISION

	--   delete BDMultinivel.dbo.listado_formas_pago
	-- delete BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL
------------------------------------------------------------------
--VERIFICAMOS CON LAS TABLAS Q INFLUYEN
        select * from BDMultinivel.dbo.GP_COMISION
		select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION
		 
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
		
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_ficha= 64741
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE

		--incentivo detalle
		select * from BDMultinivel.dbo.INCENTIVO_PAGO_COMISION
		select * from BDMultinivel.dbo.TIPO_INCENTIVO_PAGO
		

		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA group by id_comision_detalle
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE

		--VERIFICAMOS SI NO HUBO ALGUN FRELANZER FUERA DE LA COMISION EN EL GESTOR 
		select * from BDMultinivel.dbo.LOG_DETALLE_COMISION_EMPRESA_FAIL where id_ciclo=85

	--foma de pagos seleccionados
		select * from BDMultinivel.dbo.listado_formas_pago
		select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL
----------------------------------------------
--VERIFICAR FORMA PAGO CABECERA POR CICLO
 SELECT SUM(total) AS 'TOTAL', SUM(retencion_total) AS 'RETENCION TOTAL', SUM(total_descuento) AS 'TOTAL DESCUENTO', SUM(total_neto) AS 'TOTAL NETO', (SUM(retencion_total) + SUM(total_descuento) + SUM(total_neto)) as calculado    FROM OPENQUERY( [10.2.10.222], 'select * from comision_forma_pago_view ')
 where ciclo_id=85 
 --
 --VERIFICAR POR DETALLE COMISION GUARDIAN FORMA PAGO
 SELECT  SUM(total) as total,SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar   FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') 
   SELECT  SUM(total) as total,SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar   FROM OPENQUERY( [10.2.10.222], 'select * from grduit.dbo.comision_empresa_forma_pago_view ') 
   where  ciclo_id=85  

   --LISTA DETALLE COMISION CICLO POR FRELANCERS
   		  SELECT contacto_id, SUM(total) as total,SUM(retencion_total) as retencion_total, SUM(total_neto) as total_pagar   FROM OPENQUERY( [10.2.10.222], 'select * from comision_empresa_forma_pago_view ') where  ciclo_id=85  group by contacto_id

 -- 
   select * from BDMultinivel.dbo.ESTADO_AUTORIZACION_COMISION
   select * from BDMultinivel.dbo.TIPO_AUTORIZACION
   select * from BDMultinivel.dbo.AREA
   select * from BDMultinivel.dbo.AUTORIZACIONES_AREA 
   -----------------------------------------------------------------------------------------------------------------------------------------------------------------------
   -----------------------------------------------------------------
   
   ---tablas que se actualizan y crean cuando se cierra un forma de pago
   --1.- cambiar idcomision detalle
   --2.- quitar comision dublicada por resagado
   --3.- quitar autorizacion

   
        select * from BDMultinivel.dbo.ciclo
        select * from BDMultinivel.dbo.GP_COMISION
		select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I 
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE where id_comision= 5

		--update  BDMultinivel.dbo.GP_COMISION_DETALLE 
		--set id_comision=92

		select * from BDMultinivel.dbo.GP_COMISION_DETALLE GCD where id_comision=92
		select * from BDMultinivel.dbo.GP_TIPO_COMISION

		select * from BDMultinivel.dbo.GP_COMISION
		select * from BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I 
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION

		select top(500) * from BDMultinivel.dbo.GP_COMISION_DETALLE ORDER BY id_comision_detalle DESC
		select * from BDMultinivel.dbo.GP_COMISION_DETALLE_ESTADO_I
		select * from BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE

		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA

		--autorizacion comision

        select * from  BDMultinivel.dbo.AUTORIZACION_COMISION
		--foma de pagos seleccionados
		select * from BDMultinivel.dbo.listado_formas_pago
		select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL
		-- delete BDMultinivel.dbo.listado_formas_pago
		-- delete BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL
----------------------------------------------------------------------------------------------------------------------------
--PAGOS MASIVO REINICIAR POR CICLO------------------------------------------------------------------------------------------
        select * from BDPuntosCash.DBO.MOVIMIENTO order by id_movimiento desc
      	--ELIMINAR LA PLANILLA PREVIA
			select * from BDPuntosCash.DBO.COMISIONES_ESTADO_XLS
			select * from BDPuntosCash.DBO.COMISIONES_XLS where lciclo_id=81
			--DELETE BDPuntosCash.DBO.COMISIONES_XLS where lciclo_id=88

			select * from BDMultinivel.dbo.LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL 
			--delete BDMultinivel.dbo.LOG_PAGO_MASIVO_SION_PAY_COMISION_O_EMPRESA_FAIL 
			select * from BDMultinivel.dbo.EMPRESA 
       --ELIMINAR LA PLANTILLA DETALLE CICLO
		    SELECT * FROM BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=81
		   --delete BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=88
		    SELECT * FROM BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=81
		   -- delete BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=88
	 
	    --INICIALIZAR LOS MOVIMIENTO EN CERO POR CICLO DE COMISION DETALLE EMPRESA-------------------------------------------------------------------------
		 --QUITAMOS LOS MOVIMIENTO Y COMPROBANTES EN CERO

		--PARAMETRO= IDCICLO
		
				   --seleccionado idcomision activo
				   DECLARE @IDCICLO INT SET @IDCICLO= 80;
				   DECLARE @COMISIONES as table(id_comision int, idComisionDetalle  int, idFicha INT,nombre varchar(100), ci varchar(100), id_tipo_pago int, tipo_pago varchar(100), ciudad varchar(100), pais varchar(100), monto_neto DECIMAL(18,2), id_lista_formas_pago INT);
				   declare @IDCOMISION_SELECT int; SET @IDCOMISION_SELECT=0;
				   DECLARE @ID_TIPO_SION_PAY INT ; SET @ID_TIPO_SION_PAY=1
				   select  @IDCOMISION_SELECT=id_comision from BDMultinivel.dbo.GP_COMISION WHERE id_ciclo=@IDCICLO and id_tipo_comision=1 --pagocomision
				   --LISTAMOS POR COMISION ACTIVO
				   insert into @COMISIONES  select id_comision, CD.id_comision_detalle,FIC.id_ficha,FIC.nombres +' '+ FIC.apellidos AS 'nombre', FIC.ci,TIPO.id_tipo_pago, TIPO.nombre as 'tipo_pago', CIU.nombre AS 'ciudad', PAI.nombre AS 'pais', CD.monto_neto, LIFO.id_lista_formas_pago from BDMultinivel.dbo.GP_COMISION_DETALLE CD 
						INNER JOIN BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIFO ON LIFO.id_comisiones_detalle= CD.id_comision_detalle
						INNER JOIN  BDMultinivel.dbo.TIPO_PAGO TIPO ON TIPO.id_tipo_pago= LIFO.id_tipo_pago
						INNER JOIN BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha = CD.id_ficha
						left JOIN BDMultinivel.dbo.CIUDAD CIU ON CIU.id_ciudad = FIC.id_ciudad
						left JOIN BDMultinivel.dbo.PAIS PAI ON PAI.id_pais= CIU.id_pais
						where CD.id_comision= @IDCOMISION_SELECT AND LIFO.id_tipo_pago=@ID_TIPO_SION_PAY
                 --SOLO OBTENGO LOS ID DETALLES Y CON FORMA PAGO SION PAY

			      SELECT * FROM @COMISIONES 

				---CURSOR QUE REINICIARA LOS MOVIMIENTOS EN CERO Y
				--AHORA MISMO REINICIAREMOS LOS MOVIMIENTOS EN A TODOS LOS DETALLE
				     DECLARE @DE_IDCOMISIONdETALLE_ITEM INT
					 DECLARE @DE_IDLISTADOFORMAPAGOS_ITEM INT

				    DECLARE COMISION_CURSOR CURSOR FOR 
					Select  idComisionDetalle, id_lista_formas_pago from @COMISIONES
					OPEN COMISION_CURSOR
					FETCH NEXT FROM COMISION_CURSOR INTO  @DE_IDCOMISIONdETALLE_ITEM, @DE_IDLISTADOFORMAPAGOS_ITEM 

					WHILE @@FETCH_STATUS = 0  
					BEGIN 
					------------------------INICIO FORCOMISION POR EMPRESA EN CERO					
					    UPDATE BDMultinivel.dbo.COMISION_DETALLE_EMPRESA SET id_movimiento =0, id_comprobante_generico=0  where id_comision_detalle= @DE_IDCOMISIONdETALLE_ITEM
					----------------------- FIN FORCOMISION
					--eliminar su comprobante de forma de pago
				    	DELETE BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL where id_lista_formas_pago=@DE_IDLISTADOFORMAPAGOS_ITEM
					------------------------------------------
					FETCH NEXT FROM COMISION_CURSOR INTO  @DE_IDCOMISIONdETALLE_ITEM, @DE_IDLISTADOFORMAPAGOS_ITEM 
					END

					DELETE from @COMISIONES
					CLOSE COMISION_CURSOR
					DEALLOCATE COMISION_CURSOR

-------------------------------------------------------------------------
-----------------------------------------------------------------------
SELECT  lcontacto_id,dtfechaadd, stelefonofijo, stelefonomovil,scorreoelectronico,dtfechanacimiento,sdireccion,scedulaidentidad, lpatrocinante_id, lnivel_id, snombrecompleto, scontrasena, Isnull(lcuentabanco,0) as 'lcuentabanco', lcodigobanco, cbaja, dtfechabaja,ltipobaja, smotivobaja, lnit, ciclo_id   
	                          FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select co.lcontacto_id, co.dtfechaadd, co.stelefonofijo, co.stelefonomovil, co.scorreoelectronico, co.dtfechanacimiento, co.sdireccion, co.scedulaidentidad, co.lpatrocinante_id, co.lnivel_id, co.snombrecompleto, co.scontrasena, co.lcuentabanco, co.lcodigobanco, co.cbaja, co.dtfechabaja,co.ltipobaja, co.smotivobaja, co.lnit, comi.ciclo_id from grduit2.administracioncontacto co inner join grduit2.comision_forma_pago_view comi on co.lcontacto_id = comi.contacto_id  ') where ciclo_id= 90 order by lcontacto_id asc 

	
	 select * FROM OPENQUERY( [SRV-GUARDIAN-TEST], 'select * from grduit2.comision_forma_pago_view ') where ciclo_id=90