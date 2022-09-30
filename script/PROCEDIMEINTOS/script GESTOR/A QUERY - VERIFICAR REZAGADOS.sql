

--comisiones 
     select * from BDMultinivel.dbo.ciclo
     select * from BDMultinivel.dbo.GP_COMISION where id_ciclo=82
	 select * from BDMultinivel.dbo.GP_COMISION_DETALLE GCD where id_comision=1066 --primer rezagado
	 select * from BDMultinivel.dbo.GP_COMISION_DETALLE GCD where id_comision=1067 --segundo rezagado

	--verificar comision rezagado por tipo pago

	    DECLARE @IDComision INT SET @IDComision= 1066;
		DECLARE @COMISIONES as table(id_comision int, idComisionDetalle  int, idFicha INT,nombre varchar(100), ci varchar(100), id_tipo_pago int, tipo_pago varchar(100), ciudad varchar(100), pais varchar(100), monto_neto DECIMAL(18,2), id_lista_formas_pago INT);
		declare @IDCOMISION_SELECT int; SET @IDCOMISION_SELECT=0;
		DECLARE @ID_TIPO_PAGO INT ; SET @ID_TIPO_PAGO=2
		select  @IDCOMISION_SELECT=id_comision from BDMultinivel.dbo.GP_COMISION WHERE id_comision=@IDComision and id_tipo_comision=2 --pagocomision
		--LISTAMOS POR COMISION ACTIVO		
		select id_comision, CD.id_comision_detalle,FIC.id_ficha,FIC.nombres +' '+ FIC.apellidos AS 'nombre', FIC.ci,TIPO.id_tipo_pago, TIPO.nombre as 'tipo_pago', CIU.nombre AS 'ciudad', PAI.nombre AS 'pais', CD.monto_neto, LIFO.id_lista_formas_pago, DE.id as 'idPago', DE.id_estado_listado_forma_pago from BDMultinivel.dbo.GP_COMISION_DETALLE CD 
			INNER JOIN BDMultinivel.dbo.LISTADO_FORMAS_PAGO LIFO ON LIFO.id_comisiones_detalle= CD.id_comision_detalle
			INNER JOIN  BDMultinivel.dbo.TIPO_PAGO TIPO ON TIPO.id_tipo_pago= LIFO.id_tipo_pago
			INNER JOIN BDMultinivel.dbo.FICHA FIC ON FIC.id_ficha = CD.id_ficha
			left JOIN BDMultinivel.dbo.CIUDAD CIU ON CIU.id_ciudad = FIC.id_ciudad
			left JOIN BDMultinivel.dbo.PAIS PAI ON PAI.id_pais= CIU.id_pais
			left JOIN BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL DE ON DE.id_lista_formas_pago=LIFO.id_lista_formas_pago
			where CD.id_comision= @IDCOMISION_SELECT AND LIFO.id_tipo_pago=@ID_TIPO_PAGO 


		select * from BDMultinivel.dbo.GP_DETALLE_ESTADO_LISTADO_FORMA_PAGOL


		----------------------------------------------------------------------------
		---asignar para los rezagados
			--INSERT INTO BDMultinivel.dbo.ASIGNACION_EMPRESA_PAGO(id_usuario,id_empresa,id_tipo_pago,descripcion,usuario_id)
			--SELECT 1, id_empresa, 2,'',1
			--FROM BDMultinivel.dbo.vwObtenerEmpresasComisionesDetalleEmpresa
			--WHERE id_ciclo = 82 AND id_estado_comision = 16 AND id_tipo_pago = 2