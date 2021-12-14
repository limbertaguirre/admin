using System;
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
        public object ConfirmarPagosRezagadosTransferencias(ConfirmarPagosRezagadosTransferenciasInput param);
        object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body);
        object handleConfirmarPagosTransferenciasTodos(ObtenerRezagadosPagosTransferenciasInput body);
        public object BuscarFreelancerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param);
        public object PagarComisionRezagadosSionPayTodo(PagoRezagadoInput param);
        public object VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param);
        public object PagarSionPayComisionTodo(PagarSionPayInput param);
    }
}
