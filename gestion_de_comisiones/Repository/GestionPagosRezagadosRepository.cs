using System;
using System.Collections.Generic;
using System.Linq;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.GestionPagos;
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

        public object ObtenerPagosRezagadosTransferencias(ObtenerRezagadosPagosTransferenciasInput body)
        {
            try
            {

                Logger.LogInformation($" usuario: {body.user} inicio el repository ObtenerPagosRezagadosTransferencias() ");
                Logger.LogInformation($" usuario: {body.user} parametros: idciclo: {body.cicloId}");
                int cicloId = Convert.ToInt32(body.cicloId);
                int tipoPagoTransferencia = 2;
                List<VwObtenerRezagadosPago> info = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdComision == body.comisionId && x.IdTipoPago == tipoPagoTransferencia)
                    .ToList();

                var montoTotal = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdComision == body.comisionId && x.IdTipoPago == tipoPagoTransferencia)
                    .Sum(x => x.ImportePorEmpresa);

                ObtenerRezagadosPagosTransferenciasOutput o = new ObtenerRezagadosPagosTransferenciasOutput();
                o.list = info;
                o.montoTotal = montoTotal.ToString();
                return o;
            }
            catch (Exception ex)
            {
                Logger.LogError ($" usuario: {body.user} error catch ObtenerPagosRezagadosTransferencias() mensaje : {ex}");
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                return list;
            }
        }
    }
}
