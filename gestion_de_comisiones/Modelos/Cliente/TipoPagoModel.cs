using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Cliente
{
    public class TipoPagoModel
    {
        public int IdTipoPago { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
