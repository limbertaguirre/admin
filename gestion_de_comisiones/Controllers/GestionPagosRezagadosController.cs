using System;
using System.Security.Claims;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Controllers
{
    public class GestionPagosRezagadosController : Controller
    {
        private readonly ILogger<GestionPagosRezagadosController> Logger;
        public GestionPagosRezagadosController(ILogger<GestionPagosRezagadosController> logger, IGestionPagosRezagadosService service)
        {
            Logger = logger;
            Service = service;
        }
        public IGestionPagosRezagadosService Service { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetCiclos([FromHeader] string usuarioLogin)
        {
            try
            {
                var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
                Logger.LogInformation($"usuario: {usuarioLogin} inicio controller GestionPagosRezagadosController - GetCiclos()  ");
                return Ok(Service.GetCiclos(usuarioLogin));
            }
            catch (Exception e)
            {
                Logger.LogError($"usuario: {usuarioLogin} error catch GestionPagosRezagadosController - GetCiclos() controller mensaje: { e.Message }");
                Logger.LogError($"usuario: {usuarioLogin} error catch GestionPagosRezagadosController - GetCiclos() controller StackTrace: { e.StackTrace }");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Hubo un inconveniente al obtener los ciclos de los rezagados." });
            }
        }
        //POST: gestionPagos/GetComisionesPagos
        [HttpPost]
        public ActionResult GetComisionesPagos([FromBody] ComisionesPagosInput param)
        {
            try
            {
                Logger.LogInformation($"Usuario: {param.usuarioLogin} inicio el controller GestionPagosRezagadosController - GetComisionesPagos parametro: idciclo:{param.idCiclo}");
                var resulcliente = Service.GetComisionesDePagos(param);
                Logger.LogInformation($"Usuario: {param.usuarioLogin} Fin del controller GestionPagosRezagadosController - GetComisionesPagos");
                return Ok(resulcliente);
            }
            catch (Exception e)
            {
                Logger.LogError($"usuario: {param.usuarioLogin} error catch GestionPagosRezagadosController - GetComisionesPagos() controller mensaje: { e.Message }");
                Logger.LogError($"usuario: {param.usuarioLogin} error catch GestionPagosRezagadosController - GetComisionesPagos() controller StackTrace: { e.StackTrace }");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Hubo un inconveniente al listar a los rezagados del ciclo seleccionado." });
            }
        }
        /*
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
            }
            catch (Exception ex)
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
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  BuscarComisionNombre() controller mensaje:  {ex.Message}");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones por filtro tipo de pago" });
            }
        }*/
    }
}
