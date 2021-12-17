using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.BDSionPayModels
{
    public partial class Usuario
    {
        public string IdUsuario { get; set; }
        public string Usuario1 { get; set; }
        public int IdEstadoUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
