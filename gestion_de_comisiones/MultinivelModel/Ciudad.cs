using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Ciudad
    {
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }
        public int? IdPais { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string Codigo { get; set; }
    }
}
