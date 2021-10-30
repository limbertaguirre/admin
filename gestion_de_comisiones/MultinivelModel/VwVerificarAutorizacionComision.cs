using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwVerificarAutorizacionComision
    {
        public int IdUsuarioAutorizacion { get; set; }
        public int IdUsuario { get; set; }
        public int? IdAutorizacionComision { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdComision { get; set; }
        public int? IdEstadoAutorizacionComision { get; set; }
        public string Descripcion { get; set; }
    }
}
