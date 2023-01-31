using admin.Modelos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Servicios.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> SetRolByUsuario(SetRolModel model);
        Task<List<UsuarioSelectModel>> GetUsuariosForSelect(UsuariosSelectInputModel model);
        Task<List<UsuarioRolListViewModel>> GetUsuariosRol(string usuario);
        Task<bool> DeleteUserRol(DeleteUserRolInputModel model);
    }
}
