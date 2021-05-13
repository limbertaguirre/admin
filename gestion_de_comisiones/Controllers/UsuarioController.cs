using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // POST: UsuarioController/Registro
        [HttpPost]
        public ActionResult Registro([FromBody] UsuarioRegisterInputModel model)
        {
            try
            {
                string nombre = "sss";
                var Result = new GenericDataJson<string> { Code =0 , Message = "SE REGISTRO CON EXITO " };
                return Ok(Result);
            }
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO SE REGISTRO EL CLIENTE" };
                return Ok(Result);
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
