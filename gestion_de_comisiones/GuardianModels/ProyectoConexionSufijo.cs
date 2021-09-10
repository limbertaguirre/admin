using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.GuardianModels
{
    public partial class ProyectoConexionSufijo
    {
        public int IdProyectoConexionSufijo { get; set; }
        public int IdEmpresaComplejo { get; set; }
        public long IdProyectoCnx { get; set; }
        public string Sufijo { get; set; }
        public bool? Estado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
