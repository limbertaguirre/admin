using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.Incentivo;
using gestion_de_comisiones.Modelos.IncentivoSionPay;
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
    public class IncentivoSionPayController : Controller
    {
        private readonly ILogger<IncentivoSionPayController> Logger;
        public IncentivoSionPayController(ILogger<IncentivoSionPayController> logger , IIncentivoSionPayService service)
        {
            Logger = logger;
            Service = service;           
        }
        public IIncentivoSionPayService Service { get; set; }        
        // POST: IncentivoSionPay/CargarPlanillaExcel
        [HttpPost]
        public ActionResult CargarPlanillaExcel([FromBody] PlanillaPagoIncentivo planillaIncentivo)
        {
            try
            {
                Logger.LogInformation($"usuario request : {planillaIncentivo.UsuarioNombre} inicio el controller AplicacionesController => Index() parametro: idciclo:{planillaIncentivo.IdCiclo}");
                var resulcliente = Service.CargarDatosPlanillaExcel(planillaIncentivo);
                Logger.LogInformation($"usuario : {planillaIncentivo.UsuarioNombre} Fin del controller AplicacionesController => Index()");
                return Ok(resulcliente);
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario request: {planillaIncentivo.UsuarioNombre} Error catch IncentivoSionPayController , CargarPlanillaExcel() Error: {ex.Message} ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(Result);
            }
        }
        // GET: IncentivoSionPay/ObtenerCiclos       
        public ActionResult ObtenerCiclos([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} Inicio el controller obtenerCiclos()  ");
                var ciclos = Service.ObtenerCiclos(usuarioLogin);               
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerCiclos()  ");
                return Ok(ciclos);
            }
            catch(Exception ex)
            {
                Logger.LogError($"usuario request: {usuarioLogin} Error catch IncentivoSionPayController , ObtenerCiclo() Error: {ex.Message}");
                var result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(result);
            }
        }
        // GET: IncentivoSionPay/ObtenerTipoIncentivo
        public ActionResult ObtenerTipoIncentivo([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} Inicio el controller obtenerTipoIncentivo()");
                var listaTipoIncentivo= Service.ObtenerTipoIncentivo(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerCiclos()");
                return Ok(listaTipoIncentivo);
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario request: {usuarioLogin} Error catch IncentivoSionPayController , ObtenerTipoIncentivo() Error: {ex.Message} ");
                var result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(result);
            }
        }
        // GET: IncentivoSionPay/ObtenerTipoPagos
        public ActionResult ObtenerTipoPagos([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} Inicio el controller ObtenerTipoPagos()");
                var listaTipoPagos = Service.ObtenerTipoPagos(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller ObtenerTipoPagos()");
                return Ok(listaTipoPagos);
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario request: {usuarioLogin} Error catch IncentivoSionPayController , ObtenerTipoPagos() Error: {ex.Message} ");
                var result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(result);
            }
        }
        // GET: IncentivoSionPay/ObtenerTipoIncentivosSegunCicloMensual
        public ActionResult ObtenerTipoIncentivosSegunCicloMensual([FromHeader] string usuarioLogin, int nroCicloMensual)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} Inicio el controller ObtenerTipoPagos()");
                var listaTipoIncentivos = Service.ObtenerTipoIncentivosPagosSegunCiclo(nroCicloMensual, usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller ObtenerTipoPagos()");
                return Ok(listaTipoIncentivos);
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario request: {usuarioLogin} Error catch IncentivoSionPayController , ObtenerTipoIncentivosSegunCicloMensual() Error: {ex.Message} ");
                var result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(result);
            }        
        }
        // POST: IncentivoSionPay/RegistroTipoIncentivoPago
        [HttpPost]
        public ActionResult RegistroTipoIncentivoPago([FromBody] TipoIncentivoPago tipoIncentivoPago)
        {
            try
            {
                Logger.LogInformation($"usuario request : {tipoIncentivoPago.Usuario} Inicio el controller AplicacionesController ");
                var resultTipoIncentivoPago = Service.RegistrarTipoIncentivoPago(tipoIncentivoPago,tipoIncentivoPago.Usuario);
                Logger.LogInformation($"usuario : {tipoIncentivoPago.Usuario} Fin del controller AplicacionesController => Index()");
                return Ok(resultTipoIncentivoPago);
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario request: {tipoIncentivoPago.Usuario} Error catch IncentivoSionPayController , RegistroTipoIncentivoPago() Error: {ex.Message} ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(Result);
            }
        }
    }
}
