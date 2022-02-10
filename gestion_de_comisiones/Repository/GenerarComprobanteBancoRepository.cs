using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Repository
{
    public class GenerarComprobanteBancoRepository : IGenerarComprobanteBancoRepository
    {
        public BDMultinivelContext ContextMulti;
        private readonly ILogger<GenerarComprobanteBancoRepository> Logger;
        private readonly int FORMA_PAGO_REZAGADO_CERRADO = GpEstadoComision.FORMA_PAGO_DE_COMISION_REZAGADO_CERRADO;
        private readonly int ESTADO_COMISION_REZAGADOS = GpEstadoComision.PENDIENTE_FORMA_DE_PAGO_9;
        private readonly int TIPO_COMISION_REZAGADOS = GpTipoComision.PAGO_REZAGADOS_2;
        private readonly int TIPO_PAGO_SIONPAY = TipoPago.SION_PAY;
        private readonly int TIPO_PAGO_TRANSFERENCIA = TipoPago.TRANSFERENCIA;
        private readonly int ESTADO_DETALLE_EMPRESA_PENDIENTE = 1;
        private readonly int ESTADO_DETALLE_EMPRESA_CONFIRMADO = 2;
        private readonly int ESTADO_DETALLE_EMPRESA_RECHAZADO = 3;
        private readonly IEnvioCorreoRezagadoService EnvioCorreoService;

        public GenerarComprobanteBancoRepository(BDMultinivelContext contextMulti, IEnvioCorreoRezagadoService envioCorreoService, ILogger<GenerarComprobanteBancoRepository> logger)
        {
            this.Logger = logger;
            this.ContextMulti = contextMulti;
            this.EnvioCorreoService = envioCorreoService;
        }

        private async Task<List<GenerarComprobanteEvent>> GenerarAsync(List<VwObtenerInfoExcelFormatoBanco> aciAPagarList, Empresa empresa, int usuarioId, string username, int tipoComision = 1)
        {
            Logger.LogInformation($"repository inicio GenerarComprobanteBancoRepository(): GenerarAsync");
            //using var dbcontextTransaction = await ContextMulti.Database.BeginTransactionAsync();
            List<GenerarComprobanteEvent> ev = new List<GenerarComprobanteEvent>();
            try
            {
                Logger.LogInformation($"repository inicio GenerarComprobanteBancoRepository(): GenerarAsync - Cantidad de ACI's (count = {aciAPagarList.Count()})");
                if(aciAPagarList.Count() == 0)
                {
                    Logger.LogWarning($"GenerarComprobanteBancoRepository - GenerarAsync lista de ACI's vacía.");
                    throw new Exception("La consulta no devolvió lista de ACI's para generar los comprobantes de banco y contable.");                   
                }
                for (int i = 0; i < aciAPagarList.Count(); i++)
                {
                    VwObtenerInfoExcelFormatoBanco e = aciAPagarList[i];
                    Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarAsync NombreDeCliente: {e.NombreDeCliente}");

                    if (e.FechaDePago == null)
                    {
                        Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarAsync FechaDePago NULL ");
                        Utils.Utils.SetToTableLog(ContextMulti, e, empresa.CodigoCnx, "Columna FechaDePago: NULL", "", 0, usuarioId, Logger);
                        ev.Add(PostEvent(GenerarComprobanteEvent.ERROR_FECHA_PAGO_NULL, "Hubo un inconveniente con el registro de fecha de pago del ACI, por favor comuníquise con el Área de UIT."));
                    }
                    else
                    {
                        // GLOSA EJEMPLO CONTABILIDAD
                        // ABONO $US. 30,532.60.- PAGO DE COMISIONES CORRESPONDIENTE AL MES DE DICIEMBRE 2020                   
                        var glosa = $"ABONO $US. {e.ImportePorEmpresa}.- PAGO DE COMISIONES CORRESPONDIENTE AL MES DE " + e.Glosa;
                        Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarAsync glosa {glosa} ");

                        string format = "d/M/yyyy";
                        DateTime dateTime = DateTime.ParseExact(e.FechaDePago, format, null);
                        Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarAsync dateTime {dateTime}  ");

                        var ficha = ContextMulti.Fichas.Where(x => x.IdFicha == e.IdFicha).FirstOrDefault();

                        var parameterReturnGC = new SqlParameter[] {
                               new SqlParameter {
                                            ParameterName = "ReturnValueGC",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_NOMFREELANCER",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = e.NombreDeCliente
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_GLOSA",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = glosa
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_MONTO",
                                            SqlDbType =  System.Data.SqlDbType.Float,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = e.ImportePorEmpresa
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_FECHA",
                                            SqlDbType =  System.Data.SqlDbType.DateTime,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = dateTime
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_USUARIO",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = username
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_IDGUARDIAN_FREELANCER",
                                            SqlDbType =  System.Data.SqlDbType.BigInt,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = ficha.Codigo
                               }

                           };
                        Logger.LogInformation($"GenerarComprobanteBancoRepository(): GenerarAsync() parametros de entrada al sp spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES");
                        foreach (SqlParameter param in parameterReturnGC)
                        {
                            if (param.SqlValue != null)
                            {
                                Logger.LogInformation($"param: {param.ParameterName}: {param.SqlValue}");
                            }
                        }

                        var sql = $"EXEC @returnValueGC = {empresa.NombreBd.Trim()}.[dbo].[spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES] @PARAM_NOMFREELANCER, @PARAM_GLOSA, @PARAM_MONTO, @PARAM_FECHA, @PARAM_USUARIO, @PARAM_IDGUARDIAN_FREELANCER ";
                        var resultGC = await ContextMulti.Database.ExecuteSqlRawAsync(sql, parameterReturnGC);
                        int returnValueGC = (int)parameterReturnGC[0].Value;
                        Logger.LogInformation($" result: {resultGC}, repository GenerarComprobanteBancoRepository(): GenerarAsync: spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES returnValue {returnValueGC}  ");
                        if (returnValueGC != 0)
                        {
                            string descriptionLog;
                            string description;
                            string spName = $"{empresa.NombreBd.Trim()}.[dbo].[spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES]";
                            switch (returnValueGC)
                            {
                                case 1:
                                    descriptionLog = $"No se generó el comprobante caja para el ACI {e.NombreDeCliente} con CI: {e.DocDeIdentidad}";
                                    description = $"Hubo un inconveniente con la generación de comprobante de caja para el ACI {e.NombreDeCliente} con CI: {e.DocDeIdentidad}, por favor comuníquise con el Área de UIT.";
                                    break;
                                default:
                                    descriptionLog = "";
                                    description = "";
                                    break;
                            }
                            Utils.Utils.SetToTableLog(ContextMulti, e, empresa.CodigoCnx, descriptionLog, spName, returnValueGC, usuarioId, Logger);
                            ev.Add(PostEvent(GenerarComprobanteEvent.ERROR_GENERAR_COMPROBANTE_BANCO, description));
                        }
                    }
                }
                //await dbcontextTransaction.CommitAsync();
                ev.Add(PostEvent(GenerarComprobanteEvent.SUCCESS_GENERACION_COMPROBANTE_BANCO, "Se generaron los comprobantes de caja y contables correctamente."));
                return ev;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {username} error catch GenerarComprobanteBancoRepository - GenerarAsync() {ex}");
                //await dbcontextTransaction.RollbackAsync();
                //Utils.Utils.SetToTableLog(ContextMulti, null, empresa.CodigoCnx, ex.Message, ex.StackTrace, 0, usuarioId, Logger);
                EnvioCorreoService.EnviarCorreoLog(ex, "GENERAR COMPROBANTE EN PAGO DE COMISIONES POR TRANSFERENCIA",username);
                ev.Add(PostEvent(GenerarComprobanteEvent.CATCH_GENERAR_COMPROBANTE, ex.Message));
                return ev;
            }
        }

        private async Task<List<GenerarComprobanteEvent>> GenerarRezagadosAsync(List<VwObtenerRezagadosPago> aciAPagarList, Empresa empresa, int usuarioId, string username, int tipoComision = 1)
        {
            Logger.LogInformation($"repository inicio GenerarComprobanteBancoRepository(): GenerarRezagadosAsync");
            //using var dbcontextTransaction = await ContextMulti.Database.BeginTransactionAsync();
            List<GenerarComprobanteEvent> ev = new List<GenerarComprobanteEvent>();
            try
            {
                Logger.LogInformation($"GenerarComprobanteBancoRepository(): GenerarRezagadosAsync aciAPagarList.Count(): {aciAPagarList.Count()}");
                for (int i = 0; i < aciAPagarList.Count(); i++)
                {
                    VwObtenerRezagadosPago e = aciAPagarList[i];
                    Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarRezagadosAsync NombreDeCliente: {e.NombreDeCliente}");

                    if (e.FechaDePago == null)
                    {
                        Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarRezagadosAsync FechaDePago NULL ");
                        Utils.Utils.SetToTableLog(ContextMulti, e, empresa.CodigoCnx, "Columna FechaDePago: NULL", "", 0, usuarioId, Logger);
                        ev.Add(PostEvent(GenerarComprobanteEvent.ERROR_FECHA_PAGO_NULL, "Hubo un inconveniente con el registro de fecha de pago del ACI, por favor comuníquise con el Área de UIT."));
                    }
                    else
                    {
                        var glosa = $"ABONO $US. {e.ImportePorEmpresa}.- PAGO DE COMISIONES REZAGADOS CORRESPONDIENTE AL MES DE " + e.Glosa;
                        Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarRezagadosAsync glosa {glosa} ");

                        string format = "d/M/yyyy";
                        DateTime dateTime = DateTime.ParseExact(e.FechaDePago, format, null);
                        Logger.LogInformation($"repository GenerarComprobanteBancoRepository(): GenerarRezagadosAsync dateTime {dateTime}  ");

                        var ficha = ContextMulti.Fichas.Where(x => x.IdFicha == e.IdFicha).FirstOrDefault();

                        var parameterReturnGC = new SqlParameter[] {
                               new SqlParameter {
                                            ParameterName = "ReturnValueGC",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_NOMFREELANCER",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = e.NombreDeCliente
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_GLOSA",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = glosa
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_MONTO",
                                            SqlDbType =  System.Data.SqlDbType.Float,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = e.ImportePorEmpresa
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_FECHA",
                                            SqlDbType =  System.Data.SqlDbType.DateTime,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = dateTime
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_USUARIO",
                                            SqlDbType =  System.Data.SqlDbType.VarChar,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = username
                               },
                               new SqlParameter() {
                                            ParameterName = "@PARAM_IDGUARDIAN_FREELANCER",
                                            SqlDbType =  System.Data.SqlDbType.BigInt,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = ficha.Codigo
                               }

                           };
                        Logger.LogInformation($"GenerarComprobanteBancoRepository(): GenerarRezagadosAsync() parametros de entrada al sp spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES");
                        foreach (SqlParameter param in parameterReturnGC)
                        {
                            if (param.SqlValue != null)
                            {
                                Logger.LogInformation($"param: {param.ParameterName}: {param.SqlValue}");
                            }
                        }

                        var sql = $"EXEC @returnValueGC = {empresa.NombreBd.Trim()}.[dbo].[spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES] @PARAM_NOMFREELANCER, @PARAM_GLOSA, @PARAM_MONTO, @PARAM_FECHA, @PARAM_USUARIO, @PARAM_IDGUARDIAN_FREELANCER ";
                        var resultGC = await ContextMulti.Database.ExecuteSqlRawAsync(sql, parameterReturnGC);
                        int returnValueGC = (int)parameterReturnGC[0].Value;
                        Logger.LogInformation($" result: {resultGC}, repository GenerarComprobanteBancoRepository(): GenerarRezagadosAsync: spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES returnValue {returnValueGC}  ");
                        if (returnValueGC != 0)
                        {
                            string descriptionLog;
                            string description;
                            string spName = $"{empresa.NombreBd.Trim()}.[dbo].[spGENERARCOMPROBANTE_BANCO_GESTOR_COMISIONES]";
                            switch (returnValueGC)
                            {
                                case 1:
                                    descriptionLog = $"No se generó el comprobante caja para el ACI {e.NombreDeCliente} con CI: {e.DocDeIdentidad}";
                                    description = $"Hubo un inconveniente con la generación de comprobante de caja para el ACI {e.NombreDeCliente} con CI: {e.DocDeIdentidad}, por favor comuníquise con el Área de UIT.";
                                    break;
                                default:
                                    descriptionLog = "";
                                    description = "";
                                    break;
                            }
                            Utils.Utils.SetToTableLog(ContextMulti, e, empresa.CodigoCnx, descriptionLog, spName, returnValueGC, usuarioId, Logger);
                            ev.Add(PostEvent(GenerarComprobanteEvent.ERROR_GENERAR_COMPROBANTE_BANCO, description));
                        }
                    }
                }
                //await dbcontextTransaction.CommitAsync();
                ev.Add(PostEvent(GenerarComprobanteEvent.SUCCESS_GENERACION_COMPROBANTE_BANCO, "Se generaron los comprobantes de caja y contables correctamente."));
                return ev;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {username} error catch GenerarComprobanteBancoRepository - GenerarRezagadosAsync() {ex}");
                //await dbcontextTransaction.RollbackAsync();
                //Utils.Utils.SetToTableLog(ContextMulti, null, empresa.CodigoCnx, ex.Message, ex.StackTrace, 0, usuarioId, Logger);
                EnvioCorreoService.EnviarCorreoLog(ex, "GENERAR COMPROBANTE EN PAGO DE COMISIONES REZAGADOS POR TRANSFERENCIA", username);
                ev.Add(PostEvent(GenerarComprobanteEvent.CATCH_GENERAR_COMPROBANTE, ex.Message));
                return ev;
            }
        }

        public async Task<List<GenerarComprobanteEvent>> GenerarTodos(GenerarComprobanteInput body)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoRepository() GenerarTodos");
            var aciAPagarList = ContextMulti.VwObtenerInfoExcelFormatoBancoes.Where(x => x.IdComision == body.comisionId &&
                x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA && x.IdEmpresa == body.empresaId).ToList();

            var empresa = ContextMulti.Empresas.Where(x => x.IdEmpresa == body.empresaId).FirstOrDefault();
            return await GenerarAsync(aciAPagarList, empresa, body.usuarioId, body.username);
        }        

        public async Task<List<GenerarComprobanteEvent>> GenerarParcial(GenerarComprobanteInput body)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoRepository() GenerarParcial");
            var aciAPagarList = ContextMulti.VwObtenerInfoExcelFormatoBancoes.Where(x => x.IdComision == body.comisionId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA &&
                x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == ESTADO_DETALLE_EMPRESA_CONFIRMADO).ToList();

            var empresa = ContextMulti.Empresas.Where(x => x.IdEmpresa == body.empresaId).FirstOrDefault();
            return await GenerarAsync(aciAPagarList, empresa, body.usuarioId, body.username);
        }

        public async Task<List<GenerarComprobanteEvent>> GenerarTodosRezagados(GenerarComprobanteInput body)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoRepository() GenerarTodosRezagados");
            var aciAPagarList = ContextMulti.VwObtenerRezagadosPagos.Where(x => x.IdComision == body.comisionId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA &&
                x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == ESTADO_DETALLE_EMPRESA_CONFIRMADO &&
                x.IdEstadoComision == FORMA_PAGO_REZAGADO_CERRADO).ToList();

            var empresa = ContextMulti.Empresas.Where(x => x.IdEmpresa == body.empresaId).FirstOrDefault();
            return await GenerarRezagadosAsync(aciAPagarList, empresa, body.usuarioId, body.username);
        }

        public async Task<List<GenerarComprobanteEvent>> GenerarParcialRezagados(GenerarComprobanteInput body, List<int> confirmados)
        {
            Logger.LogWarning($"Inicio GenerarComprobanteBancoRepository() GenerarParcialRezagados");
            Utils.Utils.ShowValueFields(body, Logger);
            //var aciAPagarList = ContextMulti.VwObtenerRezagadosPagos.Where(x => x.IdCiclo == body.cicloId && x.IdComision == body.comisionId && x.IdEmpresa == body.empresaId && x.IdTipoPago == TIPO_PAGO_TRANSFERENCIA &&
            //                x.IdEstadoComisionDetalleEmpresa != ESTADO_DETALLE_EMPRESA_CONFIRMADO &&
            //                x.IdEstadoListadoFormaPago == 4 && x.EstadoListadoFormaPagoHabilitado == true).ToList();

            Logger.LogWarning($"Inicio GenerarComprobanteBancoRepository() GenerarParcialRezagados confirmados.count: {confirmados.Count()}");
            var a = ContextMulti.VwObtenerRezagadosPagos.Where(x => confirmados.Contains(x.IdComisionDetalleEmpresa) && x.IdComision == body.comisionId && x.IdEstadoComision == FORMA_PAGO_REZAGADO_CERRADO).ToList();

            var empresa = ContextMulti.Empresas.Where(x => x.IdEmpresa == body.empresaId).FirstOrDefault();
            return await GenerarRezagadosAsync(a, empresa, body.usuarioId, body.username);
        }

        private GenerarComprobanteEvent PostEvent(int type, string message)
        {
            GenerarComprobanteEvent e = new GenerarComprobanteEvent
            {
                eventType = type
            };
            if (message != null)
            {
                e.message = message;
            }
            return e;
        }
    }
}
