using gestion_de_comisiones.Modelos.Cliente;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
        public List<CicloOutputModel> listCiclosPendientes(string usuario )
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
                List<CicloOutputModel> lis = new List<CicloOutputModel>();
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
<<<<<<< HEAD

                            var detallesComisiones = context.ComisionDetalleEmpresas.Where(x => x.Estado == 1 && x.IdComisionDetalle == comision.idComisionDetalle).ToList();
=======
                            int idEstadoPendiente = 1;// #variable table estadodetalle comision empresa
                            var detallesComisiones = context.ComisionDetalleEmpresas.Where(x => x.Estado == idEstadoPendiente && x.IdComisionDetalle == comision.idComisionDetalle).ToList();
>>>>>>> 14c91cf7f981bd89225198cd589e8eed58349c40
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
                        decimal Neto = 0;
                        var objEstadoComisionDetalle = context.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == idComisionDetalleEmpresa).First();
                        if (objEstadoComisionDetalle != null)
                        {
                            if(estadoDetalleEmpresa == true)
                            {//sifacturo
                                retencion = objEstadoComisionDetalle.Retencion;
                                Neto= objEstadoComisionDetalle.MontoNeto + objEstadoComisionDetalle.Retencion;
                                objEstadoComisionDetalle.MontoNeto = objEstadoComisionDetalle.MontoNeto + objEstadoComisionDetalle.Retencion;
                                objEstadoComisionDetalle.Retencion = 0;
                                
                            }
                            else
                            {//no facturo
                                retencion = objEstadoComisionDetalle.Monto * porcentajeRetencion / 100;
                                Neto= objEstadoComisionDetalle.Monto - retencion;
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
                                //calcular la cabecera por false, el neto y el descuento.
                                Logger.LogInformation($" usuario: {usuarioLogin} - se calcula el neto y la retencion, destickeo la  factura");
                                var comisionDetalle = context.GpComisionDetalles.Where(x => x.IdComisionDetalle == idComisionDetalle).FirstOrDefault();
                                if(comisionDetalle != null)
                                { //suma retencion y descuenta neto
                                    decimal netoCalculado = (decimal)(comisionDetalle.MontoNeto - retencion);
                                    decimal retencionCalculo = (decimal)(comisionDetalle.MontoRetencion + retencion);
                                    comisionDetalle.MontoNeto = netoCalculado;
                                    comisionDetalle.MontoRetencion = retencionCalculo;
                                    context.SaveChanges();
                                }

                            }
                            else
<<<<<<< HEAD
                            {                                
                                var detallesEstados = context.ComisionDetalleEmpresas.Where(x => x.Estado == 1 &&  x.IdComisionDetalle == idComisionDetalle && x.SiFacturo == false).ToList();
=======
                            {
                                int estadoPendiente = 1; //variable  estado de detalle comision empresa
                                var detallesEstados = context.ComisionDetalleEmpresas.Where(x => x.Estado == estadoPendiente &&  x.IdComisionDetalle == idComisionDetalle && x.SiFacturo == false).ToList();
>>>>>>> 14c91cf7f981bd89225198cd589e8eed58349c40
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
                                //calcular la cabecera por false, el neto y el descuento.
                                 Logger.LogInformation($" usuario: {usuarioLogin} - se calcula el neto y la retencion, porque presento factura");
                                var comisionDetalle = context.GpComisionDetalles.Where(x => x.IdComisionDetalle == idComisionDetalle).FirstOrDefault();
                                    if (comisionDetalle != null)
                                    {   // decuenta retencion e incrementa neto
                                        decimal netocalculad = (decimal)(comisionDetalle.MontoNeto + retencion);
                                        decimal retencionCalculad = (decimal)(comisionDetalle.MontoRetencion - retencion);
                                        comisionDetalle.MontoNeto = netocalculad;
                                        //if (detallesEstados.Count == 0)
                                        //{
                                        //     comisionDetalle.MontoRetencion = 0;
                                        //}
                                        //else
                                        //{
                                            comisionDetalle.MontoRetencion = retencionCalculad;
                                        //}                                        
                                        context.SaveChanges();
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
                        decimal porcentaje = decimal.Parse("15.5");
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
<<<<<<< HEAD
                            
                            var detallesEstados = context.ComisionDetalleEmpresas.Where(x => x.Estado == 1 && x.IdComisionDetalle == idComisionDetalle ).ToList();
=======
                            int estadoPendiente = 1; //variable  estado de detalle comision empresa
                            var detallesEstados = context.ComisionDetalleEmpresas.Where(x => x.Estado == estadoPendiente && x.IdComisionDetalle == idComisionDetalle ).ToList();
>>>>>>> 14c91cf7f981bd89225198cd589e8eed58349c40
                            Logger.LogInformation($" usuario: {usuarioLogin} - se cantidad de detalle detalles a cambiar si facturo nro : {detallesEstados.Count}");

                            decimal retencionGlobal = 0;
                            decimal netoGlobal = 0;

                            foreach(var iten in detallesEstados)
                            {
                                //aplica a los destikear todo
                                if (estadoFacturado == false)
                                {
                                    var objEmpresa = context.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == iten.IdComisionDetalleEmpresa).First();
                                    if (objEmpresa != null)
                                    {
                                        if (objEmpresa.SiFacturo == true) //aqui se lo se le quita la factura a todos
                                        {// retencion suma - resta neto
                                            decimal retencionUnitario = objEmpresa.Monto * porcentaje / 100;
                                            decimal netoUnitario = objEmpresa.Monto - retencionUnitario;
                                            objEmpresa.Retencion = retencionUnitario;
                                            objEmpresa.MontoNeto = netoUnitario;
                                            retencionGlobal = retencionGlobal + retencionUnitario;
                                            netoGlobal = netoGlobal + netoUnitario;
                                        }
                                        objEmpresa.SiFacturo = estadoFacturado;
                                        objEmpresa.FechaActualizacion = DateTime.Now;
                                        objEmpresa.IdUsuario = usuarioId;                                        
                                        context.SaveChanges();
                                    }
                                }
                                else
                                {//aplica si ya tiene factura
                                    var objEmpresa = context.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == iten.IdComisionDetalleEmpresa).First();
                                    if (objEmpresa != null)
                                    {
                                        if (objEmpresa.SiFacturo == false)//aplixar factura a todos lo quen o tienen
                                        {
                                            decimal retencionUnitario = objEmpresa.Retencion;
                                            decimal netoUnitario = objEmpresa.MontoNeto + retencionUnitario;
                                            objEmpresa.Retencion = 0;
                                            objEmpresa.MontoNeto = netoUnitario;
                                            retencionGlobal = retencionGlobal + retencionUnitario;
                                            netoGlobal = netoGlobal + netoUnitario;
                                        }
                                        objEmpresa.SiFacturo = estadoFacturado;
                                        objEmpresa.FechaActualizacion = DateTime.Now;
                                        objEmpresa.IdUsuario = usuarioId;                                        
                                        context.SaveChanges();
                                    }
                                }
                            }
                            var objComisionDetallee= context.GpComisionDetalles.Where(x => x.IdComisionDetalle == idComisionDetalle).First();
                            if (estadoFacturado == false)
                            {// incrementa retencion , resta neto
                                decimal totalRetencionGLO = (decimal)(objComisionDetallee.MontoRetencion + retencionGlobal);
                                decimal totalNetoGlo = (decimal)(objComisionDetallee.MontoNeto - retencionGlobal);
                                objComisionDetallee.MontoRetencion = totalRetencionGLO;
                                objComisionDetallee.MontoNeto = totalNetoGlo;
                            }
                            else
                            {// estado true, resata retencion y resta neto
                                decimal totalRetencionGLO = (decimal)(objComisionDetallee.MontoRetencion - retencionGlobal);
                                decimal totalNetoGlo = (decimal)(objComisionDetallee.MontoNeto + retencionGlobal);
                                objComisionDetallee.MontoRetencion = totalRetencionGLO;
                                objComisionDetallee.MontoNeto = totalNetoGlo;
                            }
                            context.SaveChanges();
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

                        int estadoPendienteComision = 1;  // pendiente a facturacion #VARIABLE 
                        int idCerradoFactura = 2;  // a facturacion #VARIABLE 
                        int idNoFacturo = 1;  // no facturo #VARIABLE 
                        int idSiFacturo = 2;  // no facturo #VARIABLE 
                        int idResagado = 5; //  resagado por no presentar factura #VARIABLE 
                        int idNoPresentaFactura = 6; // no presenta Factura VARIABLE
                        bool habilitadofacturarGuardian = true; //VARIABLE entorno

                        var objComisionPendiente = context.GpComisions.Join(context.GpComisionEstadoComisionIs, 
                                                                          GpComision => GpComision.IdComision, GpComisionEstadoComisionI => GpComisionEstadoComisionI.IdComision,
                                                                         (GpComision, GpComisionEstadoComisionI) => new {
                                                                             idComision = GpComision.IdComision,
                                                                             idciclo = GpComision.IdCiclo,
                                                                             estadoComision = GpComisionEstadoComisionI.IdEstadoComision,
                                                                             estado = GpComisionEstadoComisionI.Habilitado
                                                                         }).Where(x => x.estado == true && x.estadoComision == estadoPendienteComision && x.idciclo == idCiclo).FirstOrDefault();
                        if (objComisionPendiente != null)
                        {
                            var objComisiones = context.VwObtenercomisiones.Where(x => x.IdComision == objComisionPendiente.idComision).ToList();
                            var ComisionMaster = context.GpComisionEstadoComisionIs.Where(x => x.IdComision == objComisionPendiente.idComision).FirstOrDefault();
                            if (ComisionMaster != null)
                            {
                                ComisionMaster.IdEstadoComision = idCerradoFactura;
                                context.SaveChanges();
                            }                     
                            var parameterReturn = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@id_ciclo",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = idCiclo
                              },
                                 new SqlParameter() {
                                            ParameterName = "@habilitado_facturar_guardian",
                                            SqlDbType =  System.Data.SqlDbType.Bit,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = habilitadofacturarGuardian
                              },
                               new SqlParameter() {
                                            ParameterName = "@usuario",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = usuarioLogin
                              }};
                            var result = context.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_PROCESAR_FACTURAS_PENDIENTES] @id_ciclo, @habilitado_facturar_guardian,  @usuario  ", parameterReturn);
                            var returnValue = parameterReturn;
                            if(result > 0)
                            {
                                dbcontextTransaction.Commit();
                                Logger.LogInformation($" usuario: {usuarioLogin}-  SE ACTUALIZO EXITOSAMENTE  el ciclo en facturado");
                                return true;
                            }
                            else
                            {
                                dbcontextTransaction.Rollback();
                                Logger.LogInformation($" usuario: {usuarioLogin}-  NO se actualizo el cierre de factura porque el [SP_PROCESAR_FACTURAS_PENDIENTES] devuelte cero (0) en registro, no hubo ningun registro ni actualizacion.");
                                return false;
                            }
                            // var result2 = context.Database.ExecuteSqlRaw("SP_PROCESAR_FACTURAS_PENDIENTES {0}", 80); //funciona pero no retorna                                                    
                        }
                        else
                        {
                            Logger.LogWarning($" usuario: {usuarioLogin} - el ciclo no se encuentra en estado pendiente para ser procesada para el cierre, idciclo :{idCiclo}  ");
                            dbcontextTransaction.Rollback();
                            return false;
                        }
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
