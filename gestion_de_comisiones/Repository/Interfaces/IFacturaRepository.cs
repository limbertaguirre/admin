using gestion_de_comisiones.Modelos.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository.Interfaces
{
    public interface IFacturaRepository
    {
        public object listCiclosPendientes(string usuario);
        public object obtenerComisiones(string usuario, int idCiclo, int idEstadoComision);
        public object buscarcomisionXnombre(string usuario, int idCiclo, int idEstadoComision, string nombreCriterio);
        public List<VwObtenerComisionesDetalleEmpresaModel> obtenerDetalleEmpresa(string usuario, int idComisionDetalle);
        public object obtenerComisionDetalle(string usuario, int idComisionDetalle);
        public List<EmpresaOutput> obtenerEmpresas(string usuario);
        public DetalleOutputModel obtenerComisionDetalleEmpresa(string usuario, int idComisionDetalle);
        public bool AcTualizarComisionDetalleEstado(ComisionDetalleInput comision, int estadoFacturado);
        public bool ActualizarEstadoFacturarEmpresa(string usuarioLogin, int usuarioId, int idComisionDetalle, int idComisionDetalleEmpresa, bool estadoDetalleEmpresa);
        public bool SubirArchivo(string usuarioLogin, int usuarioId, int idComisionDetalleEmpresa, string archivoPdf);
        public bool AplicarFacturadoEstadoFacturarEmpresa(string usuarioLogin, int usuarioId, int idComisionDetalle, bool estadoFacturado);
        public bool CerrarFactura(string usuarioLogin, int usuarioId, int idCiclo);

    }
}
