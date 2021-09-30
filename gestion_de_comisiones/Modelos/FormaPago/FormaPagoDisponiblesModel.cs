using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class FormaPagoDisponiblesModel
    {
        public int idTipoPago { get; set; }
        public string  nombre { get; set; }
        public string icono { get; set; }
        public int cantidad { get; set; }

    }
}
