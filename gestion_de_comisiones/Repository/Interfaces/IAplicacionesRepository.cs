using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IAplicacionesRepository 
    {
        public object GetCiclos(string usuario, int idEstadoComision);
        public object GetComisiones(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura);
        public object ListarFreelancerYAplicacionesProductos(DetalleAplicacionesFichaInputModel param);
        public VistaObtenerProyectoxproductoModel obtenerproyectoXProducto(GetProyectoImputModel param);
        public bool RegistrarDecuentoComisionDetalle(RegistroDescuentoInputModel param);
        public ComisionDetalleModel ObtenerComisionDetalle(string usuarioNombre, int idDetalleComision);
        public object GetComisionesPorCarnet(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, string carnet);
        public bool CerrarAplicacionCiclo(CerrarAplicacionInputModel model);

    }
}
