using admin.Modelos.Area;
using admin.OperacionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Repository
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
