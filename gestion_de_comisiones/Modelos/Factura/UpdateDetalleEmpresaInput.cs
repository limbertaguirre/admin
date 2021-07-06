using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class UpdateDetalleEmpresaInput
    {
        public string usuarioLogin { get; set; }
        public int idComisionDetalle { get; set; }
        public int idComisionDetalleEmpresa { get; set; }
        public bool estadoDetalleEmpresa { get; set; }
        public int usuarioId { get; set; }

    }
}
