using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class AutorizacionesArea
    {
        public int IdAutorizacionesArea { get; set; }
        public int IdArea { get; set; }
        public int IdTipoAutorizacion { get; set; }
        public int Cantidad { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
