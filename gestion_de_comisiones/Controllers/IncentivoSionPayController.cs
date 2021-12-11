using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.Incentivo;
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
        public IncentivoSionPayController(ILogger<IncentivoSionPayController> logger , IIncentivoSionPayService service, IFacturaService facturaService)
        {
            Logger = logger;
            Service = service;
            FService = facturaService; 
        }
        public IIncentivoSionPayService Service { get; set; }
        public IFacturaService FService { get; set; }
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
                Logger.LogError($"usuario request: {planillaIncentivo.UsuarioNombre} error catch controller  IncentivoController()  => CargarPlanillaExcel() Error: {ex.Message} ");
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
            catch
            {
                Logger.LogError($"usuario request: {usuarioLogin} Error catch controller  ObtenerCiclo() ");
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
                Logger.LogError($"usuario request: {usuarioLogin} error catch controller  IncentivoController()  => CargarPlanillaExcel() ");
                var result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(result);
            }
        }
    }
}
