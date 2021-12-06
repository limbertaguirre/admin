using System;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IGestionPagosRezagadosRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionPagoComision);
        public object GetComisionesPagos(string usuario, int idCiclo, int idEstadoComision, int idTipoComisionPagoComision, int idComision);
        public dynamic handleTransferenciasEmpresas(ComisionesPagosInput param);
        public GestionPagosRezagadosEvent handleVerificarPagosTransferenciasTodos(ObtenerRezagadosPagosTransferenciasInput body);
        public object ObtenerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param);
        GestionPagosRezagadosEvent ConfirmarPagosRezagadosTransferencias(ConfirmarPagosRezagadosTransferenciasInput param);
        object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body);
    }
}
