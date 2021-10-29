using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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
                return Ok(Service.GetCiclos(usuarioLogin));

            }
            catch (Exception e)
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerCiclos() controller mensaje:{ e.Message }");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al obtener los ciclos de pagos" });
            }
        }
        // POST: gestionPagos/GetComisionesPagos
        [HttpPost]
        public ActionResult GetComisionesPagos([FromBody] ComisionesPagosInput param)
        {
            try
            {
                Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controller AplicacionesController => Index() parametro: idciclo:{param.idCiclo}");
                var resulcliente = Service.GetComisionesDePagos(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller AplicacionesController => Index()");
                return Ok(resulcliente);
            } catch {
                Logger.LogError($"usuario request: {param.usuarioLogin} error catch controller forma pagos AplicacionesController()  => Index() ");                   
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las pendiente para forma de pagos." });
            }
        }
        public ActionResult GetFiltroFormaPagosDisponibles([FromBody] FiltroFormaPagosInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller GetFiltroFormaPagosDisponibles()  ");
                //var ciclos = Service.GetFormaPagosDisponibles(param);
                //Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller GetFiltroFormaPagosDisponibles()  ");
                return Ok(Service.GetFormaPagosDisponibles(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  GetFiltroFormaPagosDisponibles() controller ");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al obtener los listros de forma de pagos" });
            }
        }

        // POST: Pagos/BuscarComisionCarnetFormaPago
        [HttpPost]
        public ActionResult BuscarComisionCarnetFormaPago([FromBody] BuscarComisionInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller BuscarComisionNombre() parametro: idciclo:{param.idCiclo}, criterio busqueda: {param.nombreCriterio}");
                return Ok(Service.ListarComisionesFormaPagoPorCarnet(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  BuscarComisionNombre() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
                return Ok(Result);
            }
        }

        // POST: Pagos/BuscarComisionCarnetFormaPago
        [HttpPost]
        public ActionResult handleTransferenciasEmpresas([FromBody] ComisionesPagosInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller handleTransferenciasEmpresas() parametro: idciclo:{param.idCiclo}");
                return Ok(Service.handleTransferenciasEmpresas(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  handleTransferenciasEmpresas() controller ");
                //var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las empresas." });
            }
        }

        [HttpPost]
        public ActionResult handleDownloadFileEmpresas([FromBody] DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el controller handleDownloadFileEmpresas() parametro: idciclo:{body.cicloId}, empresaId: {body.empresaId}");
                return Ok(Service.handleDownloadFileEmpresas(body));               
            }
            catch(Exception e)
            {
                Logger.LogError($"usuario :  error catch  handleDownloadFileEmpresas() controller {e}");
                //var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las empresas." });
            }
        }

    }
}
