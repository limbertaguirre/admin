using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Factura;
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
        // POST: FacturaController/ListarComisionesPendientes
        [HttpPost]
        public ActionResult ListarComisionesPendientes([FromBody] ComisionesInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller ListarComisionesPendientes() parametro: idciclo:{param.idCiclo}");
                var resulcliente = Service.obtenerlistComisionesPendiente(param.usuarioLogin, param.idCiclo);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller ListarComisionesPendientes()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  ListarComisionesPendientes() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
                return Ok(Result);
            }
        }
        // POST: FacturaController/BuscarComisionNombre
        [HttpPost]
        public ActionResult BuscarComisionNombre([FromBody] BuscarInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller BuscarComisionNombre() parametro: idciclo:{param.idCiclo}, criterio busqueda: {param.nombreCriterio}");
                var resulcliente = Service.buscarComisionesPorNombre(param.usuarioLogin, param.idCiclo,param.nombreCriterio);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller BuscarComisionNombre()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  BuscarComisionNombre() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
                return Ok(Result);
            }
        }
        // POST: FacturaController/BuscarComisionNombre
        [HttpPost]
        public ActionResult ComisionesDetalleEmpresa([FromBody] DetalleEmpresaInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller ComisionesDetalleEmpresa() parametro: ");
                var resulcliente = Service.obtenerListaComisionesDetalleEmpresa(param.usuarioLogin, param.idComisionDetalleEmpresa);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller ComisionesDetalleEmpresa()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  ComisionesDetalleEmpresa() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones detalle empresa" };
                return Ok(Result);
            }
        }

    }
}
