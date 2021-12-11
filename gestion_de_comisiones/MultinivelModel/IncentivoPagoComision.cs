using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class IncentivoPagoComision
    {
        public int IdDetalle { get; set; }
        public int IdTipoIncentivoPago { get; set; }
        public int IdComisionDetalle { get; set; }
    }
}
