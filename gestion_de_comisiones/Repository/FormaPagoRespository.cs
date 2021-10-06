using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class FormaPagoRespository : IFormaPagoRepository
    {
        private readonly ILogger<FormaPagoRespository> Logger;
        private readonly IConfiguration Config;
        private readonly BDMultinivelContext ContextMulti;

        public FormaPagoRespository(ILogger<FormaPagoRespository> logger, IConfiguration config, BDMultinivelContext contextMulti)
        {
            Logger = logger;
            Config = config;
            this.ContextMulti = contextMulti;
        }

        public object GetCiclos(string usuario, int idEstadoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos");
                var ciclosR = ContextMulti.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision).ToList();
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
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() mensaje : {ex.Message}");
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
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getComisiones() mensaje : {ex.Message}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        public List<TipoPagoInputmodel> ListarFormaPagos(ParamFormaPagosOutputModel param)
        {
            try
            {
                List<TipoPagoInputmodel> newList = new List<TipoPagoInputmodel>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository ListarFormaPagos() ");
                var tipopagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new TipoPagoInputmodel(p.IdTipoPago, p.Nombre, p.Icono)).ToList();
                var verifi = ContextMulti.VwVerificarCuentasUsuarios.Where(x => x.Ci == param.carnet).FirstOrDefault();
                foreach (var list in tipopagos)
                {
                    TipoPagoInputmodel obj = new TipoPagoInputmodel();
                    obj.idTipoPago = list.idTipoPago;
                    obj.nombre = list.nombre;
                    obj.icono = list.icono;
                    if (list.idTipoPago == 1) //tiposion pay
                    {
                        obj.estado = bool.Parse(verifi.SionPay);
                        if (bool.Parse(verifi.SionPay) == false)
                        {
                            obj.descripcion = "El frelanzer no tiene SION PAY";
                        }
                        else
                        {
                            obj.descripcion = "";
                        }
                    }
                    else if (list.idTipoPago == 2)
                    {
                        obj.estado = verifi.TieneCuentaBancaria;
                        if (verifi.TieneCuentaBancaria == false)
                        {
                            obj.descripcion = "El frelanzer no tiene cuenta habilitada";
                        }
                        else
                        {
                            obj.descripcion = "";
                        }
                    }
                    else
                    {
                        obj.estado = true;
                    }
                    newList.Add(obj);

                }
                return newList;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch ListarFormaPagos() mensaje : {ex.Message}");
                List<TipoPagoInputmodel> list = new List<TipoPagoInputmodel>();
                return list;
            }
        }

        public bool AplicarFormaPago(AplicarMetodoOutput param)
        {
            Logger.LogInformation($" usuario: {param.usuarioLogin} -  inicio el RegistrarDecuentoComision() en repos");
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();

            try
            {
                bool aplicarDescuentoGuardian = Config.GetValue<bool>("RegistrarDescuentoGuardian");
                var objDetalle = ContextMulti.ListadoFormasPagoes.Where(x => x.IdComisionesDetalle == param.idComisionDetalle).FirstOrDefault();
                if (objDetalle != null)
                {
                    objDetalle.IdTipoPago = param.idTipoPago;
                    objDetalle.FechaActualizacion = DateTime.Now;
                    ContextMulti.SaveChanges();
                }
                else
                {
                    var deta = ContextMulti.GpComisionDetalles.Where(x => x.IdComisionDetalle == param.idComisionDetalle).FirstOrDefault();
                    ListadoFormasPago objL = new ListadoFormasPago();
                    objL.IdTipoPago = param.idTipoPago;
                    objL.IdComisionesDetalle = param.idComisionDetalle;
                    objL.IdUsuario = param.idUsuario;
                    objL.MontoNeto = (decimal)deta.MontoNeto;
                    objL.FechaCreacion = DateTime.Now;
                    objL.FechaActualizacion = DateTime.Now;
                    ContextMulti.ListadoFormasPagoes.Add(objL);
                    ContextMulti.SaveChanges();
                }
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
        public object GetComisionesPorCarnetListFormaPago(BuscarInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
        {
            try
            {
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetComisionesPorCarnet() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == param.idCiclo && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura) && x.Ci.Contains(param.nombreCriterio.Trim())).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetComisionesPorCarnet() mensaje : {ex}");
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                return list;
            }
        }

        public object GetComisionesPorFormaPago(FormaPagosDisponiblesInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
        {
            try
            {
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetComisionesPorCarnet() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == param.idCiclo && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                List<FormaPagoModel> LisFormaPagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new FormaPagoModel(p.IdTipoPago, p.Nombre, p.Descripcion, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion, p.Estado, p.Icono)).ToList();
                FormaPagoModel nuevoNinguno = new FormaPagoModel() { IdTipoPago = 0, Nombre = "Ninguno", Descripcion = "", IdUsuario = 1, Estado = true, Icono = "ningunPago" };
                LisFormaPagos.Add(nuevoNinguno);
                foreach (var item in LisFormaPagos)
                {
                    if (ListComisiones != null)
                    {
                        FormaPagoDisponiblesModel obj = new FormaPagoDisponiblesModel();
                        obj.idTipoPago = item.IdTipoPago;
                        obj.nombre = item.Nombre;
                        obj.icono = item.Icono;
                        int canti = ListComisiones.Where(x => x.IdTipoPago == item.IdTipoPago).Count();
                        obj.cantidad = canti;
                        list.Add(obj);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetComisionesPorCarnet() mensaje : {ex}");
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                return list;
            }
        }
        public object FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
        {
            try
            {
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository FiltrarComisionPagoPorTipoPago() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == param.idCiclo && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura) && x.IdTipoPago == param.idTipoPago).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch FiltrarComisionPagoPorTipoPago() mensaje : {ex}");
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                return list;
            }
        }
        public object VerificarAutorizadorPorComision(AutorizacionVerificarParam param)
        {
            try
            {
                int idTipoAutorizacionFormaPago = 3;
                AutorizadorRespuestaModel obj = new AutorizadorRespuestaModel();
                List<Autorizador> listAurorizadores = new List<Autorizador>();
                List<Autorizador> autorizados = new List<Autorizador>();



                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository VerificarAutorizadorPorComision() ");
                var autoriza = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdUsuario == param.idUsuario && x.IdTipoAutorizacion == idTipoAutorizacionFormaPago).FirstOrDefault();
                if (autoriza != null)
                {
                     autorizados = ObtenerAutorizadores(autoriza.IdUsuarioAutorizacion,idTipoAutorizacionFormaPago, param.idCiclo, param.usuarioLogin);
                    Autorizador addObjAutorizador = new Autorizador();
                    addObjAutorizador.nombre = autoriza.Nombres;
                    addObjAutorizador.apellido = autoriza.Apellidos;
                    addObjAutorizador.area = autoriza.DescripcionArea;
                    var confirmacionAutorizacion = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdEstadoAutorizacionComision == 0 && x.IdUsuarioAutorizacion == autoriza.IdUsuarioAutorizacion && x.IdCiclo == param.idCiclo).FirstOrDefault();
                    if (confirmacionAutorizacion != null)
                    {
                        obj.autorizador = true;
                        obj.comisionAutorizada = true;
                        obj.idciclo = param.idCiclo;
                        obj.idComision = (int)confirmacionAutorizacion.IdComision;
                        //agregamos de ultimo al autorizador
                        addObjAutorizador.aprobado = true;
                        autorizados.Add(addObjAutorizador);
                        obj.autorizadores = autorizados;
                    } else {
                        obj.autorizador = true;
                        obj.comisionAutorizada = false;
                        obj.idciclo = param.idCiclo;
                        obj.idComision = 0;//no existe relacion
                        //agregamos de ultimo al autorizador
                        addObjAutorizador.aprobado = false;
                        autorizados.Add(addObjAutorizador);
                        obj.autorizadores = autorizados;
                    }
                }
                else
                {
                    obj.autorizador = false;
                    obj.comisionAutorizada = false;
                    obj.idciclo = param.idCiclo;
                    obj.idComision = 0; //no existe relacion
                    obj.autorizadores = autorizados;
                }
                return obj;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch VerificarAutorizadorPorComision() mensaje : {ex}");
                AutorizadorRespuestaModel obj = new AutorizadorRespuestaModel();
                return obj;
            }
        }
        private List<Autorizador> ObtenerAutorizadores(int idUserAutorizacion, int tipoAutorizador,int idCiclo, string usuarioLogin)
        {
            try
            {
                List<Autorizador> list = new List<Autorizador>();

                var autorizado = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdTipoAutorizacion == tipoAutorizador && x.Estado == true && x.IdUsuarioAutorizacion != idUserAutorizacion).ToList();
                foreach(var iten in autorizado)
                {
                    Autorizador obj = new Autorizador();
                    obj.nombre = iten.Nombres;
                    obj.apellido = iten.Apellidos;
                    obj.area = iten.DescripcionArea;
                   
                    var tieneAutorizacion = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdEstadoAutorizacionComision == 0 && x.IdUsuarioAutorizacion == iten.IdUsuarioAutorizacion && x.IdCiclo == idCiclo).FirstOrDefault();
                    if (tieneAutorizacion != null) 
                    {
                        obj.aprobado = true;
                    } else {
                        obj.aprobado = false;
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch VerificarAutorizadorPorComision() mensaje : {ex}");
                List<Autorizador> obj = new List<Autorizador>();
                return obj;
            }

        }
    }
}
