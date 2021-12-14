SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[fnCiclosParaReporte]()
returns TABLE
as 
return(select DISTINCT CI.id_ciclo, CI.nombre from CICLO CI
INNER JOIN GP_COMISION COM ON COM.id_ciclo = CI.id_ciclo
INNER JOIN GP_COMISION_ESTADO_COMISION_I GCECI ON GCECI.id_comision = COM.id_comision
WHERE GCECI.habilitado = 1 AND GCECI.id_estado_comision IN (9, 13));
GO
