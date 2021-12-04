using System;
using System.Collections.Generic;
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

        public object GetCiclos(string usuario, int idEstadoComision, int idTipoComisionPagoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                var ciclosR = ContextMulti.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision && x.IdTipoComision == idTipoComisionPagoComision).ToList();
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
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() en pagos mensaje : {ex.Message}");
                List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }

        public object GetComisionesPagos(string usuario, int idCiclo, int idEstadoComision, int idTipoComisionPagoComision)
        {
            try
            {
                Logger.LogInformation($"Inicio GestionPagosRezagadosRepository - GetComisionesPagos");
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                //var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision).ToList();
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

        public GestionPagosEvent ConfirmarPagosRezagadosTransferencias(ConfirmarPagosRezagadosTransferenciasInput param)
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
                    return postEvent(GestionPagosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a confirmados, verifique e intente nuevamente.");
                }

                int tipoPagoTransferencia = 2;
                int idEstadoComisionDetalleEmpresaConfirmado = 2;
                List<VwObtenerRezagadosPago> l = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == param.cicloId && x.IdEmpresa == param.empresaId && x.IdTipoPago == tipoPagoTransferencia &&
                            x.IdEstadoComisionDetalleEmpresa != idEstadoComisionDetalleEmpresaConfirmado)
                    .ToList();

                if (!confirmarRezagadosTransferidosNoSeleccionados(param, usuarioId, l))
                {
                    return postEvent(GestionPagosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a rechazados, verifique e intente nuevamente.");
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
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn ReturnValue: {parameterReturn[0]}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn CicloId: {parameterReturn[1]}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn EmpresaId: {parameterReturn[2]}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn UsuarioId: {parameterReturn[3]}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn TipoPago: {parameterReturn[4]}");
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS] @CicloId,  @EmpresaId, @UsuarioId, @TipoPago ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, repository handleConfirmarPagosTransferencias fin SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS returnValue: {returnValue}  ");
                if (returnValue == -1)
                {
                    // Entro al catch del SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS hizo Rollback
                    Logger.LogWarning($"repository handleConfirmarPagosTransferencias() SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS @returnValue: {returnValue}");
                    dbcontextTransaction.Rollback();
                    return postEvent(GestionPagosEvent.CATCH_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Pasó algo inesperado, no se pudo registrar a los ACI rechazados.");
                }

                dbcontextTransaction.Commit();
                // Si returnValue no es -1 ni 2, es 1
                return postEvent(GestionPagosEvent.SUCCESS_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Se realizó correctamente la confirmación para pagos por transferencias de los ACI seleccionados.");
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.user} CATCH handleConfirmarPagosTransferencias() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return postEvent(GestionPagosEvent.ERROR, $"NO se pudo realizar la confirmación de los pagos por transferencia, verifique e intente nuevamente. Mensaje: {ex.Message}");
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
        private GestionPagosEvent postEvent(int type, string errorMessage) => postEvent(type, new DownloadFileTransferenciaOutput(), errorMessage);

        private GestionPagosEvent postEvent(int type, VerificarPagosTransferenciasOutput data, string message)
        {
            GestionPagosEvent e = new GestionPagosEvent();
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

        private GestionPagosEvent postEvent(int type, DownloadFileTransferenciaOutput file, string errorMessage)
        {
            GestionPagosEvent e = new GestionPagosEvent();
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
    }
}
