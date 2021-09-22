create table if not exists empresa_conexion(
	empresa_id int(20) not null,
    nombre_bd varchar(50) not null,
    descripcion varchar(255) not null,
    id_bd_ws bigint(20) not null,
    id_bd bigint(20) not null,
    estado bit not null default 1,
	id_usuario int NOT NULL DEFAULT 0,
	fecha_creacion datetime NULL,
	fecha_actualizacion datetime NULL,
    primary key(empresa_id,id_bd),
    CONSTRAINT `empresa_conexion_ibfk_3` FOREIGN KEY (`empresa_id`) REFERENCES `administracionempresa` (`lempresa_id`)
);


insert into empresa_conexion values(1,'BdConexionCCNORTE', 'SION INTERNACIONAL', 51,1,1,0,now(),now()); 
insert into empresa_conexion values(2,'BdConexionQUINTAS', 'KINTAS SRL', 55,2,1,0,now(),now());
insert into empresa_conexion values(3,'BdConexionZURIEL', 'ZURIEL SRL', 56,3,1,0,now(),now());

#insert into empresa_conexion values(0,'BdConexionFTV', 'CANAL FTV', 0,4,1,0,now(),now());#----000000000000000 
insert into empresa_conexion values(5,'BdConexionASHER', 'ASHER SRL', 58,6,1,0,now(),now());
insert into empresa_conexion values(4,'BdConexionNICA', 'NICAPOLIS SRL  ', 57,7,1,0,now(),now());

insert into empresa_conexion values(21,'BdConexionADVEL', 'AVDEL SRL', 59,8,1,0,now(),now());

#insert into empresa_conexion values(0,'BDConexionMEX', 'ZURIEL S.A. C.V', 62,9,1,0,now(),now());#---no existe guardian
insert into empresa_conexion values(10,'BdConexionROYAL', 'CLUB D. LA PRADERA DEL URUBO', 0,10,1,0,now(),now());
#insert into empresa_conexion values(0,'BdConexionELIAN', 'ELIAN SRL', 52,11,1,0,now(),now());#-----no existe guardian 

insert into empresa_conexion values(14,'BdConexionJAYIL', 'JAYIL SRL', 53,14,1,0,now(),now());

#insert into empresa_conexion values(0,'BdConexionCORAL', 'CORAL', 61,13,1,0,now(),now());#--no existe guardian 
#insert into empresa_conexion values(0,'BdConexionCORAL', 'EFRATA', 0,14,1,0,now(),now());#--no existe guardian 
#insert into empresa_conexion values(0,'BdConexionITAMAR', 'ITAMAR', 0,15,1,0,now(),now());#--no existe guardian 
#insert into empresa_conexion values(0,'BdConexionELDAD', 'ELDAD', 0,16,1,0,now(),now()); #--no existe guardian 
#insert into empresa_conexion values(0,'BdConexionJAIM', 'JAIM SRL.', 63,17,1,0,now(),now());## --no existe guardian 

#insert into empresa_conexion values(0,'BDConexionGS', 'GRUPO SION', 0,19,1,0,now(),now());##--no existe guardian 
insert into empresa_conexion values(13,'BDConexionVA', 'VALLE ANGOSTURA', 0,20,1,0,now(),now());
#insert into empresa_conexion values(20,'rpConexionRPFC', 'ROYAL PARI FC', 0,21,1,0,now(),now());#??? union avdel

#insert into empresa_conexion values(0,'rpConexionGILGAL', 'GILGAL SRL', 0,22,1,0,now(),now());#no existe guardian 
#insert into empresa_conexion values(0,'BDConexionALMAGOR', 'ALMAGOR', 0,23,1,0,now(),now());#no existe guardian 
#insert into empresa_conexion values(0,'BDConexionMFC', 'MFCH', 0,24,1,0,now(),now());#no existe guardian
#insert into empresa_conexion values(0,'BDConexionBETHEL', 'BETHEL S.A', 0,25,1,0,now(),now());#no existe guardian
#insert into empresa_conexion values(0,'BDConexionHAMELEC', 'HAMELEC S.R.L.', 0,26,1,0,now(),now());#no existe guardian

