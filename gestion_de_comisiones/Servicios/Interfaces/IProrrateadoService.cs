using gestion_de_comisiones.Modelos.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IProrrateadoService
    {
        public object GetCiclos(string usuario);
        public object GetComisionesPendienteAplicaciones(string usuario, int idCiclo);
        public object ListarComisionesAplicacionesPendientesPorCarnet(BuscarInputModel param);
    }
}
