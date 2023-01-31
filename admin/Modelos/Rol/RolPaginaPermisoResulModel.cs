using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class RolPaginaPermisoResulModel
    {
        public int idPagina { get; set; }
        public int idRol { get; set; }
        public int idPermiso { get; set; }
        public bool habilitado { get; set; }

    }
}
