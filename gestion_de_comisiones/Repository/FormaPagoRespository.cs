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

        public List<VwObtenercomisionesFormaPago> GetComisiones(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
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
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
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
        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListFormaPago(BuscarInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
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
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
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
        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInputModel param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
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
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
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
                var autoriza = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdUsuario == param.idUsuario && x.IdTipoAutorizacion == idTipoAutorizacionFormaPago && x.Estado == true).FirstOrDefault();
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
                        obj.idAutorizacionComision = (int)confirmacionAutorizacion.IdAutorizacionComision;
                        //agregamos de ultimo al autorizador
                        addObjAutorizador.aprobado = true;
                        autorizados.Add(addObjAutorizador);
                        obj.autorizadores = autorizados;
                    } else {
                        obj.autorizador = true;
                        obj.comisionAutorizada = false;
                        obj.idciclo = param.idCiclo;
                        obj.idComision = 0;//no existe relacion
                        obj.idAutorizacionComision = 0;
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
                    obj.idAutorizacionComision = 0;
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
                    obj.idArea = iten.IdArea;
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

        public bool ConfirmarAutorizacion(ConfirmarAutorizacionParam param)
        {
            try
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository ConfirmarAutorizacion() ");
                int estadoAutorizacionComision = 0; //aprobado
                var autorizado = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdUsuario == param.idUsuario ).FirstOrDefault();
                var verificarAutorizacion = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdUsuario == autorizado.IdUsuario && x.IdEstadoAutorizacionComision == estadoAutorizacionComision).FirstOrDefault(); 
                if(verificarAutorizacion == null)
                {
                    var obtenerComision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo).FirstOrDefault();
                    AutorizacionComision obj = new AutorizacionComision();
                    obj.IdComision = obtenerComision.IdComision;
                    obj.IdUsuarioAutorizacion = autorizado.IdUsuarioAutorizacion;
                    obj.IdEstadoAutorizacionComision = estadoAutorizacionComision;
                    obj.Descripcion = "";
                    obj.IdUsuarioModificacion = param.idUsuario;
                    obj.FechaCreacion = DateTime.Now;
                    obj.FechaActualizacion = DateTime.Now;
                    ContextMulti.AutorizacionComisions.Add(obj);
                    ContextMulti.SaveChanges();


                }
                  return true;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch ConfirmarAutorizacion() mensaje : {ex}");
                return false;
            }
        }
        public bool VerificarSiExisteAutorizacionFormaPagoCiclo(string usuarioLogin, int idCiclo)
        {
            try
            {

            
            Logger.LogWarning($" usuario: {usuarioLogin} iniciando la funcion VerificarSiExisteAprobacion "+ "parametros: "+"idciclo: "+idCiclo+ " ");
                int estadoAutorizacionComision = 0; //estado aprobado de la tabla ESTADO_AUTORIZACION_COMISION
                var cantidad= ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdCiclo == idCiclo && x.IdEstadoAutorizacionComision == estadoAutorizacionComision).Count();
                if(cantidad > 0)
                {
                    return true;
                } else
                {
                    return false;
                }
     
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch ConfirmarAutorizacion() mensaje : {ex}");
                return false;
            }
        }

        public ConfirmarPagoOutPut VerificarCierreFormaPago(VerificarCierreFormaPagoParam param)
        {
            try
            {

                ConfirmarPagoOutPut obj = new ConfirmarPagoOutPut();
                Logger.LogWarning($" usuario: {param.usuarioLogin} iniciando la funcion VerificarSiExisteAprobacion " + "parametros: " + "idciclo: " + param.idCiclo + " ");
                int tipoAutorizacionFormaPago = 3; //estado aprobado de la tabla ESTADO_AUTORIZACION_COMISION
                List<Autorizador> lista = ListarAutorizadoresPorTipoAutorizacion(tipoAutorizacionFormaPago, param.idCiclo, param.usuarioLogin);
                obj.Habilitado = VerificarAutorizacionPorArea(lista, tipoAutorizacionFormaPago, param.idCiclo, param.usuarioLogin);
                var detalle = ListarAutorizadoresPorAreas(lista, param.usuarioLogin);
                obj.ListaPorAreas = detalle.ListaPorAreas;
                return obj;

            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch VerificarSiExisteAutorizacionFormaPagoCiclo() mensaje : {ex}");
                ConfirmarPagoOutPut obj = new ConfirmarPagoOutPut();
                return obj;
            }
        }
        private List<Autorizador> ListarAutorizadoresPorTipoAutorizacion(int tipoAutorizador, int idCiclo, string usuarioLogin)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuarioLogin} inicio el ListarAutorizadoresPorTipoAutorizacion() ");
                List<Autorizador> list = new List<Autorizador>();
                var autorizado = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdTipoAutorizacion == tipoAutorizador && x.Estado == true).ToList();
                foreach (var iten in autorizado)
                {
                    Autorizador obj = new Autorizador();
                    obj.nombre = iten.Nombres;
                    obj.apellido = iten.Apellidos;
                    obj.area = iten.DescripcionArea;
                    obj.idArea = iten.IdArea;

                    var tieneAutorizacion = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdEstadoAutorizacionComision == 0 && x.IdUsuarioAutorizacion == iten.IdUsuarioAutorizacion && x.IdCiclo == idCiclo).FirstOrDefault();
                    if (tieneAutorizacion != null)
                    {
                        obj.aprobado = true;
                    }
                    else
                    {
                        obj.aprobado = false;
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch ListarAutorizadoresPorTipoAutorizacion() mensaje : {ex}");
                List<Autorizador> obj = new List<Autorizador>();
                return obj;
            }

        }

        private bool VerificarAutorizacionPorArea(List<Autorizador> lista,int tipoAutorizador, int idCiclo, string usuarioLogin)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuarioLogin} inicio el ListarAutorizadoresPorTipoAutorizacion() ");
                List<Autorizador> list = new List<Autorizador>();
                var autorizado = lista.GroupBy(p => new { p.idArea }).Select(g => new { idArea = g.Key.idArea, cantidad = g.Count() }).ToList();
                bool habilitado = true;

                foreach(var iten in autorizado)
                {
                    var CantiAutorizadosArea = lista.Where(x => x.idArea == iten.idArea && x.aprobado == true).Count();
                    var areaConfig = ContextMulti.AutorizacionesAreas.Where(x => x.IdArea == iten.idArea).FirstOrDefault();
                    if(CantiAutorizadosArea < areaConfig.Cantidad)
                    {
                        habilitado = false;
                    }
                }           
                return habilitado;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch VerificarAutorizacionPorArea() mensaje : {ex}");
                return false;
            }
        }
        private ConfirmarPagoOutPut ListarAutorizadoresPorAreas(List<Autorizador> lista, string usuarioLogin)
        {
            try
            {
                // AutorizacionAreaModel
                //Autorizador
                ConfirmarPagoOutPut model = new ConfirmarPagoOutPut();
                

                Logger.LogWarning($" usuario: {usuarioLogin} inicio el ListarAutorizadoresPorAreas() ");
                
                var autorizado = lista.GroupBy(p => new { p.idArea }).Select(g => new { idArea = g.Key.idArea, cantidad = g.Count() }).ToList();

                List<AutorizacionAreaModel> ListArea = new List<AutorizacionAreaModel>();
                foreach (var iten in autorizado)
                {
                    bool habilitado = false;
                    AutorizacionAreaModel area = new AutorizacionAreaModel();
                    var detalleArea = lista.Where(x => x.idArea == iten.idArea).FirstOrDefault();
                    var CantiAutorizadosArea = lista.Where(x => x.idArea == iten.idArea && x.aprobado == true).Count();
                    var areaConfig = ContextMulti.AutorizacionesAreas.Where(x => x.IdArea == iten.idArea).FirstOrDefault();
                    if (CantiAutorizadosArea >= areaConfig.Cantidad){
                        habilitado = true;
                    }                    
                    area.Area = detalleArea.area;
                    area.IdArea = detalleArea.idArea;
                    area.Habilitado = habilitado;
                    area.CantidadHabilitados = CantiAutorizadosArea; //cantidad autorizados
                    area.CantidadConfigMinima = areaConfig.Cantidad;
                    var seleccionados = lista.Where(x => x.idArea == iten.idArea).ToList();
                    List<Autorizador> autores = new List<Autorizador>();
                    foreach (var item in seleccionados)
                    {
                        Autorizador autor = new Autorizador();
                        autor.idArea = item.idArea;
                        autor.area = item.area;
                        autor.nombre = item.nombre;
                        autor.apellido = item.apellido;
                        autor.idUsuario = item.idUsuario;
                        autor.aprobado = item.aprobado;
                        autores.Add(autor);
                    }
                    area.ListaAutorizadores = autores;
                    ListArea.Add(area);
                }
                model.ListaPorAreas = ListArea;
                Logger.LogWarning($" usuario: {usuarioLogin} fin del ListarAutorizadoresPorAreas() ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch ListarAutorizadoresPorAreas() mensaje : {ex}");
                ConfirmarPagoOutPut model = new ConfirmarPagoOutPut();
                return model;
            }
        }


    }
}
