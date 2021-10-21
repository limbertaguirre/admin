using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Venta
    {
        public int IdVenta { get; set; }
        public string CodigoProducto { get; set; }
        public int IdFicha { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime? FechaVenta { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal Descuento { get; set; }
        public decimal MontoNeto { get; set; }
        public decimal PorcentajeCuotaInicial { get; set; }
        public decimal MontoCuotaInicial { get; set; }
        public int ClienteId { get; set; }
        public int ReferidoId { get; set; }
        public int ComplejoId { get; set; }
        public bool EsComisionable { get; set; }
        public string Manzano { get; set; }
        public string Lote { get; set; }
        public int Estado { get; set; }
        public int VentaConexionId { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
