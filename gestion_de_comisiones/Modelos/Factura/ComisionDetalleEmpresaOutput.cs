using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class ComisionDetalleEmpresaOutput
    {
        public ComisionDetalleEmpresaOutput(int idComisionDetalleEmpresa, decimal monto, string nroAutorizacion, int idEmpresa, decimal? montoAFacturar, decimal? montoTotalFacturar)
        {
            this.idComisionDetalleEmpresa = idComisionDetalleEmpresa;
            this.monto = monto;
            this.nroAutorizacion = nroAutorizacion;
            this.idEmpresa = idEmpresa;
            this.montoAFacturar = montoAFacturar;
            this.montoTotalFacturar = montoTotalFacturar;
        }

        public int idComisionDetalleEmpresa { get; set; }
        public decimal monto { get; set; }
        public string nroAutorizacion { get; set; }
        public int idEmpresa { get; set; }
        public decimal? montoAFacturar { get; set; }
        public decimal? montoTotalFacturar { get; set; }
    }
}
