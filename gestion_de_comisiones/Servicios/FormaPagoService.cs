using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Servicios.Interfaces;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Modelos.FormaPago;

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
                List<CicloDto> ciclos = (List<CicloDto>)Repository.GetCiclos(usuario, idEstadoCerradoProrrateo);
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
        public object ListarFormasPagos(ParamFormaPagosOutputModel param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarFormasPagos()");              
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



    }
}
