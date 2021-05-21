using gestion_de_comisiones.Modelos.Pagina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Modulo
{
    public class ModuloResulModel
    {
        public int idModulo { get; set; }
        public string nombre { get; set; }
        public List<PaginaResulModel> listmodulos { get; set; }
    }
}
