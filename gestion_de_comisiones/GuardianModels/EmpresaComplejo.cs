using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.GuardianModels
{
    public partial class EmpresaComplejo
    {
        public int IdEmpresaComplejo { get; set; }
        public int EmpresaId { get; set; }
        public long ComplejoId { get; set; }
        public bool Estado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
