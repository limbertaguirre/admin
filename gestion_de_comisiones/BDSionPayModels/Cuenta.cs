using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.BDSionPayModels
{
    public partial class Cuenta
    {
        public int IdCuenta { get; set; }
        public string IdUsuario { get; set; }
        public string NroCuenta { get; set; }
        public decimal SaldoActual { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public decimal SaldoNoTransferible { get; set; }
        public decimal SaldoTransferibleDealer { get; set; }
    }
}
