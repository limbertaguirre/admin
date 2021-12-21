using System;
using System.Collections.Generic;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Servicios
{
    public class FormasPagosRezagadosService : IFormasPagosRezagadosService
    {

        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<FormasPagosRezagadosService> Logger;

        public FormasPagosRezagadosService(ILogger<FormasPagosRezagadosService> logger, IFormasPagosRezagadosRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }

        private IFormasPagosRezagadosRepository Repository { get; }

        public object GetCiclos(string usename)
        {
            try
            {
                Logger.LogInformation($"usuario : {usename} inicio el servicio FormasPagosRezagadosService => getCiclos()");
                var ciclos = Repository.GetCiclos(usename);
                if (ciclos != null)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                }
                else
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "Ocurrió un inconveniente al obtener el ciclo.", ciclos);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"usuario : {usename} error catch obtenerlistCiclos() al obtener para pagos,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de pagos", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetComisionesRezagados(ComisionesPagosInput param)
        {
            try
            {
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio FormasPagosRezagadosService => GetComisionesDePagos() ");              
                var comisiones = Repository.GetComisionesRezagados(param);
                // preguntar
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo, param.idComision);
                obj.lista = comisiones;
                //obj.pendienteFormaPago = pendiente;
                return Respuesta.ReturnResultdo(0, "ok", obj);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch FormasPagosRezagadosService => GetComisionesDePagos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de las aplicaciones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetListarFormaPagos(ParamFormaPagosOutputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarFormasPagos()");
                var tieneUnaComisionAprobada = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo, param.comisionId);
                if (tieneUnaComisionAprobada)
                    return Respuesta.ReturnResultdo(1, "ESTA ACCIÓN NO SE PUDO COMPLETAR, MOTIVO : PROCESO DE AUTORIZACIÓN EN CURSO.", "");
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", (List<TipoPagoInputmodel>) Repository.GetListarFormaPagos(param));
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarFormasPagos() al obtener lista  Forma de pagos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de tipos de pagos", "");
            }
        }

        public object AplicarMetodoPago(AplicarMetodoOutput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio AplicarMetodoPago()");
                if (param.idTipoPago == 0)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "SELECCIONE UNA FORMA DE PAGO", "");
                }
                var tieneUnaComisionAprobada = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo, param.comisionId);
                if (tieneUnaComisionAprobada)
                {
                    return Respuesta.ReturnResultdo(1, "ESTA ACCIÓN NO SE PUDO COMPLETAR, MOTIVO : PROCESO DE AUTORIZACIÓN EN CURSO.", "");
                }
                var apli = Repository.AplicarFormaPago(param);
                if (apli)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", apli);
                }
                else
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "ok", "");
                }

            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch AplicarMetodoPago() ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener aplicar un metodo de pago", "");
            }
        }

        public object VerificarCierreFormaPago(VerificarCierreFormaPagoParam param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio VerificarCierreFormaPago() ");                
                return Respuesta.ReturnResultdo(0, "ok", Repository.VerificarCierreFormaPago(param));
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch VerificarCierreFormaPago(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al verificar la forma de pago", "");
            }
        }

        public object CerrarFormaDePago(CierreformaPagoInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio VerificarCierreFormaPago() ");
                FormasPagosRezagadosEvent result = Repository.CerrarFormaDePago(param);
                if (result.eventType == FormasPagosRezagadosEvent.SUCCESS)
                    return Respuesta.ReturnResultdo(0, "Se cerro la forma de pago correctamente.", "");
                return Respuesta.ReturnResultdo(0, result.message, "");
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch VerificarCierreFormaPago(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al verificar la forma de pago", "");
            }
        }

        public object VerificarAutorizadorPorComision(AutorizacionVerificarParam param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio VerificarAutorizadorPorComision() ");
                return Respuesta.ReturnResultdo(0, "ok", Repository.VerificarAutorizadorPorComision(param));
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch VerificarAutorizadorPorComision(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas");
            }
        }

        public object ConfirmarAutorizacionPagos(ConfirmarAutorizacionParam param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ConfirmarAutorizacionPagos() ");
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();
                var result = Repository.ConfirmarAutorizacion(param);
                if (result == true)
                {
                    obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo, param.idComision);
                    //obj.lista = comisiones;
                    return Respuesta.ReturnResultdo(0, "se autorizo la comision", obj);
                }
                else
                {
                    return Respuesta.ReturnResultdo(1, "problemas al autorizar una comision", "");
                }

            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ConfirmarAutorizacionPagos(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al autorizar una comision de pagos", "problemas");
            }
        }

        public object ListarComisionesFormaPagoPorCarnet(BuscarInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();                
                var comisiones = Repository.GetComisionesPorCarnetListFormaPago(param);
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo, param.comisionId);
                obj.lista = comisiones;
                return Respuesta.ReturnResultdo(0, "ok", obj);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarComisionesFormaPagoPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object FiltrarComisionesPorTipoPago(FiltroComisionTipoPagoInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();           
                var comisiones = Repository.FiltrarComisionPagoPorTipoPago(param);
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo, param.comisionId);
                obj.lista = comisiones;
                return Respuesta.ReturnResultdo(0, "ok", obj);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarComisionesFormaPagoPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetFormasPagosPendientes(ComisionesPagosInput param)
        {
            try
            {
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio AplicacionesService => getAplicacionesPendientes() ");               
                var comisiones = Repository.GetComisionesRezagados(param);
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo, param.idComision);
                obj.lista = comisiones;
                //obj.pendienteFormaPago = pendiente;
                return Respuesta.ReturnResultdo(0, "ok", obj);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch AplicacionesService => getAplicacionesPendientes() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de las aplicaciones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetFormaPagosDisponibles(FormaPagosDisponiblesInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio getFormaPagosDisponibles() ");                
                return Respuesta.ReturnResultdo(0, "ok", Repository.GetComisionesPorFormaPago(param));
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch getFormaPagosDisponibles(), error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de filtro de forma de pago", "problemas en el servidor, intente mas tarde");
            }
        }
    }
}
