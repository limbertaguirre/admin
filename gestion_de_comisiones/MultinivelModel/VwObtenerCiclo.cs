using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerCiclo
    {
        public int IdCiclo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdEstadoComision { get; set; }
        public string Estado { get; set; }
    }
}
