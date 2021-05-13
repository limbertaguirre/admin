using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class BitacoraDetalle
    {
        public int IdBitacoraDetalle { get; set; }
        public int? IdBitacora { get; set; }
        public string Tabla { get; set; }
        public string Accion { get; set; }
        public int? IdTupla { get; set; }
        public string Campos { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
