using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwPagosIncentivo
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
        public string ComisionPagada { get; set; }
        public string CuentaSionPay { get; set; }
    }
}
