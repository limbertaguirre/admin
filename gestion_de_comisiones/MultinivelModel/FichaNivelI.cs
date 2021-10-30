using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class FichaNivelI
    {
        public int IdFichaNivelI { get; set; }
        public int? IdFicha { get; set; }
        public int? IdNivel { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
