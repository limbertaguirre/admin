using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenercomisione
    {
        public int IdComisionDetalle { get; set; }
        public int IdComision { get; set; }
        public int? IdFicha { get; set; }
        public string Nombre { get; set; }
        public string Ci { get; set; }
        public string CuentaBancaria { get; set; }
        public int IdBanco { get; set; }
        public string NombreBanco { get; set; }
        public decimal? MontoBruto { get; set; }
        public string Factura { get; set; }
        public decimal? MontoNeto { get; set; }
        public string FacturaDescuento { get; set; }
        public int? IdCiclo { get; set; }
        public string Ciclo { get; set; }
        public int? IdEstadoComision { get; set; }
    }
}
