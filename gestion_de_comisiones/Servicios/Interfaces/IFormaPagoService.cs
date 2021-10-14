using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IFormaPagoService
    {
        public object GetCiclos(string usuario);
        public object GetFormasPagosPendientes(string usuario, int idCiclo);
        public object ListarFormasPagos(ParamFormaPagosOutputModel param);
        public object AplicarMetodoPago(AplicarMetodoOutput param);
        public object ListarComisionesFormaPagoPorCarnet(BuscarInputModel param);
        public object GetFormaPagosDisponibles(FormaPagosDisponiblesInputModel param);
        public object FiltrarComisionesPorTipoPago(FiltroComisionTipoPagoInputModel param);
        public object VerificarAutorizadorPorComision(AutorizacionVerificarParam param);
        public object ConfirmarAutorizacionPagos(ConfirmarAutorizacionParam param);
        public object VerificarCierreFormaPago(VerificarCierreFormaPagoParam param);
        public object CerrarFormaDePago(CierreformaPagoInput param);
    }
}
