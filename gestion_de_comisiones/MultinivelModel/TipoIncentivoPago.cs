using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class TipoIncentivoPago
    {
        public int IdTipoIncentivo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
