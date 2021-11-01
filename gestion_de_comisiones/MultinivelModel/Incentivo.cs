using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Incentivo
    {
        public int IdIncentivo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int Estado { get; set; }
        public int IdNiveles { get; set; }
        public int IdTipoIncentivo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
