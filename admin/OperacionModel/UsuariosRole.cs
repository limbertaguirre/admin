using System;
using System.Collections.Generic;

#nullable disable

namespace admin.OperacionModel
{
    public partial class UsuariosRole
    {
        public int IdUsuariosRoles { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public bool? Estado { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
