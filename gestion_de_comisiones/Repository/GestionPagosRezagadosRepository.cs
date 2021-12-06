using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Repository
{
    public class GestionPagosRezagadosRepository : IGestionPagosRezagadosRepository
    {
        private readonly BDMultinivelContext ContextMulti;
        private readonly ILogger<GestionPagosRezagadosRepository> Logger;
        public GestionPagosRezagadosRepository(BDMultinivelContext multinivelDbContext, ILogger<GestionPagosRezagadosRepository> logger)
        {
            this.ContextMulti = multinivelDbContext;
            this.Logger = logger;
        }

        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionRezagados)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                var ciclosR = ContextMulti.VwObtenerCiclosRezagados.Where(x => x.IdEstadoComision == idEstadoComision && x.IdTipoComision == idTipoComisionRezagados)
                    .Select(u => new
                    {
                        u.IdComision,
                        u.IdCiclo,
                        u.Nombre,
                        u.Estado
                    })
                    .ToList();                
                return ciclosR;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch GestionPagosRezagadosRepository - getCiclos() mensaje : {ex.Message}");
                Logger.LogWarning($" usuario: {usuario} error catch GestionPagosRezagadosRepository - getCiclos() StackTrace : {ex.StackTrace}");
                List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }

        public object GetComisionesPagos(string usuario, int idCiclo, int idEstadoComision, int idTipoComisionPagoComision, int idComision)
        {
            try
            {
                Logger.LogInformation($"Inicio GestionPagosRezagadosRepository - GetComisionesPagos");
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                //var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == idCiclo && x.IdComision == idComision && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision).ToList();
                Logger.LogInformation($"Fin GestionPagosRezagadosRepository - GetComisionesPagos");
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch GestionPagosRezagadosRepository - GetComisionesPagos() mensaje : {ex.Message}");
                Logger.LogWarning($" usuario: {usuario} error catch GestionPagosRezagadosRepository - GetComisionesPagos() StackTrace : {ex.StackTrace}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }

        public object ObtenerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput body)
        {
            try
            {
                Logger.LogInformation($" usuario: {body.user} inicio el repository ObtenerPagosRezagadosTransferencias() ");
                Logger.LogInformation($" usuario: {body.user} parametros: idciclo: {body.cicloId}");
                int cicloId = Convert.ToInt32(body.cicloId);
                int tipoPagoTransferencia = 2;
                List<VwObtenerRezagadosPago> info = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdComision == body.comisionId && x.IdTipoPago == tipoPagoTransferencia)
                    .ToList();

                var montoTotal = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdComision == body.comisionId && x.IdTipoPago == tipoPagoTransferencia)
                    .Sum(x => x.ImportePorEmpresa);

                ObtenerPagosRezagadosTransferenciasOutput o = new ObtenerPagosRezagadosTransferenciasOutput();
                o.list = info;
                o.montoTotal = montoTotal.ToString();
                return o;
            }
            catch (Exception ex)
            {
                Logger.LogError ($" usuario: {body.user} error catch ObtenerPagosRezagadosTransferencias() mensaje : {ex}");
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                return list;
            }
        }

        public GestionPagosRezagadosEvent ConfirmarPagosRezagadosTransferencias(ConfirmarPagosRezagadosTransferenciasInput param)
        {
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try
            {
                Logger.LogInformation($" usuario: {param.user}, inicio repository ConfirmarPagosRezagadosTransferencias(): idciclo {param.cicloId}  ");

                var usuarioId = ContextMulti.Usuarios
                    .Where(x => x.Usuario1 == param.user)
                    .Select(u => new
                    {
                        usuarioId = u.IdUsuario
                    }).FirstOrDefault();

                Logger.LogInformation($" usuarioId: {usuarioId}, inicio confirmarRechazadosTransferidosSeleccionados en repository ConfirmarPagosRezagadosTransferencias()");
                if (!confirmarRezagadosTransferidosSeleccionados(param, usuarioId))
                {
                    return postEvent(GestionPagosRezagadosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a confirmados, verifique e intente nuevamente.");
                }

                int tipoPagoTransferencia = 2;
                int idEstadoComisionDetalleEmpresaConfirmado = 2;
                int tipoComision = 2;
                int estadoComision = 11;
                List<VwObtenerRezagadosPago> l = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == param.cicloId && x.IdEmpresa == param.empresaId && x.IdTipoPago == tipoPagoTransferencia &&
                            x.IdEstadoComisionDetalleEmpresa != idEstadoComisionDetalleEmpresaConfirmado)
                    .ToList();

                if (!confirmarRezagadosTransferidosNoSeleccionados(param, usuarioId, l))
                {
                    return postEvent(GestionPagosRezagadosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a rechazados, verifique e intente nuevamente.");
                }

                // SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS
                Logger.LogInformation($" Iniciando carga de parametros de entrada para ejecutar el SP SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS");
                Logger.LogInformation($" UsuarioId: {usuarioId}, CicloId: {param.cicloId}, EmpresaId: {param.empresaId}");
                var parameterReturn = new SqlParameter[] {
                                new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@TipoComision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = tipoComision
                                },
                                new SqlParameter() {
                                            ParameterName = "@EstadoComision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = estadoComision
                                },
                                new SqlParameter() {
                                            ParameterName = "@ComisionId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.comisionId
                                },
                                new SqlParameter() {
                                            ParameterName = "@CicloId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.cicloId
                                },
                                new SqlParameter() {
                                                ParameterName = "@ComisionId",
                                                SqlDbType =  System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Input,
                                                Value = param.comisionId
                                },
                                new SqlParameter() {
                                            ParameterName = "@EmpresaId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.empresaId
                                },
                                new SqlParameter() {
                                            ParameterName = "@UsuarioId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = usuarioId.usuarioId
                                },
                                new SqlParameter() {
                                            ParameterName = "@TipoPago",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = tipoPagoTransferencia
                              }
                           };
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS] @TipoComision, @EstadoComision, @ComisionId, @CicloId,  @EmpresaId, @UsuarioId, @TipoPago ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, repository handleConfirmarPagosTransferencias fin SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS returnValue: {returnValue}  ");
                if (returnValue == -1)
                {
                    // Entro al catch del SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS hizo Rollback
                    Logger.LogWarning($"repository handleConfirmarPagosTransferencias() SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS @returnValue: {returnValue}");
                    dbcontextTransaction.Rollback();
                    return postEvent(GestionPagosRezagadosEvent.CATCH_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Pasó algo inesperado, no se pudo registrar a los ACI rechazados.");
                }

                dbcontextTransaction.Commit();
                // Si returnValue no es -1 ni 2, es 1
                return postEvent(GestionPagosRezagadosEvent.SUCCESS_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Se realizó correctamente la confirmación para pagos por transferencias de los ACI seleccionados.");
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.user} CATCH handleConfirmarPagosTransferencias() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return postEvent(GestionPagosRezagadosEvent.ERROR, $"NO se pudo realizar la confirmación de los pagos por transferencia, verifique e intente nuevamente. Mensaje: {ex.Message}");
            }
        }

        private bool confirmarRezagadosTransferidosSeleccionados(ConfirmarPagosRezagadosTransferenciasInput body, dynamic usuarioId)
        {
            try
            {
                Logger.LogInformation($" usuarioId: {usuarioId}, inicio repository confirmarRezagadosTransferidosSeleccionados() body.confirmados.Count: {body.confirmados.Count}");
                for (int i = 0; i < body.confirmados.Count; i++)
                {
                    var parameterReturn = new SqlParameter[] {
                                   new SqlParameter  {
                                                ParameterName = "ReturnValue",
                                                SqlDbType = System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Output,
                                    },
                                    new SqlParameter() {
                                                ParameterName = "@CicloId",
                                                SqlDbType =  System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Input,
                                                Value = body.cicloId
                                  },
                                    new SqlParameter() {
                                                ParameterName = "@EmpresaId",
                                                SqlDbType =  System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Input,
                                                Value = body.empresaId
                                  },
                                    new SqlParameter() {
                                                ParameterName = "@ComisionId",
                                                SqlDbType =  System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Input,
                                                Value = body.comisionId
                                  },
                                   new SqlParameter() {
                                                ParameterName = "@UsuarioId",
                                                SqlDbType =  System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Input,
                                                Value = usuarioId.usuarioId
                                  },
                                   new SqlParameter() {
                                                ParameterName = "@ComisionDetalleEmpresaId",
                                                SqlDbType =  System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Input,
                                                Value = body.confirmados[i]
                                  }
                               };

                    Logger.LogInformation($" result: {body.confirmados[i]}, inicio repository confirmarRezagadosTransferidosSeleccionados():  SP_? @ComisionDetalleEmpresaId {body.confirmados[i]}  ");
                    var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[] @CicloId,  @EmpresaId, @ComisionId, @UsuarioId, @ComisionDetalleEmpresaId", parameterReturn);
                    int returnValue = (int)parameterReturn[0].Value;

                    Logger.LogInformation($" result: {result}, inicio repository confirmarTransferidosSeleccionados():  SP_CONFIRMAR_TRANSFERENCIAS_SELECCIONADAS returnValue {returnValue} ");
                    if (returnValue == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} CATCH confirmarRezagadosTransferidosSeleccionados(): {ex}");
                return false;
            }
        }

        private bool confirmarRezagadosTransferidosNoSeleccionados(ConfirmarPagosRezagadosTransferenciasInput body, dynamic usuarioId, List<VwObtenerRezagadosPago> l)
        {
            try
            {
                Logger.LogInformation($" usuarioId: {usuarioId}, inicio repository confirmarRezagadosTransferidosNoSeleccionados() : {l.Count}");
                for (int i = 0; i < l.Count; i++)
                {
                    VwObtenerRezagadosPago o = l[i];
                    Logger.LogInformation($" repository confirmarRezagadosTransferidosNoSeleccionados() para RECHAZAR IdComisionDetalleEmpresa: {o.IdComisionDetalleEmpresa}");
                    var parameterReturn = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@CicloId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.cicloId
                              },
                                new SqlParameter() {
                                            ParameterName = "@EmpresaId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.empresaId
                              },
                                new SqlParameter() {
                                            ParameterName = "@ComisionId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.comisionId
                              },
                               new SqlParameter() {
                                            ParameterName = "@UsuarioId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = usuarioId.usuarioId
                              },
                               new SqlParameter() {
                                            ParameterName = "@ComisionDetalleEmpresaId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = o.IdComisionDetalleEmpresa
                              }
                           };

                    
                    var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS] @CicloId,  @EmpresaId, @ComisionId, @UsuarioId, @ComisionDetalleEmpresaId", parameterReturn);
                    int returnValue = (int)parameterReturn[0].Value;

                    Logger.LogInformation($" result: {result}, inicio repository confirmarRezagadosTransferidosNoSeleccionados():  SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS returnValue {returnValue} ");
                    if (returnValue == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} CATCH confirmarRezagadosTransferidosNoSeleccionados(): {ex}");
                return false;
            }
        }        

        public dynamic handleTransferenciasEmpresas(ComisionesPagosInput param)
        {
            try
            {
                int idTipoPagoTransferencia = 2;
                int idTipoComisionRezagados = 2;
                int idEstadoComisionPagosRezagados = 11;
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository handleTransferenciasEmpresas() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo: {param.idCiclo} , idTipoComisionRezagados: {idTipoComisionRezagados}, idTipoPagoTransferencia: {idTipoPagoTransferencia}");
                var empresasIds = ContextMulti.Usuarios
                .Join(ContextMulti.AsignacionEmpresaPagoes,
                      p => p.IdUsuario,
                      e => e.IdUsuario,
                      (p, e) => new
                      {
                          empresaId = e.IdEmpresa,
                          usuario = p.Usuario1,
                          idTipoPago = e.IdTipoPago
                      }
                 )
                .Where(x => x.usuario == param.usuarioLogin && x.idTipoPago == idTipoPagoTransferencia)
                .Select(u => new
                {
                    u.empresaId
                })
                .ToList();

                int[] ids = new int[empresasIds.Count];
                for (int i = 0; i < empresasIds.Count; i++)
                {
                    ids[i] = (int)empresasIds[i].empresaId;
                }
                var empresas = ContextMulti.VwObtenerEmpresasComisionesDetalleEmpresas
                    .Where(x => x.IdCiclo == param.idCiclo && x.IdComision == param.idComision && x.IdTipoComision == idTipoComisionRezagados && x.IdTipoPago == idTipoPagoTransferencia && x.IdEstadoComision == idEstadoComisionPagosRezagados && x.MontoTransferir != 0 && ids.Contains(x.IdEmpresa))
                    .Select(e => new
                    {
                        idCiclo = e.IdCiclo,
                        idEmpresa = e.IdEmpresa,
                        empresa = e.Empresa,
                        montoATransferir = e.MontoTransferir,
                    }).ToList();
                return empresas;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch handleTransferenciasEmpresas() mensaje : {ex}");
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                return list;
            }
        }

        public GestionPagosRezagadosEvent handleVerificarPagosTransferenciasTodos(ObtenerRezagadosPagosTransferenciasInput body)
        {
            try
            {
                Logger.LogWarning($" usuario: {body.user} inicio el repository handleVerificarPagosTransferenciasTodos() ");
                Logger.LogWarning($" usuario: {body.user} parametros: idciclo:{body.cicloId} empresaId: {body.empresaId}");

                var usuarioId = ContextMulti.Usuarios
                    .Where(x => x.Usuario1 == body.user)
                    .Select(u => new
                    {
                        usuarioId = u.IdUsuario
                    }).FirstOrDefault();

                var parameterReturn = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@ComisionId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.comisionId 
                                },
                                new SqlParameter() {
                                            ParameterName = "@CicloId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.cicloId
                              },
                               new SqlParameter() {
                                            ParameterName = "@UsuarioId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = usuarioId.usuarioId
                              }
                           };
                var recargarCicloActual = false;
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS] @CicloId, @UsuarioId  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, fin repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS returnValue {returnValue}  ");
                if (returnValue == -1)
                {
                    // Rollback
                    Logger.LogInformation($" result: {result}, repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS returnValue -1. Catch e hizo Rollback. Analizar la razon.");
                }
                else if (returnValue == 1)
                {
                    Logger.LogInformation($" result: {result}, repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS returnValue 1. No hay pagos de transferencias pendientes de pago y se registro en tabla detalle forma de pagos.");
                    recargarCicloActual = true;
                }
                else if (returnValue == 2)
                {
                    Logger.LogInformation($" result: {result}, repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS returnValue 2. Aun hay pagos de transferencias pendientes de pago.");
                }

                var cantidadPendientes = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 1).Count();
                var cantidadRechazados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 3).Count();
                var cantidadConfirmados = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 2).Count();

                var sumaTotalConfirmados = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 2)
                    .Sum(x => x.ImportePorEmpresa);

                var sumaTotalRechazados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 3)
                    .Sum(x => x.ImportePorEmpresa);

                var sumaTotalPendientes = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 1)
                    .Sum(x => x.ImportePorEmpresa);

                var cantidadFechasPagosNull = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 1 && x.FechaDePago == null).Count();

                var fechaPagosExcel = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 1)
                    .Select(x => new { x.FechaDePago })
                    .FirstOrDefault();

                var empresa = (ContextMulti.Empresas
                    .Where(x => x.IdEmpresa == body.empresaId)
                    .Select(u => new {
                        u.Nombre
                    }).FirstOrDefault()).Nombre;

                int tipoPagoComisiones = 1;
                var ciclo = (ContextMulti.Cicloes
                    .Join(ContextMulti.GpComisions,
                        p => p.IdCiclo,
                        e => e.IdCiclo,
                        (p, e) => new
                        {
                            p.IdCiclo,
                            p.Nombre,
                            e.IdTipoComision
                        }
                    )
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoComision == tipoPagoComisiones)
                    .Select(u => new {
                        u.Nombre
                    }).FirstOrDefault()).Nombre;

                VerificarPagosTransferenciasOutput o = new VerificarPagosTransferenciasOutput();

                double ddd = (double)sumaTotalConfirmados;
                string sumaTotalConfirmadosSS = ddd.ToString("N2", new CultureInfo("is-IS"));

                double dd = (double)sumaTotalPendientes;
                string sumaTotalPendientesSS = dd.ToString("N2", new CultureInfo("is-IS"));

                double d = (double)sumaTotalRechazados;
                string sumaTotalRechazadosSS = d.ToString("N2", new CultureInfo("is-IS"));

                if (cantidadPendientes > 0)
                {
                    o.type = VerificarPagosTransferenciasOutput.PENDIENTES;
                    o.ciclo = ciclo;
                    o.empresa = empresa;
                    o.totalPendientes = cantidadPendientes;
                    o.montoTotalPendientes = sumaTotalPendientesSS;
                }
                else if (cantidadPendientes == 0 && (cantidadRechazados > 0 || cantidadConfirmados > 0))
                {
                    o.type = VerificarPagosTransferenciasOutput.CONFIRMADOS_O_RECHAZADOS;
                    o.ciclo = ciclo;
                    o.empresa = empresa;
                    o.totalConfirmados = cantidadConfirmados;
                    o.totalRechazados = cantidadRechazados;
                    o.totalEnviadosConfirmar = cantidadRechazados + cantidadConfirmados;
                    o.montoTotalConfirmados = sumaTotalConfirmadosSS;
                    o.montoTotalRechazados = sumaTotalRechazadosSS;
                }
                o.descargarExcel = fechaPagosExcel?.FechaDePago?.ToString();
                o.recargarCicloActual = recargarCicloActual;
                Logger.LogWarning($"handleVerificarPagosTransferenciasTodos() cantidadPendientes: {cantidadPendientes}, cantidadConfirmados: {cantidadConfirmados}, cantidadRechazados: {cantidadRechazados}");

                if (cantidadPendientes > 0)
                {
                    // No se confirmo TODAS las transacciones para esta empresa en este ciclo
                    return postEvent(GestionPagosRezagadosEvent.EXISTEN_PENDIENTES, o, $"Hay pendientes para confirmar el pago por transferencia de este ciclo ({ciclo}) para la empresa {empresa}.");
                }
                else if (cantidadRechazados > 0)
                {
                    return postEvent(GestionPagosRezagadosEvent.EXISTEN_RECHAZADOS, o, $"Hay rechazados en este ciclo ({ciclo}) para la empresa {empresa}.");
                }
                else
                {
                    return postEvent(GestionPagosRezagadosEvent.NO_EXISTEN_PENDIENTES_NI_RECHAZADOS, o, $"Se confirmaron todos los pagos por transferencia de este ciclo ({ciclo}) para la empresa {empresa}.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleVerificarPagosTransferenciasTodos() mensaje : {ex}");
                return postEvent(GestionPagosRezagadosEvent.ERROR, ex.Message);
            }
        }

        private GestionPagosRezagadosEvent postEvent(int type, string errorMessage) => postEvent(type, new DownloadFileTransferenciaOutput(), errorMessage);

        private GestionPagosRezagadosEvent postEvent(int type, VerificarPagosTransferenciasOutput data, string message)
        {
            GestionPagosRezagadosEvent e = new GestionPagosRezagadosEvent();
            e.eventType = type;
            if (data != null)
            {
                e.dataVerify = data;
            }
            if (message != null)
            {
                e.message = message;
            }
            return e;
        }

        private GestionPagosRezagadosEvent postEvent(int type, DownloadFileTransferenciaOutput file, string errorMessage)
        {
            GestionPagosRezagadosEvent e = new GestionPagosRezagadosEvent();
            e.eventType = type;
            if (file != null)
            {
                e.file = file;
            }
            if (errorMessage != null)
            {
                e.message = errorMessage;
            }
            return e;
        }

        public bool handleConfirmarPagosTransferenciasTodos(ObtenerRezagadosPagosTransferenciasInput body)
        {
            try
            {
                Logger.LogInformation($" usuario: {body.user}, inicio repository handleConfirmarPagosTransferenciasTodos(): idciclo {body.cicloId}  ");
                var usuarioId = ContextMulti.Usuarios
                    .Where(x => x.Usuario1 == body.user)
                    .Select(u => new
                    {
                        usuarioId = u.IdUsuario
                    }).FirstOrDefault();

                Logger.LogInformation($" usuarioId: {usuarioId}, inicio repository handleConfirmarPagosTransferenciasTodos()");
                var parameterReturn = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@ComisionId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.cicloId
                                },
                                new SqlParameter() {
                                            ParameterName = "@CicloId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.cicloId
                                },
                                new SqlParameter() {
                                            ParameterName = "@EmpresaId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.empresaId
                                },
                                new SqlParameter() {
                                            ParameterName = "@UsuarioId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = usuarioId.usuarioId
                                }
                           };

                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_CONFIRMAR_TRANSFERENCIAS_REZAGADOS_TODOS] @ComisionId, @CicloId,  @EmpresaId, @UsuarioId  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, inicio repository handleConfirmarPagosTransferenciasTodos(): SP_CONFIRMAR_TRANSFERENCIAS_REZAGADOS_TODOS returnValue {returnValue}  ");
                if (returnValue == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //return 0;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleDownloadFileEmpresas() mensaje : {ex}");
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                return false;
            }
        }
    }
}
