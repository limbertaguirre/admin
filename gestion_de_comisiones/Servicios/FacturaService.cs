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
                var bajas = Repository.listCiclos(usuario);
                return Respuesta.ReturnResultdo(0, "ok", " " );
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }
    }
}
