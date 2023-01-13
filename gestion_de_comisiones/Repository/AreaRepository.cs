﻿using gestion_de_comisiones.Modelos.Area;
using gestion_de_comisiones.OperacionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    
    public class AreaRepository
    {
        BDOperacionContext contextMulti = new BDOperacionContext();
        public List<AreaResultModel> obtenerlistadoAreas()
        {
            var objUsuario = contextMulti.Areas.Where(x => x.Habilitado == true).Select( p => new AreaResultModel(p.IdArea,p.Nombre)).ToList();
            return objUsuario;

          
        }
        
    }
}
