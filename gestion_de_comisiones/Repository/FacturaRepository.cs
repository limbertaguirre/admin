﻿using gestion_de_comisiones.Modelos.Cliente;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class FacturaRepository: IFacturaRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<FacturaRepository> Logger;
        private readonly IHostingEnvironment EEnv;
        public FacturaRepository(ILogger<FacturaRepository> logger, IHostingEnvironment env)
        {
            Logger = logger;
            EEnv = env;
        }
        public object listCiclosPendientes(string usuario )
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el listCiclos() repository");
                int pendiente = 1;// int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION"));
                int idtipoComision = 1; // int.Parse(Environment.GetEnvironmentVariable("TIPO_PAGO_COMISIONES_ID"));

                var listiclos = contextMulti.GpComisions.Join(contextMulti.GpComisionEstadoComisionIs,
                                                  GpComision => GpComision.IdComision,
                                                  GpComisionEstadoComisionI => GpComisionEstadoComisionI.IdComision,
                                                  (GpComision, GpComisionEstadoComisionI) => new
                                                  {
                                                      idcomisiones = GpComision.IdComision,
                                                      idEstado = GpComisionEstadoComisionI.IdEstadoComision,
                                                      idTipoComision = GpComision.IdTipoComision,
                                                      idCiclo = GpComision.IdCiclo,
                                                  }).Where(x => x.idEstado == pendiente && x.idTipoComision == idtipoComision).ToList();
                List<CicloOutputModel> lista = new List<CicloOutputModel>();
                foreach (var obj in listiclos){
                    CicloOutputModel objciclo = new CicloOutputModel();
                    var ciclo = contextMulti.Cicloes.Where(x => x.IdCiclo == obj.idCiclo).FirstOrDefault();
                    if(ciclo != null)
                    {
                        objciclo.idCiclo = (int)obj.idCiclo;
                        objciclo.nombre = ciclo.Nombre;
                        objciclo.Descripcion = ciclo.Descripcion;
                        lista.Add(objciclo);
                    }
                }
                return lista;

            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch listCiclos() mensaje : {ex}");
                 List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }
        public object obtenerComisiones(string usuario, int idCiclo, int idEstadoComision)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerComisionesPendientes() ");
                Logger.LogWarning($" usuario: {usuario} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                int idEstadoSinDefinir = 0; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_SIN_DEFINIR_CERO"));
                int idEstadoNoFacturo = 1; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_NO_FACTURA"));
                int idEstadoSifacturo = 2; //int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_SI_FACTURO"));
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision && x.EstadoFacturoId == idEstadoNoFacturo || x.EstadoFacturoId == idEstadoSifacturo || x.EstadoFacturoId == idEstadoSinDefinir).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerComisionesPendientes() mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }

        public object BuscarComisiones(string usuario, int idCiclo, int idEstadoComision, string nombreCriterio)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository buscarcomisionXnombre() criterio nombre: {nombreCriterio} ");
                Logger.LogWarning($" usuario: {usuario} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision && x.Ci.Contains(nombreCriterio.Trim())).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch buscarcomisionXnombre() mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        public List<VwObtenerComisionesDetalleEmpresaModel> obtenerDetalleEmpresa(string usuario, int idComisionDetalle )
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerDetalleEmpresa()  idComisionDetalle: {idComisionDetalle} ");
                Logger.LogWarning($" usuario: {usuario} parametros: idComisionDetalle:{idComisionDetalle} ");
               var ListComisiones = contextMulti.VwObtenerComisionesDetalleEmpresas.Where(x => x.IdComisionDetalle== idComisionDetalle && x.EstadoDetalleEmpresa == true).Select(p => new VwObtenerComisionesDetalleEmpresaModel(p.IdComisionDetalleEmpresa, p.IdComisionDetalle, p.Empresa, p.Monto, p.MontoAFacturar, p.MontoTotalFacturar, p.RespaldoPath, p.NroAutorizacion, p.IdEmpresa, p.EstadoDetalleEmpresa, p.VentasPersonales, p.VentasGrupales,p.Residual, p.Retencion, p.MontoNeto, p.SiFacturo) ).ToList();
               
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerDetalleEmpresa() mensaje : {ex}");
                List<VwObtenerComisionesDetalleEmpresaModel> list = new List<VwObtenerComisionesDetalleEmpresaModel>();
                return list;
            }
        }

        public object obtenerComisionDetalle(string usuario, int idComisionDetalle)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerComisionDetalle()  ");
                Logger.LogWarning($" usuario: {usuario} parametros:  idComisionDetalle:{idComisionDetalle}");
                DetalleEmpresaModel newObj = new DetalleEmpresaModel();
                var objComision = contextMulti.VwObtenercomisiones.Where(x => x.IdComisionDetalle == idComisionDetalle).FirstOrDefault();
                if(objComision != null)
                {
                   
                    newObj.idFicla = (int)objComision.IdFicha;
                    newObj.nombreFicha = objComision.Nombre;
                    newObj.ciclo = objComision.Ciclo;
                    newObj.idCiclo = (int)objComision.IdCiclo;
                    var objCli = contextMulti.Fichas.Where(x => x.IdFicha == objComision.IdFicha).Select(p => new { p.IdFicha, p.Avatar }).FirstOrDefault();
                    if(objCli != null){
                        newObj.avatar = objCli.Avatar;
                    }else{
                        newObj.avatar = ""; 
                    }
                    var objNivel = contextMulti.FichaNivelIs.Join(contextMulti.Nivels,
                                                FichaNivelI => FichaNivelI.IdNivel,
                                                Nivel => Nivel.IdNivel,
                                                (FichaNivelI, Nivel) => new
                                                {   nombre = Nivel.Nombre,
                                                    idFicha = FichaNivelI.IdFicha,
                                                    fechaCreacion = FichaNivelI.FechaCreacion,
                                                    habilitado = FichaNivelI.Habilitado,
                                                }).Where(x => x.idFicha == objComision.IdFicha && x.habilitado == true).OrderByDescending(x => x.fechaCreacion).FirstOrDefault();
                    if (objNivel != null){
                        newObj.rango = objNivel.nombre;
                    }else {
                        newObj.rango = "";
                    }
                    var listDEmpresa = obtenerDetalleEmpresa(usuario, idComisionDetalle);
                    newObj.listDetalle = listDEmpresa;

                }
                return newObj;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerComisionDetalle() mensaje : {ex}");
                DetalleEmpresaModel obj = new DetalleEmpresaModel();
                return obj;
            }
        }

        public List<EmpresaOutput> obtenerEmpresas(string usuario)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerEmpresas() ");
                int activo = 1; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_EMPRESA_ACTIVO"));
                var ListComisiones = contextMulti.Empresas.Where(x => x.Estado == activo).Select( p => new EmpresaOutput(p.IdEmpresa, p.Nombre)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerEmpresas() mensaje : {ex}");
                List<EmpresaOutput> list = new List<EmpresaOutput>();
                return list;
            }
        }
        public DetalleOutputModel obtenerComisionDetalleEmpresa(string usuario,int  idComisionDetalle)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerEmpresas() ");
                DetalleOutputModel obj = new DetalleOutputModel();
                int activo = 1; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_EMPRESA_ACTIVO"));
                var detalle = contextMulti.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == idComisionDetalle).Select(p => new ComisionDetalleEmpresaOutput(p.IdComisionDetalleEmpresa, p.Monto, p.NroAutorizacion ,p.IdEmpresa, p.MontoAFacturar, p.MontoTotalFacturar)).FirstOrDefault();
                if(detalle != null)
                {
                    obj.idComisionDetalleEmpresa = detalle.idComisionDetalleEmpresa;
                    obj.monto = detalle.monto;
                    obj.idEmpresa = detalle.idEmpresa;
                    obj.montoAFacturar = (decimal)detalle.montoAFacturar;
                    obj.montoTotalFActurar = (decimal)detalle.montoTotalFacturar;
                    obj.NroAutorizacion = detalle.nroAutorizacion;
                    var empresas = obtenerEmpresas(usuario);
                    obj.listEmpresa = empresas;
                }
                return obj;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerEmpresas() mensaje : {ex}");
                DetalleOutputModel obj = new DetalleOutputModel();
                return obj;
            }
        }

        public bool AcTualizarComisionDetalleEstado(ComisionDetalleInput comision, int estadoFacturado)
        {
            Logger.LogInformation($" usuario: {comision.usuarioLogin} -  inicio el AcTualizarComisionDetalleEstado() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var objEstadoComisionDetalle = context.GpComisionDetalleEstadoIs.Where(x => x.IdComisionDetalle == comision.idComisionDetalle).FirstOrDefault();
                        if (objEstadoComisionDetalle != null)
                        {
                            objEstadoComisionDetalle.IdEstadoComisionDetalle = 2;
                            context.SaveChanges();

                            var detallesComisiones = context.ComisionDetalleEmpresas.Where(x => x.Estado == true && x.IdComisionDetalle == comision.idComisionDetalle).ToList();
                            if(detallesComisiones.Count > 0)
                            {
                                foreach(var item in detallesComisiones)
                                {
                                    var comisionEmpresa = context.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == item.IdComisionDetalleEmpresa).First();
                                    if(comisionEmpresa != null)
                                    {
                                        if (comisionEmpresa.SiFacturo == false)
                                        {
                                            comisionEmpresa.SiFacturo = true;
                                            comisionEmpresa.IdUsuario = comision.usuarioId;
                                            comisionEmpresa.FechaActualizacion = DateTime.Now;
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                            dbcontextTransaction.Commit();
                            Logger.LogInformation($" usuario: {comision.usuarioLogin}-  SE ACTUALIZO EXITOSAMENTE ");
                            return true;
                        }
                        else
                        {
                            Logger.LogWarning($" usuario: {comision.usuarioLogin} - RETURN!! no se encontro la comision detalle:  ");
                            dbcontextTransaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool ActualizarEstadoFacturarEmpresa( string usuarioLogin, int usuarioId, int idComisionDetalle, int idComisionDetalleEmpresa, bool estadoDetalleEmpresa)
        {
            Logger.LogInformation($" usuario: {usuarioLogin} -  inicio el ActualizarEstadoFacturarEmpresa() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        decimal porcentajeRetencion = decimal.Parse("15.5");
                        decimal retencion = 0;
                        var objEstadoComisionDetalle = context.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == idComisionDetalleEmpresa).First();
                        if (objEstadoComisionDetalle != null)
                        {
                            if(estadoDetalleEmpresa == true)
                            {//sifacturo
                                retencion = objEstadoComisionDetalle.Retencion;
                                objEstadoComisionDetalle.MontoNeto = objEstadoComisionDetalle.MontoNeto + objEstadoComisionDetalle.Retencion;
                                objEstadoComisionDetalle.Retencion = 0;

                            }
                            else
                            {//no facturo
                                retencion = objEstadoComisionDetalle.Monto * porcentajeRetencion / 100;
                                objEstadoComisionDetalle.MontoNeto = objEstadoComisionDetalle.Monto - retencion;
                                objEstadoComisionDetalle.Retencion = retencion;
                            }
                            objEstadoComisionDetalle.SiFacturo = estadoDetalleEmpresa;
                            objEstadoComisionDetalle.FechaActualizacion = DateTime.Now;

                            context.SaveChanges();
                            Logger.LogInformation($" usuario: {usuarioLogin} -  habilitara un detalle empresa facturado : estado facturado :{estadoDetalleEmpresa}");
                            if (estadoDetalleEmpresa == false)
                            {
                                Logger.LogInformation($" usuario: {usuarioLogin} - se desahabilita el comision detalle");
                                var comision = context.GpComisionDetalleEstadoIs.Where(x => x.IdComisionDetalle == idComisionDetalle).FirstOrDefault();
                                if(comision != null)
                                {
                                    comision.IdEstadoComisionDetalle = 1; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_NO_FACTURA"));
                                    comision.FechaActualizacion = DateTime.Now;
                                    comision.IdUsuario = usuarioId;
                                    context.SaveChanges();
                                }
                            }
                            else
                            {                                
                                var detallesEstados = context.ComisionDetalleEmpresas.Where(x => x.Estado == true &&  x.IdComisionDetalle == idComisionDetalle && x.SiFacturo == false).ToList();
                                Logger.LogInformation($" usuario: {usuarioLogin} - se cantidad de detalle empresas no facturadas : {detallesEstados.Count}");
                                if (detallesEstados.Count == 0)
                                {
                                  var  updatecomisiion = context.GpComisionDetalleEstadoIs.Where(x => x.IdComisionDetalle == idComisionDetalle).FirstOrDefault();
                                    if (updatecomisiion != null)
                                    {
                                        Logger.LogInformation($" usuario: {usuarioLogin} - se habilitara el comision detalle ya que todos estan en facturado");
                                        updatecomisiion.IdEstadoComisionDetalle = 2; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_SI_FACTURO"));
                                        updatecomisiion.FechaActualizacion = DateTime.Now;
                                        updatecomisiion.IdUsuario = usuarioId;
                                        context.SaveChanges();
                                    }
                                }

                            }
                          
                            dbcontextTransaction.Commit();
                            Logger.LogInformation($" usuario: {usuarioLogin}-  SE ACTUALIZO EXITOSAMENTE ");
                            return true;
                        }
                        else
                        {
                            Logger.LogWarning($" usuario: {usuarioLogin} - RETURN!! no se encontro la comision detalle para actualizar iddetalleempresa:{idComisionDetalleEmpresa}  ");
                            dbcontextTransaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool SubirArchivo(string usuarioLogin, int usuarioId, int idComisionDetalleEmpresa, string archivoPdf)
        {
            Logger.LogInformation($" usuario: {usuarioLogin} -  inicio el SubirArchivo() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var objEstadoComisionDetalle = context.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == idComisionDetalleEmpresa).First();
                        if (objEstadoComisionDetalle != null)
                        {                                               
                            string contentRootPath = EEnv.ContentRootPath + "\\Archivos\\Facturas";
                            if (!System.IO.Directory.Exists(contentRootPath))
                            {
                                System.IO.Directory.CreateDirectory(contentRootPath);
                            }                           
                            string nombreImage = "factura-"+idComisionDetalleEmpresa +"-"+ DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Minute + DateTime.Now.Second + ".pdf";
                            System.IO.File.WriteAllBytes(Path.Combine(contentRootPath, nombreImage), Convert.FromBase64String(archivoPdf.Substring(28)));
                            objEstadoComisionDetalle.FechaActualizacion = DateTime.Now;
                            objEstadoComisionDetalle.RespaldoPath = nombreImage;
                            context.SaveChanges();     
                            Logger.LogInformation($" usuario: {usuarioLogin}-  SE carga una factura pdf con el nombre: {nombreImage} ");
                            dbcontextTransaction.Commit();
                            return true;
                        }
                        else
                        {
                            Logger.LogWarning($" usuario: {usuarioLogin} - RETURN!! no se encontro la comision detalle para actualizar iddetalleempresa:{idComisionDetalleEmpresa}  ");
                            dbcontextTransaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool AplicarFacturadoEstadoFacturarEmpresa(string usuarioLogin, int usuarioId, int idComisionDetalle , bool estadoFacturado)
        {
            Logger.LogInformation($" usuario: {usuarioLogin} -  inicio el ActualizarEstadoFacturarEmpresa() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var objComisionDetalleEstado = context.GpComisionDetalleEstadoIs.Where(x => x.IdComisionDetalle == idComisionDetalle).First();
                        if (objComisionDetalleEstado != null)
                        {
                            if (estadoFacturado == true)
                            {
                                objComisionDetalleEstado.IdEstadoComisionDetalle = 2;// int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_SI_FACTURO"));//2
                            }
                            else
                            {
                                objComisionDetalleEstado.IdEstadoComisionDetalle = 1; // int.Parse(Environment.GetEnvironmentVariable("ESTADO_COMISION_DETALLE_NO_FACTURA")); // 1
                            }
                            context.SaveChanges();
                            Logger.LogInformation($" usuario: {usuarioLogin} -  habilitara un detalle empresa facturado : estado facturado :{estadoFacturado}");
                            
                            var detallesEstados = context.ComisionDetalleEmpresas.Where(x => x.Estado == true && x.IdComisionDetalle == idComisionDetalle ).ToList();
                            Logger.LogInformation($" usuario: {usuarioLogin} - se cantidad de detalle detalles a cambiar si facturo nro : {detallesEstados.Count}");

                            foreach(var iten in detallesEstados)
                            {
                                var objEmpresa= context.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == iten.IdComisionDetalleEmpresa).First();
                                if(objEmpresa != null)
                                {
                                    objEmpresa.SiFacturo = estadoFacturado;
                                    objEmpresa.FechaActualizacion = DateTime.Now;
                                    objEmpresa.IdUsuario = usuarioId;
                                    context.SaveChanges();
                                }

                            }                             
                            dbcontextTransaction.Commit();
                            Logger.LogInformation($" usuario: {usuarioLogin}-  SE ACTUALIZO EXITOSAMENTE ");
                            return true;
                        }
                        else
                        {
                            Logger.LogWarning($" usuario: {usuarioLogin} - RETURN!! no se encontro la comision detalle para actualizar iddetalleempresa:{idComisionDetalle}  ");
                            dbcontextTransaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool CerrarFactura(string usuarioLogin, int usuarioId, int idCiclo)
        {
            Logger.LogInformation($" usuario: {usuarioLogin} -  inicio el CerrarFactura() en repos");
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using (var dbcontextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //var objComisionDetalleEstado = context.GpComisionDetalleEstadoIs.Where(x => x.IdComisionDetalle == idComisionDetalle).First();
                        //if (objComisionDetalleEstado != null)
                        //{
                           
                           // dbcontextTransaction.Commit();
                            Logger.LogInformation($" usuario: {usuarioLogin}-  SE ACTUALIZO EXITOSAMENTE ");
                            return true;
                        //}
                        //else
                        //{
                        //    Logger.LogWarning($" usuario: {usuarioLogin} - RETURN!! no se encontro la comision detalle para actualizar iddetalleempresa:{idComisionDetalle}  ");
                        //    dbcontextTransaction.Rollback();
                        //    return false;
                        //}
                    }
                    catch (Exception ex)
                    {
                        Logger.LogInformation($" usuario: {usuarioLogin} -  error catch repository mensaje: {ex.Message}");
                        dbcontextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }


    }
}
