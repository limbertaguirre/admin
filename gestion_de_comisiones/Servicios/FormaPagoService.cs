using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Servicios.Interfaces;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.Factura;

namespace gestion_de_comisiones.Servicios
{
    public class FormaPagoService : IFormaPagoService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<FormaPagoService> Logger;

        public FormaPagoService(ILogger<FormaPagoService> logger, IFormaPagoRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }

        private IFormaPagoRepository Repository { get; }

        public object GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio FormaPagoService => getCiclos()");
                int idEstadoCerradoProrrateo = 8; //parametro
                int idTipoComisionPagoComision = 1; //parametro
                List<CicloDto> ciclos = (List<CicloDto>)Repository.GetCiclos(usuario, idEstadoCerradoProrrateo, idTipoComisionPagoComision);
                if (ciclos.Count > 0)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                }
                else
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "No hay ciclos disponibles para la forma de pagos. ", ciclos);
                }

            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener lista de ciclos Forma de pagos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de forma de pagos", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetFormasPagosPendientes(string usuario, int idCiclo)
        {
            try
            {
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();
                Logger.LogInformation($"usuario : {usuario} inicio el servicio AplicacionesService => getAplicacionesPendientes() ");
                int idEstadoComisionSiFacturo = 2; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                int idTipoComisionPagoComision = 1; //parametro
                var comisiones = Repository.GetComisiones(usuario, idCiclo, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, idTipoComisionPagoComision);
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(usuario, idCiclo);
                obj.lista = comisiones;
                //obj.pendienteFormaPago = pendiente;
                return Respuesta.ReturnResultdo(0, "ok", obj);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch AplicacionesService => getAplicacionesPendientes() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de las aplicaciones", "problemas en el servidor, intente mas tarde");
            }
        }
        public object ListarFormasPagos(ParamFormaPagosOutputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarFormasPagos()");
                var tieneUnaComisionAprobada = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
                if (tieneUnaComisionAprobada)
                return Respuesta.ReturnResultdo(1, "ESTA ACCIÓN NO SE PUDO COMPLETAR, MOTIVO : PROCESO DE AUTORIZACIÓN EN CURSO.", "");                
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", (List<TipoPagoInputmodel>)Repository.ListarFormaPagos(param));
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
                if(param.idTipoPago == 0)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "SELECCIONE UNA FORMA DE PAGO", "");
                }
                var tieneUnaComisionAprobada = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
                if (tieneUnaComisionAprobada){
                    return Respuesta.ReturnResultdo(1, "ESTA ACCIÓN NO SE PUDO COMPLETAR, MOTIVO : PROCESO DE AUTORIZACIÓN EN CURSO.", "");
                }
                var apli = Repository.AplicarFormaPago(param);
                if (apli) {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", apli);
                } else {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "ok", "");
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch AplicarMetodoPago() ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener aplicar un metodo de pago", "");
            }
        }
        public object ListarComisionesFormaPagoPorCarnet(BuscarInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();
                int idEstadoComisionSiFacturo = 2; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;// estado de la tabla detalle de comision
                int idTipoComisionPagoComision = 1; //parametro
                var comisiones = Repository.GetComisionesPorCarnetListFormaPago(param, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, idTipoComisionPagoComision);
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
                obj.lista = comisiones;
                return Respuesta.ReturnResultdo(0, "ok",obj);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ListarComisionesFormaPagoPorCarnet() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de comisiones", "problemas en el servidor, intente mas tarde");
            }
        }
        public object GetFormaPagosDisponibles(FormaPagosDisponiblesInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio getFormaPagosDisponibles() ");
                int idEstadoComisionSiFacturo = 2; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                int idTipoComisionPagoComision = 1; //parametro
                return Respuesta.ReturnResultdo(0, "ok", Repository.GetComisionesPorFormaPago(param, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, idTipoComisionPagoComision));
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch getFormaPagosDisponibles(), error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la Lista de filtro de forma de pago", "problemas en el servidor, intente mas tarde");
            }
        }
        public object FiltrarComisionesPorTipoPago(FiltroComisionTipoPagoInputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");
                ObjetoComisionesRespuesta obj = new ObjetoComisionesRespuesta();
                int idEstadoComisionSiFacturo = 2; //VARIABLE
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                int idTipoComisionPagoComision = 1; //parametro
                var comisiones = Repository.FiltrarComisionPagoPorTipoPago(param, idEstadoComisionSiFacturo, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, idTipoComisionPagoComision);
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
                if(result == true)
                {
                    obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
                    //obj.lista = comisiones;
                    return Respuesta.ReturnResultdo(0, "se autorizo la comision", obj );
                } else {
                    return Respuesta.ReturnResultdo(1, "problemas al autorizar una comision", "");
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch ConfirmarAutorizacionPagos(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al autorizar una comision de pagos", "problemas");
            }
        }

        public object VerificarCierreFormaPago(VerificarCierreFormaPagoParam param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio VerificarCierreFormaPago() ");
                return Respuesta.ReturnResultdo(0, "ok", Repository.VerificarCierreFormaPago(param));

            }
            catch (Exception ex) {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch VerificarCierreFormaPago(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al verificar la forma de pago", "");
            }
        }
        public object CerrarFormaDePago(CierreformaPagoInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio VerificarCierreFormaPago() ");
                var result = Repository.CerrarFormaDePago(param);
                if(result)
                return Respuesta.ReturnResultdo(0, "se cerro la forma de pago","" );
                return Respuesta.ReturnResultdo(0, "Problemas al procesar el cierre de pagos", "" );

            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch VerificarCierreFormaPago(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al verificar la forma de pago", "");
            }
        }




    }
}
