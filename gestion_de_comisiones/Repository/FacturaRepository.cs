using gestion_de_comisiones.Modelos.Factura;
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
                int pendiente = 1;
                int idtipoComision = 1;
                var listiclos = contextMulti.GpComisions.Join(contextMulti.GpComisionEstadoComisionIs,
                                                  GpComision => GpComision.IdComision,
                                                  GpComisionEstadoComisionI => GpComisionEstadoComisionI.IdComision,
                                                  (GpComision, GpComisionEstadoComisionI) => new
                                                  {
                                                      idcomisiones = GpComision.IdComision,
                                                      idEstado = GpComisionEstadoComisionI.IdEstadoComision,
                                                      idTipoComision = GpComision.IdTipoComision,
                                                      idCiclo = GpComision.IdCiclo,
                                                  }).Where(x => x.idEstado == pendiente && x.idTipoComision == idtipoComision).ToList();
                List<CicloOutputModel> lista = new List<CicloOutputModel>();
                foreach (var obj in listiclos){
                    CicloOutputModel objciclo = new CicloOutputModel();
                    var ciclo = contextMulti.Cicloes.Where(x => x.IdCiclo == obj.idCiclo).FirstOrDefault();
                    if(ciclo != null)
                    {
                        objciclo.idCiclo = (int)obj.idCiclo;
                        objciclo.nombre = ciclo.Nombre;
                        objciclo.Descripcion = ciclo.Descripcion;
                        lista.Add(objciclo);
                    }
                }
                return lista;

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
