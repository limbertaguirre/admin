using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class FacturaService : IFacturaService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<FacturaService> Logger;

        public FacturaService(ILogger<FacturaService> logger, IFacturaRepository repository )
        {
            Logger = logger;
            Repository = repository;
        }
        public IFacturaRepository Repository { get; set; }
        public object obtenerlistCiclos(string usuario)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerlistCiclos() ");
                var ciclos = Repository.listCiclosPendientes(usuario);
                return Respuesta.ReturnResultdo(0, "ok", ciclos);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistCiclos() al obtener lista de ciclos ,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista de ciclos de una factura", "problemas en el servidor, intente mas tarde");
            }
        }

        public object obtenerlistComisionesPendiente(string usuario, int idCiclo)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerlistComisionesPendiente() ");
                int idEstado = 1;// int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION"));
                var comsiones = Repository.obtenerComisiones(usuario, idCiclo, idEstado);
                return Respuesta.ReturnResultdo(0, "ok", comsiones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerlistComisionesPendiente() al obtener lista de comisiones pendientes para facturar,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener la lista comisiones para facturar", "problemas en el servidor, intente mas tarde");
            }
        }
        public object BuscarComisiones(string usuario, int idCiclo, string nombreCriterio)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio buscarComisionesPorNombre() ");
                int idEstado = 1; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION"));
                var comsiones = Repository.BuscarComisiones(usuario, idCiclo, idEstado, nombreCriterio);
                return Respuesta.ReturnResultdo(0, "ok", comsiones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch buscarComisionesPorNombre() al obtener lista de comisiones pendientes para facturar,error mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al buscar comiision por nombre", "problemas en el servidor, intente mas tarde");
            }
        }
        public object obtenerListaComisionesDetalleEmpresa(string usuario, int idDetalleEmpresa)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerListaComisionesDetalleEmpresa() ");
                var comsiones = Repository.obtenerComisionDetalle(usuario, idDetalleEmpresa);
                return Respuesta.ReturnResultdo(0, "ok", comsiones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerListaComisionesDetalleEmpresa() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener datos de detalle empresa","");
            }
        }
        public object obtenerDetalleMasEmpresas(string usuario, int idComisionDetalleEmpresa)
        {
            try
            {
                Logger.LogInformation($"usuario : {usuario} inicio el servicio obtenerDetalleMasEmpresas() ");
                var comsiones = Repository.obtenerComisionDetalleEmpresa(usuario, idComisionDetalleEmpresa);
                return Respuesta.ReturnResultdo(0, "ok", comsiones);
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {usuario} error catch obtenerDetalleMasEmpresas() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al obtener datos de detalle empresa mas empresas", "");
            }
        }
        public object ACtualizarComisionDetalleAFacturado(ComisionDetalleInput comisionDetalle)
        {
            try
            {
                Logger.LogInformation($"usuario : {comisionDetalle.usuarioLogin} inicio el servicio ACtualizarComisionDetalleAFacturado() ");
                int idEstadoFacturado = 2;// int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_SI_FACTURO"));
                var comsiones = Repository.AcTualizarComisionDetalleEstado(comisionDetalle, idEstadoFacturado);
                if (comsiones)
                {
                    return Respuesta.ReturnResultdo(0, "ok", comsiones);
                }
                else
                {
                    Logger.LogInformation($"usuario : {comisionDetalle.usuarioLogin} inicio RETORNO FALSE LA ACTUALIZACION, NO SE ACTUALIZO");
                    return Respuesta.ReturnResultdo(1, "problemas al actualizar la comision del detalle", comsiones);
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {comisionDetalle.usuarioLogin} error catch ACtualizarComisionDetalleAFacturado() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al actualizar una comision detalle a facturado", "");
            }
        }
        public object ActualizarDetalleEmpresaEstado(UpdateDetalleEmpresaInput detalle)
        {
            try
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio el servicio ActualizarDetalleEmpresaEstado() ");                
                var comsiones = Repository.ActualizarEstadoFacturarEmpresa(detalle.usuarioLogin, detalle.usuarioId, detalle.idComisionDetalle, detalle.idComisionDetalleEmpresa, detalle.estadoDetalleEmpresa);
                if (comsiones)
                {
                    return Respuesta.ReturnResultdo(0, "ok", comsiones);
                } else {
                    Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio RETORNO FALSE LA ACTUALIZACION, NO SE ACTUALIZO  el estado comision detalle empresa");
                    return Respuesta.ReturnResultdo(1, "problemas al actualizar el estado comision del detalle empresa", comsiones);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} error catch ActualizarDetalleEmpresaEstado() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al actualizar el estado  comision detalle empresa a facturado", "");
            }
        }
        public object subirArchivoPdf(SubirArchivoInput detalle)
        {
            try
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio el servicio subirArchivoPdf() iddetalleEmpresa: {detalle.idComisionDetalleEmpresa } ");
                var comsiones = Repository.SubirArchivo(detalle.usuarioLogin, detalle.usuarioId , detalle.idComisionDetalleEmpresa, detalle.archivopdf);
                if (comsiones)
                {
                    return Respuesta.ReturnResultdo(0, "ok", comsiones);
                }
                else
                {
                    Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio RETORNO FALSE LA subirArchivoPdf");
                    return Respuesta.ReturnResultdo(1, "problemas al subir el archivo pdf", comsiones);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} error catch subirArchivoPdf() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al actualizar el archivo pdf", "");
            }
        }

        public object AplicarFacturadoTodo(FacturadoTodoInput detalle)
        {
            try
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio el servicio AplicarFacturadoTodo() iddetalleEmpresa: {detalle.idComisionDetalle } ");
                var comsiones = Repository.AplicarFacturadoEstadoFacturarEmpresa(detalle.usuarioLogin, detalle.usuarioId, detalle.idComisionDetalle, detalle.estadoFacturado);
                if (comsiones)
                {
                    return Respuesta.ReturnResultdo(0, "ok", comsiones);
                }
                else
                {
                    Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio RETORNO FALSE LA AplicarFacturadoTodo");
                    return Respuesta.ReturnResultdo(1, "problemas al subir el archivo pdf", comsiones);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} error catch AplicarFacturadoTodo() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "problemas al actualizar el archivo pdf", "");
            }
        }
        public object CerrarFactura(CerrarFacturaInput detalle)
        {
            try
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio el servicio CerrarFactura()  ");
                var comsiones = Repository.CerrarFactura(detalle.usuarioLogin, detalle.usuarioId, detalle.idCiclo );
                if (comsiones)
                {
                    return Respuesta.ReturnResultdo(0, "Se realizo el cierre de la facturación con éxito.", comsiones);
                }
                else
                {
                    Logger.LogInformation($"usuario : {detalle.usuarioLogin} inicio RETORNO FALSE al CerrarFactura()");
                    return Respuesta.ReturnResultdo(1, "No se pudo procesar la factura", comsiones);
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation($"usuario : {detalle.usuarioLogin} error catch CerrarFactura() mensaje: {ex.Message}");
                return Respuesta.ReturnResultdo(1, "Problemas al cerrar la factura", "");
            }
        }




    }
}
