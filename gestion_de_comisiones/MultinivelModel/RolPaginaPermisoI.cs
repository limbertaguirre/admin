using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class RolPaginaPermisoI
    {
        public int IdRolPaginaPermisoI { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdRolPagina { get; set; }
        public int? IdPermiso { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
