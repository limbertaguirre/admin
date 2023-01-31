using admin.Modelos.Modulo;
using admin.Modelos.Pagina;
using admin.Modelos.Rol;
using admin.Modelos.Rol.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Repository.Interfaces
{
    public interface IRolRepository
    {
        public int RegistrarRol(string nombre, string descripcion, int usuarioId);
        public List<RolResulModel> obtenerRolesAll(string usuario);
        public RolResulModel obtenerRolXId(int idRol);
        public bool actualizarRoles(int idRol, string nombreRol, string descripcionRol, List<PaginaResulModelWithPermisos> paginas, int idUsuario);
        public bool validarPaginaTienePermisoActivos(List<RolPermiso> permisos);
        public RolUserResulModel obtenerRolxUsuario(int idUsuario);
        public List<ModuloModel> obtnerModulosPadres(string usuario);
        public List<ModuloModel> obtnerSubModulosXIdPadre(string usuario, int IdModulo);
        public List<PaginaModel> obtenerPaginasXModulo(string usuario, int IdModulo);
        public RolPaginaModel obtenerRolPaginaXPagina(string usuario, int IdPagina, int IdRol);
        public List<PerfilHash> obtenerPermisoXPagina(string usuario, int idRolPagina, string nombrePagina, string pathPagina);


    }
}
