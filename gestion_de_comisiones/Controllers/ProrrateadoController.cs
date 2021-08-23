using gestion_de_comisiones.Dtos;
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
    public class ProrrateadoController : ControllerBase
    {
        private readonly ILogger<ProrrateadoController> Logger;

        public ProrrateadoController(ILogger<ProrrateadoController> logger, IProrrateadoService service)
        {
            Logger = logger;
            Service = service;
        }

        public IProrrateadoService Service { get; }

        public ActionResult GetCiclos([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerCiclos prorrateo()  ");
                var ciclos = Service.GetCiclos(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerCiclos()  ");                
                return Ok(ciclos);
            }
            catch
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerCiclosprorrateo() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
                return Ok(Result);
            }
        }

    }
}
