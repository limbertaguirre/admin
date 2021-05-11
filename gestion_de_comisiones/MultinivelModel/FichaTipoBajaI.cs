using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class FichaTipoBajaI
    {
        public int IdFichaTipoBajaI { get; set; }
        public string Motivo { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int? IdFicha { get; set; }
        public int? IdTipoBaja { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
