using admin.Modelos.Modulo;
using admin.Modelos.Permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class RolMasterModel
    {
        public List<ModuloResulModel> ListModulos { get; set; }

        public List<PermisoResulModel> listPermiso { get; set; }

    }
}
