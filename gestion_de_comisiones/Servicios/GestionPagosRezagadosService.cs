using System;
using System.Collections.Generic;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Servicios
{
    public class GestionPagosRezagadosService : IGestionPagosRezagadosService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<GestionPagosRezagadosService> Logger;

        public GestionPagosRezagadosService(ILogger<GestionPagosRezagadosService> logger, IGestionPagosRezagadosRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }
        public IGestionPagosRezagadosRepository Repository { get; set; }

        public object GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio GestionPagosRezagadosService => getCiclos()");
                int idEstadoCerradoformaPago = 11; //rametro
                int idTipoComisionPagoComision = 2; //parametro
                List<CicloDto> ciclos = (List<CicloDto>)Repository.GetCiclos(usuario, idEstadoCerradoformaPago, idTipoComisionPagoComision);
                if (ciclos.Count > 0)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                }
                else
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "No hay ciclos disponibles para la de pagos.", ciclos);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {usuario} error catch obtenerlistCiclos() al obtener para pagos,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de pagos", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetComisionesDePagos(ComisionesPagosInput param)
        {
            try
            {

                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio GestionPagosRezagadosService => GetComisionesDePagos()");
                int idEstadoPagosRezagados = 11;
                int idTipoComisionRezagadosComision = 2;
                var comisiones = Repository.GetComisionesPagos(param.usuarioLogin, param.idCiclo, idEstadoPagosRezagados, idTipoComisionRezagadosComision);
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", comisiones);

            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch GestionPagosRezagadosService - GetComisionesDePagos error mensaje: {ex.Message}");
                Logger.LogError($"usuario : {param.usuarioLogin} error catch GestionPagosRezagadosService - GetComisionesDePagos error StackTrace: {ex.StackTrace}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtenerlas comisiones de pagos", "problemas en el servidor, intente mas tarde");
            }
        }

        public object ObtenerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.user} inicio el servicio ObtenerPagosRezagadosTransferencias() ");
                var folder = Repository.ObtenerPagosRezagadosTransferencias(param);
                return Respuesta.ReturnResultdo(0, "ok", folder);
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {param.user} error catch ObtenerPagosRezagadosTransferencias() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de rezagados", "problemas en el servidor, intente mas tarde");
            }
        }
        public object ConfirmarPagosRezagadosTransferencias(ConfirmarPagosRezagadosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($"GestionPagosRezagadosService - usuario : {param.user} inicio el servicio ConfirmarPagosRezagadosTransferencias() body: {param}");
                GestionPagosEvent r = Repository.ConfirmarPagosRezagadosTransferencias(param);
                Logger.LogInformation($"GestionPagosRezagadosService Repuesta del repository ConfirmarPagosRezagadosTransferencias() eventType: {r.eventType}, message: {r.message}");
                if (r.eventType == GestionPagosEvent.ERROR || r.eventType == GestionPagosEvent.ROLLBACK_ERROR ||
                    r.eventType == GestionPagosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_SELECCIONADOS ||
                    r.eventType == GestionPagosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS ||
                    r.eventType == GestionPagosEvent.CATCH_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS ||
                    r.eventType == GestionPagosEvent.ERROR_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS)
                {
                    throw new Exception(r.message);
                }
                return Respuesta.ReturnResultdo(0, r.message, "");
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"GestionPagosRezagadosService - usuario : {param.user} CATCH ConfirmarPagosRezagadosTransferencias(), error {ex}");
                return Respuesta.ReturnResultdo(1, "Pasó algo inesperado, no se pudo registrar a los ACI rechazados.", "problemas en el servidor, intente mas tarde");
            }
        }
    }
}
