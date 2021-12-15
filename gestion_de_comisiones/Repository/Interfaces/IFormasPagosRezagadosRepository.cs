using System;
using System.Collections.Generic;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IFormasPagosRezagadosRepository
    {
        public object GetCiclos(string usuario);
        List<VwObtenercomisionesFormaPago> GetComisionesRezagados(ComisionesPagosInput param);
        bool VerificarSiExisteAutorizacionFormaPagoCiclo(string usuarioLogin, int idCiclo, int comisionId);
        List<TipoPagoInputmodel> GetListarFormaPagos(ParamFormaPagosOutputModel param);
        public bool AplicarFormaPago(AplicarMetodoOutput param);
        public ConfirmarPagoOutPut VerificarCierreFormaPago(VerificarCierreFormaPagoParam param);
        public bool CerrarFormaDePago(CierreformaPagoInput param);
        public object VerificarAutorizadorPorComision(AutorizacionVerificarParam param);
        public bool ConfirmarAutorizacion(ConfirmarAutorizacionParam param);
        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListFormaPago(BuscarInputModel param);
        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInputModel param);
    }
}
