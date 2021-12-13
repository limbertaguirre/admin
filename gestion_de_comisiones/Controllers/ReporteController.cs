using System;
using gestion_de_comisiones.Modelos.Reporte;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Controllers
{
    public class ReporteController: Controller
    {
        private readonly ILogger<ReporteController> logger;

        public IReporteService service { get; }

        public ReporteController(ILogger<ReporteController> logger, IReporteService service)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult obtenerReporteCiclo([FromBody] ReporteCicloModelRequest reporteCicloModelRequest)
        {
            var result = service.obtenerReporteCiclo(reporteCicloModelRequest.idCiclo, reporteCicloModelRequest.mode);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult obtenerReporteDetalleCiclo([FromBody] ReporteDetalleCicloModelRequest reporteDetalleCicloModelRequest)
        {
            var result = service.obtenerReporteDetalleCiclo(reporteDetalleCicloModelRequest.idComisionDetalle);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult buscarFreelancerPorNombre([FromBody] BuscarFichaModelRequest buscarFichaModelRequest)
        {
            var result = service.buscarFreelancerPorNombre(buscarFichaModelRequest.query);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult obtenerReportePorFreelancer([FromBody] ReporteFreelancerModelRequest reporteFreelancerModelRequest)
        {
            var result = service.obtenerReportePorFreelancer(reporteFreelancerModelRequest.idFicha);
            return Ok(result);
        }

        public ActionResult obtenerCiclosReporte()
        {
            var result = service.listaCiclosReporte();
            return Ok(result);
        }

    }
}
