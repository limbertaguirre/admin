using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
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


    }
}
