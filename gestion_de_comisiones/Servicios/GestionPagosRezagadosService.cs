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
                int idEstadoComisionRezagados = 11; //rametro
                int idTipoComisionRezagados = 2; //parametro
                var ciclos = Repository.GetCiclos(usuario, idEstadoComisionRezagados, idTipoComisionRezagados);
                if (true)
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
                var comisiones = Repository.GetComisionesPagos(param.usuarioLogin, param.idCiclo, idEstadoPagosRezagados, idTipoComisionRezagadosComision, param.idComision);
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", comisiones);

            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {param.usuarioLogin} error catch GestionPagosRezagadosService - GetComisionesDePagos error mensaje: {ex.Message}");
                Logger.LogError($"usuario : {param.usuarioLogin} error catch GestionPagosRezagadosService - GetComisionesDePagos error StackTrace: {ex.StackTrace}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtenerlas comisiones de pagos", "problemas en el servidor, intente mas tarde");
            }
        }

        public object handleTransferenciasEmpresas(ComisionesPagosInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");
                var empresas = Repository.handleTransferenciasEmpresas(param);
                if (empresas.Count > 0)
                {
                    return Respuesta.ReturnResultdo(0, "ok", empresas);
                }
                else
                {
                    return Respuesta.ReturnResultdo(1, "No tiene empresas asignadas para transferir.", "");
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarComisionesFormaPagoPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object handleVerificarPagosTransferenciasTodos(ObtenerRezagadosPagosTransferenciasInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el servicio handleConfirmarPagosTransferenciasTodos() ");
                GestionPagosRezagadosEvent @event = Repository.handleVerificarPagosTransferenciasTodos(body);
                Logger.LogInformation($"handleConfirmarPagosTransferenciasTodos() response @event {@event}");
                if (@event.eventType == GestionPagosRezagadosEvent.EXISTEN_PENDIENTES)
                {
                    return Respuesta.ReturnResultdo(2, @event.message, @event.dataVerify);
                }
                else if (@event.eventType == GestionPagosRezagadosEvent.EXISTEN_RECHAZADOS)
                {
                    return Respuesta.ReturnResultdo(3, @event.message, @event.dataVerify);
                }
                else if (@event.eventType == GestionPagosRezagadosEvent.NO_EXISTEN_PENDIENTES_NI_RECHAZADOS)
                {
                    return Respuesta.ReturnResultdo(0, @event.message, @event.dataVerify);
                }
                else
                {
                    throw new Exception(@event.message);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {body.user} error catch handleVerificarPagosTransferenciasTodos() al obtener lista de ciclos ,error mensaje: {ex}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
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
                GestionPagosRezagadosEvent r = Repository.ConfirmarPagosRezagadosTransferencias(param);
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

        public object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el servicio handleDownloadFileEmpresas() ");
                GestionPagosRezagadosEvent r = (GestionPagosRezagadosEvent) Repository.handleDownloadFileEmpresas(body);
                if (r.eventType == GestionPagosRezagadosEvent.ERROR || r.eventType == GestionPagosEvent.ROLLBACK_ERROR)
                {
                    throw new Exception(r.message);
                }
                return Respuesta.ReturnResultdo(0, "ok", r.file);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {body.user} error catch handleDownloadFileEmpresas() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, ex.Message, "problemas en el servidor, intente mas tarde");
            }
        }
        public object BuscarFreelancerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.user} inicio el servicio BuscarFreelancerPagosRezagadosTransferencias() ");
                var file = Repository.BuscarFreelancerPagosRezagadosTransferencias(param);
                if (file == null)
                {
                    return Respuesta.ReturnResultdo(0, "No se encontraron resultados", file);
                }
                else
                {
                    return Respuesta.ReturnResultdo(0, "Resultado encontrado", file);
                }

            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.user} error catch BuscarFreelancerPagosRezagadosTransferencias()  {ex.Message}");
                return Respuesta.ReturnResultdo(1, "Error de conexion", "Ocurrió algo inesperado con la comunicación con el servidor.");
            }
        }

    }
}
