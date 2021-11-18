using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class RolPaginaI
    {
        public int IdRolPaginaI { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdRol { get; set; }
        public int? IdPagina { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
