using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpComisionDetalleEstadoI
    {
        public int IdComisionDetalleEstadoI { get; set; }
        public int? IdComisionDetalle { get; set; }
        public int? IdEstadoComisionDetalle { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
