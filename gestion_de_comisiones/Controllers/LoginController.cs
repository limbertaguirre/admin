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

namespace gestion_de_comisiones.Controllers
{

    public class LoginController : Controller
    {
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
                BDMultinivelContext contextMulti = new BDMultinivelContext();

                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "gruposionbo.scz"))
                {
                   
                    bool valid = context.ValidateCredentials(model.userName, model.password);
                    if (valid)
                    {
                        LoginService LogiService = new LoginService();
                        var usuario = LogiService.VerificarUsuario(model.userName);
                        return Ok(usuario);                        
                    }
                    else
                    {
                        var Result = new GenericDataJson<string> { Code = 1, Message = "Credenciales Invalidas de GRUPO SION" };
                        return Ok(Result);
                    }
                }
            }
            catch (Exception ex) {
                var Result = new GenericDataJson<string> { Code = 1, Message = "Intente mas tarde", Data = ex.Message };
                return Ok(Result);
            }
        } 



    }
}