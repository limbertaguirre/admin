using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerRezagadosPago
    {
        public int IdComision { get; set; }
        public int? IdTipoComision { get; set; }
        public int? IdCiclo { get; set; }
        public string Nombre { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int IdListaFormasPago { get; set; }
        public int? IdEstadoListadoFormaPago { get; set; }
        public bool? EstadoListadoFormaPagoHabilitado { get; set; }
        public int IdComisionesDetalle { get; set; }
        public int IdComisionDetalleEmpresa { get; set; }
        public byte? IdEstadoComisionDetalleEmpresa { get; set; }
        public int IdTipoPago { get; set; }
        public DateTime? FechaCreacionComision { get; set; }
        public DateTime? FechaActualizacionComision { get; set; }
        public int IdFicha { get; set; }
        public string CodigoDeCliente { get; set; }
        public string NroDeCuenta { get; set; }
        public string NombreBanco { get; set; }
        public string NombreDeCliente { get; set; }
        public string DocDeIdentidad { get; set; }
        public decimal? ImportePorEmpresa { get; set; }
        public decimal ImporteNeto { get; set; }
        #nullable enable
        public string? FechaDePago { get; set; }
        public int FormaDePago { get; set; }        
        public string? MonedaDestino { get; set; }
        public string? EntidadDestino { get; set; }
        public string? SucursalDestino { get; set; }
        public string? Glosa { get; set; }
    }
}
