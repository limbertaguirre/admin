using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class RespuestaPorTipoPagoModel
    {
        public int CodigoRespuesta { get; set; }
        public int Cantidad { get; set; }
        public decimal totalPagoSionPay { get; set; }

    }
}
