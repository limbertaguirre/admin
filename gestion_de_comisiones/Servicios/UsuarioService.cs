using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using gestion_de_comisiones.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class UsuarioService
    {
        public object RegistraUsuario(UsuarioRegisterInputModel user)
        {
            UsuarioRepository UserRepos = new UsuarioRepository();
            var objetoo = UserRepos.ObtenerUsuarioPorId(user.userName);
            if (objetoo == null)
            {
                var resulrRegister = UserRepos.RegistrarUsuario(user);
                if (resulrRegister)
                {
                    var Result = new GenericDataJson<object> { Code = 0, Message = "se registro correctamente", Data = objetoo };
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
