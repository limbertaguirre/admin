using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class GestionPagosController : Controller
    {
        private readonly ILogger<GestionPagosController> Logger;
        public GestionPagosController(ILogger<GestionPagosController> logger, IGestionPagosService service)
        {
            Logger = logger;
            Service = service;
        }
        public IGestionPagosService Service { get; set; }

        public IActionResult Index()
        {
            return View();
        }
        // GET: gestionPagos/GetCiclos
        public ActionResult GetCiclos([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerCiclos()  ");
             //   return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al obtener ciclos de pagos" });
                return Ok(Service.GetCiclos(usuarioLogin));

            }
            catch (Exception e)
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerCiclos() controller mensaje:{ e.Message }");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al obtener los ciclos de pagos" });
            }
        }

    }
}
