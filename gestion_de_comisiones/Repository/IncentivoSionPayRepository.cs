using gestion_de_comisiones.Modelos.Incentivo;
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
        private readonly int  tipoComisionIncentivo = 3;
        private readonly BDMultinivelContext ContextMulti;

        public IncentivoSionPayRepository(BDMultinivelContext contextMulti)
        {
            this.ContextMulti = contextMulti;
        }
        private List<GpComisionDetalle> armarComisionDetallesPersistir(List<DatosPlanillaExcel> DatosClientes, int idComision)
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
            List<GpComisionDetalle>  lista = DatosClientes.GroupBy(d => d.CiCliente).Select(
                                    g => new GpComisionDetalle
                                    {
                                        MontoNeto = g.Sum(s => s.Monto),
                                        MontoBruto = 0,
                                        PorcentajeRetencion = 0,
                                        MontoRetencion = 0,
                                        MontoAplicacion = 0,                                        
                                        IdComision = idComision,
                                        IdFicha = ContextMulti.Fichas.Where((item) => item.Ci == g.First().CiCliente).FirstOrDefault().IdFicha, 
                                        IdUsuario = (ContextMulti.Usuarios.Where((item) => item.Usuario1 == g.First().Usuario).FirstOrDefault() != null) ? ContextMulti.Usuarios.Where((item) => item.Usuario1 == g.First().Usuario).FirstOrDefault().IdUsuario : 0,
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
            int estadoComisionIncentivoPendiente = 14; // quemado
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
                estadoPendienteFacturacion = 1;

                GpComisionEstadoComisionI detalleComisionEstado = new GpComisionEstadoComisionI()
                {
                    Habilitado = true,
                    IdComision = idComisionGenerada,
                    IdEstadoComision = estadoPendienteFacturacion,
                    IdUsuario = elem.IdUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                };

                ContextMulti.Add(detalleComisionEstado);
                ContextMulti.SaveChanges();

                List<GpComisionDetalle> comisionesDetalle = armarComisionDetallesPersistir(planillaIncentivo.DatosClientes, idComisionGenerada);
                    ContextMulti.GpComisionDetalles.AddRange(comisionesDetalle);
                    ContextMulti.SaveChanges();

                List<GpComisionDetalleEstadoI>  comisionesDetallesEstados = armarComisionesDetallesEstados(comisionesDetalle);
                ContextMulti.GpComisionDetalleEstadoIs.AddRange(comisionesDetallesEstados);
                ContextMulti.SaveChanges();

                List<IncentivoPagoComision> incentivosPagosComisiones = armarIncentivosPagosComisiones(comisionesDetalle);
                ContextMulti.IncentivoPagoComisions.AddRange(incentivosPagosComisiones);
                ContextMulti.SaveChanges();

                List<ComisionDetalleEmpresa>  comisionesDetallePersistir = armarComisionesDetalleEmpresaPersistir(planillaIncentivo.DatosClientes, comisionesDetalle);
                ContextMulti.ComisionDetalleEmpresas.AddRange(comisionesDetallePersistir);
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

        private List<IncentivoPagoComision> armarIncentivosPagosComisiones(List<GpComisionDetalle> comisionesDetalle)
        {
            List<IncentivoPagoComision> lista = new List<IncentivoPagoComision>();
            foreach (GpComisionDetalle elem in comisionesDetalle)
            {
                IncentivoPagoComision incentivoPagoComi = new IncentivoPagoComision()
                {
                    IdTipoIncentivoPago = 1,
                    IdComisionDetalle = elem.IdComisionDetalle
                };
                lista.Add(incentivoPagoComi);
            }
            return lista;
        }

        public List<DatosPlanillaExcel> verificarIncentivosEmpresaCiNoRepetidos(PlanillaPagoIncentivo planillaIncentivo)
        {
            int idComision = ContextMulti.GpComisions.Where((item) => item.IdCiclo == planillaIncentivo.IdCiclo).FirstOrDefault().IdComision;
            //List<DatosPlanillaExcel> lista = new List<DatosPlanillaExcel>();
            bool observada = false;
            foreach (DatosPlanillaExcel elem in planillaIncentivo.DatosClientes)
            {
                var idFicha = ContextMulti.Fichas.Where((item) => item.Ci == elem.CiCliente).FirstOrDefault(); //.IdFicha
                if (idFicha != null)
                {
                    var idComisionDetalle = ContextMulti.GpComisionDetalles.Where((item) => item.IdComision == idComision && item.IdFicha ==  idFicha.IdFicha).FirstOrDefault(); //.IdComisionDetalle
                    //var idComisionDetalle = ContextMulti.GpComisionDetalles.Where((item) => item.IdComision == idComision && item.IdFicha == idFicha.IdFicha).FirstOrDefault();

                    if (idComisionDetalle != null)
                    {
                        var idEmpresa = ContextMulti.ComisionDetalleEmpresas.Where((item) => item.IdComisionDetalle == idComisionDetalle.IdComisionDetalle && item.IdEmpresa == elem.IdEmpresa).FirstOrDefault();
                        var tipoIncentivoPago = ContextMulti.IncentivoPagoComisions.Where((item) => item.IdComisionDetalle == idComisionDetalle.IdComisionDetalle).FirstOrDefault();
                        if (tipoIncentivoPago != null)
                        {
                            if(tipoIncentivoPago.IdTipoIncentivoPago == elem.IdTipoIncentivoPago)
                            {
                                observada = true;
                                elem.Observada = true;
                                elem.MotivoObservacion = $"ya existe un registro de este freelancers en el ciclo: ${planillaIncentivo.IdCiclo} con la misma empresa: {elem.Empresa}";
                            }
                            
                        }
                    }
                    
                }
                               
            }

            return (observada == true)? planillaIncentivo.DatosClientes : null;
        }
    }
}
