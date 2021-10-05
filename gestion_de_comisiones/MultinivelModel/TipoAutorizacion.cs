using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class TipoAutorizacion
    {
        public int IdTipoAutorizacion { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public bool? Estado { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
