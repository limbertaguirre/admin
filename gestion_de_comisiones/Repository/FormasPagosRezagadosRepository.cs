using System;
using System.Collections.Generic;
using System.Linq;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Repository
{
    public class FormasPagosRezagadosRepository : IFormasPagosRezagadosRepository
    {

        private readonly BDMultinivelContext ContextMulti;
        private readonly ILogger<FormasPagosRezagadosRepository> Logger;
        private readonly int ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS = GpEstadoComision.PENDIENTE_FORMA_DE_PAGO_9;
        private readonly int TIPO_COMISION_REZAGADOS = GpTipoComision.PAGO_REZAGADOS_2;
        private readonly int TIPO_PAGO_TRANSFERENCIA = TipoPago.TRANSFERENCIA;
        private readonly IEnvioCorreoRezagadoService EnvioCorreoService;
        private readonly IConfiguration Config;

        public FormasPagosRezagadosRepository(BDMultinivelContext multinivelDbContext, IEnvioCorreoRezagadoService envioCorreoService, IConfiguration config ,ILogger<FormasPagosRezagadosRepository> logger)
        {
            this.ContextMulti = multinivelDbContext;
            this.Logger = logger;
            this.EnvioCorreoService = envioCorreoService;
            Config = config;
        }

        public object GetCiclos(string username)
        {
            try
            {
                Logger.LogInformation($" usuario: {username}, inicio repository FormasPagosRezagadosRepository -> getCiclos() ");
                List<int> cicloRezagadoDoble = new List<int>();
                using (var command = ContextMulti.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $"select case COUNT(r.id_ciclo) " +
                                        "when 1 then(select rr.id_comision from BDMultinivel.dbo.VwObtenerCiclosRezagados rr where rr.id_ciclo = r.id_ciclo) " +
                                        "when 2 then(select top 1 rr.id_comision from BDMultinivel.dbo.VwObtenerCiclosRezagados rr where rr.id_ciclo = r.id_ciclo) end as id_comision, " +
                                        "r.id_ciclo from BDMultinivel.dbo.VwObtenerCiclosRezagados r " +
                                        $"where r.id_estado_comision = {ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS} and r.id_tipo_comision = {TIPO_COMISION_REZAGADOS} " +
                                        "group by r.id_ciclo";
                    command.CommandType = System.Data.CommandType.Text;
                    ContextMulti.Database.OpenConnection();
                    using var resultQuery = command.ExecuteReader();
                    if (resultQuery.HasRows)
                    {
                        while (resultQuery.Read())
                        {
                            Logger.LogInformation($"FormasPagosRezagadosRepository - GetCiclos command id_comision: {resultQuery["id_comision"]}");
                            Logger.LogInformation($"FormasPagosRezagadosRepository - GetCiclos command id_ciclo: {resultQuery["id_ciclo"]}");
                            cicloRezagadoDoble.Add(Convert.ToInt32(resultQuery["id_comision"]));
                        }
                    }
                }
                var ciclosR = ContextMulti.VwObtenerCiclosRezagados.Where(x => x.IdEstadoComision == ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS && x.IdTipoComision == TIPO_COMISION_REZAGADOS)
                    .Select(u => new
                    {
                        u.IdComision,
                        u.IdCiclo,
                        u.Nombre,
                        //u.Estado,
                        u.IdEstadoComision
                    })
                    .Where(x => cicloRezagadoDoble.Contains(x.IdComision))
                    .Distinct()
                    .OrderBy(c => c.IdComision)
                    .ToList();
                return ciclosR;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {username} error catch FormasPagosRezagadosRepository - getCiclos() mensaje : {ex.Message}");
                Logger.LogWarning($" usuario: {username} error catch FormasPagosRezagadosRepository - getCiclos() StackTrace : {ex.StackTrace}");
                List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }

        public List<VwObtenercomisionesFormaPago> GetComisionesRezagados(ComisionesPagosInput param)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository obtenerComisionesPendientes() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo: {param.idCiclo} , idComision: {param.idComision}");                
                int idEstadoDetalleSifacturo = 2; // Si factur[o la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;                
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.idComision && x.IdCiclo == param.idCiclo && x.IdTipoComision == TIPO_COMISION_REZAGADOS && (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetComisiones() mensaje : {ex.Message}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }

        public bool VerificarSiExisteAutorizacionFormaPagoCiclo(string usuarioLogin, int idCiclo, int comisionId)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuarioLogin} iniciando la funcion VerificarSiExisteAprobacion " + "parametros: " + "idciclo: " + idCiclo + " ");
                int estadoAutorizacionComision = 0; //estado aprobado de la tabla ESTADO_AUTORIZACION_COMISION
                var cantidad = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdCiclo == idCiclo && x.IdComision == comisionId && x.IdEstadoAutorizacionComision == estadoAutorizacionComision).Count();
                if (cantidad > 0)
                {
                    return true;
                }
                else
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

        public List<TipoPagoInputmodel> GetListarFormaPagos(ParamFormaPagosOutputModel param)
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

        public ConfirmarPagoOutPut VerificarCierreFormaPago(VerificarCierreFormaPagoParam param)
        {
            try
            {
                ConfirmarPagoOutPut obj = new ConfirmarPagoOutPut();
                Logger.LogWarning($" usuario: {param.usuarioLogin} iniciando la funcion VerificarSiExisteAprobacion " + "parametros: " + "idciclo: " + param.idCiclo + " ");
                int tipoAutorizacionFormaPago = 3; //estado aprobado de la tabla ESTADO_AUTORIZACION_COMISION
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                List<Autorizador> lista = ListarAutorizadoresPorTipoAutorizacion(tipoAutorizacionFormaPago, param.idCiclo, param.usuarioLogin);
                obj.Habilitado = VerificarAutorizacionPorArea(lista, tipoAutorizacionFormaPago, param.idCiclo, param.usuarioLogin);
                var detalle = ListarAutorizadoresPorAreas(lista, param.usuarioLogin);
                obj.ListaPorAreas = detalle.ListaPorAreas;
                obj.ListSeleccionados = ConsultarCierreInfoFormaPagos(param.idCiclo, param.usuarioLogin, ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS, idEstadoDetalleSifacturo, idEstadoDetalleNoPresentaFactura, TIPO_COMISION_REZAGADOS);
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

        private bool VerificarAutorizacionPorArea(List<Autorizador> lista, int tipoAutorizador, int idCiclo, string usuarioLogin)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuarioLogin} inicio el ListarAutorizadoresPorTipoAutorizacion() ");
                List<Autorizador> list = new List<Autorizador>();
                var autorizado = lista.GroupBy(p => new { p.idArea }).Select(g => new { idArea = g.Key.idArea, cantidad = g.Count() }).ToList();
                bool habilitado = true;

                foreach (var iten in autorizado)
                {
                    var CantiAutorizadosArea = lista.Where(x => x.idArea == iten.idArea && x.aprobado == true).Count();
                    var areaConfig = ContextMulti.AutorizacionesAreas.Where(x => x.IdArea == iten.idArea).FirstOrDefault();
                    if (CantiAutorizadosArea < areaConfig.Cantidad)
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
                    if (CantiAutorizadosArea >= areaConfig.Cantidad)
                    {
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

        private List<FormaPagoDisponiblesModel> ConsultarCierreInfoFormaPagos(int idCiclo, string usuarioLogin, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision)
        {
            try
            {
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                Logger.LogWarning($" usuario: {usuarioLogin} inicio el repository ConsultarCierreInfoFormaPagos() ");
                Logger.LogWarning($" usuario: {usuarioLogin} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();

                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdEstadoComision == idEstadoComision && (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                List<FormaPagoModel> LisFormaPagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new FormaPagoModel(p.IdTipoPago, p.Nombre, p.Descripcion, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion, (bool)p.Estado, p.Icono)).ToList();
                FormaPagoModel nuevoNinguno = new FormaPagoModel() { IdTipoPago = 0, Nombre = "Sin Asignar", Descripcion = "", IdUsuario = 1, Estado = true, Icono = "ningunPago" };
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
                Logger.LogWarning($" usuario: {usuarioLogin} error catch ConsultarCierreInfoFormaPagos() mensaje : {ex}");
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                return list;
            }
        }

        public bool CerrarFormaDePago(CierreformaPagoInput param)
        {
            Logger.LogInformation($" usuario: {param.usuarioLogin} -  inicio el RegistrarDecuentoComision() en repos");
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try
            {
                var parameterReturn = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@comision_id",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.comisionId
                                },
                                new SqlParameter() {
                                            ParameterName = "@id_ciclo",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.idCiclo
                                },
                                new SqlParameter() {
                                            ParameterName = "@id_usuario",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.idUsuario
                                }
                };
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_PROCESAR_CERRAR_FORMA_PAGO_REZAGADOS] @comision_id, @id_ciclo, @id_usuario  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                if (returnValue > 0)
                {
                    dbcontextTransaction.Commit();
                    Logger.LogInformation($" usuario: {param.usuarioLogin}-  Se proceso la forma de pago DE FORMA EXISTOSA EL [SP_PROCESAR_CERRAR_FORMA_PAGO_REZAGADOS].");
                    return true;
                }
                else
                {
                    dbcontextTransaction.Rollback();
                    Logger.LogInformation($" usuario: {param.usuarioLogin}-  NO ROLLBACK EN EL SP [SP_PROCESAR_CERRAR_FORMA_PAGO_REZAGADOS]");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch CerrarFormaDePago() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return false;
            }
        }

        public object VerificarAutorizadorPorComision(AutorizacionVerificarParam param)
        {
            try
            {
                int idTipoAutorizacionFormaPago = 4;
                AutorizadorRespuestaModel obj = new AutorizadorRespuestaModel();
                List<Autorizador> listAurorizadores = new List<Autorizador>();
                List<Autorizador> autorizados = new List<Autorizador>();

                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository VerificarAutorizadorPorComision() ");
                var autoriza = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdUsuario == param.idUsuario && x.IdTipoAutorizacion == idTipoAutorizacionFormaPago && x.Estado == true).FirstOrDefault();
                if (autoriza != null)
                {
                    autorizados = ObtenerAutorizadores(autoriza.IdUsuarioAutorizacion, idTipoAutorizacionFormaPago, param.comisionId, param.idCiclo, param.usuarioLogin);
                    Autorizador addObjAutorizador = new Autorizador
                    {
                        nombre = autoriza.Nombres,
                        apellido = autoriza.Apellidos,
                        area = autoriza.DescripcionArea
                    };
                    var confirmacionAutorizacion = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdEstadoAutorizacionComision == 0 && x.IdUsuarioAutorizacion == autoriza.IdUsuarioAutorizacion && x.IdCiclo == param.idCiclo && x.IdComision == param.comisionId).FirstOrDefault();
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
                    }
                    else
                    {
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

        private List<Autorizador> ObtenerAutorizadores(int idUserAutorizacion, int tipoAutorizador, int comisionId, int idCiclo, string usuarioLogin)
        {
            try
            {
                List<Autorizador> list = new List<Autorizador>();

                var autorizado = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdTipoAutorizacion == tipoAutorizador && x.Estado == true && x.IdUsuarioAutorizacion != idUserAutorizacion).ToList();
                foreach (var iten in autorizado)
                {
                    Autorizador obj = new Autorizador();
                    obj.nombre = iten.Nombres;
                    obj.apellido = iten.Apellidos;
                    obj.area = iten.DescripcionArea;
                    obj.idArea = iten.IdArea;
                    var tieneAutorizacion = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdEstadoAutorizacionComision == 0 && x.IdUsuarioAutorizacion == iten.IdUsuarioAutorizacion && x.IdCiclo == idCiclo && x.IdComision == comisionId).FirstOrDefault();
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
                var autorizado = ContextMulti.VwListarAutorizacionesTipoes.Where(x => x.IdUsuario == param.idUsuario).FirstOrDefault();
                var verificarAutorizacion = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdComision == param.idComision && x.IdUsuario == autorizado.IdUsuario && x.IdEstadoAutorizacionComision == estadoAutorizacionComision).FirstOrDefault();
                if (verificarAutorizacion == null)
                {                    
                    AutorizacionComision obj = new AutorizacionComision();
                    obj.IdComision = param.idComision;
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

        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListFormaPago(BuscarInputModel param)
        {
            try
            {
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetComisionesPorCarnet() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS}");
                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;// estado de la tabla detalle de comision
                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == TIPO_COMISION_REZAGADOS).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdEstadoComision == ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS && (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                var lista = ListComisiones.Where(x => x.Ci.Contains(param.nombreCriterio.Trim())).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetComisionesPorCarnet() mensaje : {ex}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }

        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInputModel param)
        {
            try
            {
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository FiltrarComisionPagoPorTipoPago() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS}");
                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdComision == param.comisionId && x.IdTipoComision == TIPO_COMISION_REZAGADOS).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdEstadoComision == ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS && x.IdTipoPago == param.idTipoPago).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch FiltrarComisionPagoPorTipoPago() mensaje : {ex}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }

        public object GetComisionesPorFormaPago(FormaPagosDisponiblesInputModel param)
        {
            try
            {
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetComisionesPorCarnet() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS}");

                int idEstadoDetalleSifacturo = 2; //variable , si facturo la comision detalle
                int idEstadoDetalleNoPresentaFactura = 6;
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.comisionId && x.IdEstadoComision == ESTADO_COMISION_REZAGADOS_FORMAS_PAGOS && (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                List<FormaPagoModel> LisFormaPagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new FormaPagoModel(p.IdTipoPago, p.Nombre, p.Descripcion, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion, (bool)p.Estado, p.Icono)).ToList();
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
    }
}
