



select * from BDMultinivel.dbo.PAIS
select * from BDMultinivel.dbo.PAGINA
select * from  BDMultinivel.dbo.GP_TIPO_COMISION
select * from  BDMultinivel.dbo.banco
select * from  BDMultinivel.dbo.nivel
select * from  BDMultinivel.dbo.GP_ESTADO_COMISION_DETALLE

select * from BDComisiones.dbo.PECIUDAD  


 --insert BDMultinivel.dbo.PAIS select pa.IDPAIS, pa.DESCRIPCION, 1, GETDATE(), GETDATE() from BDComisiones.dbo.PEPAIS pa  
 -- insert BDMultinivel.dbo.ciudad select c.IDCIUDAD, c.DESCRIPCION, c.IDPAIS, 1, GETDATE(), GETDATE() from BDComisiones.dbo.PECIUDAD c  
 -- insert BDMultinivel.dbo.banco select B.IDENTIDAD, B.DESCRIPCION, '',0,1,GETDATE(), GETDATE()  from  BDComisiones.dbo.INENTIDAD_FIN B

--delete BDMultinivel.dbo.banco


select * from  BDComisiones.dbo.INENTIDAD_FIN

select * from  BDMultinivel.dbo.nivel
select * FROM OPENQUERY( [10.2.10.222], 'select * from administracionnivel ') 