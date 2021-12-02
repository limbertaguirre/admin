using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerEmpresasComisionesDetalleEmpresa
    {
        public int? IdCiclo { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int? IdTipoComision { get; set; }
        public int IdTipoPago { get; set; }
        public int? IdEstadoComision { get; set; }
        public decimal? MontoTransferir { get; set; }
    }
}
