using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class TipoPago
    {
        public int IdTipoPago { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool Estado { get; set; }
        public string Icono { get; set; }
    }
}
