using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.IncentivoSionPay
{
    public class PagoIncentivo
    {
        public string NombreCompleto { get; set; }
        public int IdComision { get; set; }
        public string CedulaIdentidad { get; set; }
        public string CuentaBanco { get; set; }
        public string Banco { get; set; }
        public decimal? MontoTotalNeto { get; set; }
        public int IdTipoIncentivoPago { get; set; }
        public string TipoIncentivo { get; set; }
        public string TipoPago { get; set; }
        public int? IdCiclo { get; set; }
        public int IdTipoIncentivo { get; set; }
        public bool pagado { get; set; }
    }
}
