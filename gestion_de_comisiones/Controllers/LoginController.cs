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

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
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
                        var usuario = LogiService.ObtenerUsuario(model.userName);
                        if (usuario != null)
                        {
                            var Result = new GenericDataJson<string> { Code = 0, Message = "prueba", Data = model.userName };
                            return Ok(Result);
                        }
                        else
                        {
                            var Result = new GenericDataJson<string> { Code = 2, Message = "El usaurio no se encuentra registrado"};
                            return Ok(Result);
                        }
                        
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

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}