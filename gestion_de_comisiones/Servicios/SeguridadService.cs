using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class SeguridadService : ISeguridadService
    {
        private readonly IConfiguration Config;
        private readonly ILogger<SeguridadService> Logger;

        public SeguridadService(IConfiguration config, ILogger<SeguridadService> logger)
        {
            Config = config;
            Logger = logger;
        }

        public string EncriptarAes(string Cadena)
        {
            return Cadena; //Task.FromResult("");
        }

    }
}
