using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class AplicacionDetalleProducto
    {
        public int IdAplicacionDetalleProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public decimal Subtotal { get; set; }
        public int IdProyecto { get; set; }
        public string CodigoProducto { get; set; }
        public int IdComisionesDetalle { get; set; }
        public int IdBdqishur { get; set; }
        public int IdTipoAplicaciones { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
