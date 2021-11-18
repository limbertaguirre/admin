using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
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
                return Ok(Service.GetFormaPagosDisponibles(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  GetFiltroFormaPagosDisponibles() controller ");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al obtener los listros de forma de pagos" });
            }
        }

        // POST: gestionPagos/BuscarComisionCarnetFormaPago
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
        // POST: gestionPagos/BuscarComisionCarnetFormaPago
        [HttpPost]
        public ActionResult PagarComisionSionPay([FromBody] PagarSionPayInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.UsuarioLogin} inicio el controller BuscarComisionNombre() parametro: idciclo:{param.idCiclo}");             
                return Ok(Service.PagarSionPayComisionTodo(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.UsuarioLogin} error catch  BuscarComisionNombre() controller ");        
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" });
            }
        }
        // POST: gestionPagos/VerificarPagosSionPayFormaPago 
        [HttpPost]
        public ActionResult VerificarPagosSionPayFormaPagoCiclo([FromBody] VerificarPagoSionPayInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller BuscarComisionNombre() parametro: idciclo:{param.idCiclo} ");
                return Ok(Service.VerificarPagoSionPayCiclo(param));
            } catch (Exception ex)
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  verificarPagosSionPayFormaPagoCiclo() controller {ex.Message}");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al verificar los pagos por sion pay" });
            }
        }

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
            catch (Exception e)
            {
                Logger.LogError($"usuario :  error catch  handleDownloadFileEmpresas() controller {e}");
                //var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las empresas." });
            }
        }
        // POST: gestionPagos/handleConfirmarPagosTransferencias
        [HttpPost]
        public ActionResult handleConfirmarPagosTransferencias([FromBody] ConfirmarPagosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.user} inicio el controlador handleConfirmarPagosTransferencias() parametro: idciclo:{param.cicloId}");
                return Ok(Service.handleConfirmarPagosTransferencias(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.user} error catch  handleConfirmarPagosTransferencias() controller ");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" });
            }
        }
        // POST: gestionPagos/handleRechazadosPagosTransferencias
        [HttpPost]
        public ActionResult handleRechazadosPagosTransferencias([FromBody] ConfirmarPagosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.user} inicio el controlador handleRechazadosPagosTransferencias() parametro: idciclo:{param.cicloId}");
                return Ok(Service.handleRechazadosPagosTransferencias(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.user} error catch  handleConfirmarPagosTransferencias() controller ");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" });
            }
        }
        [HttpPost]
        public ActionResult handleObtenerPagosTransferencias([FromBody] DownloadFileTransferenciaInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.user} inicio el controlador handleObtenerPagosTransferencias() parametro: idciclo:{param.cicloId}");
                return Ok(Service.handleObtenerPagosTransferencias(param));
            }
            catch
            {
                Logger.LogError($"usuario : {param.user} error catch  handleObtenerPagosTransferencias() controller ");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" });
            }
        }
        [HttpPost]
        public ActionResult handleConfirmarPagosTransferenciasTodos([FromBody] DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el controller handleConfirmarTodos() parametro: cicloId: {body.cicloId}");
                return Ok(Service.handleConfirmarPagosTransferenciasTodos(body));
            }
            catch
            {
                Logger.LogError($"usuario : {body.user} error catch  handleConfirmarTodos() controller ");
                //var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las empresas." });
            }
        }

        [HttpPost]
        public ActionResult handleVerificarPagosTransferenciasTodos([FromBody] DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el controller handleConfirmarTodos() parametro: cicloId: {body.cicloId}");
                return Ok(Service.handleVerificarPagosTransferenciasTodos(body));
            }
            catch
            {
                Logger.LogError($"usuario : {body.user} error catch  handleConfirmarTodos() controller ");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las empresas." });
            }
        }
        // POST: gestionPagos/FiltrarComisionPagoPorTipoPago
        [HttpPost]
        public ActionResult FiltrarComisionPagoPorTipoPago([FromBody] FiltroComisionTipoPagoInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller BuscarComisionNombre() parametro: idciclo:{param.idCiclo}, criterioidtipo busqueda busqueda: {param.idTipoPago}");
                return Ok(Service.FiltrarComisionesPorTipoPago(param));
            } catch (Exception ex){
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  BuscarComisionNombre() controller mensaje:  {ex.Message}");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones por filtro tipo de pago" });
            }
        }
    }
}
