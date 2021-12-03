using gestion_de_comisiones.Modelos.GestionPagos;
using System;
namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IGestionPagosRezagadosRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionPagoComision);
        public object GetComisionesPagos(string usuario, int idCiclo, int idEstadoComision, int idTipoComisionPagoComision);
        public object ObtenerPagosRezagadosTransferencias(ObtenerRezagadosPagosTransferenciasInput param);
    }
}
