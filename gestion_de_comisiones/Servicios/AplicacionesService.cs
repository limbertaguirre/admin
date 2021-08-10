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
    public class AplicacionesService : IAplicacionesService
    {

        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<AplicacionesService> Logger;

        public AplicacionesService(ILogger<AplicacionesService> logger, IAplicacionesRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }
        public IAplicacionesRepository Repository { get; set; }

        public object GetAplicacionesPendientes(string usuario, int idCiclo)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio AplicacionesService => getAplicacionesPendientes() ");
                var ciclos = Repository.GetComisiones(usuario, idCiclo, int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION")));
                return Respuesta.ReturnResultdo(0, "ok", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch AplicacionesService => getAplicacionesPendientes() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de las aplicaciones", "problemas en el servidor, intente mas tarde");
            }
        }

        public object GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio AplicacionesService => getCiclos()");
                //ESTADO_COMISION_PENDIENTE_APLICACION
                //ESTADO_PENDIENTE_COMISION
                List<CicloDto> ciclos = (List<CicloDto>) Repository.GetCiclos(usuario, int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION")));
                if(ciclos.Count > 0) { 
                    return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
                } else { 
                    return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "Lista de ciclos vacía", ciclos);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }

        /*public object IAplicacionesService.GetCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio AplicacionesService => getCiclos()");
                //ESTADO_COMISION_PENDIENTE_APLICACION
                //ESTADO_PENDIENTE_COMISION
                var ciclos = Repository.GetCiclos(usuario, int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION")));
                return Respuesta.ReturnResultdo(ConfiguracionService.SUCCESS, "ok", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(ConfiguracionService.ERROR, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }*/
    }
}
