using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class FacturaRepository: IFacturaRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<FacturaRepository> Logger;
        public FacturaRepository(ILogger<FacturaRepository> logger)
        {
            Logger = logger;
        }
        public object listCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el listCiclos() repository");
                var ciclos = contextMulti.Cicloes.Select(p => new { p.IdCiclo, p.Nombre, p.FechaInicio, p.FechaFin }).ToList();
                return ciclos;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch listCiclos() mensaje : {ex}");
                 List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }

    }
}
