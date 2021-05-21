using gestion_de_comisiones.Modelos.Permiso;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class PermisoRepository
    {

        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public List<PermisoResulModel> obtenerPermisos()
        {
            try
            {
                var ListPermiso = contextMulti.Permisoes.Where(x => x.IdPermiso  > 0).Select(p => new PermisoResulModel(p.IdPermiso, p.Permiso1)).ToList();
                return ListPermiso;
            }
            catch (Exception ex)
            {
                List<PermisoResulModel> list = new List<PermisoResulModel>();
                return list;
            }
        }
    }
}
