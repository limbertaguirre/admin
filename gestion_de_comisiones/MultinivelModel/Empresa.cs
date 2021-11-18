using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Empresa
    {
        public int IdEmpresa { get; set; }
        public int Codigo { get; set; }
        public int CodigoCnx { get; set; }
        public string Nombre { get; set; }
        public string NombreBd { get; set; }
        public int Estado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
