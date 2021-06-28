using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IFacturaService
    {
        public object obtenerlistCiclos(string usuario);
        public object obtenerlistComisionesPendiente(string usuario, int idCiclo);
        public object buscarComisionesPorNombre(string usuario, int idCiclo, string nombreCriterio);
        public object obtenerListaComisionesDetalleEmpresa(string usuario, int idDetalleEmpresa);
        public object obtenerDetalleMasEmpresas(string usuario, int idComisionDetalleEmpresa);

    }
}
