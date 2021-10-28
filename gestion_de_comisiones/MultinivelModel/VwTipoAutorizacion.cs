using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwTipoAutorizacion
    {
        public int IdTipoAutorizacion { get; set; }
        public string Nombre { get; set; }
        public int CantidadLimite { get; set; }
        public int CantidadAprobacionMinimaArea { get; set; }
    }
}
