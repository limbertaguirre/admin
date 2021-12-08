using gestion_de_comisiones.Modelos.Incentivo;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class IncentivoSionPayRepository : IIncentivoSionPayRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<IncentivoSionPayRepository> Logger;

        public object GuardarPlanillaIncentivoSionPay(PlanillaExcelInput param)
        {
            using (BDMultinivelContext context = new BDMultinivelContext())
            {


                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        dbContextTransaction.Commit();
                        Logger.LogInformation($" usuario: {param.UsuarioNombre} -  SE REGISTRO LA PLANILLA EXITOSAMENTE EXITOSAMENTE ");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogWarning($" usuario: {param.UsuarioNombre} - RETURN!! error: {ex} ");
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
