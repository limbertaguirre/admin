using gestion_de_comisiones.Modelos.Modulo;
using gestion_de_comisiones.Modelos.Permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol
{
    public class RolMasterModel
    {
        public List<ModuloResulModel> ListModulos { get; set; }

        public List<PermisoResulModel> listPermiso { get; set; }

    }
}
