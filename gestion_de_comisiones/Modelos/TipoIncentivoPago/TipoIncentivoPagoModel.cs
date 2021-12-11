using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.TipoIncentivoPago
{
    public class TipoIncentivoPagoModel
    {
        public int IdTipoIncentivo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
