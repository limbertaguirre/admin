using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IFacturaRepository
    {
        public object listCiclosPendientes(string usuario);
        public object obtenerComisiones(string usuario, int idCiclo, int idEstadoComision);
    }
}
