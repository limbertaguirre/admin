using System;
namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IGestionPagosRezagadosRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionPagoComision);
        public object GetComisionesPagos(string usuario, int idCiclo, int idEstadoComision, int idTipoComisionPagoComision);
        /*public object GetFiltroComisionesPorFormaPago(FiltroFormaPagosInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision);
        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListPagos(BuscarComisionInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision);
        public bool PagarSionPayComision(PagarSionPayInput param);
        public RespuestaSionPayModel VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision, int idTipoFormaPagoSionPay);
        public dynamic handleTransferenciasEmpresas(ComisionesPagosInput param);
        object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body);
        bool handleConfirmarPagosTransferenciasTodos(DownloadFileTransferenciaInput body);
        GestionPagosEvent handleVerificarPagosTransferenciasTodos(DownloadFileTransferenciaInput body);
        GestionPagosEvent handleConfirmarPagosTransferencias(ConfirmarPagosTransferenciasInput body);
        object handleObtenerPagosTransferencias(DownloadFileTransferenciaInput body);
        object handleRechazadosPagosTransferencias(ConfirmarPagosTransferenciasInput param);
        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInput param, int idEstadoComision, int idTipoComisionPagoComision);
        public bool VerificarSiExisteAutorizacionFormaPagoCiclo(string usuarioLogin, int idCiclo);*/
    }
}
