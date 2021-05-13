using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class FichaIncentivo
    {
        public int IdFichaIncentivo { get; set; }
        public decimal Monto { get; set; }
        public int IdFicha { get; set; }
        public int IdIncentivo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
