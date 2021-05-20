using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol
{
    public class PaginasInputModel
    {
        public int idPagina { get; set; }
        public string nombrePagina { get; set; }
        public List<PermisoInputModel> permisos { get; set; }

    }
}
