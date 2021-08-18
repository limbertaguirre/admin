using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IAplicacionesService
    {
        public object GetCiclos(string usuario);
        public object GetAplicacionesPendientes(string usuario, int idCiclo);
        public object obtenerDetalleAplicacionesXFreelancers(DetalleAplicacionesFichaInputModel param);
        public object obtenerProyectoXproduto(GetProyectoImputModel param);
        public object RegistrarDescuentoComisionDetalle(RegistroDescuentoInputModel param);

    }
}
