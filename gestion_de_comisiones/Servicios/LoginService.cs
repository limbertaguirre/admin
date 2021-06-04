using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using Newtonsoft.Json;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Servicios
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> Logger;
        public LoginService(ILogger<LoginService> logger )
        {
            Logger = logger;
        }

        public object VerificarUsuario(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario : {usuario} inicio la funcionalidad VerificarUsuario()");
                UsuarioRepository UserRepos = new UsuarioRepository();
                var objetoo = UserRepos.ObtenerUsuarioPorId(usuario);
                if (objetoo != null)
                {
                    Logger.LogInformation($" usuario : {usuario} repuesta obtener usuario: {JsonConvert.SerializeObject(objetoo)}");
                    var Result = new GenericDataJson<object> { Code = 0, Message = "ok", Data = objetoo };
                    return Result;
                }
                else
                {
                    Logger.LogWarning($" usuario : {usuario} no se encontro el registro");
                    var Result = new GenericDataJson<string> { Code = 2, Message = "El usaurio no se encuentra registrado" };
                    return Result;
                }

            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario : {usuario} catch error f,fin {ex.Message}");
                return ex;
            }
        }


    }
}
