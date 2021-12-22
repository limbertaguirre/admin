﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.Modelos.GestionPagosRezagados;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace gestion_de_comisiones.Repository
{
    public class GestionPagosRezagadosRepository : IGestionPagosRezagadosRepository
    {
        private readonly BDMultinivelContext ContextMulti;
        private readonly ILogger<GestionPagosRezagadosRepository> Logger;
        private readonly int FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO = GpEstadoComision.FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO;
        private readonly int TIPO_COMISION_REZAGADOS = GpTipoComision.PAGO_REZAGADOS_2;
        private readonly int TIPO_PAGO_SIONPAY = TipoPago.SION_PAY;
        private readonly int TIPO_PAGO_TRANSFERENCIA = TipoPago.TRANSFERENCIA;        
        private readonly IEnvioCorreoRezagadoService EnvioCorreoService;

        public GestionPagosRezagadosRepository(BDMultinivelContext multinivelDbContext, IEnvioCorreoRezagadoService envioCorreoService, ILogger<GestionPagosRezagadosRepository> logger)
        {
            this.ContextMulti = multinivelDbContext;
            this.Logger = logger;
            this.EnvioCorreoService = envioCorreoService;
        }

        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionRezagados)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                List<int> cicloRezagadoDoble = new List<int>();
                using (var command = ContextMulti.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $"select case COUNT(r.id_ciclo) " +
                                        $"when 1 then(select rr.id_comision from BDMultinivel.dbo.VwObtenerCiclosRezagados rr where rr.id_ciclo = r.id_ciclo and rr.id_estado_comision = {FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO}) " +
                                        $"when 2 then(select top 1 rr.id_comision from BDMultinivel.dbo.VwObtenerCiclosRezagados rr where rr.id_ciclo = r.id_ciclo and rr.id_estado_comision = {FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO}) end as id_comision, " +
                                        "r.id_ciclo from BDMultinivel.dbo.VwObtenerCiclosRezagados r " +
                                        $"where r.id_estado_comision = {FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO} and r.id_tipo_comision = {TIPO_COMISION_REZAGADOS} " +
                                        "group by r.id_ciclo";
                    command.CommandType = System.Data.CommandType.Text;
                    ContextMulti.Database.OpenConnection();
                    using var resultQuery = command.ExecuteReader();
                    if (resultQuery.HasRows)
                    {
                        while (resultQuery.Read())
                        {
                            Logger.LogInformation($"GestionPagosRezagadosRepository - GetCiclos command id_comision: {resultQuery["id_comision"]}");
                            Logger.LogInformation($"GestionPagosRezagadosRepository - GetCiclos command id_ciclo: {resultQuery["id_ciclo"]}");
                            cicloRezagadoDoble.Add(Convert.ToInt32(resultQuery["id_comision"]));
                        }
                    }
                }
                var ciclosR = ContextMulti.VwObtenerCiclosRezagados.Where(x => x.IdEstadoComision == idEstadoComision && x.IdTipoComision == idTipoComisionRezagados)
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
                Logger.LogInformation($"GetComisionesPagos usuario: {usuario}, idCiclo: {idCiclo}, idEstadoComision: {idEstadoComision}, idTipoComisionPagoComision: {idTipoComisionPagoComision}, idComision: {idComision}");
                //var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                int[] tiposPagos = { 1, 2};                                              
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes
                    .Where(x => x.IdCiclo == idCiclo && x.IdComision == idComision &&
                    x.IdTipoComision == TIPO_COMISION_REZAGADOS && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                   (x.IdTipoPago == 1 || x.IdTipoPago == 2)).ToList();
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
                List<VwObtenerRezagadosPago> info = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdComision == body.comisionId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA
                        && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO)
                    .ToList();

                var montoTotal = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdComision == body.comisionId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA)
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
                Logger.LogInformation($" usuario: {param.user}, inicio repository ConfirmarPagosRezagadosTransferencias(): idciclo {param.cicloId}, idempresa{param.empresaId}, idcomision {param.comisionId}");

                var usuarioId = ContextMulti.Usuarios
                    .Where(x => x.Usuario1 == param.user)
                    .Select(u => new
                    {
                        usuarioId = u.IdUsuario
                    }).FirstOrDefault();

                var count = ContextMulti.GpComisions.Join(ContextMulti.Cicloes,
                                              GpComision => GpComision.IdCiclo,
                                              Ciclo => Ciclo.IdCiclo,
                                          (GpComision, Ciclo) => new
                                          {
                                              idComision = GpComision.IdComision,
                                              tipoComision = GpComision.IdTipoComision,
                                              idCiclo = Ciclo.IdCiclo
                                          }).Join(ContextMulti.GpComisionEstadoComisionIs,
                                                  GpComision => GpComision.idComision,
                                                  GpComisionEstadoComisionI => GpComisionEstadoComisionI.IdComision,
                                                  (GpComision, GpComisionEstadoComisionI) => new
                                                  {
                                                      idEstadoComision = GpComisionEstadoComisionI.IdEstadoComision,
                                                      habilitado = GpComisionEstadoComisionI.Habilitado,
                                                      idComision = GpComision.idComision,
                                                      idCiclo = GpComision.idCiclo,
                                                      tipoComision = GpComision.tipoComision
                                                  }
                                      ).Where(x => x.habilitado == true && x.idCiclo == param.cicloId && x.idEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO
                                        && x.tipoComision == TIPO_COMISION_REZAGADOS).Count();
                
                Logger.LogInformation($"Repository GestionPagosRezagadosRepository - ConfirmarPagosRezagadosTransferencias cantidad de registros rezagados {count}");
                if (count == 2)
                {
                    return postEvent(GestionPagosRezagadosEvent.EXISTE_DOS_REGISTROS_COMISIONES_REZAGADOS, "Es necesario que cierre el registro para el ciclo actual de rezagados para poder continuar al siguiente registro de rezagados.");
                }

                Logger.LogInformation($" usuarioId: {usuarioId}, inicio confirmarRechazadosTransferidosSeleccionados en repository ConfirmarPagosRezagadosTransferencias()");
                if (!confirmarRezagadosTransferidosSeleccionados(param, usuarioId))
                {
                    return postEvent(GestionPagosRezagadosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a confirmados, verifique e intente nuevamente.");
                }

                int idEstadoComisionDetalleEmpresaConfirmado = 2;
                List<VwObtenerRezagadosPago> l = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == param.cicloId && x.IdEmpresa == param.empresaId &&
                        x.IdComision == param.comisionId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEstadoComisionDetalleEmpresa != idEstadoComisionDetalleEmpresaConfirmado)
                    .ToList();

                if (!confirmarRezagadosTransferidosNoSeleccionados(param, usuarioId, l))
                {
                    Logger.LogError($" ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS");
                    return postEvent(GestionPagosRezagadosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a rechazados, verifique e intente nuevamente.");
                }

                // SP_REGISTRAR_REZAGADOS_DE_REZAGADOS_DE_PAGOS_RECHAZADOS
                Logger.LogInformation($" Iniciando carga de parametros de entrada para ejecutar el SP SP_REGISTRAR_REZAGADOS_DE_REZAGADOS_DE_PAGOS_RECHAZADOS");
                Logger.LogInformation($" UsuarioId: {usuarioId}, CicloId: {param.cicloId}, ComisionId: {param.comisionId}, EmpresaId: {param.empresaId}");
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
                                            Value = TIPO_COMISION_REZAGADOS
                                },
                                new SqlParameter() {
                                            ParameterName = "@EstadoComision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO
                                },
                                new SqlParameter() {
                                            ParameterName = "@CicloId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.cicloId
                                },
                                new SqlParameter() {
                                            ParameterName = "@EmpresaId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.empresaId
                                },
                                new SqlParameter() {
                                                ParameterName = "@ComisionId",
                                                SqlDbType =  System.Data.SqlDbType.Int,
                                                Direction = System.Data.ParameterDirection.Input,
                                                Value = param.comisionId
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
                                            Value = TIPO_PAGO_TRANSFERENCIA
                              }
                           };
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_REGISTRAR_REZAGADOS_DE_REZAGADOS_DE_PAGOS_RECHAZADOS] @TipoComision, @EstadoComision, @ComisionId, @CicloId,  @EmpresaId, @UsuarioId, @TipoPago ", parameterReturn);
                //var result = "Todo Ok @TipoComision, @EstadoComision, @ComisionId, @CicloId,  @EmpresaId, @UsuarioId, @TipoPago";
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, repository ConfirmarPagosRezagadosTransferencias fi" + $"" + $"n SP_REGISTRAR_REZAGADOS_DE_REZAGADOS_DE_PAGOS_RECHAZADOS returnValue: {returnValue}  ");
                if (returnValue == -1)
                {
                    // Entro al catch del SP_REGISTRAR_REZAGADOS_DE_REZAGADOS_DE_PAGOS_RECHAZADOS hizo Rollback
                    Logger.LogWarning($"repository ConfirmarPagosRezagadosTransferencias() SP_REGISTRAR_REZAGADOS_DE_REZAGADOS_DE_PAGOS_RECHAZADOS @returnValue: {returnValue}");
                    dbcontextTransaction.Rollback();
                    return postEvent(GestionPagosRezagadosEvent.CATCH_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Pasó algo inesperado, no se pudo registrar a los ACI rechazados.");
                }

                List<VwObtenerRezagadosPago> rezagados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == param.cicloId && x.IdComision == returnValue && x.IdEmpresa == param.empresaId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA &&
                            x.IdEstadoComisionDetalleEmpresa != idEstadoComisionDetalleEmpresaConfirmado)
                    .ToList();
                dbcontextTransaction.Commit();
                Logger.LogInformation($" usuario: {param.user}, despues del commit");
                // Si returnValue no es -1 ni 2, es 1
                if (rezagados.Count > 0)
                {
                    string asunto = "Lista de Rechazados en ciclo " + rezagados.ElementAt(0).Glosa + " Rezagados, por Empresa " + rezagados.ElementAt(0).Empresa;
                    EnvioCorreoService.EnviarCorreoRezagados(rezagados, asunto);
                }
                // Si returnValue no es -1 ni 2, es 1
                return postEvent(GestionPagosRezagadosEvent.SUCCESS_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Se realizó correctamente la confirmación para pagos por transferencias de los ACI seleccionados.");
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario: {param.user} CATCH ConfirmarPagosRezagadosTransferencias() mensaje : {ex}");
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

                    Logger.LogInformation($" result: {body.confirmados[i]}, inicio repository confirmarRezagadosTransferidosSeleccionados():  SP_CONFIRMAR_TRANSFERENCIAS_SELECCIONADAS_REZAGADOS @ComisionDetalleEmpresaId {body.confirmados[i]}  ");
                    var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_CONFIRMAR_TRANSFERENCIAS_SELECCIONADAS_REZAGADOS] @CicloId, @EmpresaId, @UsuarioId, @ComisionDetalleEmpresaId", parameterReturn);
                    //var result = "Todo OK @CicloId,  @EmpresaId, @ComisionId, @UsuarioId, @ComisionDetalleEmpresaId";
                    int returnValue = (int)parameterReturn[0].Value;

                    Logger.LogInformation($" result: {result}, inicio repository confirmarRezagadosTransferidosSeleccionados():  SP_CONFIRMAR_TRANSFERENCIAS_SELECCIONADAS_REZAGADOS returnValue {returnValue} ");
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
                    var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS_REZAGADOS] @CicloId,  @EmpresaId, @UsuarioId, @ComisionDetalleEmpresaId", parameterReturn);
                    //var result = "Todo OK@CicloId,  @EmpresaId, @ComisionId, @UsuarioId, @ComisionDetalleEmpresaId";
                    int returnValue = (int)parameterReturn[0].Value;
                    Logger.LogInformation($" result: {result}, inicio repository confirmarRezagadosTransferidosNoSeleccionados():  SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS_REZAGADOS returnValue {returnValue} ");
                    if (returnValue == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario: {body.user} CATCH confirmarRezagadosTransferidosNoSeleccionados(): {ex}");
                return false;
            }
        }        

        public dynamic handleTransferenciasEmpresas(ComisionesPagosInput param)
        {
            try
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository handleTransferenciasEmpresas() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo: {param.idCiclo}");
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
                .Where(x => x.usuario == param.usuarioLogin && x.idTipoPago == TIPO_PAGO_TRANSFERENCIA)
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
                    .Where(x => x.IdCiclo == param.idCiclo && x.IdComision == param.idComision && x.IdTipoComision == TIPO_COMISION_REZAGADOS && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO && x.MontoTransferir != 0 && ids.Contains(x.IdEmpresa))
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

                int estadoComisionDetalleEmpresaPendiente = 1;
                int estadoComisionDetalleEmpresaConfirmado = 2;
                int estadoComisionDetalleEmpresaRechazado = 3;
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
                              },
                               new SqlParameter() {
                                            ParameterName = "@EstadoComisionRezagadoId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO
                              }
                           };
                var recargarCicloActual = false;
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_REZAGADOS_CONFIRMADAS] @ComisionId, @CicloId, @EstadoComisionRezagadoId, @UsuarioId  ", parameterReturn);
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

                var cantidadPendientes = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdEmpresa == body.empresaId && x.IdComision == body.comisionId &&
                    x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendiente && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO).Count();

                var cantidadRechazados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA
                    /*  && x.IdComision == body.comisionId */ &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendiente).Count();
                var cantidadConfirmados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdComision == body.comisionId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaConfirmado).Count();

                var sumaTotalConfirmados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdComision == body.comisionId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaConfirmado)
                    .Sum(x => x.ImportePorEmpresa);

                var sumaTotalRechazados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA &&
                        //x.IdComision == body.comisionId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendiente)
                    .Sum(x => x.ImportePorEmpresa);

                var sumaTotalPendientes = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdComision == body.comisionId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendiente)
                    .Sum(x => x.ImportePorEmpresa);

                var cantidadFechasPagosNull = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdComision == body.comisionId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendiente && x.FechaDePago == null).Count();

                var fechaPagosExcel = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdComision == body.comisionId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendiente)
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
                Utils.Utils.ShowValueFields(body, Logger);
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
                                            Value = body.comisionId
                                },
                                new SqlParameter() {
                                            ParameterName = "@EstadoComisionId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO
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

                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_CONFIRMAR_TRANSFERENCIAS_REZAGADOS_TODOS] @ComisionId, @CicloId, @EstadoComisionId, @EmpresaId, @UsuarioId  ", parameterReturn);
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

        public object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body)
        {
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try
            {
                Logger.LogWarning($" usuario: {body.user} inicio el repository handleDownloadFileEmpresas() ");
                Logger.LogWarning($" usuario: {body.user} handleDownloadFileEmpresas parametros: idciclo: {body.cicloId}, fecha: {body.date.ToString("yyyyMMdd")}");

                Logger.LogInformation($" usuario: {body.user}, inicio repository handleDownloadFileEmpresas(): idciclo {body.cicloId}  ");
                var usuarioId = ContextMulti.Usuarios
                    .Where(x => x.Usuario1 == body.user)
                    .Select(u => new
                    {
                        usuarioId = u.IdUsuario
                    }).FirstOrDefault();

                Logger.LogInformation($" usuarioId: {usuarioId}, inicio repository handleDownloadFileEmpresas()");
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
                                            ParameterName = "@EstadoComisionId",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO
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
                               },
                               new SqlParameter() {
                                            ParameterName = "@FechaPago",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = body.date.ToString("yyyyMMdd")
                               }
                           };
                Logger.LogInformation($" usuarioId: {usuarioId}, handleDownloadFileEmpresas inicio SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS_REZAGADOS parameterReturn: {parameterReturn}");
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS_REZAGADOS] @ComisionId, @EstadoComisionId, @CicloId,  @EmpresaId, @UsuarioId, @FechaPago ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, inicio repository handleDownloadFileEmpresas(): SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS_REZAGADOS returnValue {returnValue}  ");
                if (returnValue == 1)
                {
                    //throw new Exception("Pas[o algo inesperado, no se pudo realizar la actualizacion.");
                    return postEvent(GestionPagosRezagadosEvent.ERROR, "Pas[o algo inesperado, no se pudo realizar la actualizacion.");
                }
                else if (returnValue == 2)
                {
                    return postEvent(GestionPagosRezagadosEvent.ROLLBACK_ERROR, "Hubo problemas de conexion, por favor intenta mas tarde.");
                }

                int cicloId = Convert.ToInt32(body.cicloId);
                int tipoPagoTransferencia = 2;
                int estadoComisionDetalleEmpresaPendienteId = 1;
                List<VwObtenerRezagadosPago> info = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdComision == body.comisionId && x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        x.IdTipoPago == tipoPagoTransferencia && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendienteId)
                    .ToList();

                Logger.LogWarning($"handleDownloadFileEmpresas Count: {info.Count}");
                if (info.Count == 0)
                {

                }
                using (var p = new ExcelPackage())
                {
                    var ws = p.Workbook.Worksheets.Add($"{info[0].Empresa}");

                    string[] headers = { "NRO. DE ORDEN", "CODIGO DE CLIENTE", "NRO. DE CUENTA", "NOMBRE DE CLIENTE",
                                         "DOC. DE IDENTIDAD", "IMPORTE", "FECHA DE PAGO", "FORMA DE PAGO", "MONEDA DESTINO",
                                        "ENTIDAD DESTINO", "SUCURSAL DESTINO", "GLOSA", "CODIGO UNICO"};

                    for (int i = 1; i <= 13; i++)
                    {
                        ws.Cells[1, i].Value = headers[i - 1];
                        ws.Cells[1, i].AutoFitColumns(1);
                    }

                    var range = ws.Cells["A1:M13"];
                    range.AutoFilter = true;
                    ws.AutoFilter.ApplyFilter();
                    NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands; ;
                    CultureInfo provider = new CultureInfo("is-IS");

                    for (int i = 2; i <= info.Count + 1; i++)
                    {
                        VwObtenerRezagadosPago f = info[i - 2];
                        ws.Cells[i, 1].Value = i - 1;
                        //ws.Cells[i, 1].AutoFitColumns(1);
                        ws.Cells[i, 2].Value = int.Parse(f.CodigoDeCliente);
                        //ws.Cells[i, 2].AutoFitColumns(1);
                        ws.Cells[i, 3].Value = Convert.ToString(f.NroDeCuenta);
                        //ws.Cells[i, 3].AutoFitColumns(1);
                        ws.Cells[i, 4].Value = Convert.ToString(f.NombreDeCliente);
                        ws.Cells[i, 4].AutoFitColumns(1);
                        ws.Cells[i, 5].Value = Convert.ToString(f.DocDeIdentidad);
                        ws.Cells[i, 5].AutoFitColumns(1);
                        double dd = (double)f.ImportePorEmpresa;
                        string dd2 = dd.ToString("N2", new CultureInfo("is-IS"));
                        ws.Cells[i, 6].Value = Decimal.Parse(dd2, style, provider);
                        //ws.Cells[i, 7].Value = body.date.ToString("dd/MM/yyyy");
                        ws.Cells[i, 7].Value = f.FechaDePago;
                        ws.Cells[i, 7].AutoFitColumns(1);
                        ws.Cells[i, 8].Value = f.FormaDePago;
                        //ws.Cells[i, 8].AutoFitColumns(1);
                        if (!String.IsNullOrEmpty(f.MonedaDestino))
                            ws.Cells[i, 9].Value = int.Parse(f.MonedaDestino);
                        else
                            ws.Cells[i, 9].Value = "";
                        //ws.Cells[i, 9].AutoFitColumns(1);
                        if (!String.IsNullOrEmpty(f.EntidadDestino))
                            ws.Cells[i, 10].Value = int.Parse(f.EntidadDestino);
                        else
                            ws.Cells[i, 10].Value = "";
                        //ws.Cells[i, 10].AutoFitColumns(1);
                        ws.Cells[i, 11].Value = f.SucursalDestino;
                        ws.Cells[i, 12].Value = "PAGO DE COMISIONES DEL MES DE " + Convert.ToString(f.Glosa);
                        ws.Cells[i, 12].AutoFitColumns(1);
                        ws.Cells[i, 13].Value = "";
                    }
                    DownloadFileTransferenciaOutput r = new DownloadFileTransferenciaOutput();
                    r.file = Convert.ToBase64String(p.GetAsByteArray());
                    r.fileName = info[0].Empresa;
                    dbcontextTransaction.Commit();
                    return postEvent(GestionPagosRezagadosEvent.SUCCESS, r, "Archivo excel generado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleDownloadFileEmpresas() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return postEvent(GestionPagosRezagadosEvent.ERROR, "Pasó un inconveniente, por favor intente más tarde mientras lo resolvemos, ¡gracias!.");
            }
        }
        public object BuscarFreelancerPagosRezagadosTransferencias(ObtenerPagosRezagadosTransferenciasInput param)
        {
            try
            {
                Logger.LogInformation($" usuario: {param.user} -  inicio el BuscarFreelancerPagosTransferencias() ");
                int cicloId = Convert.ToInt32(param.cicloId);
                var Buscar = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.DocDeIdentidad == param.ci && x.IdCiclo == cicloId &&
                        x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO &&
                        param.empresaId == x.IdEmpresa && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA)
                    .ToList();
                return Buscar;
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario: {param.user} error catch BuscarFreelancerPagosTransferencias() mensaje : {ex}");
                return false;
            }
        }
        public bool PagarComisionRezagadosSionPayTodo(PagoRezagadoInput param)
        {
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try
            {
                Logger.LogInformation($" usuario: {param.UsuarioLogin}, inicio repository PagarSionPayComision(): IdComsion  {param.IdComision}  ");
                Utils.Utils.ShowValueFields(param, Logger);
                var parameterReturn = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@id_Comision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.IdComision
                              },
                               new SqlParameter() {
                                            ParameterName = "@id_usuario",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.IdUsuario
                              }
                           };
                var parameterReturn2 = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@id_Comision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.IdComision
                              },
                               new SqlParameter() {
                                            ParameterName = "@id_usuario",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.IdUsuario
                              }
                           };
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP2_PAGAR_RESAGADO_SION_PAY_COMISION] @id_Comision,  @id_usuario  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                if (returnValue > 0)
                {
                    var result2 = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP2_PROCESAR_PAGO_REZAGADO_SION_PAY_UPDATE_DETALLES] @id_Comision,  @id_usuario  ", parameterReturn2);
                    int returnValue2 = (int)parameterReturn2[0].Value;

                    dbcontextTransaction.Commit();
                    Logger.LogInformation($" usuario: {param.UsuarioLogin}-  Se proceso la forma de pago DE FORMA EXISTOSA EL [SP_PROCESAR_CERRAR_FORMA_PAGO].");
                    return true;
                }
                else
                {
                    dbcontextTransaction.Rollback();
                    Logger.LogInformation($" usuario: {param.UsuarioLogin}-  NO ROLLBACK EN EL SP [SP_PROCESAR_CERRAR_FORMA_PAGO]");
                    return false;
                }


            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.UsuarioLogin} error catch PagarSionPayComision() en pagos mensaje : {ex.Message}");
                dbcontextTransaction.Rollback();
                return false;
            }
        }
        public RespuestaSionPayModel VerificarPagoRezagadoSionPay(PagoRezagadoInput param, int idEstadoComision,int idTipoComision, int idTipoFormaPagoSionPay,int idEstadoListaFormaPago)
        {
            try 
            {
                RespuestaSionPayModel model = new RespuestaSionPayModel();
                Logger.LogWarning($" usuario: {param.UsuarioLogin} inicio el repository VerificarPagoRezagadoSionPay() ");
                Logger.LogWarning($" usuario: {param.UsuarioLogin} parametros: idcomision:{param.IdComision} , idEstado:{idEstadoComision}");                
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.IdComision && x.IdTipoComision == idTipoComision && x.IdEstadoComision == idEstadoComision && x.IdTipoPago == idTipoFormaPagoSionPay ).ToList();
                model.Cantidad = ListComisiones.Where(x => x.PagoDetalleHabilitado == false).Count();
                model.totalPagoSionPay = (decimal)ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPagoSionPay && x.PagoDetalleHabilitado == false).Sum(c => c.MontoNeto);
                model.CodigoRespuesta = 1; //valor positivo
                Logger.LogWarning($" usuario: {param.UsuarioLogin} se verifico antes del cierre validando la cantidad de no pagados en porsion pay :  {JsonConvert.SerializeObject(model)} ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.UsuarioLogin} error catch VerificarPagoRezagadoSionPay() mensaje : {ex.Message}");
                RespuestaSionPayModel model = new RespuestaSionPayModel { CodigoRespuesta = -1 };
                return model;
            }
        }
        public RespuestaSionPayModel ValidarCantidadComisionRezagada(PagoRezagadoInput param, int idEstadoComision, int idTipoComision, int idTipoFormaPagoSionPay)
        {
            try
            {
                RespuestaSionPayModel model = new RespuestaSionPayModel();
                Logger.LogWarning($" usuario: {param.UsuarioLogin} inicio el repository ValidarCantidadComisionRezagada() ");               
                model.Cantidad = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.IdComision && x.IdTipoComision == idTipoComision && x.IdEstadoComision == idEstadoComision && x.IdTipoPago == idTipoFormaPagoSionPay).Count();
                model.totalPagoSionPay = 0;
                model.CodigoRespuesta = 1; //valor positivo
                Logger.LogWarning($" usuario: {param.UsuarioLogin} se verifico la cantidad de comision rezagado a pagar :  {JsonConvert.SerializeObject(model)} ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.UsuarioLogin} error catch ValidarCantidadComisionRezagada() mensaje : {ex.Message}");
                RespuestaSionPayModel model = new RespuestaSionPayModel { CodigoRespuesta = -1 };
                return model;
            }
        }

        public RespuestaSionPayModel VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param)
        {
            try
            {
                RespuestaSionPayModel model = new RespuestaSionPayModel();
                Logger.LogWarning($"Inicio el repository VerificarPagoSionPayCiclo() ");
                //Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} ");
                Utils.Utils.ShowValueFields(param, Logger);
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.comisionId && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO).ToList();
                model.Cantidad = ListComisiones.Where(x => x.IdTipoPago == TIPO_PAGO_SIONPAY && x.PagoDetalleHabilitado == false).Count();
                model.totalPagoSionPay = (decimal) ListComisiones.Where(x => x.IdTipoPago == TIPO_PAGO_SIONPAY && x.PagoDetalleHabilitado == false).Sum(c => c.MontoNeto);
                model.CodigoRespuesta = 1; //valor positivo
                Logger.LogWarning($" usuario: {param.usuarioLogin} se verifico antes del cierre validando la cantidad de no pagados en porsion pay :  {JsonConvert.SerializeObject(model)} ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch VerificarPagoSionPayCiclo() mensaje : {ex}");
                RespuestaSionPayModel model = new RespuestaSionPayModel { CodigoRespuesta = -1 };
                return model;
            }
        }

        public bool PagarSionPayComision(PagarSionPayInput param)
        {
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();

            try
            {
                Logger.LogInformation($" usuario: {param.UsuarioLogin}, inicio repository PagarSionPayComision(): idciclo {param.idCiclo}  ");
                var parameterReturn = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@id_Comision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.comisionId
                              },                                
                               new SqlParameter() {
                                            ParameterName = "@id_usuario",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.idUsuario
                              }
                           };
                var parameterReturn2 = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                                },
                                new SqlParameter() {
                                            ParameterName = "@id_Comision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.comisionId
                              },
                               new SqlParameter() {
                                            ParameterName = "@id_usuario",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = param.idUsuario
                              }
                           };
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP2_PAGAR_RESAGADO_SION_PAY_COMISION] @id_Comision, @id_usuario  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                if (returnValue > 0)
                {
                    var result2 = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP2_PROCESAR_PAGO_REZAGADO_SION_PAY_UPDATE_DETALLES] @id_Comision, @id_usuario  ", parameterReturn2);
                    int returnValue2 = (int)parameterReturn2[0].Value;

                    dbcontextTransaction.Commit();
                    Logger.LogInformation($" usuario: {param.UsuarioLogin}-  Se proceso la forma de pago DE FORMA EXISTOSA EL [SP_PROCESAR_CERRAR_FORMA_PAGO].");
                    return true;
                }
                else
                {
                    dbcontextTransaction.Rollback();
                    Logger.LogInformation($" usuario: {param.UsuarioLogin}-  NO ROLLBACK EN EL SP [SP_PROCESAR_CERRAR_FORMA_PAGO]");
                    return false;
                }


            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.UsuarioLogin} error catch PagarSionPayComision() en pagos mensaje : {ex.Message}");
                dbcontextTransaction.Rollback();
                return false;
            }
        }

        public object GetFiltroComisionesPorFormaPago(FiltroFormaPagosInput param)
        {
            try
            {
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                Logger.LogWarning($"Inicio repository GestionPagosRezagadosRepository - GetFiltroComisionesPorFormaPago() ");
                Utils.Utils.ShowValueFields(param, Logger);

                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.comisionId && x.IdTipoComision == TIPO_COMISION_REZAGADOS && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO).ToList();
                List<FormaPagoModel> LisFormaPagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new FormaPagoModel(p.IdTipoPago, p.Nombre, p.Descripcion, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion, (bool)p.Estado, p.Icono)).ToList();
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
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetFiltroComisionesPorFormaPago() mensaje : {ex}");
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                return list;
            }
        }

        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListPagos(BuscarComisionInput param)
        {
            try
            {
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                Logger.LogInformation($"Inicio repository GestionPagosRezagadosRepository - GetComisionesPorCarnetListPagos");
                Utils.Utils.ShowValueFields(param, Logger);
                
                if (param.nombreCriterio != "")
                {
                    var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.comisionId && x.IdTipoComision == TIPO_COMISION_REZAGADOS && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO).ToList();
                    var lista = ListComisiones.Where(x => x.IdTipoPago != 0 && x.Ci.Contains(param.nombreCriterio.Trim())).ToList();
                    return lista;
                }
                else
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"CATCH GetComisionesPorCarnetListPagos() Message: {ex.Message}");
                Logger.LogWarning($"CATCH GetComisionesPorCarnetListPagos() StackTrace: {ex.StackTrace}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }

        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInput param)
        {
            try
            {
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                Logger.LogWarning($"Inicio el repository FiltrarComisionPagoPorTipoPago() ");
                Utils.Utils.ShowValueFields(param, Logger);
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.comisionId && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO && x.IdTipoPago == param.idTipoPago).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"CATCH FiltrarComisionPagoPorTipoPago() Message : {ex.Message}");
                Logger.LogWarning($"CATCH FiltrarComisionPagoPorTipoPago() StackTrace : {ex.StackTrace}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }

        public bool VerificarSiExisteAutorizacionFormaPagoCiclo(FiltroComisionTipoPagoInput param)
        {
            try
            {                
                int estadoAutorizacionComision = 0; //estado aprobado de la tabla ESTADO_AUTORIZACION_COMISION
                var cantidad = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdComision == param.comisionId && x.IdEstadoAutorizacionComision == estadoAutorizacionComision).Count();
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
                Logger.LogWarning($"CATCH VerificarSiExisteAutorizacionFormaPagoCiclo() Message : {ex.Message}");
                Logger.LogWarning($"CATCH VerificarSiExisteAutorizacionFormaPagoCiclo() StackTrace : {ex.StackTrace}");
                return false;
            }
        }

        public RespuestaPorTipoPagoModel VerificarTipoPagoCiclo(CerrarPagoParam param, int tipoFormaPagoId)
        {
            try
            {
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel();
                Logger.LogWarning($"Inicio repository GestionPagosRezagadosRepository - VerificarTipoPago ");
                Logger.LogWarning($"Inicio repository GestionPagosRezagadosRepository - VerificarTipoPago | tipoFormaPagoId: {tipoFormaPagoId}");
                Utils.Utils.ShowValueFields(param, Logger);                
                //var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdComision == param.idCiclo && x.IdTipoComision == TIPO_COMISION_REZAGADOS).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.comisionId && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO).ToList();
                model.Cantidad = ListComisiones.Where(x => x.IdTipoPago == tipoFormaPagoId && x.PagoDetalleHabilitado == false).Count();
                model.totalPagoSionPay = (decimal)ListComisiones.Where(x => x.IdTipoPago == tipoFormaPagoId && x.PagoDetalleHabilitado == false).Sum(c => c.MontoNeto);
                model.CodigoRespuesta = 1; //valor positivo
                Logger.LogWarning($" usuario: {param.usuarioLogin} se verifico antes del cierre validando la cantidad de no pagados en porsion pay :  {JsonConvert.SerializeObject(model)} ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch VerificarTipoPagoCiclo() Message : {ex.Message}");
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch VerificarTipoPagoCiclo() StackTrace : {ex.StackTrace}");
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel { CodigoRespuesta = -1 };
                return model;
            }
        }

        public RespuestaPorTipoPagoModel VerificarTransaccionRechazadoMontoCero(CerrarPagoParam param, int idTipoFormaPago)
        {
            try
            {
                int idEstadoDEtalleListadoFormaPago = 4;//parametro
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel();
                Logger.LogInformation($" usuario: {param.usuarioLogin} inicio el cierre de transaccion repository VerificarTransaccionRechazadoMontoCero() ");
                Logger.LogInformation($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO} , idtipoFormaPago: {idTipoFormaPago}, idTipoComisionPagoComision: {TIPO_COMISION_REZAGADOS}");
                Logger.LogInformation($" usuario: {param.usuarioLogin} verificamos en las comisiones rechazadas que no existe montos mayor a cero, ya que estas comisiones fueron rezagas y el monto de planilla tiene que estas en cero");
                //var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == param.comisionId && x.IdEstadoComision == FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO && x.IdEstadoListadoFormaPago == idEstadoDEtalleListadoFormaPago).ToList();

                //cantidad debe volver monto cero ya que fueron resagados las transferencias
                model.Cantidad = ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPago && x.MontoNeto > 0).Count();
                if (model.Cantidad > 0)
                {
                    Logger.LogInformation($" usuario: {param.usuarioLogin} HUBO transacciones rechazados con monto mayor a cero verifique");
                    Logger.LogInformation($" usuario: {param.usuarioLogin} exiten rechazado con monto mayor a cero :  {JsonConvert.SerializeObject(ListComisiones)} ");
                }
                model.totalPagoSionPay = (decimal)ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPago && x.PagoDetalleHabilitado == false).Sum(c => c.MontoNeto);
                model.CodigoRespuesta = 1; //valor positivo
                Logger.LogInformation($" usuario: {param.usuarioLogin} se verifico antes del cierre validando la cantidad de no pagados en porsion pay :  {JsonConvert.SerializeObject(model)} ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch VerificarPagoSionPayCiclo() mensaje : {ex}");
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel { CodigoRespuesta = -1 };
                return model;
            }
        }

        public int CerrarPagoComisionPorTipoComision(CerrarPagoParam param)
        {           
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try
            {
                Logger.LogInformation($" usuario: {param.usuarioLogin} -  inicio el CerrarPagoComisionPorTipoComision() ");
                Utils.Utils.ShowValueFields(param, Logger);
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
                                            Value = param.usuarioId
                                },
                                  new SqlParameter() {
                                            ParameterName = "@id_tipo_comision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = TIPO_COMISION_REZAGADOS
                                },
                                  new SqlParameter() {
                                            ParameterName = "@estado_comision_id",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO
                                }
                };
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_CERRAR_COMISION_PAGO_POR_TIPO_REZAGADOS] @comision_id, @id_ciclo, @id_usuario, @id_tipo_comision, @estado_comision_id ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($"FIN SP [SP_CERRAR_COMISION_PAGO_POR_TIPO_REZAGADOS] returnValue: {returnValue}");
                if (returnValue == -1)
                {
                    Logger.LogInformation($" usuario: {param.usuarioLogin}-  NO ROLLBACK EN EL SP [SP_PROCESAR_CERRAR_FORMA_PAGO]");
                    dbcontextTransaction.Rollback();
                    return -1;      
                }
                Logger.LogInformation($" usuario: {param.usuarioLogin}-  Se cerro el pago de comision EL [SP_CERRAR_COMISION_PAGO_POR_TIPO_REZAGADOS].");
                //Logger.LogInformation($" usuario: {param.usuarioLogin}-  respuesta sp: {returnValue}");
                dbcontextTransaction.Commit();
                return returnValue;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch CerrarFormaDePago() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return -2;
            }
        }
    }
}
