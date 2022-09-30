using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class FacturadoTodoInput
    {
        public string usuarioLogin { get; set; }
        public int idComisionDetalle { get; set; }
        public bool estadoFacturado { get; set; }
        public int usuarioId { get; set; }

    }
}
