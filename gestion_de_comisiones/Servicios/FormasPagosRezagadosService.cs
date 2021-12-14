using System;
using System.Collections.Generic;
using gestion_de_comisiones.Dtos;
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
                obj.PendienteFormaPago = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
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
                var tieneUnaComisionAprobada = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
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
                var tieneUnaComisionAprobada = Repository.VerificarSiExisteAutorizacionFormaPagoCiclo(param.usuarioLogin, param.idCiclo);
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
                var result = Repository.CerrarFormaDePago(param);
                if (result)
                    return Respuesta.ReturnResultdo(0, "se cerro la forma de pago", "");
                return Respuesta.ReturnResultdo(0, "Problemas al procesar el cierre de pagos", "");
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} error catch VerificarCierreFormaPago(),error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al verificar la forma de pago", "");
            }
        }
    }
}
