using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class TipoPagoInputmodel
    {
        public TipoPagoInputmodel(int idTipoPago, string nombre)
        {
            this.idTipoPago = idTipoPago;
            this.nombre = nombre;
        }

        public int idTipoPago { get; set; }
        public string nombre { get; set; }
    }
}
