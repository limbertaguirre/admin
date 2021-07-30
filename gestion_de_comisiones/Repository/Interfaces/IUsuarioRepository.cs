using gestion_de_comisiones.Modelos.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> SetRolByUsuario(SetRolModel model);
        Task<List<UsuarioSelectModel>> GetUsuarios(UsuariosSelectInputModel model);
        Task<List<UsuarioRolListViewModel>> GetUsuariosRol(string usuario);
        Task<bool> DeleteUsuarioRol(DeleteUserRolInputModel model);
    }
}
