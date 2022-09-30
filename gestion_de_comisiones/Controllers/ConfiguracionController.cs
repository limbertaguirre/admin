using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: ConfiguracionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ConfiguracionController/Areas
        public ActionResult Areas()
        {
            try
            {
                ConfiguracionService Config = new ConfiguracionService();
                var respuesta = Config.ObtenerListAreas();
                return Ok(respuesta);
            }
            catch(Exception ex)
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO SE ENCONTRO AREAS" };
                return Ok(Result);
            }
        }

        // GET: ConfiguracionController/Sucursales
        public ActionResult Sucursales()
        {
            try
            {
                ConfiguracionService Config = new ConfiguracionService();
                var respuesta = Config.ObtenerListSucursales();
                return Ok(respuesta);
            }
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO SE ENCONTRO SUCURSALES" };
                return Ok(Result);
            }
        }

        // POST: ConfiguracionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ConfiguracionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }



    }
}
