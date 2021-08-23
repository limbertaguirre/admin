using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IProrrateadoRepository
    {
        public object GetCiclos(string usuario, int idEstadoComision);

    }
}
