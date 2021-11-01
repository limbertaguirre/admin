using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenercomisionesFormaPago
    {
        public int IdComisionDetalle { get; set; }
        public int IdComision { get; set; }
        public int? IdTipoComision { get; set; }
        public int? IdFicha { get; set; }
        public string Nombre { get; set; }
        public string Ci { get; set; }
        public string CuentaBancaria { get; set; }
        public int IdBanco { get; set; }
        public string NombreBanco { get; set; }
        public decimal? MontoBruto { get; set; }
        public string Factura { get; set; }
        public decimal? MontoNeto { get; set; }
        public int? EstadoFacturoId { get; set; }
        public string EstadoDetalleFacturaNombre { get; set; }
        public int? IdCiclo { get; set; }
        public string Ciclo { get; set; }
        public int? IdEstadoComision { get; set; }
        public decimal? MontoRetencion { get; set; }
        public decimal? MontoAplicacion { get; set; }
        public int? IdListaFormasPago { get; set; }
        public int? IdTipoPago { get; set; }
        public string TipoPagoDescripcion { get; set; }
        public int? IdDetalleEstadoFormaPago { get; set; }
        public bool? PagoDetalleHabilitado { get; set; }
        public int? IdEstadoListadoFormaPago { get; set; }
    }
}
