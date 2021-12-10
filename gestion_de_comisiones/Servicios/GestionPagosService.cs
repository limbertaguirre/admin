using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class GestionPagosService : IGestionPagosService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<GestionPagosService> Logger;

        public GestionPagosService(ILogger<GestionPagosService> logger, IGestionPagoRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }
        public IGestionPagoRepository Repository { get; set; }

        public object GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio FormaPagoService => getCiclos()");
                int idEstadoCerradoformaPago = 10; //rametro
                int idTipoComisionPagoComision = 1; //parametro
                List<CicloDto> ciclos = (List<CicloDto>)Repository.GetCiclos(usuario, idEstadoCerradoformaPago, idTipoComisionPagoComision);
                if (ciclos.Count > 0) {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                } else {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "No hay ciclos disponibles para la de pagos.", ciclos);
                }
            }
            catch (Exception ex) {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener para pagos,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de pagos", "problemas en el servidor, intente mas tarde");
            }
        }
      
        public object GetComisionesDePagos(ComisionesPagosInput param)
        {
            try
            {
               
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio FormaPagoService => getCiclos()");
                int idEstadoCerradoformaPago = 10; //rametro
                int idTipoComisionPagoComision = 1; //parametro
                var  comisiones = Repository.GetComisionesPagos(param.usuarioLogin, param.idCiclo, idEstadoCerradoformaPago, idTipoComisionPagoComision);                
                 return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", comisiones);
            
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch obtenerlistCiclos() al obtener para pagos,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtenerlas comisiones de pagos", "problemas en el servidor, intente mas tarde");
            }
        }
        public object GetFormaPagosDisponibles(FiltroFormaPagosInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio getFormaPagosDisponibles() ");
                int idEstadoComisionSiFacturo = 10; //VARIABLE cerrado forma de pago forma de pago
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                int idTipoComisionPagoComision = 1; //parametro
                return Respuesta.ReturnResultdo(0, "ok", Repository.GetFiltroComisionesPorFormaPago(param, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, idTipoComisionPagoComision));
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch getFormaPagosDisponibles(), error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de filtro de forma de pago", "problemas en el servidor, intente mas tarde");
            }
        }
        public object ListarComisionesFormaPagoPorCarnet(BuscarComisionInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");              
                int idEstadoComisionSiFacturo = 10; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;// estado de la tabla detalle de comision
                int idTipoComisionPagoComision = 1; //parametro
                var comisiones = Repository.GetComisionesPorCarnetListPagos(param, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, idTipoComisionPagoComision);
                           
                return Respuesta.ReturnResultdo(0, "ok", comisiones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarComisionesFormaPagoPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }
        public object PagarSionPayComisionTodo(PagarSionPayInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.UsuarioLogin} inicio el servicio FormaPagoService => getCiclos()");              
                var pay = Repository.PagarSionPayComision(param);
                if (pay)                
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "se pago con sion pay a todos", pay);               
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "No hay ciclos disponibles para la de pagos.", pay);                
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.UsuarioLogin} error catch obtenerlistCiclos() al obtener para pagos,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de pagos", "problemas en el servidor, intente mas tarde");
            }
        }
        public object VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio VerificarPagoSionPayCiclo() ");
                int idEstadoComisionSiFacturo = 10; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;// estado de la tabla detalle de comision
                int idTipoComisionPagoComision = 1; //parametro
                int idTipoFormaPagoSionPay = 1; //parametro
                RespuestaSionPayModel comisiones = Repository.VerificarPagoSionPayCiclo(param, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, idTipoComisionPagoComision, idTipoFormaPagoSionPay);
                if (comisiones.CodigoRespuesta == -1)
                 return Respuesta.ReturnResultdo(1, "problemas al verificar los pagos realizados por SION PAY", " ");
                if (comisiones.Cantidad > 0 )
                 return Respuesta.ReturnResultdo(0, "valido para pagar", comisiones);
                 return Respuesta.ReturnResultdo(1, "Ya se ha procesado los pagos SION PAY", comisiones);                              
            } catch (Exception ex) {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch VerificarPagoSionPayCiclo() el nro de  pagos en sion pay: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener al verificar los pagos realizados", "problemas en el servidor, intente mas tarde");
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
                } else { 
                    return Respuesta.ReturnResultdo(1, "No tiene empresas asignadas para transferir.", "");
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarComisionesFormaPagoPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el servicio handleDownloadFileEmpresas() ");
                GestionPagosEvent r = (GestionPagosEvent) Repository.handleDownloadFileEmpresas(body);
                if(r.eventType == GestionPagosEvent.ERROR || r.eventType == GestionPagosEvent.ROLLBACK_ERROR)
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

        public object handleConfirmarPagosTransferenciasTodos(DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el servicio handleConfirmarPagosTransferenciasTodos() ");
                var confirm = Repository.handleConfirmarPagosTransferenciasTodos(body);                
                if (confirm)
                {
                    return Respuesta.ReturnResultdo(0, "Se realizó la confirmación correctamente.", "");
                }
                else
                {
                    return Respuesta.ReturnResultdo(1, "No se pudo realizar la confirmacion de las transferencias.", "");
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {body.user} error catch handleConfirmarPagosTransferenciasTodos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object handleConfirmarPagosTransferencias(ConfirmarPagosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($"GestionPagosServices - usuario : {param.user} inicio el servicio handleConfirmarPagosTransferencias() body: {param}");
                GestionPagosEvent r = Repository.handleConfirmarPagosTransferencias(param);
                Logger.LogInformation($"GestionPagosServices Repuesta del repository handleConfirmarPagosTransferencias() eventType: {r.eventType}, message: {r.message}");
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
                Logger.LogInformation($"GestionPagosServices - usuario : {param.user} CATCH handleConfirmarPagosTransferencias(), error {ex}");
                return Respuesta.ReturnResultdo(1, "Pasó algo inesperado, no se pudo registrar a los ACI rechazados.", "problemas en el servidor, intente mas tarde");
            }
        }

        public object handleObtenerPagosTransferencias(DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el servicio handleObtenerPagosTransferencias() ");
                var file = Repository.handleObtenerPagosTransferencias(body);
                return Respuesta.ReturnResultdo(0, "ok", file);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {body.user} error catch handleObtenerPagosTransferencias() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object handleRechazadosPagosTransferencias(ConfirmarPagosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.user} inicio el servicio handleRechazadosPagosTransferencias() ");
                var file = Repository.handleRechazadosPagosTransferencias(param);
                return Respuesta.ReturnResultdo(0, "ok", file);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.user} error catch handleRechazadosPagosTransferencias() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }
        public object handleVerificarPagosTransferenciasTodos(DownloadFileTransferenciaInput body)
        {
            try
            {
                Logger.LogInformation($"usuario : {body.user} inicio el servicio handleConfirmarPagosTransferenciasTodos() ");
                GestionPagosEvent @event = Repository.handleVerificarPagosTransferenciasTodos(body);
                Logger.LogInformation($"handleConfirmarPagosTransferenciasTodos() response @event {@event}");
                if (@event.eventType == GestionPagosEvent.EXISTEN_PENDIENTES)
                {
                    return Respuesta.ReturnResultdo(2, @event.message, @event.dataVerify);
                }
                else if (@event.eventType == GestionPagosEvent.EXISTEN_RECHAZADOS)
                {
                    return Respuesta.ReturnResultdo(3, @event.message, @event.dataVerify);
                }
                else if (@event.eventType == GestionPagosEvent.NO_EXISTEN_PENDIENTES_NI_RECHAZADOS)
                {
                    return Respuesta.ReturnResultdo(0, @event.message, @event.dataVerify);
                } else
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
        public object FiltrarComisionesPorTipoPago(FiltroComisionTipoPagoInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();
                int idEstadoComision = 10; //VARIABLE           
                int idTipoComisionPagoComision = 1; //parametro
                var comisiones = Repository.FiltrarComisionPagoPorTipoPago(param, idEstadoComision, idTipoComisionPagoComision);
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
                obj.lista = comisiones;
                return Respuesta.ReturnResultdo(0, "ok", obj);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarComisionesFormaPagoPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object CerrarPagoComision(CerrarPagoParam param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio CerrarPagoComision() ");

                int idEstadoComision = 10; //VARIABLE
                int idTipoComisionPagoComision = 1; //parametro
                int idTipoFormaPagoSionPay = 1; //parametro
                int idTipoFormaPagoTransferencia = 2; //parametro
                                                      //
                RespuestaPorTipoPagoModel sionPay = Repository.VerificarTipoPagoCiclo(param.idCiclo, param.usuarioLogin, idEstadoComision, idTipoComisionPagoComision, idTipoFormaPagoSionPay);
                if (sionPay.CodigoRespuesta == -1)
                    return Respuesta.ReturnResultdo(1, "Problemas al verificar los pagos realizados por SION PAY.", " ");
                if (sionPay.Cantidad > 0)
                   return Respuesta.ReturnResultdo(1, "Pagos pendientes en el pago de sion pay.", sionPay);

                RespuestaPorTipoPagoModel transacion = Repository.VerificarTipoPagoCiclo(param.idCiclo, param.usuarioLogin, idEstadoComision, idTipoComisionPagoComision, idTipoFormaPagoTransferencia);
                if (transacion.CodigoRespuesta == -1)
                    return Respuesta.ReturnResultdo(1, "Problemas al verificar los pagos por transferencias.", " ");
                if (transacion.Cantidad > 0)
                    return Respuesta.ReturnResultdo(1, "Pago Pendientes en los Pagos de trasferencia, verifique los montos", transacion);

                RespuestaPorTipoPagoModel verificarMonto = Repository.VerificarTransaccionRechazadoMontoCero(param.idCiclo, param.usuarioLogin, idEstadoComision, idTipoComisionPagoComision, idTipoFormaPagoTransferencia);
                if(verificarMonto.CodigoRespuesta == -1)
                    return Respuesta.ReturnResultdo(1, "Problemas al verificar los pagos por transferencias.", " ");
                if (verificarMonto.Cantidad > 0)
                    return Respuesta.ReturnResultdo(1, "Pago Pendientes verificar los monto de las transferencias ", verificarMonto);

                var cerrarPago = Repository.CerrarPagoComisionPorTipoComision(param, idTipoComisionPagoComision);
                 if(cerrarPago < 0)
                  return Respuesta.ReturnResultdo(1, "Problemas al ejecutar el cierre", "");
                 if (cerrarPago == 2)
                 return Respuesta.ReturnResultdo(0, "la comision se cerro con exito", "");
                 return Respuesta.ReturnResultdo(1, "Problemas al ejecutar el cierre", "");                
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch CerrarPagoComision()  {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener al cerrar la comision", "problemas en el servidor, intente mas tarde");
            }
        }
        public object BuscarFreelancerPagosTransferencias(DownloadFileTransferenciaInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.user} inicio el servicio BuscarFreelancerPagosTransferencias() ");
                var file = Repository.BuscarFreelancerPagosTransferencias(param);
                if (file == null)
                {
                    return Respuesta.ReturnResultdo(1, "No se encontraron resultados", file);
                }
                else
                {
                    return Respuesta.ReturnResultdo(0, "Resultado encontrado", file);
                }

            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.user} error catch BuscarFreelancerPagosTransferencias()  {ex.Message}");
                return Respuesta.ReturnResultdo(1, "Error de conexion", "Ocurrió algo inesperado con la comunicación con el servidor.");
            }
        }
    }
}
