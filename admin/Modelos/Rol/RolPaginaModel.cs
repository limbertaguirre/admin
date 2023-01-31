using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class RolPaginaModel
    {
        public RolPaginaModel()
        {
        }

        public RolPaginaModel(int? idRolPaginaI, bool? habilitado, int? idRol, int? idPagina, int? idUsuario, DateTime? fechaCreacion, DateTime? fechaActualizacion)
        {
            IdRolPaginaI = idRolPaginaI;
            Habilitado = habilitado;
            IdRol = idRol;
            IdPagina = idPagina;
            IdUsuario = idUsuario;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
        }

        public int? IdRolPaginaI { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdRol { get; set; }
        public int? IdPagina { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
