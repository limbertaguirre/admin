using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IFormaPagoService
    {
        public object GetCiclos(string usuario);
        public object GetFormasPagosPendientes(string usuario, int idCiclo);
    }
}
