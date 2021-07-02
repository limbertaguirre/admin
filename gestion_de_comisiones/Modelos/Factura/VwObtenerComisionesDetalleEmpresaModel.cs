using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class VwObtenerComisionesDetalleEmpresaModel
    {
        public VwObtenerComisionesDetalleEmpresaModel(int idComisionDetalleEmpresa, int idComisionDetalle, string empresa, decimal monto, decimal? montoAFacturar, decimal? montoTotalFacturar, string respaldoPath, string nroAutorizacion, int idEmpresa, bool estadoDetalleEmpresa, decimal ventasPersonales, decimal ventasGrupales, decimal residual, decimal montoNeto, bool siFacturo)
        {
            IdComisionDetalleEmpresa = idComisionDetalleEmpresa;
            IdComisionDetalle = idComisionDetalle;
            Empresa = empresa;
            Monto = monto;
            MontoAFacturar = montoAFacturar;
            MontoTotalFacturar = montoTotalFacturar;
            RespaldoPath = respaldoPath;
            NroAutorizacion = nroAutorizacion;
            IdEmpresa = idEmpresa;
            EstadoDetalleEmpresa = estadoDetalleEmpresa;
            VentasPersonales = ventasPersonales;
            VentasGrupales = ventasGrupales;
            Residual = residual;
            MontoNeto = montoNeto;
            SiFacturo = siFacturo;
        }

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
        public decimal VentasPersonales { get; set; }
        public decimal VentasGrupales { get; set; }
        public decimal Residual { get; set; }
        public decimal Retencion { get; set; }
        public decimal MontoNeto { get; set; }
        public bool SiFacturo { get; set; }


    }
}
