using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol
{
    public class ModuloInputModel
    {
        public int idModulo { get; set; }
        public string nombreModulo { get; set; }

        public List<PaginasInputModel> paginas { get; set; }

    }
}
