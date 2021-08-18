﻿using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class AplicacionesRepository : IAplicacionesRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<AplicacionesRepository> Logger;
        public AplicacionesRepository(ILogger<AplicacionesRepository> logger)
        {
            Logger = logger;
        }

        public object GetCiclos(string usuario, int idEstadoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision}, => AplicacionesRepository|getCiclos");             
                var ciclosR = contextMulti.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision).ToList();
                List<CicloDto> ciclos = new List<CicloDto>();
                foreach(var c in ciclosR)
                {
                    Logger.LogInformation($" usuario: {usuario} ciclosR => IdCiclo: {c.IdCiclo} Nombre: {c.Nombre} Estado: {c.Estado}");
                    ciclos.Add(new CicloDto(c.IdCiclo, c.Nombre));
                }
                return ciclos;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() mensaje : {ex}");
                List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }

        public object GetComisiones(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerComisionesPendientes() ");
                Logger.LogWarning($" usuario: {usuario} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision && x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getComisiones() mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        public object ListarFreelancerYAplicacionesProductos(DetalleAplicacionesFichaInputModel param)
        {
            try
            {
                DetalleAplicacionOutputModel objDetalle = new DetalleAplicacionOutputModel();
                var objComision = contextMulti.VwObtenercomisiones.Where(x => x.IdComisionDetalle == param.idComisionDetalle).FirstOrDefault();
                if(objComision != null)
                {
                    objDetalle.idFicla = (int)objComision.IdFicha;
                    objDetalle.nombreFicha = objComision.Nombre;
                    objDetalle.ciclo = objComision.Ciclo;
                    objDetalle.idCiclo = (int)objComision.IdCiclo;
                    var objCli = contextMulti.Fichas.Where(x => x.IdFicha == objComision.IdFicha).Select(p => new { p.IdFicha, p.Avatar }).FirstOrDefault();
                    if (objCli != null)
                    {
                        objDetalle.avatar = objCli.Avatar;
                    }
                    else
                    {
                        objDetalle.avatar = "";
                    }
                    var objNivel = contextMulti.FichaNivelIs.Join(contextMulti.Nivels,
                                                FichaNivelI => FichaNivelI.IdNivel,
                                                Nivel => Nivel.IdNivel,
                                                (FichaNivelI, Nivel) => new
                                                {
                                                    nombre = Nivel.Nombre,
                                                    idFicha = FichaNivelI.IdFicha,
                                                    fechaCreacion = FichaNivelI.FechaCreacion,
                                                    habilitado = FichaNivelI.Habilitado,
                                                }).Where(x => x.idFicha == objComision.IdFicha && x.habilitado == true).OrderByDescending(x => x.fechaCreacion).FirstOrDefault();
                    if (objNivel != null)
                    {
                        objDetalle.rango = objNivel.nombre;
                    }
                    else
                    {
                        objDetalle.rango = "";
                    }
                    objDetalle.listAplicaciones = obtenerDetalleAplicacionXId(param.usuarioLogin, param.idComisionDetalle);

                }
               
                return objDetalle;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch get detalle aplicaciones () mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        private List<WwObtenerComisionesDetalleAplicacionesModel> obtenerDetalleAplicacionXId(string usuario, int idComisionDetalle)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerDetalleAplicacionXId()  idComisionDetalle: {idComisionDetalle} ");
                Logger.LogWarning($" usuario: {usuario} parametros: idComisionDetalle:{idComisionDetalle} ");
                var ListComisiones = contextMulti.VwObtenerComisionesDetalleAplicaciones.Where(x => x.IdComisionDetalle == idComisionDetalle ).Select(p => new WwObtenerComisionesDetalleAplicacionesModel(p.IdAplicacionDetalleProducto, p.IdComisionDetalle, p.Descripcion, p.Monto, p.Cantidad, p.Subtotal, p.IdProyecto, p.IdEmpresa, p.NombreEmpresa, p.CodigoProducto)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerDetalleAplicacionXId() mensaje : {ex}");
                List<WwObtenerComisionesDetalleAplicacionesModel> list = new List<WwObtenerComisionesDetalleAplicacionesModel>();
                return list;
            }
        }
        public VistaObtenerProyectoxproductoModel obtenerproyectoXProducto(GetProyectoImputModel param)
        {
            try
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository obtenerproyectoXProducto() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros de busqueda: idproducto:{param.producto} ");
                var ListComisiones = contextMulti.VwObtenerProyectoxProductoes.Where(x => x.Producto == param.producto).Select(p => new VistaObtenerProyectoxproductoModel(p.IdProyecto, p.IdEmpresa, p.Producto, p.NombreProyecto, p.NombreEmpresa)).FirstOrDefault();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch obtenerproyectoXProducto() mensaje : {ex}");
                VistaObtenerProyectoxproductoModel obj = new VistaObtenerProyectoxproductoModel();
                return obj;
            }
        }

        public bool RegistrarDecuentoComisionDetalle(RegistroDescuentoInputModel param)
        {
            Logger.LogInformation($" usuario: {param.usuarioLogin} -  inicio el RegistrarDecuentoComision() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var objDetalle = context.GpComisionDetalles.Where(x => x.IdComisionDetalle == param.idComisionDetalle).FirstOrDefault();
                        objDetalle.MontoAplicacion = objDetalle.MontoAplicacion + param.monto;
                        objDetalle.MontoNeto = objDetalle.MontoNeto - param.monto;
                        context.SaveChanges();

                        AplicacionDetalleProducto objApli = new AplicacionDetalleProducto();
                        objApli.CodigoProducto = param.producto;
                        objApli.Monto = param.monto;
                        objApli.IdComisionesDetalle = param.idComisionDetalle;
                        objApli.Descripcion = param.descripcion;
                        objApli.Cantidad = param.cantidad;
                        objApli.IdUsuario = param.usuarioId;
                        objApli.Subtotal = param.monto;
                        objApli.IdProyecto = param.idProyecto;
                        objApli.IdBdqishur = 0;
                        objApli.FechaActualizacion = DateTime.Now;
                        objApli.FechaCreacion = DateTime.Now;
                        context.AplicacionDetalleProductoes.Add(objApli);
                        context.SaveChanges();
                        Logger.LogInformation($" usuario: {param.usuarioLogin} -  se registro el descuento con el idAplicacionDetalleProducto = {objApli.IdAplicacionDetalleProducto}");
                        dbcontextTransaction.Commit();
                        Logger.LogInformation($" usuario: {param.usuarioLogin}-  SE REGISTRO EXITOSAMENTE ");
                        return true;                      
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($" usuario: {param.usuarioLogin} -  hubo un error  RegistrarDecuentoComision() en repos mensaje : {ex.Message}");
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public ComisionDetalleModel ObtenerComisionDetalle( string usuarioNombre ,int idDetalleComision)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuarioNombre} inicio el repository obtenerComisionesPendientes() ");
                Logger.LogWarning($" usuario: {usuarioNombre} parametros: iDetalleComision:{idDetalleComision}");
                var objDetalle = contextMulti.GpComisionDetalles.Where(x => x.IdComisionDetalle == idDetalleComision).Select(p => new ComisionDetalleModel(p.IdComisionDetalle, p.MontoBruto, p.PorcentajeRetencion, p.MontoRetencion, p.MontoAplicacion, p.MontoNeto, p.IdComision, p.IdFicha, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion)).FirstOrDefault();                
                return objDetalle;
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario: {usuarioNombre} error catch getComisiones() mensaje : {ex}");
                ComisionDetalleModel obj = new ComisionDetalleModel();
                return obj;
            }
        }

        public object GetComisionesPorCarnet(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, string carnet)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository GetComisionesPorCarnet() ");
                Logger.LogWarning($" usuario: {usuario} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision && (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura) && x.Ci.Contains(carnet.Trim())).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch GetComisionesPorCarnet() mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }

    }

}
