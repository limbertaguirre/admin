using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class GestionPagoRepository : IGestionPagoRepository
    {

        private readonly BDMultinivelContext ContextMulti;
        private readonly ILogger<GestionPagoRepository> Logger;

        public GestionPagoRepository(BDMultinivelContext multinivelDbContext, ILogger<GestionPagoRepository> logger)
        {
            this.ContextMulti = multinivelDbContext;
            this.Logger = logger;
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
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == idCiclo && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision ).ToList();
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
                var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                List<FormaPagoModel> LisFormaPagos = ContextMulti.TipoPagoes.Where(x => x.Estado == true).Select(p => new FormaPagoModel(p.IdTipoPago, p.Nombre, p.Descripcion, p.IdUsuario, p.FechaCreacion, p.FechaActualizacion, p.Estado, p.Icono)).ToList();               
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
                    var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdComision == comision.IdComision && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
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

        public object handleTransferenciasEmpresas(ComisionesPagosInput param)
        {
            //throw new NotImplementedException();
            ///*
            try
            {
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
                int idTipoPago = 1;
                int idTipoComision = 1;
                Logger.LogWarning($" usuario: {param.usuarioLogin} inicio el repository handleTransferenciasEmpresas() ");
                Logger.LogWarning($" usuario: {param.usuarioLogin} parametros: idciclo: {param.idCiclo} , idTipoComision: {idTipoComision}, idTipoPago: {idTipoPago}");

                var empresas = ContextMulti.VwObtenerEmpresasComisionesDetalleEmpresas
                    .Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComision && x.IdTipoPago == idTipoPago)
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
            try
            {

                Logger.LogWarning($" usuario: {body.user} inicio el repository handleDownloadFileEmpresas() ");
                Logger.LogWarning($" usuario: {body.user} parametros: idciclo: {body.cicloId}");
                int cicloId = Convert.ToInt32(body.cicloId);
                int tipoPagoTransferencia = 2;
                List<VwObtenerInfoExcelFormatoBanco> info = ContextMulti.VwObtenerInfoExcelFormatoBancoes
                    .Where(x => x.IdCiclo == cicloId && x.IdEmpresa == body.empresaId && x.IdTipoPago == tipoPagoTransferencia)
                    .ToList();

                Logger.LogWarning($"handleDownloadFileEmpresas Count: {info.Count}");
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

                    for (int i = 2; i <= info.Count; i++)
                    {
                        VwObtenerInfoExcelFormatoBanco f = info[i - 2];
                        ws.Cells[i, 1].Value = Convert.ToString(i - 1);
                        //ws.Cells[i, 1].AutoFitColumns(1);
                        ws.Cells[i, 2].Value = Convert.ToString(f.CodigoDeCliente);
                        //ws.Cells[i, 2].AutoFitColumns(1);
                        ws.Cells[i, 3].Value = Convert.ToString(f.NroDeCuenta);
                        //ws.Cells[i, 3].AutoFitColumns(1);
                        ws.Cells[i, 4].Value = Convert.ToString(f.NombreDeCliente);
                        ws.Cells[i, 4].AutoFitColumns(1);
                        ws.Cells[i, 5].Value = Convert.ToString(f.DocDeIdentidad);
                        ws.Cells[i, 5].AutoFitColumns(1);                        
                        ws.Cells[i, 6].Value = Convert.ToString(f.ImportePorEmpresa).Replace(".",",");
                        //ws.Cells[i, 6].AutoFitColumns(1);
                        ws.Cells[i, 7].Value = Convert.ToString(f.FechaDePago);
                        ws.Cells[i, 7].AutoFitColumns(1);
                        ws.Cells[i, 8].Value = Convert.ToString(f.FormaDePago);
                        //ws.Cells[i, 8].AutoFitColumns(1);
                        ws.Cells[i, 9].Value = Convert.ToString(f.MonedaDestino);
                        //ws.Cells[i, 9].AutoFitColumns(1);
                        ws.Cells[i, 10].Value = Convert.ToString(f.EntidadDestino);
                        //ws.Cells[i, 10].AutoFitColumns(1);
                        ws.Cells[i, 11].Value = "";
                        ws.Cells[i, 12].Value = "PAGO DE COMISIONES DEL MES DE " + Convert.ToString(f.Glosa);
                        ws.Cells[i, 12].AutoFitColumns(1);
                        ws.Cells[i, 13].Value = "";
                    }
                    DownloadFileTransferenciaOutput r = new DownloadFileTransferenciaOutput();
                    r.file = Convert.ToBase64String(p.GetAsByteArray());
                    r.fileName = info[0].Empresa;
                    return r;
                }                
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {body.user} error catch handleDownloadFileEmpresas() mensaje : {ex}");
                List<VwObtenerEmpresasComisionesDetalleEmpresa> list = new List<VwObtenerEmpresasComisionesDetalleEmpresa>();
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
    }
}
