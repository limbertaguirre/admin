using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.AplicacionDetalleProducto
{
    public class WwObtenerComisionesDetalleAplicacionesModel
    {
        public WwObtenerComisionesDetalleAplicacionesModel(int idAplicacionDetalleProducto, int idComisionDetalle, string descripcion, decimal monto, int cantidad, decimal subtotal, int idProyecto, int? idEmpresa, string nombreEmpresa, string codigoProducto)
        {
            IdAplicacionDetalleProducto = idAplicacionDetalleProducto;
            IdComisionDetalle = idComisionDetalle;
            Descripcion = descripcion;
            Monto = monto;
            Cantidad = cantidad;
            Subtotal = subtotal;
            IdProyecto = idProyecto;
            IdEmpresa = idEmpresa;
            NombreEmpresa = nombreEmpresa;
            CodigoProducto = codigoProducto;
        }

        public int IdAplicacionDetalleProducto { get; set; }
        public int IdComisionDetalle { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public int IdProyecto { get; set; }
        public int? IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string CodigoProducto { get; set; }
    }
}
