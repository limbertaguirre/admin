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
        Task<List<UsuarioSelectModel>> GetUsuariosForSelect(UsuariosSelectInputModel model);
        Task<List<UsuarioRolListViewModel>> GetUsuariosRol(string usuario);
        Task<bool> DeleteUsuarioRol(DeleteUserRolInputModel model);
        public UsuarioModel ObtenerUsuarioPorUsuario(string usuario);
        public ControlUsuarioModel VerificarSession(string usuario, string netSessionId, int estado);
        public UsuarioModel ObtenerUsuarioPorId(string usuario);
        public bool ActualizarIntentoUsuario(string usuario, string netSessionId, int cantidad, int estado);
        public bool InsertarIntentoUsuario(string usuario, string netSessionId, int cantidad);
    }
}
