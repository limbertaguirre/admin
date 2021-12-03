using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
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
        public GestionPagosRezagadosRepository()
        {

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
                    .Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionRezagados && x.IdTipoPago == idTipoPagoTransferencia && x.IdEstadoComision == idEstadoComisionPagosRezagados && x.MontoTransferir != 0 && ids.Contains(x.IdEmpresa))
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
    }
}
