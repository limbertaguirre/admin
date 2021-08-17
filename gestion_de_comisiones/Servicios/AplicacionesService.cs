using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class AplicacionesService : IAplicacionesService
    {

        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<AplicacionesService> Logger;

        public AplicacionesService(ILogger<AplicacionesService> logger, IAplicacionesRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }
        public IAplicacionesRepository Repository { get; set; }

        public object GetAplicacionesPendientes(string usuario, int idCiclo)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio AplicacionesService => getAplicacionesPendientes() ");
                int idEstadoComisionSiFacturo = 2; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                var ciclos = Repository.GetComisiones(usuario, idCiclo, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura);
                return Respuesta.ReturnResultdo(0, "ok", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch AplicacionesService => getAplicacionesPendientes() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de las aplicaciones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio AplicacionesService => getCiclos()");
                int idEstadoCerradoFacturacion = 2; //parametro
                List<CicloDto> ciclos = (List<CicloDto>) Repository.GetCiclos(usuario, idEstadoCerradoFacturacion);
                if(ciclos.Count > 0) { 
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                } else { 
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "No hay ciclos en estado de cierre. ", ciclos);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }

        public object obtenerDetalleAplicacionesXFreelancers(DetalleAplicacionesFichaInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio obtenerListaComisionesDetalleEmpresa() ");
                var detalleAplicaicones = Repository.ListarFreelancerYAplicacionesProductos(param);
                return Respuesta.ReturnResultdo(0, "ok", detalleAplicaicones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch obtenerListaComisionesDetalleEmpresa() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener datos de detalle empresa", "");
            }
        }
        public object obtenerProyectoXproduto(GetProyectoImputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio obtenerProyectoXproduto() ");
                var detalleAplicaicones = Repository.obtenerproyectoXProducto(param);
                if(detalleAplicaicones != null)
                {
                    return Respuesta.ReturnResultdo(0, "ok", detalleAplicaicones);
                }
                else
                {
                    return Respuesta.ReturnResultdo(1, "No existe producto", detalleAplicaicones);
                }               
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch obtenerProyectoXproduto() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener el proyecto", "");
            }
        }



    }
}
