using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class UsuarioAutorizacion
    {
        public int IdUsuarioAutorizacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoAutorizacion { get; set; }
        public bool? Estado { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
