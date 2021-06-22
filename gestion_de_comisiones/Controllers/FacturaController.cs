using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class FacturaController : Controller
    {
        private readonly ILogger<FacturaController> Logger;
        public FacturaController(ILogger<FacturaController> logger, IFacturaService service)
        {
            Logger = logger;
            Service = service;
        }
        public IFacturaService Service { get; set; }

        // GET: FacturaController
        public ActionResult Index()
        {
            return View();
        }
        // GET: FacturacionController/ObtenerCiclos
        public ActionResult ObtenerCiclos([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerCiclos()  ");
                var Result = Service.obtenerlistCiclos(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerCiclos()  ");
                return Ok(Result);
            }
            catch
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerCiclos() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
                return Ok(Result);
            }
        }


    }
}
