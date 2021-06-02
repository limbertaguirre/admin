using gestion_de_comisiones.Modelos.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IRolRepository
    {
        public int RegistrarRol(string nombre, string descripcion, int usuarioId);
        public List<RolResulModel> obtenerRolesAll(string usuario);
        public RolResulModel obtenerRolXId(int idRol);
        public bool actualizarRoles(int idRol, string nombreRol, string descripcionRol, List<PaginaResulModelWithPermisos> paginas, int idUsuario);
        public bool validarPaginaTienePermisoActivos(List<RolPermiso> permisos);

    }
}
