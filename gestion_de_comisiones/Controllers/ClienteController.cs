using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Cliente;
using gestion_de_comisiones.Modelos.Pais;
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
        // POST: ClienteController/BuscarCliente
        [HttpPost]
        public ActionResult BuscarCliente([FromBody] ClientInput  param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller BuscarCliente() criterio : {param.criterio} ");
                var resulcliente = Service.buscarClientesNombre(param.usuarioLogin, param.criterio);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller BuscarCliente()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  BuscarCliente() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al buscar clientes" };
                return Ok(Result);
            }
        }
        // POST: ClienteController/IdObtenerCliente
        [HttpPost]
        public ActionResult IdObtenerCliente([FromBody] ClienteInputObtenerModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller ObtenerCliente() criterio : {param.idCliente} ");
                var resulcliente = Service.obtenerClientePorID(param.usuarioLogin, param.idCliente);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller ObtenerCliente()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  ObtenerCliente() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al buscar clientes" };
                return Ok(Result);
            }
        }
        // GET: ClienteController/ListaPaises
        public ActionResult ListaPaises([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller ListaPaises()  ");
                var resulcliente = Service.ListarPaises(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller ListaPaises()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  ListaPaises() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener los clientes" };
                return Ok(Result);
            }
        }
        // POST: ClienteController/ListarCiudadesPais
        [HttpPost]
        public ActionResult ListarCiudadesPais([FromBody] CiudadInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller ListarCiudadesPais()  ");
                var resulcliente = Service.listaCiudadesXPais(param.usuarioLogin, param.idPais);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller ListarCiudadesPais()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  ListarCiudadesPais() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener los clientes" };
                return Ok(Result);
            }
        }
        // GET: ClienteController/ObtenerBajasClientes
        public ActionResult ObtenerBajasClientes([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($" es el usuario : {usuarioLogin} inicio el controller ObtenerBajasClientes()  ");
                var resulcliente = Service.obtenerListadeBajas(usuarioLogin);
                Logger.LogInformation($" es el usuario : {usuarioLogin} Fin del controller ObtenerBajasClientes()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($" es el usuario : {usuarioLogin} error catch  ObtenerBajasClientes() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
                return Ok(Result);
            }
        }
        // GET: ClienteController/obtenerBancosClientes
        public ActionResult obtenerBancosClientes([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerBancosClientes()  ");
                var resulcliente = Service.obtenerBancoParaclientes(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerBancosClientes()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerBancosClientes() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
                return Ok(Result);
            }
        }

        // GET: ClienteController/obtenerNivelesClientes
        public ActionResult obtenerNivelesClientes([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerNivelesClientes()  ");
                var resulcliente = Service.obtenerNivelesCliente(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerNivelesClientes()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerNivelesClientes() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
                return Ok(Result);
            }
        }

    }
}
