using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class FacturaService : IFacturaService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<FacturaService> Logger;

        public FacturaService(ILogger<FacturaService> logger, IFacturaRepository repository )
        {
            Logger = logger;
            Repository = repository;
        }
        public IFacturaRepository Repository { get; set; }
        public object obtenerlistCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerlistCiclos() ");
                var ciclos = Repository.listCiclosPendientes(usuario);
                return Respuesta.ReturnResultdo(0, "ok", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }

        public object obtenerlistComisionesPendiente(string usuario, int idCiclo)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerlistComisionesPendiente() ");
                int idEstado= int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION"));
                var comsiones = Repository.obtenerComisiones(usuario, idCiclo, idEstado);
                return Respuesta.ReturnResultdo(0, "ok", comsiones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistComisionesPendiente() al obtener lista de comisiones pendientes para facturar,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista comisiones para facturar", "problemas en el servidor, intente mas tarde");
            }
        }
        public object buscarComisionesPorNombre(string usuario, int idCiclo, string nombreCriterio)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio buscarComisionesPorNombre() ");
                int idEstado = int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION"));
                var comsiones = Repository.buscarcomisionXnombre(usuario, idCiclo, idEstado, nombreCriterio);
                return Respuesta.ReturnResultdo(0, "ok", comsiones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch buscarComisionesPorNombre() al obtener lista de comisiones pendientes para facturar,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al buscar comiision por nombre", "problemas en el servidor, intente mas tarde");
            }
        }
    }
}
