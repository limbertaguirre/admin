using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Factura
{
    public class SubirArchivoInput
    {
        public string usuarioLogin { get; set; }
        public int idComisionDetalleEmpresa { get; set; }
        public string archivopdf { get; set; }
        public int usuarioId { get; set; }

    }
}
