using System;
using System.Collections.Generic;
using System.Linq;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Repository
{
    public class GestionPagosRezagadosRepository : IGestionPagosRezagadosRepository
    {
        private readonly BDMultinivelContext ContextMulti;
        private readonly ILogger<GestionPagosRezagadosRepository> Logger;

        public GestionPagosRezagadosRepository(BDMultinivelContext multinivelDbContext, ILogger<GestionPagosRezagadosRepository> logger)
        {
            this.ContextMulti = multinivelDbContext;
            this.Logger = logger;
        }
        public GestionPagosRezagadosRepository()
        {

        }

        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionPagoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                var ciclosR = ContextMulti.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision && x.IdTipoComision == idTipoComisionPagoComision).ToList();
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
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() en pagos mensaje : {ex.Message}");
                List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }

        public object GetComisionesPagos(string usuario, int idCiclo, int idEstadoComision, int idTipoComisionPagoComision)
        {
            try
            {
                Logger.LogInformation($"Inicio GestionPagosRezagadosRepository - GetComisionesPagos");
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                //var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision).ToList();
                Logger.LogInformation($"Fin GestionPagosRezagadosRepository - GetComisionesPagos");
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch GestionPagosRezagadosRepository - GetComisionesPagos() mensaje : {ex.Message}");
                Logger.LogWarning($" usuario: {usuario} error catch GestionPagosRezagadosRepository - GetComisionesPagos() StackTrace : {ex.StackTrace}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;

            }
        }
    }
}
