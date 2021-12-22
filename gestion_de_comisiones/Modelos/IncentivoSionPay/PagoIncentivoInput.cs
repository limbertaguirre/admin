using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.IncentivoSionPay
{
    public class PagoIncentivoInput
    {
        public string UsuarioLogin { get; set; }
        public List<PagoIncentivo> IncentivosPagar { get; set; }
    }
}
