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
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> Logger;
        public ClienteController(ILogger<ClienteController> logger, IClienteService service )
        {
            Logger = logger;
            Service = service;
        }
        public IClienteService Service { get; set; }

        // GET: ClienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ClienteController/ObtenerClientes
        public ActionResult ObtenerClientes([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($" es el usuario : {usuarioLogin} inicio el controller ObtenerClientes()  ");
                var resulcliente = Service.ObtenerClientes(usuarioLogin);
                Logger.LogInformation($" es el usuario : {usuarioLogin} Fin del controller ObtenerClientes()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($" es el usuario : {usuarioLogin} error catch  ObtenerClientes() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener los clientes" };
                return Ok(Result);
            }
        }



    }
}
