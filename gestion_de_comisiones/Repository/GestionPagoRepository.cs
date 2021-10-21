using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class GestionPagoRepository : IGestionPagoRepository
    {

        private readonly BDMultinivelContext ContextMulti;
        private readonly ILogger<GestionPagoRepository> Logger;

        public GestionPagoRepository(BDMultinivelContext multinivelDbContext, ILogger<GestionPagoRepository> logger)
        {
            this.ContextMulti = multinivelDbContext;
            this.Logger = logger;
        }
        public GestionPagoRepository()
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
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision ).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() en pagos mensaje : {ex.Message}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;

            }
        }
        public object GetFiltroComisionesPorFormaPago(FiltroFormaPagosInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision)
        {
            try
            {
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetFiltroComisionesPorFormaPago() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                List<FormaPagoModel> LisFormaPagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new FormaPagoModel(p.IdTipoPago, p.Nombre, p.Descripcion, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion, p.Estado, p.Icono)).ToList();               
                foreach (var item in LisFormaPagos)
                {
                    if (ListComisiones != null) {
                        FormaPagoDisponiblesModel obj = new FormaPagoDisponiblesModel();
                        obj.idTipoPago = item.IdTipoPago;
                        obj.nombre = item.Nombre;
                        obj.icono = item.Icono;
                        int canti = ListComisiones.Where(x => x.IdTipoPago == item.IdTipoPago).Count();
                        obj.cantidad = canti;
                        list.Add(obj);
                    }
                }
                return list;
            } catch (Exception ex) {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetFiltroComisionesPorFormaPago() mensaje : {ex}");
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                return list;
            }
        }

        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListPagos(BuscarComisionInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision)
        {
            try
            {
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetComisionesPorCarnet() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");
                if (param.nombreCriterio != "")
                {
                    var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoPago != 0 && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura) && x.Ci.Contains(param.nombreCriterio.Trim())).ToList();
                    return ListComisiones;
                } else {
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetComisionesPorCarnet() mensaje : {ex}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }




    }
}
