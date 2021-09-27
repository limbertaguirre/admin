using gestion_de_comisiones.Modelos.FormaPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IFormaPagoRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision);
        public object GetComisiones(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura);
        public List<TipoPagoInputmodel> ListarFormaPagos(ParamFormaPagosOutputModel param);
    }
}
