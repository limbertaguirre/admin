using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class RolController : Controller
    {
        // GET: RolController
        public ActionResult Index()
        {
            return View();
        }


        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: RolController/Registrar
        [HttpPost]
        public ActionResult Registrar([FromBody] RolRegisterInputModel objetdatao)
        {
            RolService refRol = new RolService();

            try
            {
                var resulRol = refRol.RegistraRol(objetdatao);
                var Result = new GenericDataJson<string> { Code = 0, Message = "SE REGISTRO CORRECTAMENTE" };
                return Ok(Result);
            }
            catch (Exception ex)
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO SE ENCONTRO AREAS" };
                return Ok(Result);
            }
        }
    }
}
