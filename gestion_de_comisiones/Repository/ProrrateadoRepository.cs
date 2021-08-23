using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class ProrrateadoRepository : IProrrateadoRepository
    {
        private readonly BDMultinivelContext multinivelDbContext;
        private readonly ILogger<ProrrateadoRepository> Logger;
        public ProrrateadoRepository(BDMultinivelContext multinivelDbContext, ILogger<ProrrateadoRepository> logger)
        {
            this.multinivelDbContext = multinivelDbContext;
            this.Logger = logger;
        }

        public object GetCiclos(string usuario, int idEstadoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision}, => AplicacionesRepository|getCiclos");
                var ciclosR = multinivelDbContext.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision).ToList();
                List<CicloDto> ciclos = new List<CicloDto>();
                foreach (var c in ciclosR)
                {
                    Logger.LogInformation($" usuario: {usuario} ciclosR => IdCiclo: {c.IdCiclo} Nombre: {c.Nombre} Estado: {c.Estado}");
                    ciclos.Add(new CicloDto(c.IdCiclo, c.Nombre));
                }
                return ciclos;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() mensaje : {ex}");
                List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }


    }
}
