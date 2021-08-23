using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class ProrrateadoController : ControllerBase
    {
        private readonly ILogger<ProrrateadoController> Logger;

        public ProrrateadoController(ILogger<ProrrateadoController> logger, IProrrateadoService service)
        {
            Logger = logger;
            Service = service;
        }

        public IProrrateadoService Service { get; }

        public ActionResult GetCiclos([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerCiclos prorrateo()  ");
                var ciclos = Service.GetCiclos(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerCiclos()  ");                
                return Ok(ciclos);
            }
            catch
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerCiclosprorrateo() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
                return Ok(Result);
            }
        }
        [HttpPost]
        public ActionResult ObtenerAplicacionesPendintes([FromBody] ComisionesInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controllerObtenerAplicacionesPendintesController => Index() parametro: idciclo:{param.idCiclo}");
                var resulcliente = Service.GetComisionesPendienteAplicaciones(param.usuarioLogin, param.idCiclo);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller AplicacionesController => Index()");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario request: {param.usuarioLogin} error catch controller ObtenerAplicacionesPendintesController()  => Index() ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las aplicaciones pendientes" };
                return Ok(Result);
            }
        }
        public ActionResult BuscarComisionPendientesAplicacionXCarnet([FromBody] BuscarInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el controller BuscarComisionCerradosXCarnet() parametro: idciclo:{param.idCiclo}, criterio busqueda: {param.nombreCriterio}");
                var resulcliente = Service.ListarComisionesAplicacionesPendientesPorCarnet(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller BuscarComisionCerradosXCarnet()  ");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch  BuscarComisionCerradosXCarnet() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las comisiones por CI" };
                return Ok(Result);
            }
        }


    }
}
