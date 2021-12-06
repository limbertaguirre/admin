using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class IncentivoPorSionPay
    {
        public int IdIncentivoPlanilla { get; set; }
        public string Usuario { get; set; }
        public string Empresa { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreCliente { get; set; }
        public string CuentaSionpay { get; set; }
        public decimal Monto { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Detalle { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdUsuarioSistema { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
