using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class LogDetalleComisionEmpresaFail
    {
        public int IdDetalleComisioEmpresaFail { get; set; }
        public int IdCiclo { get; set; }
        public int IdFicha { get; set; }
        public int CodigoCliente { get; set; }
        public decimal TotalMontoBruto { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
