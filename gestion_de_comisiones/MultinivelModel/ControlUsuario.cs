using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class ControlUsuario
    {
        public int IdControlUsuario { get; set; }
        public string Usuario { get; set; }
        public int? CantidadIntentos { get; set; }
        public DateTime? FechaBloquedo { get; set; }
        public DateTime? FechaDesbloqueo { get; set; }
        public string NetSessionId { get; set; }
        public int Estado { get; set; }
    }
}
