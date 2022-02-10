using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IGestionPagoRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionPagoComision);
        public object GetComisionesPagos(string usuario, int idCiclo, int idEstadoComision, int idTipoComisionPagoComision);
        public object GetFiltroComisionesPorFormaPago(FiltroFormaPagosInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision);
        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListPagos(BuscarComisionInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision);
        public bool PagarSionPayComision(PagarSionPayInput param);
        public RespuestaSionPayModel VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision, int idTipoFormaPagoSionPay);
        public dynamic handleTransferenciasEmpresas(ComisionesPagosInput param);
        object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body);
        Task<bool> handleConfirmarPagosTransferenciasTodosAsync(DownloadFileTransferenciaInput body);
        GestionPagosEvent handleVerificarPagosTransferenciasTodos(DownloadFileTransferenciaInput body);
        Task<GestionPagosEvent> handleConfirmarPagosTransferenciasAsync(ConfirmarPagosTransferenciasInput body, string serverIp);
        object handleObtenerPagosTransferencias(DownloadFileTransferenciaInput body);
        object handleRechazadosPagosTransferencias(ConfirmarPagosTransferenciasInput param);
        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInput param, int idEstadoComision, int idTipoComisionPagoComision);
        public bool VerificarSiExisteAutorizacionFormaPagoCiclo(string usuarioLogin, int idCiclo);
        public RespuestaPorTipoPagoModel VerificarTipoPagoCiclo(int idCiclo, string usuarioLogin, int idEstadoComision, int idTipoComisionPagoComision, int idTipoFormaPago);
        public RespuestaPorTipoPagoModel VerificarTransaccionRechazadoMontoCero(int idCiclo, string usuarioLogin, int idEstadoComision, int idTipoComisionPagoComision, int idTipoFormaPago);
        public int CerrarPagoComisionPorTipoComision(CerrarPagoParam param, int idTipoComision);
        public object BuscarFreelancerPagosTransferencias(DownloadFileTransferenciaInput param);
        public List<RespuestaDetalleComision> ObtenerDetalleComision(ParametrosDetalleComision param);
    }
}
