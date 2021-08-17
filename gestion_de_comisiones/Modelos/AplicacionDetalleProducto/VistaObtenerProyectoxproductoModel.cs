using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.AplicacionDetalleProducto
{
    public class VistaObtenerProyectoxproductoModel
    {
        public VistaObtenerProyectoxproductoModel()
        {
        }

        public VistaObtenerProyectoxproductoModel(int idProyecto, int idEmpresa, string producto, string nombreProyecto, string nombreEmpresa)
        {
            IdProyecto = idProyecto;
            IdEmpresa = idEmpresa;
            Producto = producto;
            NombreProyecto = nombreProyecto;
            NombreEmpresa = nombreEmpresa;
        }

        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public int IdEmpresa { get; set; }
        public string Producto { get; set; }
        public string NombreEmpresa { get; set; }
    }
}
