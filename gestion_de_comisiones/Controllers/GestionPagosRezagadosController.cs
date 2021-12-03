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
        public ActionResult handleVerificarPagosTransferenciasTodos([FromBody] ObtenerRezagadosPagosTransferenciasInput body)
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
    }
}
