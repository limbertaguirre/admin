using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class ListadoFormasPago
    {
        public int IdListaFormasPago { get; set; }
        public decimal MontoNeto { get; set; }
        public int IdTipoPago { get; set; }
        public int IdComisionesDetalle { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
