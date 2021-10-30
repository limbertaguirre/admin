using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerInfoExcelFormatoBanco
    {
        public int? IdCiclo { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int IdComisionesDetalle { get; set; }
        public string CodigoDeCliente { get; set; }
        public string NroDeCuenta { get; set; }
        public string NombreDeCliente { get; set; }
        public string DocDeIdentidad { get; set; }
        public decimal? ImportePorEmpresa { get; set; }
        public decimal ImporteNeto { get; set; }
        public string FechaDePago { get; set; }
        public int FormaDePago { get; set; }
        public int MonedaDestino { get; set; }
        public int EntidadDestino { get; set; }
        public string Glosa { get; set; }
        public int IdTipoPago { get; set; }
    }
}
