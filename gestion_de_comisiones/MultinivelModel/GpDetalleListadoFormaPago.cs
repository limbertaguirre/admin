using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpDetalleListadoFormaPago
    {
        public int IdGpDetalleListadoFormaPago { get; set; }
        public decimal Monto { get; set; }
        public int IdListaFormasPago { get; set; }
        public int IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
