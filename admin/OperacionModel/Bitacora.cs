using System;
using System.Collections.Generic;

#nullable disable

namespace admin.OperacionModel
{
    public partial class Bitacora
    {
        public int IdBitacora { get; set; }
        public int? IdPagina { get; set; }
        public string Ip { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
