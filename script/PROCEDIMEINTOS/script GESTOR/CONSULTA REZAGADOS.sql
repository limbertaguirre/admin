


	 select * from BDMultinivel.dbo.GP_COMISION 

	--LISTAR REZAGADOS------------------------------------------------------------------------------------------------------
	-------------------------------------------------------------------------------------------------------------------------

	DECLARE @COMISIONES as table( id_comision_detalle  int,id_comision int, id_ficha INT, monto_neto DECIMAL(18,2));
				 
	insert @COMISIONES SELECT CODE.id_comision_detalle,CODE.id_comision, CODE.id_ficha, CODE.monto_neto FROM BDMultinivel.dbo.GP_COMISION Co
	inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= Co.id_comision
	inner join BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I CES ON CES.id_comision=Co.id_comision
	where Co.id_ciclo=80 and CES.id_estado_comision=9 and Co.id_tipo_comision = 2
	--ver 
	select * from @COMISIONES

	    DECLARE @DE_IDCOMISIONdETALLE_ITEM INT

		DECLARE COMISION_CURSOR CURSOR FOR 
		Select  id_comision_detalle from @COMISIONES
		OPEN COMISION_CURSOR
		FETCH NEXT FROM COMISION_CURSOR INTO  @DE_IDCOMISIONdETALLE_ITEM 

		WHILE @@FETCH_STATUS = 0  
		BEGIN 
		------------------------INICIO FORCOMISION POR EMPRESA EN CERO	
		
		select * from BDMultinivel.dbo.COMISION_DETALLE_EMPRESA where id_comision_detalle= @DE_IDCOMISIONdETALLE_ITEM

		------------------------------------------
		FETCH NEXT FROM COMISION_CURSOR INTO  @DE_IDCOMISIONdETALLE_ITEM 
		END

		DELETE from @COMISIONES
		CLOSE COMISION_CURSOR
		DEALLOCATE COMISION_CURSOR
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 select * from BDMultinivel.dbo.GP_COMISION 

 ---VERIFICAR LISTA RESAGADO por tipo pago SION PAY
      SELECT  CODE.*,FI.ci
	  FROM BDMultinivel.dbo.GP_COMISION Co
	         inner join  BDMultinivel.dbo.GP_COMISION_ESTADO_COMISION_I CES on CES.id_comision= Co.id_comision
			 inner join BDMultinivel.dbo.GP_COMISION_DETALLE CODE on CODE.id_comision= Co.id_comision
			 inner join BDMultinivel.dbo.FICHA FI on FI.id_ficha = CODE.id_ficha
		    inner join BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIS on LIS.id_comisiones_detalle = CODE.id_comision_detalle
			 where  Co.id_ciclo=81 and Co.id_tipo_comision = 2--RESAGAD
			        and LIS.id_tipo_pago=1

	--verificamos cuenta sion pay
	select  * from BDPuntosCash.dbo.CUENTA where id_usuario in ('5876712', '4455730','3999408', '4283629','4283629', '4495997', '6442648','4503172', '7752568','3353465', '4939230', '4284319','4261411','3124454' )
	
	-- INSERT MANUAL sion pay
	select monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario from BDMultinivel.dbo.listado_formas_pago where id_comisiones_detalle= 13751 

	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('180.78',1,13751,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('1887.4',1,13752,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('516.12',1,13754,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('1696.85',1,13755,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('889.79',1,13756,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('1264.16',1,13757,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('680.44',1,13758,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('301.31',1,13759,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('1778.87',1,13761,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('1384.22',1,13762,1 )
	--insert into BDMultinivel.dbo.listado_formas_pago (monto_neto, id_tipo_pago,id_comisiones_detalle, id_usuario) values('1325.67',1,13763,1 )

	select  *, id_ciclo from BDMultinivel.dbo.GP_COMISION WHERE id_comision=1123 and id_tipo_comision=@PARAM_ID_TIPO_COMISION_PAGO_COMISION

	--comision resagado rolando gonzales

	  SELECT * from BDPuntosCash.DBO.COMISIONES_XLS where lciclo_id=81 order by id_comisiones_xls desc
	  SELECT * FROM BDPuntosCash.DBO.COMISIONES_MAESTRO WHERE lciclo_id=81  order by id_comisiones_maestro desc 
      SELECT * FROM BDPuntosCash.DBO.COMISIONES_DETALLE where lciclo_id=81 order by id_comisiones_detalle  desc 


	
