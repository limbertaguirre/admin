using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
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
	public class PagosController : Controller
	{
		private readonly ILogger<PagosController> Logger;
		public PagosController(ILogger<PagosController> logger, IFormaPagoService service )
		{
			Logger = logger;
			Service = service;
		}
		public IFormaPagoService Service { get; set; }

		// GET: PagosController
		public ActionResult Index()
		{
			return View();
		}

		// GET: PagosController/Create
		public ActionResult Create()
		{
			return View();
		}

		// GET: Pagos/GetCiclos
		public ActionResult GetCiclos([FromHeader] string usuarioLogin)
		{
			try
			{
				Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerCiclos()  ");
				var ciclos = Service.GetCiclos(usuarioLogin);
				Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerCiclos()  ");
				return Ok(ciclos);
			}

			catch
			{
				Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerCiclos() controller ");
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
				return Ok(Result);
			}
		}

		// POST: Pagos/ObtenerAplicaciones
		[HttpPost]
		public ActionResult ObtenerAplicaciones([FromBody] ComisionesInputModel param)
		{
			try
			{
				Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controller AplicacionesController => Index() parametro: idciclo:{param.idCiclo}");
				var resulcliente = Service.GetFormasPagosPendientes(param.usuarioLogin, param.idCiclo);
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

        public ActionResult GetListarFormaPagos([FromBody] ParamFormaPagosOutputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller GetListarFormaPagos()  ");
                var ciclos = Service.ListarFormasPagos(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller GetListarFormaPagos()  ");
                return Ok(ciclos);
            }

            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  GetListarFormaPagos() controller ");
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
		// POST: Pagos/BuscarComisionCarnetFormaPago
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
		// GET: Pagos/GetFormaPagosDisponibles
		public ActionResult GetFormaPagosDisponibles([FromBody] FormaPagosDisponiblesInputModel param)
		{
			try
			{
				Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller getFormaPagosDisponibles()  ");
				var ciclos = Service.GetFormaPagosDisponibles(param);
				Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller getFormaPagosDisponibles()  ");
				return Ok(ciclos);
			}
			catch
			{
				Logger.LogError($"usuario : {param.usuarioLogin} error catch  getFormaPagosDisponibles() controller ");
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener los tipos de pagos" };
				return Ok(Result);
			}
		}

		// POST: Pagos/BuscarComisionCarnetFormaPago
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
				var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones pendientes" };
				return Ok(Result);
			}
		}





	}
}
