using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.AplicacionDetalleProducto
{
    public class RegistroDescuentoInputModel
    {
        public string usuarioLogin { get; set; }
        public string producto { get; set; }
        public decimal monto { get; set; }
        public int cantidad { get; set; }
        public string descripcion { get; set; }
        public int  idProyecto { get; set; }
        public int idComisionDetalle { get; set; }

    }
}
