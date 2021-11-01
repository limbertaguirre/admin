using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpPorrateroDetalleIncentivo
    {
        public int IdGpPorrateroDetalleIncentivo { get; set; }
        public decimal Monto { get; set; }
        public int Estado { get; set; }
        public int IdEmpresaAsume { get; set; }
        public int IdFichaIncentivo { get; set; }
        public int? IdGpEstadoProrrateoDetalleIncentivo { get; set; }
        public int ReciboId { get; set; }
        public int ComprobanteId { get; set; }
        public string Gestion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
