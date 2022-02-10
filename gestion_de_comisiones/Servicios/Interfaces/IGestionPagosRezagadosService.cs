using System;
using System.Threading.Tasks;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Modelos.GestionPagosRezagados;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IGestionPagosRezagadosService
    {
        public object GetCiclos(string usuario);
        public object GetComisionesDePagos(ComisionesPagosInput param);
        public object handleTransferenciasEmpresas(ComisionesPagosInput param);
        object handleVerificarPagosTransferenciasTodos(ObtenerRezagadosPagosTransferenciasInput body);
        public object ObtenerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param);
        public Task<object> ConfirmarPagosRezagadosTransferenciasAsync(ConfirmarPagosRezagadosTransferenciasInput param);
        object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body);
        Task<object> handleConfirmarPagosTransferenciasTodosAsync(ObtenerRezagadosPagosTransferenciasInput body);
        public object BuscarFreelancerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param);
        public object PagarComisionRezagadosSionPayTodo(PagoRezagadoInput param);
        public object VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param);
        public object PagarSionPayComisionTodo(PagarSionPayInput param);
        public object GetFormaPagosDisponibles(FiltroFormaPagosInput param);
        public object ListarComisionesFormaPagoPorCarnet(BuscarComisionInput param);
        public object FiltrarComisionesPorTipoPago(FiltroComisionTipoPagoInput param);
        public object CerrarPagoComision(CerrarPagoParam param);
    }
}
