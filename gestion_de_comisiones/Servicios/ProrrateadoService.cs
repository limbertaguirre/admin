using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class ProrrateadoService :IProrrateadoService
    {

        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<ProrrateadoService> Logger;
        private IProrrateadoRepository Repository { get;  }
        private IAplicacionesRepository AplicacionRepository { get;  }

        public ProrrateadoService(ILogger<ProrrateadoService> logger, IProrrateadoRepository repository, IAplicacionesRepository aplicacionRepository)
        {
            this.Logger = logger;
            this.Repository = repository;
            this.AplicacionRepository = aplicacionRepository;
        }
       

        public object GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio prorrateo service => getCiclos()");
                int idEstadoPendienteAplicacion = 4; //parametro
                List<CicloDto> ciclos = (List<CicloDto>)Repository.GetCiclos(usuario, idEstadoPendienteAplicacion);
                if (ciclos.Count > 0)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                } else {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "No hay ciclos  pendientes a prorrateo. ", ciclos);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch GetCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetComisionesPendienteAplicaciones(string usuario, int idCiclo)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio AplicacionesService => GetComisionesPendienteAplicaciones() ");
                int idEstadoComisionPendienteAplicacion = 4; //VARIABLE estado Aplicacion Pendiente
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                var ciclos = Repository.GetComisionesPendienteAplicaciones(usuario, idCiclo, idEstadoComisionPendienteAplicacion, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura);
                return Respuesta.ReturnResultdo(0, "ok", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch AplicacionesService => GetComisionesPendienteAplicaciones() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de las aplicaciones pendientes", "problemas en el servidor, intente mas tarde");
            }
        }
        public object ListarComisionesAplicacionesPendientesPorCarnet(BuscarInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio AplicacionesService => ListarComisionesAplicacionesPendientesPorCarnet() ");
                int idEstadoComisionPendienteAplicacion = 4; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                var ciclos = AplicacionRepository.GetComisionesPorCarnet(param.usuarioLogin, param.idCiclo, idEstadoComisionPendienteAplicacion, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, param.nombreCriterio);
                return Respuesta.ReturnResultdo(0, "ok", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch AplicacionesService => ListarComisionesAplicacionesPendientesPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de las aplicaciones pendiente por carnet", "problemas en el servidor, intente mas tarde");
            }
        }



    }
}
