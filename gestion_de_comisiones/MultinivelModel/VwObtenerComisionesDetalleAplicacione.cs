using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class VwObtenerComisionesDetalleAplicacione
    {
        public int IdAplicacionDetalleProducto { get; set; }
        public int IdComisionDetalle { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public int IdProyecto { get; set; }
        public int? IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
    }
}
