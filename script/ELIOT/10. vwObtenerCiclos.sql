USE BDMultinivel;
GO
CREATE VIEW [dbo].[vwObtenerCiclosRezagados]
AS
	SELECT
		cc.id_comision,
		C.id_ciclo,
		C.nombre,
		C.descripcion,
		E.id_estado_comision,
		E.estado,
		CC.id_tipo_comision
	FROM BDMultinivel.DBO.CICLO C
		INNER JOIN BDMultinivel.DBO.GP_COMISION CC ON C.id_ciclo = CC.id_ciclo
		INNER JOIN BDMultinivel.DBO.GP_TIPO_COMISION T ON CC.id_tipo_comision = T.id_tipo_comision
		INNER JOIN BDMultinivel.DBO.GP_COMISION_ESTADO_COMISION_I CE ON CE.id_comision = CC.id_comision
		INNER JOIN BDMultinivel.DBO.GP_ESTADO_COMISION E ON E.id_estado_comision = CE.id_estado_comision
	WHERE CE.habilitado='true' and t.id_tipo_comision = 2 -- TipoComisionRezagados
GO