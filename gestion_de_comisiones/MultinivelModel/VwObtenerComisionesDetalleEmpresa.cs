using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerComisionesDetalleEmpresa
    {
        public int IdComisionDetalleEmpresa { get; set; }
        public int IdComisionDetalle { get; set; }
        public string Empresa { get; set; }
        public decimal Monto { get; set; }
        public decimal? MontoAFacturar { get; set; }
        public decimal? MontoTotalFacturar { get; set; }
        public string RespaldoPath { get; set; }
        public string NroAutorizacion { get; set; }
        public int IdEmpresa { get; set; }
        public bool EstadoDetalleEmpresa { get; set; }
    }
}
