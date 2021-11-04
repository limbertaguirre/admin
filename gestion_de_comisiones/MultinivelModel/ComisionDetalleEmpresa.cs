using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class ComisionDetalleEmpresa
    {
        public int IdComisionDetalleEmpresa { get; set; }
        public decimal Monto { get; set; }
        public byte? Estado { get; set; }
        public string RespaldoPath { get; set; }
        public string NroAutorizacion { get; set; }
        public decimal? MontoAFacturar { get; set; }
        public decimal? MontoTotalFacturar { get; set; }
        public int IdComisionDetalle { get; set; }
        public int IdEmpresa { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public decimal VentasPersonales { get; set; }
        public decimal VentasGrupales { get; set; }
        public decimal Residual { get; set; }
        public decimal Retencion { get; set; }
        public decimal MontoNeto { get; set; }
        public bool SiFacturo { get; set; }
        public long IdComprobanteGenerico { get; set; }
        public int IdMovimiento { get; set; }
    }
}
