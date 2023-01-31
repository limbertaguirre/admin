using admin.Modelos.Modulo;
using admin.Modelos.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Servicios
{
    public interface IRolService
    {
        public object RegistraRol(RolRegisterInputModel data);
        public object ObtenerMooduloPaginas();
        public object ObtenerPermiso();
        public List<ModuloResulModel> FuncionObtenerMooduloPaginas();
        public object ObtenerListaRol(int idROl);
        public List<ModuloResulwithPermisoModel> FuncionObtenerListaXRol(int idROl);
        public object ObtenerListaRolesWithModulos( string usuario);
        public object ActualizarRol(RolActualizarInputModel objRol);
        public List<PaginaResulModelWithPermisos> funcionRecargarpaginas(RolActualizarInputModel objRol);

        public List<RolResulModel> GetAllRoles(string usuario);

    }
}
