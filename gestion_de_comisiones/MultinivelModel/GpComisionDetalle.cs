using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpComisionDetalle
    {
        public int IdComisionDetalle { get; set; }
        public decimal? MontoBruto { get; set; }
        public decimal? PorcentajeRetencion { get; set; }
        public decimal? MontoRetencion { get; set; }
        public decimal? MontoAplicacion { get; set; }
        public decimal? MontoNeto { get; set; }
        public int? IdComision { get; set; }
        public int? IdFicha { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
