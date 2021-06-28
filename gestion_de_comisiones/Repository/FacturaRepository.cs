﻿using gestion_de_comisiones.Modelos.Cliente;
using gestion_de_comisiones.Modelos.Factura;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class FacturaRepository: IFacturaRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<FacturaRepository> Logger;
        public FacturaRepository(ILogger<FacturaRepository> logger)
        {
            Logger = logger;
        }
        public object listCiclosPendientes(string usuario )
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el listCiclos() repository");
                int pendiente = int.Parse(Environment.GetEnvironmentVariable("ESTADO_PENDIENTE_COMISION"));
                int idtipoComision = int.Parse(Environment.GetEnvironmentVariable("TIPO_PAGO_COMISIONES_ID"));

                var listiclos = contextMulti.GpComisions.Join(contextMulti.GpComisionEstadoComisionIs,
                                                  GpComision => GpComision.IdComision,
                                                  GpComisionEstadoComisionI => GpComisionEstadoComisionI.IdComision,
                                                  (GpComision, GpComisionEstadoComisionI) => new
                                                  {
                                                      idcomisiones = GpComision.IdComision,
                                                      idEstado = GpComisionEstadoComisionI.IdEstadoComision,
                                                      idTipoComision = GpComision.IdTipoComision,
                                                      idCiclo = GpComision.IdCiclo,
                                                  }).Where(x => x.idEstado == pendiente && x.idTipoComision == idtipoComision).ToList();
                List<CicloOutputModel> lista = new List<CicloOutputModel>();
                foreach (var obj in listiclos){
                    CicloOutputModel objciclo = new CicloOutputModel();
                    var ciclo = contextMulti.Cicloes.Where(x => x.IdCiclo == obj.idCiclo).FirstOrDefault();
                    if(ciclo != null)
                    {
                        objciclo.idCiclo = (int)obj.idCiclo;
                        objciclo.nombre = ciclo.Nombre;
                        objciclo.Descripcion = ciclo.Descripcion;
                        lista.Add(objciclo);
                    }
                }
                return lista;

            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch listCiclos() mensaje : {ex}");
                 List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }
        public object obtenerComisiones(string usuario, int idCiclo, int idEstadoComision)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerComisionesPendientes() ");
                Logger.LogWarning($" usuario: {usuario} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerComisionesPendientes() mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        public object buscarcomisionXnombre(string usuario, int idCiclo, int idEstadoComision, string nombreCriterio)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository buscarcomisionXnombre() criterio nombre: {nombreCriterio} ");
                Logger.LogWarning($" usuario: {usuario} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision && x.Nombre.Contains(nombreCriterio)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch buscarcomisionXnombre() mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        public List<VwObtenerComisionesDetalleEmpresaModel> obtenerDetalleEmpresa(string usuario, int idComisionDetalle )
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerDetalleEmpresa()  idComisionDetalle: {idComisionDetalle} ");
                Logger.LogWarning($" usuario: {usuario} parametros: idComisionDetalle:{idComisionDetalle} ");
               var ListComisiones = contextMulti.VwObtenerComisionesDetalleEmpresas.Where(x => x.IdComisionDetalle== idComisionDetalle && x.EstadoDetalleEmpresa == true).Select(p => new VwObtenerComisionesDetalleEmpresaModel(p.IdComisionDetalleEmpresa, p.IdComisionDetalle, p.Empresa, p.Monto, p.MontoAFacturar, p.MontoTotalFacturar, p.RespaldoPath, p.NroAutorizacion, p.IdEmpresa, p.EstadoDetalleEmpresa) ).ToList();
               
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerDetalleEmpresa() mensaje : {ex}");
                List<VwObtenerComisionesDetalleEmpresaModel> list = new List<VwObtenerComisionesDetalleEmpresaModel>();
                return list;
            }
        }

        public object obtenerComisionDetalle(string usuario, int idComisionDetalle)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerComisionDetalle()  ");
                Logger.LogWarning($" usuario: {usuario} parametros:  idComisionDetalle:{idComisionDetalle}");
                DetalleEmpresaModel newObj = new DetalleEmpresaModel();
                var objComision = contextMulti.VwObtenercomisiones.Where(x => x.IdComisionDetalle == idComisionDetalle).FirstOrDefault();
                if(objComision != null)
                {
                   
                    newObj.idFicla = (int)objComision.IdFicha;
                    newObj.nombreFicha = objComision.Nombre;
                    newObj.ciclo = objComision.Ciclo;
                    newObj.idCiclo = (int)objComision.IdCiclo;
                    var objCli = contextMulti.Fichas.Where(x => x.IdFicha == objComision.IdFicha).Select(p => new { p.IdFicha, p.Avatar }).FirstOrDefault();
                    if(objCli != null){
                        newObj.avatar = objCli.Avatar;
                    }else{
                        newObj.avatar = ""; 
                    }
                    var objNivel = contextMulti.FichaNivelIs.Join(contextMulti.Nivels,
                                                FichaNivelI => FichaNivelI.IdNivel,
                                                Nivel => Nivel.IdNivel,
                                                (FichaNivelI, Nivel) => new
                                                {   nombre = Nivel.Nombre,
                                                    idFicha = FichaNivelI.IdFicha,
                                                    fechaCreacion = FichaNivelI.FechaCreacion,
                                                    habilitado = FichaNivelI.Habilitado,
                                                }).Where(x => x.idFicha == objComision.IdFicha && x.habilitado == true).OrderByDescending(x => x.fechaCreacion).FirstOrDefault();
                    if (objNivel != null){
                        newObj.rango = objNivel.nombre;
                    }else {
                        newObj.rango = "";
                    }
                    var listDEmpresa = obtenerDetalleEmpresa(usuario, idComisionDetalle);
                    newObj.listDetalle = listDEmpresa;

                }
                return newObj;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerComisionDetalle() mensaje : {ex}");
                DetalleEmpresaModel obj = new DetalleEmpresaModel();
                return obj;
            }
        }

        public List<EmpresaOutput> obtenerEmpresas(string usuario)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerEmpresas() ");
                int activo= int.Parse(Environment.GetEnvironmentVariable("ESTADO_EMPRESA_ACTIVO"));
                var ListComisiones = contextMulti.Empresas.Where(x => x.Estado == activo).Select( p => new EmpresaOutput(p.IdEmpresa, p.Nombre)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerEmpresas() mensaje : {ex}");
                List<EmpresaOutput> list = new List<EmpresaOutput>();
                return list;
            }
        }
        public DetalleOutputModel obtenerComisionDetalleEmpresa(string usuario,int  idComisionDetalle)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerEmpresas() ");
                DetalleOutputModel obj = new DetalleOutputModel();
                int activo = int.Parse(Environment.GetEnvironmentVariable("ESTADO_EMPRESA_ACTIVO"));
                var detalle = contextMulti.ComisionDetalleEmpresas.Where(x => x.IdComisionDetalleEmpresa == idComisionDetalle).Select(p => new ComisionDetalleEmpresaOutput(p.IdComisionDetalleEmpresa, p.Monto,p.IdEmpresa, p.MontoAFacturar, p.MontoTotalFacturar)).FirstOrDefault();
                if(detalle != null)
                {
                    obj.idComisionDetalleEmpresa = detalle.idComisionDetalleEmpresa;
                    obj.monto = detalle.monto;
                    obj.idEmpresa = detalle.idEmpresa;
                    obj.montoAFacturar = (decimal)detalle.montoAFacturar;
                    obj.montoTotalFActurar = (decimal)detalle.montoTotalFacturar;
                    var empresas = obtenerEmpresas(usuario);
                    obj.listEmpresa = empresas;
                }
                return obj;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerEmpresas() mensaje : {ex}");
                DetalleOutputModel obj = new DetalleOutputModel();
                return obj;
            }
        }
    }
}
