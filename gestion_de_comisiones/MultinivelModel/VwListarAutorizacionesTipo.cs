using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwListarAutorizacionesTipo
    {
        public int IdUsuarioAutorizacion { get; set; }
        public int IdArea { get; set; }
        public string DescripcionArea { get; set; }
        public bool Estado { get; set; }
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public int IdTipoAutorizacion { get; set; }
        public string NombreTipoAutorizacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
