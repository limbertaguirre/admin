﻿using System;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Modelos.GestionPagosRezagados;

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
        public object BuscarFreelancerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param);
        public bool PagarComisionRezagadosSionPayTodo(PagoRezagadoInput param);
        public RespuestaSionPayModel VerificarPagoRezagadoSionPay(PagoRezagadoInput param, int idEstadoComision, int idTipoComision, int idTipoFormaPagoSionPay, int idEstadoListaFormaPago);
        public RespuestaSionPayModel ValidarCantidadComisionRezagada(PagoRezagadoInput param, int idEstadoComision, int idTipoComision, int idTipoFormaPagoSionPay);
    }
}
