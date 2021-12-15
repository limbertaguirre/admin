using System;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IFormasPagosRezagadosService
    {
        public object GetCiclos(string usuario);
        public object GetComisionesRezagados(ComisionesPagosInput param);
        public object GetListarFormaPagos(ParamFormaPagosOutputModel param);
        public object AplicarMetodoPago(AplicarMetodoOutput param);
        public object VerificarCierreFormaPago(VerificarCierreFormaPagoParam param);
        public object CerrarFormaDePago(CierreformaPagoInput param);
        public object VerificarAutorizadorPorComision(AutorizacionVerificarParam param);
        public object ConfirmarAutorizacionPagos(ConfirmarAutorizacionParam param);
        public object ListarComisionesFormaPagoPorCarnet(BuscarInputModel param);
        public object FiltrarComisionesPorTipoPago(FiltroComisionTipoPagoInputModel param);
        public object GetFormasPagosPendientes(ComisionesPagosInput param);
    }
}
