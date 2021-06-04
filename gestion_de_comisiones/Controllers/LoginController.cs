using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Modelos.Usuario;
using gestion_de_comisiones.Servicios;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace gestion_de_comisiones.Controllers
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
        public ActionResult Sesion([FromBody] LoginInputModel model)
        {
            try
            {
                Logger.LogInformation($" usuario : {model.userName} inicio el servicio Sesion() ");
                BDMultinivelContext contextMulti = new BDMultinivelContext();

                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "gruposionbo.scz"))
                {                   
                    bool valid = context.ValidateCredentials(model.userName, model.password);
                    if (valid)
                    {                     
                        var usuario = Service.VerificarUsuario(model.userName);
                        Logger.LogInformation($" usuario : {model.userName} fin de servicio sesion() : {JsonConvert.SerializeObject(usuario)}");
                        return Ok(usuario);                        
                    }
                    else
                    {
                        var Result = new GenericDataJson<string> { Code = 1, Message = "Credenciales Invalidas de GRUPO SION" };
                        Logger.LogWarning($" usuario : {model.userName} fin de servicio - Credenciales Invalidas de GRUPO SION");
                        return Ok(Result);
                    }
                }
            }
            catch (Exception ex) {
                Logger.LogError($" usuario : {model.userName} catch sesion() error : : {ex.Message}");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Intente mas tarde", Data = ex.Message };
                return Ok(Result);
            }
        } 



    }
}