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
        public IncentivoSionPayController(ILogger<IncentivoSionPayController> logger , IIncentivoSionPayService service)
        {
            Logger = logger;
            Service = service;
        }
        public IIncentivoSionPayService Service { get; set; }
        // POST: Incentivo/CargarPlanillaExcel
        [HttpPost]
        public ActionResult CargarPlanillaExcel([FromBody] PlanillaExcelInput param)
        {
            try
            {
                Logger.LogInformation($"usuario request : {param.UsuarioNombre} inicio el controller AplicacionesController => Index() parametro: idciclo:{param.IdCiclo}");
                var resulcliente = Service.CargarDatosPlanillaExcel(param);
                //Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller AplicacionesController => Index()");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario request: {param.UsuarioNombre} error catch controller  IncentivoController()  => CargarPlanillaExcel() ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al cargar plantillas." };
                return Ok(Result);
            }
        }
    }
}
