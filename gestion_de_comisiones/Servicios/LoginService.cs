using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using Newtonsoft.Json;

namespace gestion_de_comisiones.Servicios
{
    public class LoginService
    {
        //BDMultinivelContext contextMulti = new BDMultinivelContext();

        public object VerificarUsuario(string usuario)
        {
            try
            {
                UsuarioRepository UserRepos = new UsuarioRepository();
                var objetoo = UserRepos.ObtenerUsuarioPorId(usuario);
                if (objetoo != null)
                {
                    var Result = new GenericDataJson<object> { Code = 0, Message = "prueba", Data = objetoo };
                    return Result;
                }
                else
                {
                    var Result = new GenericDataJson<string> { Code = 2, Message = "El usaurio no se encuentra registrado" };
                    return Result;
                }

            }
            catch (Exception ex)
            {
                return ex;
            }
        }


    }
}
