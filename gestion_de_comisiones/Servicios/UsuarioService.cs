using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using gestion_de_comisiones.Repository;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class UsuarioService:IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<bool> SetRolByUsuario(SetRolModel model)
        {
            return await usuarioRepository.SetRolByUsuario(model);
        }

        public async Task<List<UsuarioSelectModel>> GetUsuarios(string usuario)
        {
            return await usuarioRepository.GetUsuarios(usuario);
        }

        public object RegistraUsuario(UsuarioRegisterInputModel user)
        {
            UsuarioRepository UserRepos = (UsuarioRepository)usuarioRepository;
            var objetoo = UserRepos.ObtenerUsuarioPorId(user.userName);
            if (objetoo == null)
            {
                var resulrRegister = UserRepos.RegistrarUsuario(user);
                if (resulrRegister)
                {
                    var Result = new GenericDataJson<object> { Code = 0, Message = "se registro exitosamente", Data = objetoo };
                    return Result;
                }
                else
                {
                    var Result = new GenericDataJson<string> { Code = 1, Message = "El usuario se encuentra ya registrado" };
                    return Result;
                }
            }
            else
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "El usuario se encuentra ya registrado" };
                return Result;
            }
           
        }
    }
}
