using System;
using System.Collections.Generic;

#nullable disable

namespace admin.OperacionModel
{
    public partial class Sucursal
    {
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
