using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public interface IBaseRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision);
        public object GetComisiones(string usuario, int idCiclo, int idEstadoComision);
    }
}
