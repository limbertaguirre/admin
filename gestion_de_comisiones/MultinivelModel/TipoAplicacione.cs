using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class TipoAplicacione
    {
        public int IdTipoAplicaciones { get; set; }
        public int GuardianIdCicloDescuentoTipo { get; set; }
        public string Descripcion { get; set; }
        public bool? ValidoGuardian { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
