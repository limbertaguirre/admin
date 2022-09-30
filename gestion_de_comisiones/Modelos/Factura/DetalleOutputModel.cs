using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class DetalleOutputModel
    {
        public int idComisionDetalleEmpresa { get; set; }
        public decimal monto { get; set; }
        public string NroAutorizacion { get; set; }
        public int idEmpresa { get; set; }
        public decimal montoAFacturar { get; set; }
        public decimal montoTotalFActurar { get; set; }
        public List<EmpresaOutput> listEmpresa { get; set; }

    }
}
