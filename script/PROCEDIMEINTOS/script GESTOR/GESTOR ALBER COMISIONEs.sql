


SELECT contacto_id,  SUM(total)  FROM OPENQUERY( [10.2.10.222], 'select * from provision where ciclo_id = 85 ') group by contacto_id, empresa_id 


--comisiones totales por grupales,p ersonales 
--
		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from provision where ciclo_id = 85 and contacto_id <> 6474  ')
--detalle
--el bono de comision, bono de servicio(venta grupal + venta residual + bono liderazgo)
-- tipo_id   1 bono comision,  2 bono de servicio
		SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from detalle_provision where ciclo_id = 85 ')

--retencion por empresa
	SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from tbl_retencionempresa where lciclo_id = 85 and lcontacto_id <> 6474  ') 

SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from tbl_retencionempresa ') where lciclo_id = 85 


--- reglas usuario excluidos
--- en el guardian hay una tabla donde esta 
    --tabla donde no se le paga al freelanzers..
    SELECT * FROM OPENQUERY( [10.2.10.222], 'select * from provision_contacto_excepcion')

	se grdsion; /*Aqui unir con la provision y realizar la cabecera del excel(casos de prueba)*/

	select d.* ,co.snombrecompleto,ec.empresa_id,co.lcontacto_id,c.lciclo_id,sum(d.dmonto) total  from administraciondescuentociclodetalle d 
	       inner join administraciondescuentociclo c on c.ldescuentociclo_id = d.ldescuentociclo_id 
		   inner join administracioncontacto co on co.lcontacto_id = c.lcontacto_id 
		   inner join empresa_complejo ec on ec.complejo_id = d.lcomplejo_id 
		   where  c.lciclo_id=85 and co.lcontacto_id =95 
		   Group by co.snombrecompleto,ec.empresa_id,co.lcontacto_id,c.lciclo_id;  
		   
		  



