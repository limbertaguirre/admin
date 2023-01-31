using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class PaginasInputModel
    {
        public int idPagina { get; set; }
        public string nombrePagina { get; set; }
        public List<PermisoInputModel> permisos { get; set; }

    }
}
