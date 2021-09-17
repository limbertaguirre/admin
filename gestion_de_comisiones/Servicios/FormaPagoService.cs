using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Servicios.Interfaces;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Repository.Interfaces;

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



    }
}
