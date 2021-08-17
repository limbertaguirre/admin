using AutoMapper;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.MultinivelModel;
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
    /*[Route("api/aplicaciones")]
    [ApiController]*/
    public class AplicacionesController: Controller
    {
        private readonly ILogger<AplicacionesController> Logger;

        //private readonly IMapper _mapper;

        public AplicacionesController(ILogger<AplicacionesController> logger, IAplicacionesService service)
        {
            Logger = logger;
            Service = service;
            //_mapper = mapper;
        }

        public IAplicacionesService Service { get; set; }

        // GET: AplicacionesController/ObtenerCiclos
        public ActionResult<CicloDto> GetCiclos([FromHeader] string usuarioLogin)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuarioLogin} inicio el controller obtenerCiclos()  ");
                var ciclos = Service.GetCiclos(usuarioLogin);
                Logger.LogInformation($"usuario : {usuarioLogin} Fin del controller obtenerCiclos()  ");
                //return Ok(_mapper.Map<IReadOnlyList<VwObtenerCiclo>, IReadOnlyList<CicloDto>>((IReadOnlyList<VwObtenerCiclo>)ciclos));
                return Ok(ciclos);
            }
            catch
            {
                Logger.LogError($"usuario : {usuarioLogin} error catch  obtenerCiclos() controller ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener las bajas" };
                return Ok(Result);
            }
        }

        // POST: Aplicaciones/ObtenerAplicaciones

        [HttpPost]
        public ActionResult ObtenerAplicaciones([FromBody] ComisionesInputModel param)
        {
                try
                {               
                    Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controller AplicacionesController => Index() parametro: idciclo:{param.idCiclo}");
                    var resulcliente = Service.GetAplicacionesPendientes(param.usuarioLogin, param.idCiclo);
                    Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller AplicacionesController => Index()");
                    return Ok(resulcliente);
                }
                catch
                {
                    Logger.LogError($"usuario request: {param.usuarioLogin} error catch controller AplicacionesController()  => Index() ");
                    var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar las aplicaciones pendientes" };
                    return Ok(Result);
                }
        }
        //post Aplicaciones/ListarDetalleAplicacionesXFreelancer
        [HttpPost]
        public ActionResult ListarDetalleAplicacionesXFreelancer([FromBody] DetalleAplicacionesFichaInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controller ListarDetalleAplicacionesXFreelancer => Index() parametro: idComisionDetalle:{param.idComisionDetalle}");
                var resulcliente = Service.obtenerDetalleAplicacionesXFreelancers(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller ListarDetalleAplicacionesXFreelancer => Index()");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario request: {param.usuarioLogin} error catch controller ListarDetalleAplicacionesXFreelancer()  => Index() ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al listar ListarDetalleAplicacionesXFreelancers" };
                return Ok(Result);
            }
        }
        //post Aplicaciones/ObtenerProyectoPorProducto
        [HttpPost]
        public ActionResult ObtenerProyectoPorProducto([FromBody] GetProyectoImputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario request : {param.usuarioLogin} inicio el controller ObtenerProyectoPorProducto => Index() parametro: idciclo:{param.producto}");
                var resulcliente = Service.obtenerProyectoXproduto(param);
                Logger.LogInformation($"usuario : {param.usuarioLogin} Fin del controller ObtenerProyectoPorProducto => Index()");
                return Ok(resulcliente);
            }
            catch
            {
                Logger.LogError($"usuario request: {param.usuarioLogin} error catch controller ObtenerProyectoPorProducto()  => Index() ");
                var Result = new GenericDataJson<string> { Code = 1, Message = "Error al obtener el proyecto por producto" };
                return Ok(Result);
            }
        }

    }
}
