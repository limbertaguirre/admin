using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol
{
    public class RolPaginaPermisoModel
    {
        public RolPaginaPermisoModel(int idRolPaginaPermisoI, bool? habilitado, int? idRolPagina, int? idPermiso, int? idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion)
        {
            IdRolPaginaPermisoI = idRolPaginaPermisoI;
            Habilitado = habilitado;
            IdRolPagina = idRolPagina;
            IdPermiso = idPermiso;
            IdUsuario = idUsuario;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
        }

        public int IdRolPaginaPermisoI { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdRolPagina { get; set; }
        public int? IdPermiso { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

    }
}
