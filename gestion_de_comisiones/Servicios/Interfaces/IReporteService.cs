using System;
using System.Collections.Generic;
using gestion_de_comisiones.Modelos.Reporte;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IReporteService
    {
        public object obtenerReporteCiclo(int idCiclo, int mode);
        public object obtenerReporteDetalleCiclo(string idComisionDetalle);
        public object buscarFreelancerPorNombre(string query);
        public object obtenerReportePorFreelancer(int idFicha);
        public object listaCiclosReporte();
    }
}
