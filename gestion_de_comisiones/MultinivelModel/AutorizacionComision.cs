using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class AutorizacionComision
    {
        public int IdAutorizacionComision { get; set; }
        public int IdComision { get; set; }
        public int IdUsuarioAutorizacion { get; set; }
        public int IdEstadoAutorizacionComision { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
