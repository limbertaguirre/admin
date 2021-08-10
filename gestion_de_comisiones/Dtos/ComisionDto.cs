using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers.Dtos
{
    public class ComisionDto
    {
        public int IdComision { get; set; }
        public decimal? MontoTotalBruto { get; set; }
        public decimal? PorcentajeRetencion { get; set; }
        public decimal? MontoTotalRetencion { get; set; }
        public decimal? MontoTotalAplicacion { get; set; }
        public decimal? MontoTotalNeto { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdTipoComision { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
