using System;
using gestion_de_comisiones.Modelos.GestionPagos;

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
    }
}
