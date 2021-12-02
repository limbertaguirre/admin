using System;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IGestionPagosRezagadosService
    {
        public object GetCiclos(string usuario);
        public object GetComisionesDePagos(ComisionesPagosInput param);
        /*public object GetFormaPagosDisponibles(FiltroFormaPagosInput param);
        public object ListarComisionesFormaPagoPorCarnet(BuscarComisionInput param);
        public object PagarSionPayComisionTodo(PagarSionPayInput param);
        public object VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param);
        object handleTransferenciasEmpresas(ComisionesPagosInput param);
        object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body);
        object handleConfirmarPagosTransferenciasTodos(DownloadFileTransferenciaInput body);
        object handleVerificarPagosTransferenciasTodos(DownloadFileTransferenciaInput body);
        object handleConfirmarPagosTransferencias(ConfirmarPagosTransferenciasInput param);
        object handleObtenerPagosTransferencias(DownloadFileTransferenciaInput param);
        object handleRechazadosPagosTransferencias(ConfirmarPagosTransferenciasInput param);
        public object FiltrarComisionesPorTipoPago(FiltroComisionTipoPagoInput param);*/
    }
}
