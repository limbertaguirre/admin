using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class LogPagoMasivoSionPayComisionOEmpresaFail
    {
        public int IdSionPayComisioEmpresaFail { get; set; }
        public int IdCiclo { get; set; }
        public int IdFicha { get; set; }
        public string Carnet { get; set; }
        public string CuentaSionPay { get; set; }
        public int IdDetalleComision { get; set; }
        public int? IdDetalleComisionEmpresa { get; set; }
        public decimal? Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int IdEmpresaCnx { get; set; }
        public string NombreEmpresa { get; set; }
    }
}
