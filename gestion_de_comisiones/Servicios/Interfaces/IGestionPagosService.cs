using gestion_de_comisiones.Modelos.GestionPagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IGestionPagosService
    {
        public object GetCiclos(string usuario);
        public object GetComisionesDePagos(ComisionesPagosInput param);
        public object GetFormaPagosDisponibles(FiltroFormaPagosInput param);
        public object ListarComisionesFormaPagoPorCarnet(BuscarComisionInput param);
        public object PagarSionPayComisionTodo(PagarSionPayInput param);
        public object VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param);
        object handleTransferenciasEmpresas(ComisionesPagosInput param);
        object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body);
        object handleConfirmarPagosTransferencias(ConfirmarPagosTransferenciasInput param);
        object handleObtenerPagosTransferencias(DownloadFileTransferenciaInput param);
    }
}
