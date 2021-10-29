using gestion_de_comisiones.Dtos;
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
                int idEstadoComisionSiFacturo = 2; //VARIABLE
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
                int idEstadoComisionSiFacturo = 2; //VARIABLE
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

        public object handleTransferenciasEmpresas(ComisionesPagosInput param)
        {
            try
            {
                Logger.LogInformation($"usuario : {param.usuarioLogin} inicio el servicio ListarComisionesFormaPagoPorCarnet() ");                
                var empresas = Repository.handleTransferenciasEmpresas(param);
                return Respuesta.ReturnResultdo(0, "ok", empresas);
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
                DownloadFileTransferenciaOutput file = Repository.handleDownloadFileEmpresas(body);
                Logger.LogInformation($"handleDownloadFileEmpresas file: {file}");
                if(file != null) {
                    return Respuesta.ReturnResultdo(0, "ok", file);
                } else
                {
                    Logger.LogInformation($"usuario : {body.user} handleDownloadFileEmpresas() Reporte vacío desde base de datos.");
                    return Respuesta.ReturnResultdo(1, "Hay inconvenientes al generar el archivo.  Intente más tarde.", "");
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {body.user} error catch handleDownloadFileEmpresas() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "Hay inconvenientes al generar el archivo. Intente más tarde.", "");
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
    }
}
