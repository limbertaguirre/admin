using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
                var resulcliente = Service.BuscarComisiones(param.usuarioLogin, param.idCiclo,param.nombreCriterio);
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
        // POST: FacturaController/obtenerCDetalleEmpresa
        [HttpPost]
        public ActionResult obtenerCDetalleEmpresa([FromBody] DetalleEmpresaInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller obtenerCDetalleEmpresa() parametro: ");
                var resulcliente = Service.obtenerDetalleMasEmpresas(param.usuarioLogin, param.idComisionDetalleEmpresa);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller obtenerCDetalleEmpresa()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  obtenerCDetalleEmpresa() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones detalle empresa" };
                return Ok(Result);
            }
        }
        // POST: FacturaController/FacturarComisionDetalle
        [HttpPost]
        public ActionResult FacturarComisionDetalle([FromBody] ComisionDetalleInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller FacturarComisionDetalle() parametro: ");
                var updateComisionDetalle = Service.ACtualizarComisionDetalleAFacturado(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller FacturarComisionDetalle()  ");
                return Ok(updateComisionDetalle);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  FacturarComisionDetalle() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al facturar el detalle comision" };
                return Ok(Result);
            }
        }
        // POST: FacturaController/ActualizarDetalleEmpresaEstado
        [HttpPost]
        public ActionResult ActualizarDetalleEmpresaEstado([FromBody] UpdateDetalleEmpresaInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller ActualizarDetalleEmpresaEstado() parametro: ");
                var updateComisionDetalle = Service.ActualizarDetalleEmpresaEstado(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller ActualizarDetalleEmpresaEstado()  ");
                return Ok(updateComisionDetalle);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  ActualizarDetalleEmpresaEstado() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al actualizar  elestado  detalle comision empresa" };
                return Ok(Result);
            }
        }
        // POST: FacturaController/SubirArchivoFacturaPdfEmpresa
        [HttpPost]
        public ActionResult SubirArchivoFacturaPdfEmpresa([FromBody] SubirArchivoInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller SubirArchivoFacturaPdfEmpresa() parametro: ");
                var updateComisionDetalle = Service.subirArchivoPdf(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller SubirArchivoFacturaPdfEmpresa()  ");
                return Ok(updateComisionDetalle);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  SubirArchivoFacturaPdfEmpresa() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al actualizar  elestado  detalle comision empresa" };
                return Ok(Result);
            }
        }
        // POST: FacturaController/AplicarFacturaTodoEstado
        [HttpPost]
        public ActionResult AplicarFacturaTodoEstado([FromBody] FacturadoTodoInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller AplicarFacturaTodoEstado() parametro: ");
                var updateComisionDetalle = Service.AplicarFacturadoTodo(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller AplicarFacturaTodoEstado()  ");
                return Ok(updateComisionDetalle);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  AplicarFacturaTodoEstado() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al actualizar  elestado  detalle comision empresa" };
                return Ok(Result);
            }
        }
        // POST: FacturaController/CerrarFactura
        [HttpPost]
        public ActionResult CerrarFactura([FromBody] CerrarFacturaInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller CerrarFactura() parametro: {JsonConvert.SerializeObject(param)}");
               var updateComisionDetalle = Service.CerrarFactura(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller CerrarFactura()  ");
               // var Result = new GenericDataJson<string> { Code = 0, Message = "Se cerró la factura con éxito" };
                return Ok(updateComisionDetalle);
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch controller  CerrarFactura() controller mensaje:{ex.Message} ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error  al cerrar factura" };
                return Ok(Result);
            }
        }


    }
}
