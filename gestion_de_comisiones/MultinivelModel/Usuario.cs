using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Usuario1 { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Corporativo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdRol { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdArea { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool? Estado { get; set; }
    }
}
