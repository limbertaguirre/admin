using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class ClienteRespository : IClienteRepository
    {
        private readonly ILogger<ClienteRespository> Logger;

        public ClienteRespository(ILogger<ClienteRespository> logger)
        {
            Logger = logger;
        }

        public object obtenerAllClientes(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el obtenerRolesAll");

                //var ListRoles = contextMulti.Rols.Where(x => x.Habilitado == true).Select(p => new RolResulModel(p.IdRol, p.Nombre, p.Descripcion, p.Habilitado)).ToList();
                return null;
            }
            catch (Exception ex)
            {
               // Logger.LogInformation($" usuario: {usuario} error catch repositorio cliente inicio el obtenerRolesAll");
                return null;
            }
        }

    }
}
