using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class ObjetoComisionesRespuesta
    {
        public bool PendienteFormaPago { get; set; }
        public List<VwObtenercomisionesFormaPago> lista { get; set; }
    }
}
