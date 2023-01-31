using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;
using admin.Modelos.Usuario;
using admin.Servicios;
using admin.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using admin.OperacionModel;

namespace admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<RolController> Logger;
        public LoginController(ILogger<RolController> logger, ILoginService service)
        {
            Logger = logger;
            Service = service;  

        }
        public ILoginService Service { get; }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/Sesion        
        [HttpPost]
        public async Task<ActionResult> Sesion([FromBody] LoginInputModel model)
        {
            try
            {
                Logger.LogInformation($" usuario : {model.userName} inicio el servicio Sesion() ");
                BDOperacionContext contextMulti = new BDOperacionContext();

                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "gruposionbo.scz"))
                {
                    bool valid = context.ValidateCredentials(model.userName, model.password);
                    if (valid)
                    {
                        var usuario = await Service.VerificarUsuarioAsync(model.userName);
                        var t = Service.verificarSession(model.userName, Request.Cookies["ASP.NET_SessionId"], 1);
                        Logger.LogInformation($" usuario : {model.userName} fin de servicio sesion() : {JsonConvert.SerializeObject(usuario)}");
                        return Ok(usuario);
                    }
                    else
                    {
                        var responseTiempoBloqueo = Service.verificarSession(model.userName, Request.Cookies["ASP.NET_SessionId"], 0);
                        if (responseTiempoBloqueo == null)
                        {
                            var Result = new GenericDataJson<string> { Code = 1, Message = "Credenciales Invalidas de GRUPO SION" };
                            Logger.LogWarning($" usuario : {model.userName} fin de servicio - Credenciales Invalidas de GRUPO SION");
                            return Ok(Result);
                        }
                        else
                        {
                            var Result = new GenericDataJson<string> { Code = 3, Message = "Usuario bloqueado", Data = responseTiempoBloqueo.ToString() };
                            Logger.LogWarning($" usuario : {model.userName} fin de servicio - Usuario bloqueado, tiempo bloqueado: { responseTiempoBloqueo.ToString()}");
                            return Ok(Result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario : {model.userName} catch sesion() error : : {ex.Message}");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Intente mas tarde", Data = ex.Message };
                return Ok(Result);
            }
        }
    }
}