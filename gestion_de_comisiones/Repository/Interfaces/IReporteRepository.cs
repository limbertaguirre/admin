using System;
using System.Collections.Generic;
using gestion_de_comisiones.Modelos.Reporte;
using gestion_de_comisiones.MultinivelModel;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IReporteRepository
    {
        public List<ReporteCicloModel> listaReporteCiclos(int idCiclo, int mode);
        public List<ReporteDetalleCicloModel> listaReporteDetalleCiclo(int idComisionDetalle);
        public List<Ficha> listaFichaClientes(string query);
        public List<ReporteFreelancerModel> listaReportePorFreelancer(int idFicha);
    }
}
