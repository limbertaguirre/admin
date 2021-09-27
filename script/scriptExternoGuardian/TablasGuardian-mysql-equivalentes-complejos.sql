
# tablas que se ejecutaran en la base de datos grdsion - en mysql

drop table if not exists empresa_complejo;
CREATE TABLE IF NOT EXISTS empresa_complejo(
    id_empresa_complejo int not null AUTO_INCREMENT,
    empresa_id int(11) not null,
    complejo_id bigint(20) not null,
    estado bit not null,
     id_usuario int NOT NULL DEFAULT 0,
	  fecha_creacion datetime NULL,
	  fecha_actualizacion datetime NULL,
    primary key(id_empresa_complejo,empresa_id,complejo_id) USING BTREE,
	KEY `fk_provision_empresa_id` (`empresa_id`) USING BTREE,
	KEY `fk_provision_complejo_id` (`complejo_id`) USING BTREE,
    CONSTRAINT `empresa_complejo_ibfk_1` FOREIGN KEY (`empresa_id`) REFERENCES `administracionempresa` (`lempresa_id`),
	CONSTRAINT `empresa_complejo_ibfk_2` FOREIGN KEY (`complejo_id`) REFERENCES `administracioncomplejo` (`lcomplejo_id`)
);

CREATE TABLE IF NOT EXISTS proyecto_conexion_sufijo(
    id_proyecto_conexion_sufijo int not null AUTO_INCREMENT,
    id_empresa_complejo int not null,
    id_proyecto_cnx bigint(20) not null,
    sufijo varchar(100) not null,
    estado bit not null DEFAULT 1 ,
    id_usuario int NOT NULL DEFAULT 0,
	fecha_creacion datetime NULL,
	fecha_actualizacion datetime NULL,
   CONSTRAINT proyecto_conexion_sufijo_pk PRIMARY KEY (id_proyecto_conexion_sufijo)
);
ALTER TABLE proyecto_conexion_sufijo AUTO_INCREMENT = 1;

/*SION*/
#(1, 2, 5) then 1
INSERT INTO empresa_complejo VALUES(1,1,1,1,0,now(),now());#--5
         INSERT INTO proyecto_conexion_sufijo VALUES(0,1,3,'',1,0,now(),now());#--5     
         
INSERT INTO empresa_complejo VALUES(2,1,2,1,0,now(),now());#--4
         INSERT INTO proyecto_conexion_sufijo VALUES(0,2,1,'',1,0,now(),now());#--4         
INSERT INTO empresa_complejo VALUES(3,1,5,1,0,now(),now()); #--7
         INSERT INTO proyecto_conexion_sufijo VALUES(0,3,6,'TS-UV',1,0,now(),now()); #--7
/*KINTAS*/
#(3, 4, 6, 11,66,67,68,69,70,71,72,73,74,75) then 2
INSERT INTO empresa_complejo VALUES(4,2,4,1,0,now(),now()); #--9
          INSERT INTO proyecto_conexion_sufijo VALUES(0,4,7,'',1,0,now(),now()); #--9
          
INSERT INTO empresa_complejo VALUES(5,2,3,1,0,now(),now()); #--6
          INSERT INTO proyecto_conexion_sufijo VALUES(0,5,4,'',1,0,now(),now()); #--6
          
INSERT INTO empresa_complejo VALUES(6,2,6,1,0,now(),now());#--10
          INSERT INTO proyecto_conexion_sufijo VALUES(0,6,9,'',1,0,now(),now());#--10
INSERT INTO empresa_complejo VALUES(7,2,11,1,0,now(),now());# --15
          INSERT INTO proyecto_conexion_sufijo VALUES(0,7,14,'',1,0,now(),now());# --15
          
INSERT INTO empresa_complejo VALUES(8,2,66,1,0,now(),now());#--18
          INSERT INTO proyecto_conexion_sufijo VALUES(0,8,16,'',1,0,now(),now());#--18
          
INSERT INTO empresa_complejo VALUES(9,2,67,1,0,now(),now());#--41
          INSERT INTO proyecto_conexion_sufijo VALUES(0,9,35,'',1,0,now(),now());#--41
          
INSERT INTO empresa_complejo VALUES(10,2,68,1,0,now(),now());#--44
          INSERT INTO proyecto_conexion_sufijo VALUES(0,10,38,'',1,0,now(),now());#--44
          
INSERT INTO empresa_complejo VALUES(11,2,69,1,0,now(),now());#--45
          INSERT INTO proyecto_conexion_sufijo VALUES(0,11,39,'',1,0,now(),now());#--45
          
INSERT INTO empresa_complejo VALUES(12,2,70,1,0,now(),now());#--46
           INSERT INTO proyecto_conexion_sufijo VALUES(0,12,40,'',1,0,now(),now());#--46
           
INSERT INTO empresa_complejo VALUES(13,2,71,1,0,now(),now());#--47
           INSERT INTO proyecto_conexion_sufijo VALUES(0,13,41,'',1,0,now(),now());#--47
           
INSERT INTO empresa_complejo VALUES(14,2,72,1,0,now(),now());#--51
           INSERT INTO proyecto_conexion_sufijo VALUES(0,14,44,'',1,0,now(),now());#--51
           
INSERT INTO empresa_complejo VALUES(15,2,73,1,0,now(),now());#--54
           INSERT INTO proyecto_conexion_sufijo VALUES(0,15,47,'',1,0,now(),now());#--54
           
INSERT INTO empresa_complejo VALUES(16,2,74,1,0,now(),now());#--64
           INSERT INTO proyecto_conexion_sufijo VALUES(0,16,61,'',1,0,now(),now());#--64
           
INSERT INTO empresa_complejo VALUES(17,2,75,1,0,now(),now());#--65
           INSERT INTO proyecto_conexion_sufijo VALUES(0,17,62,'',1,0,now(),now());#--65
           
/*zuriel*/
#(7, 8, 9, 10, 52,53, 54, 57,60,86) then 3
INSERT INTO empresa_complejo VALUES(18,3,7,1,0,now(),now()); #--11
          INSERT INTO proyecto_conexion_sufijo VALUES(0,18,10,'',1,0,now(),now()); #--11
          
INSERT INTO empresa_complejo VALUES(19,3,8,1,0,now(),now()); #--12
          INSERT INTO proyecto_conexion_sufijo VALUES(0,19,11,'',1,0,now(),now()); #--12
          
INSERT INTO empresa_complejo VALUES(20,3,9,1,0,now(),now());#--13
          INSERT INTO proyecto_conexion_sufijo VALUES(0,20,12,'',1,0,now(),now());#--13
          
INSERT INTO empresa_complejo VALUES(21,3,10,1,0,now(),now());#--14
          INSERT INTO proyecto_conexion_sufijo VALUES(0,21,13,'',1,0,now(),now());#--14
          
INSERT INTO empresa_complejo VALUES(22,3,52,1,0,now(),now());#--55
          INSERT INTO proyecto_conexion_sufijo VALUES(0,22,48,'',1,0,now(),now());#--55
          
INSERT INTO empresa_complejo VALUES(23,3,53,1,0,now(),now());#--57
          INSERT INTO proyecto_conexion_sufijo VALUES(0,23,50,'',1,0,now(),now());#--57
          
INSERT INTO empresa_complejo VALUES(24,3,54,1,0,now(),now());#--58
          INSERT INTO proyecto_conexion_sufijo VALUES(0,24,52,'',1,0,now(),now());#--58
          
INSERT INTO empresa_complejo VALUES(25,3,57,1,0,now(),now());#--61
          INSERT INTO proyecto_conexion_sufijo VALUES(0,25,55,'',1,0,now(),now());#--61
          
INSERT INTO empresa_complejo VALUES(26,3,60,1,0,now(),now());#--62
          INSERT INTO proyecto_conexion_sufijo VALUES(0,26,57,'',1,0,now(),now());#--62
          
INSERT INTO empresa_complejo VALUES(27,3,86,1,0,now(),now());#--68
          INSERT INTO proyecto_conexion_sufijo VALUES(0,27,64,'',1,0,now(),now());#--68
          
/*nicapolis NO HAY*/ 
#(13,37,40,41,42,43, 47, 50, 61,64) then 4
INSERT INTO empresa_complejo VALUES(28,4,13,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(29,4,37,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(30,4,40,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(31,4,41,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(32,4,42,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(33,4,43,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(34,4,47,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(35,4,50,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(36,4,61,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(37,4,64,1,0,now(),now());
/*ASHER NO HAY*/
#(16,19,21,26,38, 51) then 5
INSERT INTO empresa_complejo VALUES(38,5,16,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(39,5,19,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(40,5,21,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(41,5,26,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(42,5,38,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(43,5,51,1,0,now(),now());
/*SHOFAR NO HAY*/
#(17,20, 25) then 6
INSERT INTO empresa_complejo VALUES(44,6,17,1,0,now(),now());#--22
            INSERT INTO proyecto_conexion_sufijo VALUES(0,44,98,'STS-',1,0,now(),now());#--22
            
INSERT INTO empresa_complejo VALUES(45,6,20,1,0,now(),now());#--26
            INSERT INTO proyecto_conexion_sufijo VALUES(0,45,21,'',1,0,now(),now());#--26
            
INSERT INTO empresa_complejo VALUES(46,6,25,1,0,now(),now());#--8
            INSERT INTO proyecto_conexion_sufijo VALUES(0,46,6,'',1,0,now(),now());#--8
/*MEXICO */
#(18) then 8
INSERT INTO empresa_complejo VALUES(47,8,18,1,0,now(),now());#--24
            INSERT INTO proyecto_conexion_sufijo VALUES(0,47,20,'',1,0,now(),now());#--24
            INSERT INTO proyecto_conexion_sufijo VALUES(0,47,99,'',1,0,now(),now());#--63
            
/*CLUB DEPORTIVO URUBO */
#(22, 58, 59, 62) then 10
INSERT INTO empresa_complejo VALUES(48,10,22,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(49,10,58,1,0,now(),now());#--35
            INSERT INTO proyecto_conexion_sufijo VALUES(0,49,29,'',1,0,now(),now());#--35
            
INSERT INTO empresa_complejo VALUES(50,10,59,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(51,10,62,1,0,now(),now());
/* MURANO*/
#(27) then 11
INSERT INTO empresa_complejo VALUES(52,11,27,1,0,now(),now());#--30
            INSERT INTO proyecto_conexion_sufijo VALUES(0,52,23,'BCCPN',1,0,now(),now());#--30
            
/*EFRATA EMPREENDIMENTOS TURISTICOS LTDA. */
#(29) then 12
INSERT INTO empresa_complejo VALUES(53,12,29,1,0,now(),now());
/* CLUB DEPORTIVO VALLE ANGOSTURA -13*/
#(28) then 13
INSERT INTO empresa_complejo VALUES(54,13,28,1,0,now(),now());#--31
            INSERT INTO proyecto_conexion_sufijo VALUES(0,54,26,'',1,0,now(),now());#--31
            
/* JAYIL S.R.L - 14 */
#(30,32,35,36,39,33,45,44, 48, 55,65,76,77,78,79,80,82,83,84) then 14
INSERT INTO empresa_complejo VALUES(55,14,30,1,0,now(),now());#--32
            INSERT INTO proyecto_conexion_sufijo VALUES(0,55,24,'J-RDE',1,0,now(),now());#--32
            
INSERT INTO empresa_complejo VALUES(56,14,32,1,0,now(),now());#--38
            INSERT INTO proyecto_conexion_sufijo VALUES(0,56,31,'',1,0,now(),now());#--38
            
INSERT INTO empresa_complejo VALUES(57,14,35,1,0,now(),now());#--39
            INSERT INTO proyecto_conexion_sufijo VALUES(0,57,33,'',1,0,now(),now());#--39
            
INSERT INTO empresa_complejo VALUES(58,14,36,1,0,now(),now());#--40
            INSERT INTO proyecto_conexion_sufijo VALUES(0,58,34,'',1,0,now(),now());#--40
            
INSERT INTO empresa_complejo VALUES(59,14,39,1,0,now(),now());#--43
            INSERT INTO proyecto_conexion_sufijo VALUES(0,59,37,'',1,0,now(),now());#--43
            
INSERT INTO empresa_complejo VALUES(60,14,33,1,0,now(),now());#--36
            INSERT INTO proyecto_conexion_sufijo VALUES(0,60,30,'TSN2-J',1,0,now(),now());#--36
            
INSERT INTO empresa_complejo VALUES(61,14,45,1,0,now(),now());#--49
            INSERT INTO proyecto_conexion_sufijo VALUES(0,61,42,'TSN3-J',1,0,now(),now());#--49
            
INSERT INTO empresa_complejo VALUES(62,14,44,1,0,now(),now());#--48
            INSERT INTO proyecto_conexion_sufijo VALUES(0,62,42,'TSN3-M',1,0,now(),now());#--48
            
INSERT INTO empresa_complejo VALUES(63,14,48,1,0,now(),now());#--52
            INSERT INTO proyecto_conexion_sufijo VALUES(0,63,45,'',1,0,now(),now());#--52
            
INSERT INTO empresa_complejo VALUES(64,14,55,1,0,now(),now());#--60
             INSERT INTO proyecto_conexion_sufijo VALUES(0,64,54,'',1,0,now(),now());#--60
             
INSERT INTO empresa_complejo VALUES(65,14,65,1,0,now(),now());#--66
            INSERT INTO proyecto_conexion_sufijo VALUES(0,65,63,'',1,0,now(),now());#--66
            
INSERT INTO empresa_complejo VALUES(66,14,76,1,0,now(),now());#--21
            INSERT INTO proyecto_conexion_sufijo VALUES(0,66,98,'ATS-U',1,0,now(),now());#--21
            INSERT INTO proyecto_conexion_sufijo VALUES(0,66,98,'KTS-U',1,0,now(),now());#--21
            
INSERT INTO empresa_complejo VALUES(67,14,77,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(68,14,78,1,0,now(),now());#--25
            INSERT INTO proyecto_conexion_sufijo VALUES(0,68,21,'A-TSU',1,0,now(),now());#--25
            INSERT INTO proyecto_conexion_sufijo VALUES(0,68,21,'A2TSU',1,0,now(),now());#--25
            INSERT INTO proyecto_conexion_sufijo VALUES(0,68,21,'A3TSU',1,0,now(),now());#--25
            INSERT INTO proyecto_conexion_sufijo VALUES(0,68,21,'A4-TSU',1,0,now(),now());#--25
            
INSERT INTO empresa_complejo VALUES(69,14,79,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(70,14,80,1,0,now(),now());#--27
            INSERT INTO proyecto_conexion_sufijo VALUES(0,70,22,'A-CRU',1,0,now(),now());#--27
            
INSERT INTO empresa_complejo VALUES(71,14,82,1,0,now(),now());#--29
            INSERT INTO proyecto_conexion_sufijo VALUES(0,71,23,'ACCPN',1,0,now(),now());#--29
            
INSERT INTO empresa_complejo VALUES(72,14,83,1,0,now(),now());
INSERT INTO empresa_complejo VALUES(73,14,84,1,0,now(),now());#--42
            INSERT INTO proyecto_conexion_sufijo VALUES(0,73,36,'',1,0,now(),now());#--42
            
/* NEIZAN / JAYIL S.R.L. -15 */
#(31,81) then 15
INSERT INTO empresa_complejo VALUES(74,15,31,1,0,now(),now());#--33
            INSERT INTO proyecto_conexion_sufijo VALUES(0,74,24,'N-RDE',1,0,now(),now());#--33
            
INSERT INTO empresa_complejo VALUES(75,15,81,1,0,now(),now());
/* NEIZAN / ASHER S.R.L. -16 */
#(23) then 16
INSERT INTO empresa_complejo VALUES(76,16,23,1,0,now(),now());#--28
            INSERT INTO proyecto_conexion_sufijo VALUES(0,76,22,'N-CRU',1,0,now(),now());#--28
            
/* CLUB DEPORTIVO ROYAL PARI -17 */
#(46, 49) then 17
INSERT INTO empresa_complejo VALUES(77,17,46,1,0,now(),now());#--50
            INSERT INTO proyecto_conexion_sufijo VALUES(0,77,43,'',1,0,now(),now());#--50
            INSERT INTO proyecto_conexion_sufijo VALUES(0,77,53,'',1,0,now(),now());#--59
INSERT INTO empresa_complejo VALUES(78,17,49,1,0,now(),now());#--53
            INSERT INTO proyecto_conexion_sufijo VALUES(0,78,46,'',1,0,now(),now());#--53
            INSERT INTO proyecto_conexion_sufijo VALUES(0,78,49,'',1,0,now(),now());#--53
            
/* INMOBILIARIA MENORAH S.R.L -18 */
#(34) then 18
INSERT INTO empresa_complejo VALUES(79,18,34,1,0,now(),now());#--37
            INSERT INTO proyecto_conexion_sufijo VALUES(0,79,30,'TSN2-S',1,0,now(),now());#--37
/* AVDEL S.R.L. -21 */
#(85) then 21
INSERT INTO empresa_complejo VALUES(80,21,85,1,0,now(),now());#--34
            INSERT INTO proyecto_conexion_sufijo VALUES(0,80,25,'',1,0,now(),now());#--34
            
            


