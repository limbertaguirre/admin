using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class AsignacionEmpresaPago
    {
        public int IdAsignacionEmpresaPago { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTipoPago { get; set; }
        public string Descripcion { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
