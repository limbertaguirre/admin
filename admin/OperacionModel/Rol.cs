using System;
using System.Collections.Generic;

#nullable disable

namespace admin.OperacionModel
{
    public partial class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
