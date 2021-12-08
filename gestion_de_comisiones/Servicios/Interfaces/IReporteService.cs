using System;
using System.Collections.Generic;
using gestion_de_comisiones.Modelos.Reporte;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IReporteService
    {
        public object obtenerReporteCiclo(int idCiclo);
        public object obtenerReporteDetalleCiclo(int idComisionDetalle);
        public object buscarFreelancerPorNombre(string query);
        public object obtenerReportePorFreelancer(int idFicha);
    }
}
