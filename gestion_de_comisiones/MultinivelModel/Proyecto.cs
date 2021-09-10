using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Proyecto
    {
        public int IdProyecto { get; set; }
        public string Nombre { get; set; }
        public int IdEmpresa { get; set; }
        public int ProyectoConexionId { get; set; }
        public int ComplejoidGuardian { get; set; }
        public bool Estado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
