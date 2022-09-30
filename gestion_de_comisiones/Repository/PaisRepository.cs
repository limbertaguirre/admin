using gestion_de_comisiones.Modelos.Pais;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class PaisRepository : IPaisRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<PaisRepository> Logger;

        public PaisRepository(ILogger<PaisRepository> logger)
        {
            Logger = logger;
        }

        public object ListaPaises(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el ListaPaises");
                var listaPaises = contextMulti.Pais.Select(p => new PaisOutPutModel( p.IdPais, p.Nombre )).ToList();
                return listaPaises;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch mensaje : {ex}");
                List<PaisOutPutModel> list = new List<PaisOutPutModel>();
                return list;
            }
        }
        public List<CiudadOutPutModel> obtenerCiudadXpais(string usuario, int idPais)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el ListaPaises");
                var listaPaises = contextMulti.Ciudads.Where(x=> x.IdPais == idPais ).Select(p => new CiudadOutPutModel(p.IdCiudad, p.Nombre)).ToList();
                return listaPaises;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch mensaje : {ex}");
                List<CiudadOutPutModel> list = new List<CiudadOutPutModel>();
                return list;
            }
        }


    }
}
