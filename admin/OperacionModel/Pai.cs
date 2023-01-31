using System;
using System.Collections.Generic;

#nullable disable

namespace admin.OperacionModel
{
    public partial class Pai
    {
        public int IdPais { get; set; }
        public string Nombre { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
