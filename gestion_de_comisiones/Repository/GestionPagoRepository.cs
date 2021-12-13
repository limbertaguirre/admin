﻿using gestion_de_comisiones.Controllers.Events;
using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using gestion_de_comisiones.Servicios.Interfaces;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class GestionPagoRepository : IGestionPagoRepository
    {

        private readonly BDMultinivelContext ContextMulti;
        private readonly ILogger<GestionPagoRepository> Logger;

        private readonly IEnvioCorreoRezagadoService EnvioCorreoService;
        public GestionPagoRepository(BDMultinivelContext multinivelDbContext, IEnvioCorreoRezagadoService envioCorreoService, ILogger<GestionPagoRepository> logger)
        {
            this.ContextMulti = multinivelDbContext;
            this.Logger = logger;
           
            this.EnvioCorreoService = envioCorreoService;
        }
        public GestionPagoRepository()
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
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos() ");
                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision== comision.IdComision && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision ).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() en pagos mensaje : {ex.Message}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;

            }
        }
        public object GetFiltroComisionesPorFormaPago(FiltroFormaPagosInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision)
        {
            try
            {
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetFiltroComisionesPorFormaPago() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");

                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision).ToList();
                List<FormaPagoModel> LisFormaPagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new FormaPagoModel(p.IdTipoPago, p.Nombre, p.Descripcion, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion, (bool)p.Estado, p.Icono)).ToList();               
                foreach (var item in LisFormaPagos)
                {
                    if (ListComisiones != null) {
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
            } catch (Exception ex) {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetFiltroComisionesPorFormaPago() mensaje : {ex}");
                List<FormaPagoDisponiblesModel> list = new List<FormaPagoDisponiblesModel>();
                return list;
            }
        }

        public List<VwObtenercomisionesFormaPago> GetComisionesPorCarnetListPagos(BuscarComisionInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision)
        {
            try
            {
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository GetComisionesPorCarnet() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");
                if (param.nombreCriterio != "")
                {
                    var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                    var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision ).ToList();
                    var lista = ListComisiones.Where(x => x.IdTipoPago !=0 &&  x.Ci.Contains(param.nombreCriterio.Trim())).ToList();
                    return lista;
                } else {
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch GetComisionesPorCarnet() mensaje : {ex}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
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
                var parameterReturn2 = new SqlParameter[] {
                               new SqlParameter  {
                                            ParameterName = "ReturnValue",
                                            SqlDbType = System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Output,
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
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_PAGAR_SION_PAY_COMISIONES_CICLO] @id_ciclo,  @id_usuario  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                if (returnValue > 0)
                {
                    var result2 = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_2_PROCESAR_PAGO_SION_PAY_UPDATE_DETALLES] @id_ciclo,  @id_usuario  ", parameterReturn2);
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

        public RespuestaSionPayModel VerificarPagoSionPayCiclo(VerificarPagoSionPayInput param, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura, int idTipoComisionPagoComision, int idTipoFormaPagoSionPay)
        {
            try
            {
                RespuestaSionPayModel model = new RespuestaSionPayModel();              
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository VerificarPagoSionPayCiclo() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");     
                    var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                    var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision  && x.IdEstadoComision == idEstadoComision ).ToList();
                    model.Cantidad = ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPagoSionPay && x.PagoDetalleHabilitado == false ).Count();
                    model.totalPagoSionPay = (decimal)ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPagoSionPay && x.PagoDetalleHabilitado == false).Sum(c => c.MontoNeto);
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

        public dynamic handleTransferenciasEmpresas(ComisionesPagosInput param)
        {
            //throw new NotImplementedException();
            ///*
            try
            {
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                int idTipoPagoTransferencia = 2;
                int idTipoComision = 1;
                int IdEstadoComisionCerradoFormaPago = 10;
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository handleTransferenciasEmpresas() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo: {param.idCiclo} , idTipoComision: {idTipoComision}, idTipoPagoTransferencia: {idTipoPagoTransferencia}");
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
                for(int i = 0; i < empresasIds.Count; i++)
                {
                    ids[i] = (int) empresasIds[i].empresaId;
                }
                var empresas = ContextMulti.VwObtenerEmpresasComisionesDetalleEmpresas
                    .Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComision && x.IdTipoPago == idTipoPagoTransferencia && x.IdEstadoComision == IdEstadoComisionCerradoFormaPago && x.MontoTransferir != 0 && ids.Contains(x.IdEmpresa))
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
            //*/
        }

        public object handleDownloadFileEmpresas(DownloadFileTransferenciaInput body)
        {
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try {
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
                Logger.LogInformation($" usuarioId: {usuarioId}, handleDownloadFileEmpresas inicio SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS parameterReturn: {parameterReturn}");
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS] @CicloId,  @EmpresaId, @UsuarioId, @FechaPago ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, inicio repository handleDownloadFileEmpresas(): SP_ACTUALIZAR_FECHA_PAGO_TRANSFERENCIAS returnValue {returnValue}  ");
                if (returnValue == 1)
                {
                    //throw new Exception("Pas[o algo inesperado, no se pudo realizar la actualizacion.");
                    return postEvent(GestionPagosEvent.ERROR, "Pas[o algo inesperado, no se pudo realizar la actualizacion.");
                }
                else if (returnValue == 2)
                {
                    return postEvent(GestionPagosEvent.ROLLBACK_ERROR, "Hubo problemas de conexion, por favor intenta mas tarde.");
                }

                int cicloId = Convert.ToInt32(body.cicloId);
                int tipoPagoTransferencia = 2;
                int estadoComisionDetalleEmpresaPendienteId = 1;
                List<VwObtenerInfoExcelFormatoBanco> info = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdTipoPago == tipoPagoTransferencia && x.IdEstadoComisionDetalleEmpresa == estadoComisionDetalleEmpresaPendienteId)
                    .ToList();

                Logger.LogWarning($"handleDownloadFileEmpresas Count: {info.Count}");
                if(info.Count == 0)
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
                        VwObtenerInfoExcelFormatoBanco f = info[i - 2];
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
                        double dd = (double) f.ImportePorEmpresa;
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
                    return postEvent(GestionPagosEvent.SUCCESS, r, "Archivo excel generado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleDownloadFileEmpresas() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return postEvent(GestionPagosEvent.ERROR, "Pasó un inconveniente, por favor intente más tarde mientras lo resolvemos, ¡gracias!.");
            }
        }

        public bool handleConfirmarPagosTransferenciasTodos(DownloadFileTransferenciaInput body)
        {
            try {
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

                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_CONFIRMAR_TRANSFERENCIAS_TODOS] @CicloId,  @EmpresaId, @UsuarioId  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, inicio repository handleConfirmarPagosTransferenciasTodos(): SP_CONFIRMAR_TRANSFERENCIAS_TODOS returnValue {returnValue}  ");
                if (returnValue == 0)
                {
                    return true;
                } else
                {
                    return false;
                }
                //return 0;
            } catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleDownloadFileEmpresas() mensaje : {ex}");
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                return false;
            }
        }

        public GestionPagosEvent handleVerificarPagosTransferenciasTodos(DownloadFileTransferenciaInput body)
        {
            try
            {
                List<VwObtenerInfoExcelFormatoBanco> list = new List<VwObtenerInfoExcelFormatoBanco>();
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
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_CONFIRMADAS] @CicloId, @UsuarioId  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, fin repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_CONFIRMADAS returnValue {returnValue}  ");
                if (returnValue == -1)
                {
                    // Rollback
                    Logger.LogInformation($" result: {result}, repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_CONFIRMADAS returnValue -1. Catch e hizo Rollback. Analizar la razon.");
                }
                else if (returnValue == 1)
                {
                    Logger.LogInformation($" result: {result}, repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_CONFIRMADAS returnValue 1. No hay pagos de transferencias pendientes de pago y se registro en tabla detalle forma de pagos.");
                    recargarCicloActual = true;
                } else if (returnValue == 2)
                {
                    Logger.LogInformation($" result: {result}, repository handleVerificarPagosTransferenciasTodos(): SP_REGISTRAR_TODAS_TRANSFERENCIAS_PAGOS_CONFIRMADAS returnValue 2. Aun hay pagos de transferencias pendientes de pago.");
                }

                var cantidadPendientes = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 1).Count();
                var cantidadRechazados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 1).Count();
                var cantidadConfirmados = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 2).Count();

                var sumaTotalConfirmados = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 2)
                    .Sum(x => x.ImportePorEmpresa);

                var sumaTotalRechazados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdTipoPago == 2 && x.IdEmpresa == body.empresaId && x.IdEstadoComisionDetalleEmpresa == 1)
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

                double ddd = (double) sumaTotalConfirmados;
                string sumaTotalConfirmadosSS = ddd.ToString("N2", new CultureInfo("is-IS"));

                double dd = (double) sumaTotalPendientes;
                string sumaTotalPendientesSS = dd.ToString("N2", new CultureInfo("is-IS"));

                double d = (double) sumaTotalRechazados;
                string sumaTotalRechazadosSS = d.ToString("N2", new CultureInfo("is-IS"));

                if (cantidadPendientes > 0)
                {
                    o.type = VerificarPagosTransferenciasOutput.PENDIENTES;
                    o.ciclo = ciclo;
                    o.empresa = empresa;
                    o.totalPendientes = cantidadPendientes;
                    o.montoTotalPendientes = sumaTotalPendientesSS;
                } else if(cantidadPendientes == 0 && (cantidadRechazados > 0 || cantidadConfirmados > 0))
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
                    return postEvent(GestionPagosEvent.EXISTEN_PENDIENTES, o, $"Hay pendientes para confirmar el pago por transferencia de este ciclo ({ciclo}) para la empresa {empresa}.");
                } else if (cantidadRechazados > 0)
                {
                    return postEvent(GestionPagosEvent.EXISTEN_RECHAZADOS, o, $"Hay rechazados en este ciclo ({ciclo}) para la empresa {empresa}.");
                } else
                {
                    return postEvent(GestionPagosEvent.NO_EXISTEN_PENDIENTES_NI_RECHAZADOS, o, $"Se confirmaron todos los pagos por transferencia de este ciclo ({ciclo}) para la empresa {empresa}.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleVerificarPagosTransferenciasTodos() mensaje : {ex}");
                return postEvent(GestionPagosEvent.ERROR, ex.Message);
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
            if(file != null)
            {
                e.file = file;
            }
            if(errorMessage != null)
            {
                e.message = errorMessage;
            }
            return e;
        }

        public object handleObtenerPagosTransferencias(DownloadFileTransferenciaInput body)
        {
            try
            {

                Logger.LogInformation($" usuario: {body.user} inicio el repository handleObtenerPagosTransferencias() ");
                Logger.LogInformation($" usuario: {body.user} parametros: idciclo: {body.cicloId}");
                int cicloId = Convert.ToInt32(body.cicloId);
                int tipoPagoTransferencia = 2;
                List<VwObtenerInfoExcelFormatoBanco> info = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdTipoPago == tipoPagoTransferencia)
                    .ToList();

                var montoTotal = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdTipoPago == tipoPagoTransferencia)
                    .Sum(x => x.ImportePorEmpresa);

                ObtenerPagosTransferenciasOutput o = new ObtenerPagosTransferenciasOutput();
                o.list = info;
                o.montoTotal = montoTotal.ToString();
                return o;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleObtenerPagosTransferencias() mensaje : {ex}");
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                return list;
            }
        }

        public GestionPagosEvent handleConfirmarPagosTransferencias(ConfirmarPagosTransferenciasInput body)
        {
            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try
            {
                Logger.LogInformation($" usuario: {body.user}, inicio repository handleConfirmarPagosTransferencias(): idciclo {body.cicloId}  ");
                
                var usuarioId = ContextMulti.Usuarios
                    .Where(x => x.Usuario1 == body.user)
                    .Select(u => new
                    {
                        usuarioId = u.IdUsuario
                    }).FirstOrDefault();
               
                Logger.LogInformation($" usuarioId: {usuarioId}, inicio confirmarTransferidosSeleccionados en repository handleConfirmarPagosTransferencias()");
                if (!confirmarTransferidosSeleccionados(body, usuarioId))
                {
                    return postEvent(GestionPagosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a confirmados, verifique e intente nuevamente.");
                }

                int tipoPagoTransferencia = 2;
                int idEstadoComisionDetalleEmpresaConfirmado = 2;
                List<VwObtenerInfoExcelFormatoBanco> l = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == body.cicloId && x.IdEmpresa == body.empresaId && x.IdTipoPago == tipoPagoTransferencia &&
                            x.IdEstadoComisionDetalleEmpresa != idEstadoComisionDetalleEmpresaConfirmado)
                    .ToList();

                if (!confirmarTransferidosNoSeleccionados(body, usuarioId, l))
                {
                    return postEvent(GestionPagosEvent.ERROR_CONFIRMAR_TRANSFERIDOS_NO_SELECCIONADOS, "No se pudo realizar la confirmación de los pagos por transferencia a rechazados, verifique e intente nuevamente.");
                }

                // SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS
                int estadoComisionRezagadoId = 9;
                Logger.LogInformation($" Iniciando carga de parametros de entrada para ejecutar el SP SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS");
                Logger.LogInformation($" UsuarioId: {usuarioId}, CicloId: {body.cicloId}, EmpresaId: {body.empresaId}");
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
                                            ParameterName = "@TipoPago",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = tipoPagoTransferencia
                              },
                               new SqlParameter() {
                                            ParameterName = "@EstadoComision",
                                            SqlDbType =  System.Data.SqlDbType.Int,
                                            Direction = System.Data.ParameterDirection.Input,
                                            Value = estadoComisionRezagadoId
                              }
                           };
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn ReturnValue: {parameterReturn[0].Value}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn CicloId: {parameterReturn[1].Value}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn EmpresaId: {parameterReturn[2].Value}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn UsuarioId: {parameterReturn[3].Value}");
                Logger.LogInformation($"repository handleConfirmarPagosTransferencias inicio SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS parameterReturn TipoPago: {parameterReturn[4].Value}");
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS] @EstadoComision, @CicloId,  @EmpresaId, @UsuarioId, @TipoPago ", parameterReturn);
                int returnValue = (int) parameterReturn[0].Value;
                Logger.LogInformation($" result: {result}, repository handleConfirmarPagosTransferencias fin SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS returnValue: {returnValue}  ");
                if (returnValue == -1)
                {
                    // Entro al catch del SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS hizo Rollback
                    Logger.LogWarning($"repository handleConfirmarPagosTransferencias() SP_REGISTRAR_REZAGADOS_POR_PAGOS_RECHAZADOS @returnValue: {returnValue}");
                    dbcontextTransaction.Rollback();
                    return postEvent(GestionPagosEvent.CATCH_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Pasó algo inesperado, no se pudo registrar a los ACI rechazados.");
                }
                
                
                List<VwObtenerRezagadosPago> rezagados = ContextMulti.VwObtenerRezagadosPagos
                    .Where(x => x.IdCiclo == body.cicloId && x.IdComision == returnValue && x.IdEmpresa == body.empresaId && x.IdTipoPago == tipoPagoTransferencia &&
                            x.IdEstadoComisionDetalleEmpresa != idEstadoComisionDetalleEmpresaConfirmado)
                    .ToList();
                dbcontextTransaction.Commit();
                Logger.LogInformation($" usuario: {body.user}, despues del commit");
                // Si returnValue no es -1 ni 2, es 1
                if (rezagados.Count > 0)
                {
                    string asunto = "Lista de Rechazados en ciclo " + rezagados.ElementAt(0).Glosa + " Por Empresa " + rezagados.ElementAt(0).Empresa;
                    EnvioCorreoService.EnviarCorreoRezagados(rezagados, asunto);
                    
                }                
                return postEvent(GestionPagosEvent.SUCCESS_SP_REGISTRAR_REZAGADOS_POR_PAGOS_TRANSFERENCIAS_RECHAZADOS, "Se realizó correctamente la confirmación para pagos por transferencias de los ACI seleccionados.");
            }
            catch (Exception ex)
            {                
                Logger.LogWarning($" usuario: {body.user} CATCH handleConfirmarPagosTransferencias() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return postEvent(GestionPagosEvent.ERROR, $"NO se pudo realizar la confirmación de los pagos por transferencia, verifique e intente nuevamente. Mensaje: {ex.Message}");
            }
        }

        public object handleRechazadosPagosTransferencias(ConfirmarPagosTransferenciasInput body)
        {
            Logger.LogWarning($" usuario: {body.user} error catch handleConfirmarPagosTransferencias() idEmpresa : {body.empresaId}");
            for (int i = 0; i < body.rechazados.Count; i++)
            {
                var a = body.rechazados[i];
                Logger.LogWarning($" usuario: {body.user} error catch handleConfirmarPagosTransferencias() idComisionDetalleEmpresa : {a}");
            }
            return 0;
        }

        private bool confirmarTransferidosSeleccionados(ConfirmarPagosTransferenciasInput body, dynamic usuarioId)
        {
            try {
                Logger.LogInformation($" usuarioId: {usuarioId}, inicio repository confirmarTransferidosSeleccionados() body.confirmados.Count: {body.confirmados.Count}");
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

                    Logger.LogInformation($" result: {body.confirmados[i]}, inicio repository confirmarTransferidosSeleccionados():  SP_CONFIRMAR_TRANSFERENCIAS_SELECCIONADAS @ComisionDetalleEmpresaId {body.confirmados[i]}  ");
                    var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_CONFIRMAR_TRANSFERENCIAS_SELECCIONADAS] @CicloId,  @EmpresaId, @UsuarioId, @ComisionDetalleEmpresaId", parameterReturn);
                    int returnValue = (int)parameterReturn[0].Value;

                    Logger.LogInformation($" result: {result}, inicio repository confirmarTransferidosSeleccionados():  SP_CONFIRMAR_TRANSFERENCIAS_SELECCIONADAS returnValue {returnValue} ");
                    if (returnValue == 1)
                    {
                        return false;
                    }                
                }
                return true;
            } catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} CATCH confirmarTransferidosSeleccionados(): {ex}");
                return false;
            }
        }
       
        private bool confirmarTransferidosNoSeleccionados(ConfirmarPagosTransferenciasInput body, dynamic usuarioId, List<VwObtenerInfoExcelFormatoBanco> l)
        {
            try {
            Logger.LogInformation($" usuarioId: {usuarioId}, inicio repository confirmarTransferidosNoSeleccionados() : {l.Count}");                                
            for (int i = 0; i < l.Count; i++)
            {
                VwObtenerInfoExcelFormatoBanco o = l[i];
                Logger.LogInformation($" repository confirmarTransferidosNoSeleccionados() para RECHAZAR IdComisionDetalleEmpresa: {o.IdComisionDetalleEmpresa}");
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

                //Logger.LogInformation($" result: {body.confirmados[i]}, inicio repository confirmarTransferidosNoSeleccionados():  SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS @ComisionDetalleEmpresaId {body.confirmados[i]}  ");
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS] @CicloId,  @EmpresaId, @UsuarioId, @ComisionDetalleEmpresaId", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;

                Logger.LogInformation($" result: {result}, inicio repository confirmarTransferidosNoSeleccionados():  SP_RECHAZAR_TRANSFERENCIAS_NO_SELECCIONADAS returnValue {returnValue} ");
                if (returnValue == 1)
                {
                    return false;
                }
            }
                return true;
            } catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} CATCH confirmarTransferidosNoSeleccionados(): {ex}");
                return false;
            }
        }
        public List<VwObtenercomisionesFormaPago> FiltrarComisionPagoPorTipoPago(FiltroComisionTipoPagoInput param, int idEstadoComision, int idTipoComisionPagoComision)
        {
            try
            {
                List<VwObtenercomisionesFormaPagoes> list = new List<VwObtenercomisionesFormaPagoes>();
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository FiltrarComisionPagoPorTipoPago() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo:{param.idCiclo} , idEstado:{idEstadoComision}");
                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdEstadoComision == idEstadoComision && x.IdTipoPago == param.idTipoPago).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch FiltrarComisionPagoPorTipoPago() mensaje : {ex}");
                List<VwObtenercomisionesFormaPago> list = new List<VwObtenercomisionesFormaPago>();
                return list;
            }
        }
        public bool VerificarSiExisteAutorizacionFormaPagoCiclo(string usuarioLogin, int idCiclo)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuarioLogin} iniciando la funcion VerificarSiExisteAprobacion " + "parametros: " + "idciclo: " + idCiclo + " ");
                int estadoAutorizacionComision = 0; //estado aprobado de la tabla ESTADO_AUTORIZACION_COMISION
                var cantidad = ContextMulti.VwVerificarAutorizacionComisions.Where(x => x.IdCiclo == idCiclo && x.IdEstadoAutorizacionComision == estadoAutorizacionComision).Count();
                if (cantidad > 0){
                    return true;
                }
                else{
                    return false;
                }
            }
            catch (Exception ex) {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch ConfirmarAutorizacion() mensaje : {ex}");
                return false;
            }
        }

        public RespuestaPorTipoPagoModel VerificarTipoPagoCiclo(int idCiclo, string usuarioLogin, int idEstadoComision, int idTipoComisionPagoComision, int idTipoFormaPago)
        {
            try
            {
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel();
                Logger.LogWarning($" usuario: {usuarioLogin} inicio el repository VerificarTipoPago() ");
                Logger.LogWarning($" usuario: {usuarioLogin} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdEstadoComision == idEstadoComision).ToList();
                model.Cantidad = ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPago && x.PagoDetalleHabilitado == false).Count();
                model.totalPagoSionPay = (decimal)ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPago && x.PagoDetalleHabilitado == false).Sum(c => c.MontoNeto);
                model.CodigoRespuesta = 1; //valor positivo
                Logger.LogWarning($" usuario: {usuarioLogin} se verifico antes del cierre validando la cantidad de no pagados en porsion pay :  {JsonConvert.SerializeObject(model)} ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch VerificarPagoSionPayCiclo() mensaje : {ex}");
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel { CodigoRespuesta = -1 };
                return model;
            }
        }

        public RespuestaPorTipoPagoModel VerificarTransaccionRechazadoMontoCero(int idCiclo, string usuarioLogin, int idEstadoComision, int idTipoComisionPagoComision, int idTipoFormaPago)
        {
            try
            {
                int idEstadoDEtalleListadoFormaPago = 4;//parametro
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel();
                Logger.LogInformation($" usuario: {usuarioLogin} inicio el cierre de transaccion repository VerificarTransaccionRechazadoMontoCero() ");
                Logger.LogInformation($" usuario: {usuarioLogin} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision} , idtipoFormaPago: {idTipoFormaPago}, idTipoComisionPagoComision: {idTipoComisionPagoComision}");
                Logger.LogInformation($" usuario: {usuarioLogin} verificamos en las comisiones rechazadas que no existe montos mayor a cero, ya que estas comisiones fueron rezagas y el monto de planilla tiene que estas en cero");               
                var comision = ContextMulti.GpComisions.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision).FirstOrDefault();
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdEstadoComision == idEstadoComision && x.IdEstadoListadoFormaPago == idEstadoDEtalleListadoFormaPago).ToList();

                //cantidad debe volver monto cero ya que fueron resagados las transferencias
                model.Cantidad = ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPago && x.MontoNeto > 0).Count();
                if(model.Cantidad > 0)
                {
                    Logger.LogInformation($" usuario: {usuarioLogin} HUBO transacciones rechazados con monto mayor a cero verifique");
                    Logger.LogInformation($" usuario: {usuarioLogin} exiten rechazado con monto mayor a cero :  {JsonConvert.SerializeObject(ListComisiones)} ");
                }
                model.totalPagoSionPay = (decimal)ListComisiones.Where(x => x.IdTipoPago == idTipoFormaPago && x.PagoDetalleHabilitado == false).Sum(c => c.MontoNeto);
                model.CodigoRespuesta = 1; //valor positivo
                Logger.LogInformation($" usuario: {usuarioLogin} se verifico antes del cierre validando la cantidad de no pagados en porsion pay :  {JsonConvert.SerializeObject(model)} ");
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuarioLogin} error catch VerificarPagoSionPayCiclo() mensaje : {ex}");
                RespuestaPorTipoPagoModel model = new RespuestaPorTipoPagoModel { CodigoRespuesta = -1 };
                return model;
            }
        }

        public int CerrarPagoComisionPorTipoComision(CerrarPagoParam param, int idTipoComision)
        {
            Logger.LogInformation($" usuario: {param.usuarioLogin} -  inicio el CerrarPagoComisionPorTipoComision() ");
            Logger.LogInformation($" usuario: {param.usuarioLogin} -  parametros :{ param},  idtipoComision: {idTipoComision}");
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
                                            Value = idTipoComision
                                }
                };
                var result = ContextMulti.Database.ExecuteSqlRaw("EXEC @returnValue = [dbo].[SP_CERRAR_COMISION_PAGO_POR_TIPO] @id_ciclo,  @id_usuario, @id_tipo_comision  ", parameterReturn);
                int returnValue = (int)parameterReturn[0].Value;
                if (returnValue > 0)
                {
                    dbcontextTransaction.Commit();
                    Logger.LogInformation($" usuario: {param.usuarioLogin}-  Se cerro el pago de comision EL [SP_CERRAR_COMISION_PAGO_POR_TIPO].");
                    Logger.LogInformation($" usuario: {param.usuarioLogin}-  respuesta sp: {returnValue}");
                    return returnValue;
                }
                else
                {
                    dbcontextTransaction.Rollback();
                    Logger.LogInformation($" usuario: {param.usuarioLogin}-  NO ROLLBACK EN EL SP [SP_PROCESAR_CERRAR_FORMA_PAGO]");
                    return -1;
                }

            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch CerrarFormaDePago() mensaje : {ex}");
                dbcontextTransaction.Rollback();
                return -2;
            }
        }
        public object BuscarFreelancerPagosTransferencias(DownloadFileTransferenciaInput param)
        {
            try
            {
                Logger.LogInformation($" usuario: {param.user} -  inicio el BuscarFreelancerPagosTransferencias() ");
                int cicloId = Convert.ToInt32(param.cicloId);
                int tipoPagoTransferencia = 2;
                var Buscar = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.DocDeIdentidad == param.ci && x.IdCiclo == cicloId && param.empresaId == x.IdEmpresa && x.IdTipoPago == tipoPagoTransferencia)
                    .ToList();
                return Buscar;
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario: {param.user} error catch BuscarFreelancerPagosTransferencias() mensaje : {ex}");
                return false;
            }
        }
    }
}
