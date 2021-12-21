﻿using gestion_de_comisiones.Modelos.Incentivo;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class IncentivoSionPayRepository : IIncentivoSionPayRepository
    {
        //BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<IncentivoSionPayRepository> Logger;
        private readonly int tipoComisionIncentivo = 3;
        private readonly BDMultinivelContext ContextMulti;
        public IncentivoSionPayRepository(BDMultinivelContext contextMulti, ILogger<IncentivoSionPayRepository> logger)
        {
            Logger = logger;
            this.ContextMulti = contextMulti;
        }
        private List<GpComisionDetalle> armarComisionDetallesPersistir(List<DatosPlanillaExcel> DatosClientes, int idComision, string usuario)
        {
            GpComisionDetalle elemento = new GpComisionDetalle
            {
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
                IdComision = 1,
                IdFicha = 1,
                IdUsuario = 1,
                MontoAplicacion = 1,
                MontoBruto = 10,
                MontoNeto = 10,
                MontoRetencion = 10,
                PorcentajeRetencion = 10
            };

            List<GpComisionDetalle> listaAux = new List<GpComisionDetalle>();
            List<GpComisionDetalle> lista = DatosClientes.GroupBy(d => d.CiCliente).Select(
                                    g => new GpComisionDetalle
                                    {
                                        MontoNeto = g.Sum(s => s.Monto),
                                        MontoBruto = 0,
                                        PorcentajeRetencion = 0,
                                        MontoRetencion = 0,
                                        MontoAplicacion = 0,
                                        IdComision = idComision,
                                        IdFicha = ContextMulti.Fichas.Where((item) => item.Ci == g.First().CiCliente).FirstOrDefault().IdFicha, 
                                        IdUsuario = (ContextMulti.Usuarios.Where((item) => item.Usuario1 == usuario).FirstOrDefault() != null) ? ContextMulti.Usuarios.Where((item) => item.Usuario1 == usuario).FirstOrDefault().IdUsuario : 0,
                                        FechaCreacion = DateTime.Now,
                                        FechaActualizacion = DateTime.Now

                                    }
                         ).ToList();




            return lista;
        }
        private List<ComisionDetalleEmpresa> armarComisionesDetalleEmpresaPersistir(List<DatosPlanillaExcel> planillaDatosClientes, List<GpComisionDetalle> comisionesDetalles)
        {
            List<ComisionDetalleEmpresa> comisionesDetalleEmpresas = new List<ComisionDetalleEmpresa>();
            byte estadoComisionDetalleEmpresaPendiente = 1; //  parametro quemado
            foreach (DatosPlanillaExcel elemen in planillaDatosClientes)
            {
                int idFicha = ContextMulti.Fichas.Where((item) => item.Ci == elemen.CiCliente).FirstOrDefault().IdFicha;
                ComisionDetalleEmpresa comisionDetalleEmpresa = new ComisionDetalleEmpresa()
                {
                    Monto = elemen.Monto,
                    Estado = estadoComisionDetalleEmpresaPendiente,
                    RespaldoPath = "",
                    NroAutorizacion = "",
                    MontoAFacturar = elemen.Monto,
                    MontoTotalFacturar = elemen.Monto,
                    IdComisionDetalle = comisionesDetalles.Where((item) => item.IdFicha == idFicha).FirstOrDefault().IdComisionDetalle,
                    //ContextMulti.GpComisionDetalles.Where((item) => item.IdFicha == idFicha).FirstOrDefault().IdComisionDetalle,
                    IdEmpresa = elemen.IdEmpresa,
                    VentasPersonales = 0,
                    VentasGrupales = 0,
                    Residual = 0,
                    Retencion = 0,
                    MontoNeto = elemen.Monto,
                    SiFacturo = true,
                    IdComprobanteGenerico = 0,
                    IdMovimiento = 0,
                    IdUsuario = (ContextMulti.Usuarios.Where((item) => item.Usuario1 == elemen.Usuario).FirstOrDefault() != null) ? ContextMulti.Usuarios.Where((item) => item.Usuario1 == elemen.Usuario).FirstOrDefault().IdUsuario : 0,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                    FechaPago = DateTime.Now
                };
                comisionesDetalleEmpresas.Add(comisionDetalleEmpresa);
            }
            return comisionesDetalleEmpresas;
        }
        private List<GpComisionDetalleEstadoI> armarComisionesDetallesEstados(List<GpComisionDetalle> comisionesDetallesEstados)
        {
            int estadoComisionIncentivoPendiente = 7; // quemado
            List<GpComisionDetalleEstadoI> lista = new List<GpComisionDetalleEstadoI>();
            foreach (GpComisionDetalle elem in comisionesDetallesEstados)
            {
                GpComisionDetalleEstadoI comisionDetalleEstadoI = new GpComisionDetalleEstadoI()
                {
                    IdComisionDetalle = elem.IdComisionDetalle,
                    IdEstadoComisionDetalle = estadoComisionIncentivoPendiente,
                    Habilitado = true,
                    IdUsuario = elem.IdUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };
                lista.Add(comisionDetalleEstadoI);
            }
            return lista;
        }

        private List<ListadoFormasPago> armarListadoFormasPago(List<GpComisionDetalle> comisionesDetalles)
        {
            List<ListadoFormasPago> lista = new List<ListadoFormasPago>();
            int tipoPagoSionPay = 1;
            foreach (GpComisionDetalle elem in comisionesDetalles)
            {
                ListadoFormasPago formaPago = new ListadoFormasPago()
                {
                    MontoNeto = (decimal)elem.MontoNeto,
                    IdTipoPago = tipoPagoSionPay,
                    IdComisionesDetalle = elem.IdComisionDetalle,
                    IdUsuario = (int)elem.IdUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };
                lista.Add(formaPago);
            }
            return lista;
        }

        public object GuardarPlanillaIncentivoSionPay(PlanillaPagoIncentivo planillaIncentivo)
        {

            using var dbcontextTransaction = ContextMulti.Database.BeginTransaction();
            try
                {
                
                GpComision elem = new GpComision();
                    elem.MontoTotalNeto = (decimal)planillaIncentivo.DatosClientes.Sum(item => item.Monto);
                    elem.MontoTotalAplicacion = 0;
                    elem.MontoTotalBruto = 0;
                    elem.MontoTotalRetencion = 0;
                    elem.IdCiclo = planillaIncentivo.IdCiclo;
                    elem.IdTipoComision = tipoComisionIncentivo;
                    elem.PorcentajeRetencion = 0;
                    elem.FechaCreacion = DateTime.Now;
                    elem.FechaActualizacion = DateTime.Now;
                    elem.IdUsuario = ContextMulti.Usuarios.Where(item => item.Usuario1 == planillaIncentivo.UsuarioNombre).FirstOrDefault().IdUsuario;
                    elem.IdCiclo = planillaIncentivo.IdCiclo;
                    ContextMulti.Add(elem);
                    ContextMulti.SaveChanges();

                int idComisionGenerada = elem.IdComision,
                estadoPendienteFacturacion = 1,
                estadoIncentivoPendiente = 14;

                GpComisionEstadoComisionI detalleComisionEstado = new GpComisionEstadoComisionI()
                {
                    Habilitado = true,
                    IdComision = idComisionGenerada,
                    IdEstadoComision = estadoIncentivoPendiente,
                    IdUsuario = elem.IdUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                };

                ContextMulti.Add(detalleComisionEstado);
                ContextMulti.SaveChanges();

                List<GpComisionDetalle> comisionesDetalle = armarComisionDetallesPersistir(planillaIncentivo.DatosClientes, idComisionGenerada, planillaIncentivo.UsuarioNombre);
                    ContextMulti.GpComisionDetalles.AddRange(comisionesDetalle);
                    ContextMulti.SaveChanges();

                List<GpComisionDetalleEstadoI> comisionesDetallesEstados = armarComisionesDetallesEstados(comisionesDetalle);
                ContextMulti.GpComisionDetalleEstadoIs.AddRange(comisionesDetallesEstados);
                ContextMulti.SaveChanges();



                List<ComisionDetalleEmpresa> comisionesDetalleEmpresaPersistir = armarComisionesDetalleEmpresaPersistir(planillaIncentivo.DatosClientes, comisionesDetalle);
                ContextMulti.ComisionDetalleEmpresas.AddRange(comisionesDetalleEmpresaPersistir);
                ContextMulti.SaveChanges();

                List<IncentivoPagoComision> incentivosPagosComisiones = armarIncentivosPagosComisiones(comisionesDetalle, planillaIncentivo.DatosClientes, comisionesDetalleEmpresaPersistir);
                ContextMulti.IncentivoPagoComisions.AddRange(incentivosPagosComisiones);
                ContextMulti.SaveChanges();

                List<ListadoFormasPago> formasPagos = armarListadoFormasPago(comisionesDetalle);
                ContextMulti.ListadoFormasPagoes.AddRange(formasPagos);
                ContextMulti.SaveChanges();



                dbcontextTransaction.Commit();
                return elem;
            }
            catch (Exception e)
            {
                dbcontextTransaction.Rollback();
                Console.WriteLine("Error occurred.");
                return true;
            }




        }

        private List<IncentivoPagoComision> armarIncentivosPagosComisiones(List<GpComisionDetalle> comisionesDetalle, List<DatosPlanillaExcel> planillaDatosClientes, List<ComisionDetalleEmpresa> comisionesDetalleEmpresaPersistir)
        {
            List<IncentivoPagoComision> lista = new List<IncentivoPagoComision>();
            foreach (GpComisionDetalle elem in comisionesDetalle)
            {
                string ciCliente = ContextMulti.Fichas.Where((item) => item.IdFicha == elem.IdFicha ).FirstOrDefault().Ci;
                int idComisionDetalle = comisionesDetalle.Where((item) => item.IdFicha == elem.IdFicha && item.IdComision == elem.IdComision).FirstOrDefault().IdComisionDetalle;
                int idEmpresa = comisionesDetalleEmpresaPersistir.Where((item) => item.IdComisionDetalle == idComisionDetalle).FirstOrDefault().IdEmpresa;
                int idTipoIncentivo = planillaDatosClientes.Where((item) => item.IdEmpresa == idEmpresa && item.CiCliente == ciCliente).FirstOrDefault().IdTipoIncentivoPago;
                IncentivoPagoComision incentivoPagoComi = new IncentivoPagoComision()
                {
                    IdTipoIncentivoPago = idTipoIncentivo,
                    IdComisionDetalle = elem.IdComisionDetalle
                };
                lista.Add(incentivoPagoComi);
            }
            return lista;
        }

        public List<DatosPlanillaExcel> verificarIncentivosEmpresaCiNoRepetidos(PlanillaPagoIncentivo planillaIncentivo)
        {
            var idComisionParam = ContextMulti.GpComisions.Where((item) => item.IdCiclo == planillaIncentivo.IdCiclo).OrderByDescending((item1) => item1.IdComision).FirstOrDefault();
            int idComision = 0;
            if (idComisionParam == null)
            {
                return null;
            }
            else
            {
                idComision = idComisionParam.IdComision;
            }
            //List<DatosPlanillaExcel> lista = new List<DatosPlanillaExcel>();
            bool observada = false;
            foreach (DatosPlanillaExcel elem in planillaIncentivo.DatosClientes)
            {
                var idFicha = ContextMulti.Fichas.Where((item) => item.Ci == elem.CiCliente).FirstOrDefault(); //.IdFicha
                if (idFicha != null)
                {
                    var idComisionDetalle = ContextMulti.GpComisionDetalles.Where((item) => item.IdComision == idComision && item.IdFicha == idFicha.IdFicha).FirstOrDefault(); //.IdComisionDetalle
                    //var idComisionDetalle = ContextMulti.GpComisionDetalles.Where((item) => item.IdComision == idComision && item.IdFicha == idFicha.IdFicha).FirstOrDefault();

                    if (idComisionDetalle != null)
                    {
                        var idEmpresa = ContextMulti.ComisionDetalleEmpresas.Where((item) => item.IdComisionDetalle == idComisionDetalle.IdComisionDetalle && item.IdEmpresa == elem.IdEmpresa).FirstOrDefault();
                        var tipoIncentivoPago = ContextMulti.IncentivoPagoComisions.Where((item) => item.IdComisionDetalle == idComisionDetalle.IdComisionDetalle).FirstOrDefault();
                        if (tipoIncentivoPago != null)
                        {
                            if (tipoIncentivoPago.IdTipoIncentivoPago == elem.IdTipoIncentivoPago)
                            {
                                observada = true;
                                elem.Observada = true;
                                elem.MotivoObservacion = $"ya existe un registro de este freelancers en el ciclo: ${planillaIncentivo.IdCiclo} con la misma empresa: {elem.Empresa}";
                            }

                        }
                    }

                }

            }

            return (observada == true) ? planillaIncentivo.DatosClientes : null;
        }
        public object ObtenerCiclos(string usuario)
        {
            try
            {
                var ciclos = ContextMulti.Cicloes.OrderByDescending(x => x.IdCiclo).Take(2);
                return ciclos;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch mensaje : {ex}");
                List<Ciclo> list = new List<Ciclo>();
                return list;
            }
        }

        public object ObtenerTipoIncentivo(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} Inicio ObtenerTipoIncentivo ");
                var ciclos = ContextMulti.TipoIncentivoPagoes.OrderByDescending(x => x.IdTipoIncentivo).ToList();
                return ciclos;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch mensaje : {ex}");
                List<Ciclo> list = new List<Ciclo>();
                return list;
            }
        }

        public object ObtenerTiposPagos(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} Inicio ObtenerTiposPagos ");
                var tipoPagos = ContextMulti.TipoPagoes.OrderByDescending(x => x.IdTipoPago).ToList();
                return tipoPagos;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch mensaje : {ex}");
                List<TipoPago> list = new List<TipoPago>();
                return list;
            }
        }

        public object ObtenerTipoIncentivosPagosSegunCiclo(int nroCicloMensual, string usuario)
        {
            Logger.LogInformation($" usuario: {usuario} Inicio ObtenerTiposPagos ");


            var listaTipoIncentivo = ContextMulti.GpComisions.Join(ContextMulti.GpComisionDetalles,
                GpComision => GpComision.IdComision,
                GpComisionDetalle => GpComisionDetalle.IdComision,
                 (GpComision, GpComisionDetalle) => new
                 {
                     idComision = GpComision.IdComision,
                     idCiclo = GpComision.IdCiclo,
                     IdComisionDetalle = GpComisionDetalle.IdComisionDetalle

                 }).Join(ContextMulti.IncentivoPagoComisions,
                            GpComisionDetalle => GpComisionDetalle.IdComisionDetalle,
                            IncentivoPagoComision => IncentivoPagoComision.IdComisionDetalle,
                            (GpComisionDetalle, IncentivoPagoComision) => new
                            {
                                IdTipoIncentivoPago = IncentivoPagoComision.IdTipoIncentivoPago,
                                idCiclo = GpComisionDetalle.idCiclo,

                            }).Where(item => item.idCiclo == nroCicloMensual).Join(ContextMulti.TipoIncentivoPagoes,
                                    IncentivoPagoComision => IncentivoPagoComision.IdTipoIncentivoPago,
                                    TipoIncentivoPago => TipoIncentivoPago.IdTipoIncentivo,
                                    (IncentivoPagoComision, TipoIncentivoPago) =>
                                    new
                                    {
                                        Nombre = TipoIncentivoPago.Descripcion,
                                        IdTipoIncentivo = TipoIncentivoPago.IdTipoIncentivo,
                                        idCiclo = IncentivoPagoComision.idCiclo
                                        

                                    }
                            ).Where(x => x.idCiclo == nroCicloMensual).ToList().Distinct();
            
            return listaTipoIncentivo;
        }
        public object RegistrarTipoIncentivoPago(string descripcion)
        {
            try
            {
                Logger.LogInformation($" Inicio ObtenerTipoIncentivo ");
                TipoIncentivoPago tipoIncentivoPago = new TipoIncentivoPago();
                tipoIncentivoPago.Descripcion = descripcion;
                tipoIncentivoPago.Estado = "ACTIVO";
                ContextMulti.Add(tipoIncentivoPago);
                ContextMulti.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"  Error catch RegistrarTipoIncentivoPago mensaje : {ex.Message}");
                return false;
            }

        }

        public object ObtenerPagosIncentivosSegunCicloIdTipoIncentivo(int nroCicloMensual, int tipoIncentivo, string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} Inicio ObtenerTiposPagos ");
                var incentivosAPagar = ContextMulti.VwPagosIncentivos.Where(item => item.IdCiclo == nroCicloMensual && item.IdTipoIncentivo == tipoIncentivo).ToList();
                return incentivosAPagar;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch mensaje : {ex}");
                List<VwPagosIncentivo> list = new List<VwPagosIncentivo>();
                return list;
            }
        }
    }
}
