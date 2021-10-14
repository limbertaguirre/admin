using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IFormaPagoRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision);
        public List<VwObtenercomisionesFormaPago> GetComisiones(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura);
        public List<TipoPagoInputmodel> ListarFormaPagos(ParamFormaPagosOutputModel param);
        public bool AplicarFormaPago(AplicarMetodoOutput param);
        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListFormaPago(BuscarInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura);
        public object GetComisionesPorFormaPago(FormaPagosDisponiblesInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura);
        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura);
        public object VerificarAutorizadorPorComision(AutorizacionVerificarParam param);
        public bool ConfirmarAutorizacion(ConfirmarAutorizacionParam param);
        public bool VerificarSiExisteAutorizacionFormaPagoCiclo(string usuarioLogin, int idCiclo);
        public ConfirmarPagoOutPut VerificarCierreFormaPago(VerificarCierreFormaPagoParam param);
        public bool CerrarFormaDePago(CierreformaPagoInput param);
    }
}
