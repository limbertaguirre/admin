using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpDetalleEstadoListadoFormaPagol
    {
        public int Id { get; set; }
        public bool Habilitado { get; set; }
        public int IdListaFormasPago { get; set; }
        public int IdEstadoListadoFormaPago { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
