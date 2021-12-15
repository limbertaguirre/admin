using System;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Controllers
{
    public class FormasPagosRezagadosController : Controller
	{
		private readonly ILogger<FormasPagosRezagadosController> Logger;
		public FormasPagosRezagadosController(ILogger<FormasPagosRezagadosController> logger, IFormasPagosRezagadosService service)
		{
			Logger = logger;
			Service = service;
		}
		public IFormasPagosRezagadosService Service { get; set; }

		// GET: Pagos/GetCiclos
		public ActionResult GetCiclos([FromHeader] string usuarioLogin)
		{
			try
			{
				Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller GetCiclos()  ");
				var ciclos = Service.GetCiclos(usuarioLogin);
				Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller GetCiclos()  ");
				return Ok(ciclos);
			}

			catch
			{
				Logger.LogError($"usuario : {usuarioLogin} error catch  GetCiclos() controller ");
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
				return Ok(Result);
			}
		}

		// POST: Pagos/ObtenerFormasPagos
		[HttpPost]
		public ActionResult GetComisionesRezagados([FromBody] ComisionesPagosInput param)
		{
			try
			{
				Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controller AplicacionesController => Index() parametro: idciclo:{param.idCiclo}");
				var resulcliente = Service.GetComisionesRezagados(param);
				Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller AplicacionesController => Index()");
				return Ok(resulcliente);
			}
			catch
			{
				Logger.LogError($"usuario request: {param.usuarioLogin} error catch controller forma pagos AplicacionesController()  => Index() ");
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las pendiente para forma de pagos." };
				return Ok(Result);
			}
		}

		[HttpPost]
		public ActionResult GetListarFormaPagos([FromBody] ParamFormaPagosOutputModel param)
		{
			try
			{
				Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller GetListarFormaPagos()  ");
				var ciclos = Service.GetListarFormaPagos(param);
				Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller GetListarFormaPagos()  ");
				return Ok(ciclos);
			}
			catch (Exception e)
			{
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  GetListarFormaPagos() mensaje: {e.Message}");
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  GetListarFormaPagos() stacktrace: {e.StackTrace}");
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener los tipos de pagos" };
				return Ok(Result);
			}
		}

		[HttpPost]
		public ActionResult aplicarMetodoPagoComision([FromBody] AplicarMetodoOutput param)
		{
			try
			{
				Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller aplicarMetodoPagoComision()  ");
				var ciclos = Service.AplicarMetodoPago(param);
				Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller aplicarMetodoPagoComision()  ");
				return Ok(ciclos);
			}

			catch
			{
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  aplicarMetodoPagoComision() controller ");
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al aplicar un tipo de pago" };
				return Ok(Result);
			}
		}
		
		[HttpPost]
		public ActionResult VerificarCierreFormaPago([FromBody] VerificarCierreFormaPagoParam param)
		{
			try
			{
				Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller VerificarCierreFormaPago() parametro: idciclo:{param.idCiclo}");
				return Ok(Service.VerificarCierreFormaPago(param));
			}
			catch
			{
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  VerificarCierreFormaPago() controller ");
				return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al autorizr el pago" });
			}
		}

		[HttpPost]
		public ActionResult CerrarFormaDePago([FromBody] CierreformaPagoInput param)
		{
			try
			{
				Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller VerificarCierreFormaPago() parametro: idciclo:{param.idCiclo}");
				return Ok(Service.CerrarFormaDePago(param));

			}
			catch
			{
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  VerificarCierreFormaPago() controller ");
				return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al autorizr el pago" });
			}
		}

		[HttpPost]
		public ActionResult VerificarAutorizadorPorComision([FromBody] AutorizacionVerificarParam param)
		{
			try
			{
				Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller VerificarAutorizadorPorComision() parametro: idciclo:{param.idCiclo}");
				return Ok(Service.VerificarAutorizadorPorComision(param));
			}
			catch
			{
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  VerificarAutorizadorPorComision() controller ");
				return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al verificar el autorizador de pagos" });
			}
		}

		public ActionResult ConfirmarAutorizacion([FromBody] ConfirmarAutorizacionParam param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller ConfirmarAutorizacion() parametro: idCiclo: {param.idCiclo}, idComision: {param.idComision}");
                return Ok(Service.ConfirmarAutorizacionPagos(param));

            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  ConfirmarAutorizacion() controller ");
                return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al autorizr el pago" });
            }
        }

		[HttpPost]
		public ActionResult BuscarComisionCarnetFormaPago([FromBody] BuscarInputModel param)
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

		[HttpPost]
		public ActionResult FiltrarComisionPagoPorTipoPago([FromBody] FiltroComisionTipoPagoInputModel param)
		{
			try
			{
				Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller BuscarComisionNombre() parametro: idciclo:{param.idCiclo}, criterioidtipo busqueda busqueda: {param.idTipoPago}");
				return Ok(Service.FiltrarComisionesPorTipoPago(param));
			}
			catch
			{
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  BuscarComisionNombre() controller ");
				return Ok(new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones por tipo de pago" });
			}
		}

		[HttpPost]
		public ActionResult ObtenerFormasPagos([FromBody] ComisionesPagosInput param)
		{
			try
			{
				Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controller AplicacionesController => Index() parametro: idciclo:{param.idCiclo}");
				var resulcliente = Service.GetFormasPagosPendientes(param);
				Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller AplicacionesController => Index()");
				return Ok(resulcliente);
			}
			catch
			{
				Logger.LogError($"usuario request: {param.usuarioLogin} error catch controller forma pagos AplicacionesController()  => Index() ");
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las pendiente para forma de pagos." };
				return Ok(Result);
			}
		}
	}
}
