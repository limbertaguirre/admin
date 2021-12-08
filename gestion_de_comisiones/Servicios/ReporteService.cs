using System;
using System.Collections.Generic;
using gestion_de_comisiones.Modelos.Reporte;
using gestion_de_comisiones.Repository;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
namespace gestion_de_comisiones.Servicios
{
    public class ReporteService : IReporteService
    {

        ConfiguracionService respuesta = new ConfiguracionService();

        private readonly ILogger<ReporteService> logger;

        public IReporteRepository reporteRepository { get; }

        public ReporteService(ILogger<ReporteService> logger, IReporteRepository reporteRepository)
        {
            this.logger = logger;
            this.reporteRepository = reporteRepository;
        }

        public object obtenerReporteCiclo(int idCiclo)
        {
            var listReporte =  reporteRepository.listaReporteCiclos(idCiclo);
            return respuesta.ReturnResultdo(0, "Ok", listReporte);
        }

        public object obtenerReporteDetalleCiclo(int idComisionDetalle)
        {
            var listReporte = reporteRepository.listaReporteDetalleCiclo(idComisionDetalle);
            return respuesta.ReturnResultdo(0, "Ok", listReporte);
        }

        public object buscarFreelancerPorNombre(string query)
        {
            var listaFichas = reporteRepository.listaFichaClientes(query);
            return respuesta.ReturnResultdo(0, "Ok", listaFichas);
        }

        public object obtenerReportePorFreelancer(int idFicha)
        {
            var listaReporte = reporteRepository.listaReportePorFreelancer(idFicha);
            return respuesta.ReturnResultdo(0, "Ok", listaReporte);
        }
    }
}
