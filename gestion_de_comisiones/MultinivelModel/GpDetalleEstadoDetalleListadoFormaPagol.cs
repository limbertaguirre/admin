using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpDetalleEstadoDetalleListadoFormaPagol
    {
        public int Id { get; set; }
        public bool Habilitado { get; set; }
        public int IdGpDetalleListadoFormaPago { get; set; }
        public int IdEstadoDetalleListadoFormaPago { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
