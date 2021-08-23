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
    public class ProrrateadoService :IProrrateadoService
    {

        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<ProrrateadoService> Logger;
        private IProrrateadoRepository Repository { get; set; }

        public ProrrateadoService(ILogger<ProrrateadoService> logger, IProrrateadoRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }
       

        public object GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio prorrateo service => getCiclos()");
                int idEstadoPendienteAplicacion = 4; //parametro
                List<CicloDto> ciclos = (List<CicloDto>)Repository.GetCiclos(usuario, idEstadoPendienteAplicacion);
                if (ciclos.Count > 0)
                {
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                } else {
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "No hay ciclos  pendientes a prorrateo. ", ciclos);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch GetCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }


    }
}
