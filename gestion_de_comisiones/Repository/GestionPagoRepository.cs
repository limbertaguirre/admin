using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos.FormaPago;
using gestion_de_comisiones.Modelos.GestionPagos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

                    var ListComisiones = ContextMulti.VwObtenercomisionesFormaPagoes.Where(x => x.IdCiclo == param.idCiclo && x.IdTipoComision == idTipoComisionPagoComision && x.IdEstadoComision == idEstadoComision || (x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                    var lista = ListComisiones.Where(x =>  x.Ci.Contains(param.nombreCriterio.Trim())).ToList();
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





    }
}
