using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.GuardianModels;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration Config;
        private readonly BDMultinivelContext ContextMulti;
        private readonly grdsionContext contextGuardian;

        public AplicacionesRepository(ILogger<AplicacionesRepository> logger, IConfiguration config, BDMultinivelContext contextMulti, grdsionContext contextGuardian)
        {
            Logger = logger;
            Config = config;
            this.ContextMulti = contextMulti;
            this.contextGuardian = contextGuardian;
        }

        public object GetCiclos(string usuario, int idEstadoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision}, => AplicacionesRepository|getCiclos");
                var ciclosR = contextMulti.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision).ToList();
                List<CicloDto> ciclos = new List<CicloDto>();
                foreach (var c in ciclosR)
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
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
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
                if (objComision != null)
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
                var ListComisiones = contextMulti.VwObtenerComisionesDetalleAplicaciones.Where(x => x.IdComisionDetalle == idComisionDetalle).Select(p => new WwObtenerComisionesDetalleAplicacionesModel(p.IdAplicacionDetalleProducto, p.IdComisionDetalle, p.Descripcion, p.Monto, p.Cantidad, p.Subtotal, p.IdProyecto, p.IdEmpresa, p.NombreEmpresa, p.CodigoProducto)).ToList();
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
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();

            try
            {
                bool aplicarDescuentoGuardian = Config.GetValue<bool>("RegistrarDescuentoGuardian");
                int tipoOtrosGuardianId = Config.GetValue<int>("idTipoDescuentoOtrosGuardian");
                int idDescuentoguardianGenerico = 0; 
                if (param.idProyecto == 0)
                {
                    param.idProyecto = Config.GetValue<int>("IdProyectoGestorDeEmpresaAsumidoraDescuentoOtros");
                    Logger.LogWarning($" usuario: {param.usuarioLogin} el tipo de pago es otros y se asignara una empresa que asumira el pago idcomplejo  :{ param.idProyecto} ");
                }
                if (aplicarDescuentoGuardian)
                {

                    var comisionCiclo = ContextMulti.GpComisionDetalles.Join(ContextMulti.GpComisions,
                                                GpComisionDetalle => GpComisionDetalle.IdComision,
                                                GpComision => GpComision.IdComision,
                                            (GpComisionDetalle, GpComision) => new
                                            {
                                                idComisionDetalle = GpComisionDetalle.IdComisionDetalle,
                                                idficha = GpComisionDetalle.IdFicha,
                                                idciclo = GpComision.IdCiclo
                                            }).Join(ContextMulti.Fichas,
                                                    GpComisionDetalle => GpComisionDetalle.idficha,
                                                    Ficha => Ficha.IdFicha,
                                                    (GpComisionDetalle, Ficha) => new
                                                    {
                                                        contactoId = Ficha.Codigo,
                                                        idComisionDetalle = GpComisionDetalle.idComisionDetalle,
                                                        idCiclo = GpComisionDetalle.idciclo
                                                    }
                                        ).Where(x => x.idComisionDetalle == param.idComisionDetalle).FirstOrDefault();
                    var proyecto = ContextMulti.Proyectoes.Where(x => x.IdProyecto == param.idProyecto).FirstOrDefault();
                    var idTipodescuentoGuardian = ContextMulti.TipoAplicaciones.Where(x => x.IdTipoAplicaciones == param.idTipoDescuento).FirstOrDefault();

                    var descuento = contextGuardian.Administraciondescuentocicloes.Where(x => x.LcicloId == comisionCiclo.idCiclo && x.LcontactoId == (comisionCiclo.contactoId)).FirstOrDefault();
                    if (descuento != null)
                    {
                        Logger.LogInformation($" usuario: {param.usuarioLogin}-  se aplicara el registro en guardian, la tabla de descuento");
                        int ultimoId = this.obtenerIdDetalleDescuentoNuevo();

                        if(idTipodescuentoGuardian.GuardianIdCicloDescuentoTipo != tipoOtrosGuardianId) //agregar condision id otros idtipodescuentoGuardian otros
                        {
                            int complejoGuardi = this.ObtenerComplejoGuardian(param.usuarioLogin,param.producto, proyecto.ProyectoConexionId);
                        //------
                        descuento.Dtotal = descuento.Dtotal + param.monto;
                        descuento.Dtfechamod = DateTime.Now;
                        contextGuardian.SaveChanges();
                        Administraciondescuentociclodetalle newdesc = new Administraciondescuentociclodetalle();
                        newdesc.LdescuentociclodetalleId = ultimoId;
                        newdesc.Susuarioadd = param.usuarioLogin;
                        newdesc.Susuariomod = param.usuarioLogin;
                        newdesc.Dtfechaadd = DateTime.Now;
                        newdesc.Dtfechamod = DateTime.Now;
                        newdesc.LdescuentocicloId = descuento.LdescuentocicloId;
                        newdesc.LdescuentociclotipoId = idTipodescuentoGuardian.GuardianIdCicloDescuentoTipo;
                        newdesc.LcomplejoId = complejoGuardi; // //quemado que la tabla proyecto tenga equivalencia con el proyectoid guardian ya que ahora estan con cero.
                        newdesc.Smanzano = "";
                        newdesc.Slote = "";
                        newdesc.Suv = param.producto;
                        newdesc.Dmonto = param.monto;
                        newdesc.Sobservacion = param.descripcion;
                        contextGuardian.Administraciondescuentociclodetalles.Add(newdesc);
                        contextGuardian.SaveChanges();
                            //-------
                        }
                        else
                        {   //se agrega un complejo por default
                            descuento.Dtotal = descuento.Dtotal + param.monto;
                            descuento.Dtfechamod = DateTime.Now;
                            contextGuardian.SaveChanges();
                            Administraciondescuentociclodetalle newdesc = new Administraciondescuentociclodetalle();
                            newdesc.LdescuentociclodetalleId = ultimoId;
                            newdesc.Susuarioadd = param.usuarioLogin;
                            newdesc.Susuariomod = param.usuarioLogin;
                            newdesc.Dtfechaadd = DateTime.Now;
                            newdesc.Dtfechamod = DateTime.Now;
                            newdesc.LdescuentocicloId = descuento.LdescuentocicloId;
                            newdesc.LdescuentociclotipoId = idTipodescuentoGuardian.GuardianIdCicloDescuentoTipo;
                            newdesc.LcomplejoId = Config.GetValue<int>("idComplejoAvdelDetaultParaDescuentoTipoOtrosGuardianID"); 
                            newdesc.Smanzano = "";
                            newdesc.Slote = "";
                            newdesc.Suv = param.producto;
                            newdesc.Dmonto = param.monto;
                            newdesc.Sobservacion = param.descripcion;
                            contextGuardian.Administraciondescuentociclodetalles.Add(newdesc);
                            contextGuardian.SaveChanges();
                        }
                        idDescuentoguardianGenerico = (int)ultimoId;

                    }
                }
                var objDetalle = ContextMulti.GpComisionDetalles.Where(x => x.IdComisionDetalle == param.idComisionDetalle).FirstOrDefault();
                objDetalle.MontoAplicacion = objDetalle.MontoAplicacion + param.monto;
                objDetalle.MontoNeto = objDetalle.MontoNeto - param.monto;
                ContextMulti.SaveChanges();

                AplicacionDetalleProducto objApli = new AplicacionDetalleProducto();
                objApli.CodigoProducto = param.producto;
                objApli.Monto = param.monto;
                objApli.IdComisionesDetalle = param.idComisionDetalle;
                objApli.Descripcion = param.descripcion;
                objApli.Cantidad = param.cantidad;
                objApli.IdUsuario = param.usuarioId;
                objApli.Subtotal = param.monto;
                objApli.IdProyecto = param.idProyecto;
                objApli.IdBdqishur = idDescuentoguardianGenerico;
                objApli.FechaActualizacion = DateTime.Now;
                objApli.FechaCreacion = DateTime.Now;
                objApli.IdTipoAplicaciones = param.idTipoDescuento;
                ContextMulti.AplicacionDetalleProductoes.Add(objApli);
                ContextMulti.SaveChanges();
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
        public ComisionDetalleModel ObtenerComisionDetalle(string usuarioNombre, int idDetalleComision)
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
        public bool CerrarAplicacionCiclo(CerrarAplicacionInputModel model)
        {
            Logger.LogInformation($" usuario: {model.usuarioLogin} -  inicio el SubirArchivo() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int idEstadoComisionPendienteAplicacion = 4; //#variable
                        var comisionEstado = context.GpComisions.Join(context.GpComisionEstadoComisionIs,
                                                                     GpComision => GpComision.IdComision,
                                                                     GpComisionEstadoComisionI => GpComisionEstadoComisionI.IdComision,
                                                                     (GpComision, GpComisionEstadoComisionI) => new
                                                                     {
                                                                         idCiclo = GpComision.IdCiclo,
                                                                         idComision = GpComision.IdComision,
                                                                         habilitado = GpComisionEstadoComisionI.Habilitado,
                                                                         idEstadoComision = GpComisionEstadoComisionI.IdComisionEstadoComisionI
                                                                     }).Where(x => x.idCiclo == model.idCiclo && x.habilitado == true).FirstOrDefault();
                        if (comisionEstado != null)
                        {
                            Logger.LogWarning($" usuario: {model.usuarioLogin} - RETURN!! no se encontro la comision detalle para actualizar iddetalleempresa.");
                            var estadoObj = context.GpComisionEstadoComisionIs.Where(x => x.IdComisionEstadoComisionI == comisionEstado.idEstadoComision).FirstOrDefault();
                            estadoObj.Habilitado = false;
                            context.SaveChanges();

                            GpComisionEstadoComisionI newobj = new GpComisionEstadoComisionI();
                            newobj.Habilitado = true;
                            newobj.IdComision = comisionEstado.idComision;
                            newobj.IdEstadoComision = idEstadoComisionPendienteAplicacion;
                            newobj.IdUsuario = model.usuarioId;
                            newobj.FechaActualizacion = DateTime.Now;
                            newobj.FechaCreacion = DateTime.Now;
                            context.GpComisionEstadoComisionIs.Add(newobj);
                            context.SaveChanges();

                            dbcontextTransaction.Commit();
                            return true;
                        }
                        else
                        {
                            Logger.LogWarning($" usuario: {model.usuarioLogin} - no existe la comision de ciclo con el estado en habilitado");
                            dbcontextTransaction.Rollback();
                            return false;

                        }


                    }
                    catch (Exception ex)
                    {
                        Logger.LogWarning($" usuario: {model.usuarioLogin} -errror catch CerrarAplicacionCiclo. mensaje : {ex.Message}");
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public object ObtenerTipoDescuentosGuardian(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, => AplicacionesRepository|getCiclos");
                var ciclosR = contextMulti.TipoAplicaciones.Where(x => x.ValidoGuardian == true).ToList();
                return ciclosR;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() mensaje : {ex}");
                List<TipoAplicacione> lis = new List<TipoAplicacione>();
                return lis;
            }
        }

        private int obtenerIdDetalleDescuentoNuevo()
        {
            int ultimoId = (int)contextGuardian.Administraciondescuentociclodetalles.Max(x => x.LdescuentociclodetalleId) + 1;
            while (contextGuardian.Administraciondescuentociclodetalles.Any(x => x.LdescuentociclodetalleId == ultimoId))
            {
                ultimoId++;

            }
            return ultimoId;
        }
        private int ObtenerComplejoGuardian(string usuarioLogin, string producto, int proyectoCnx)
        {
            Logger.LogError($" usuario: {usuarioLogin} -  incio ObtenerComplejoGuardian con el producto:{producto} - idproyectoCNX :{proyectoCnx} ");

            var proyect = contextGuardian.ProyectoConexionSufijoes.Where(x => x.IdProyectoCnx == proyectoCnx && x.Estado == true).ToList();
            var proyectosG = contextGuardian.ProyectoConexionSufijoes.Join(contextGuardian.EmpresaComplejoes,
                                                                         ProyectoConexionSufijo => ProyectoConexionSufijo.IdEmpresaComplejo,
                                                                         EmpresaComplejo =>EmpresaComplejo.IdEmpresaComplejo,
                                                                         (ProyectoConexionSufijo, EmpresaComplejo) => new
                                                                         {
                                                                             iddetalle= ProyectoConexionSufijo.IdProyectoConexionSufijo,                                                                             
                                                                             idProyectoCnx = ProyectoConexionSufijo.IdProyectoCnx,
                                                                             idComplejo = EmpresaComplejo.ComplejoId,
                                                                             estado = ProyectoConexionSufijo.Estado,
                                                                             sufijo= ProyectoConexionSufijo.Sufijo
                                                                         }
                                                                 ).Where(x => x.idProyectoCnx == proyectoCnx && x.estado == true).ToList();
            int proyectoSeleccionado = 0;
            if(proyectosG.Count > 0)
            {
                int idproyectoDefaultVacio = 0;
                int idproyectoSeleccionado = 0;
                foreach (var item in proyectosG )
                {
                    idproyectoDefaultVacio = (int)(item.sufijo == "" ? item.idComplejo : 0);
                    if (producto.Contains(item.sufijo) && item.sufijo != "")
                    {
                        idproyectoSeleccionado = (int)item.idComplejo;
                    }

                }
                if(idproyectoSeleccionado > 0)
                {
                    return idproyectoSeleccionado;
                }else if(idproyectoDefaultVacio > 0)
                {
                    return idproyectoDefaultVacio;
                } else {
                    return Config.GetValue<int>("idComplejoAvdelDetaultParaDescuentoTipoOtrosGuardianID");
                }                

            } else {
                 return  Config.GetValue<int>("idComplejoAvdelDetaultParaDescuentoTipoOtrosGuardianID");
            }

        }



    }

}
