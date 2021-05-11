using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpProrrateoDetalle
    {
        public int IdGpPorrateoDetalle { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public int IdGpEstadoProrrateoDetalle { get; set; }
        public int IdEmpresaPresta { get; set; }
        public int IdEmpresaRecibe { get; set; }
        public int IdAplicacionDetalleProducto { get; set; }
        public int ReciboId { get; set; }
        public int ComprobanteId { get; set; }
        public string Gestion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
