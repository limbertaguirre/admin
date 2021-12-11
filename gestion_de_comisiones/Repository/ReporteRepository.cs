using System;
using gestion_de_comisiones.MultinivelModel;
using System.Collections.Generic;
using gestion_de_comisiones.Modelos.Reporte;
using System.Linq;
using gestion_de_comisiones.Repository.Interfaces;

namespace gestion_de_comisiones.Repository
{
    public class ReporteRepository : IReporteRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();

        public List<Ficha> listaFichaClientes(string query)
        {
            string[] stringSplitted = query.Split(" ");

            var predicate = PredicateBuilder.False<Ficha>();
            foreach (string queryString in stringSplitted)
            {
                predicate = predicate.Or(p => p.Ci.Contains(queryString));
                predicate = predicate.Or(p => p.Nombres.Contains(queryString));
                predicate = predicate.Or(p => p.Apellidos.Contains(queryString));
            }
                
            var result = contextMulti.Fichas.AsQueryable()
               .Where(predicate)
               .Take(50)
               .ToList();

            return result;
        }

        public List<ReporteCicloModel> listaReporteCiclos(int idCiclo, int mode)
        {           
            var result =  contextMulti.FfReportePorCiclo(idCiclo, mode).OrderByDescending(t => t.montoNeto).ToList();
            return result;
        }

        public List<ReporteDetalleCicloModel> listaReporteDetalleCiclo(int idComisionDetalle)
        {
            var result = contextMulti.FfReporteDetalleCiclo(idComisionDetalle).OrderByDescending(t => t.montoNeto).ToList();
            return result;
        }

        public List<ReporteFreelancerModel> listaReportePorFreelancer(int idFicha)
        {
            var result = contextMulti.FfReporteFreelancer(idFicha).ToList();
            return result;
        }
    }
}
