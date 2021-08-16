using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.AplicacionDetalleProducto;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class AplicacionesRepository : IAplicacionesRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<AplicacionesRepository> Logger;
        public AplicacionesRepository(ILogger<AplicacionesRepository> logger)
        {
            Logger = logger;
        }

        public object GetCiclos(string usuario, int idEstadoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision}, => AplicacionesRepository|getCiclos");             
                var ciclosR = contextMulti.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision).ToList();
                List<CicloDto> ciclos = new List<CicloDto>();
                foreach(var c in ciclosR)
                {
                    Logger.LogInformation($" usuario: {usuario} ciclosR => IdCiclo: {c.IdCiclo} Nombre: {c.Nombre} Estado: {c.Estado}");
                    ciclos.Add(new CicloDto(c.IdCiclo, c.Nombre));
                }
                return ciclos;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() mensaje : {ex}");
                List<Ciclo> lis = new List<Ciclo>();
                return lis;
            }
        }

        public object GetComisiones(string usuario, int idCiclo, int idEstadoComision, int idEstadoDetalleSifacturo, int idEstadoDetalleNoPresentaFactura)
        {
            try
            {
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerComisionesPendientes() ");
                Logger.LogWarning($" usuario: {usuario} parametros: idciclo:{idCiclo} , idEstado:{idEstadoComision}");
                var ListComisiones = contextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision && x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getComisiones() mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        public object ListarFreelancerYAplicacionesProductos(DetalleAplicacionesFichaInputModel param)
        {
            try
            {
                DetalleAplicacionOutputModel objDetalle = new DetalleAplicacionOutputModel();
                var objComision = contextMulti.VwObtenercomisiones.Where(x => x.IdComisionDetalle == param.idComisionDetalle).FirstOrDefault();
                if(objComision != null)
                {
                    objDetalle.idFicla = (int)objComision.IdFicha;
                    objDetalle.nombreFicha = objComision.Nombre;
                    objDetalle.ciclo = objComision.Ciclo;
                    objDetalle.idCiclo = (int)objComision.IdCiclo;
                    var objCli = contextMulti.Fichas.Where(x => x.IdFicha == objComision.IdFicha).Select(p => new { p.IdFicha, p.Avatar }).FirstOrDefault();
                    if (objCli != null)
                    {
                        objDetalle.avatar = objCli.Avatar;
                    }
                    else
                    {
                        objDetalle.avatar = "";
                    }
                    var objNivel = contextMulti.FichaNivelIs.Join(contextMulti.Nivels,
                                                FichaNivelI => FichaNivelI.IdNivel,
                                                Nivel => Nivel.IdNivel,
                                                (FichaNivelI, Nivel) => new
                                                {
                                                    nombre = Nivel.Nombre,
                                                    idFicha = FichaNivelI.IdFicha,
                                                    fechaCreacion = FichaNivelI.FechaCreacion,
                                                    habilitado = FichaNivelI.Habilitado,
                                                }).Where(x => x.idFicha == objComision.IdFicha && x.habilitado == true).OrderByDescending(x => x.fechaCreacion).FirstOrDefault();
                    if (objNivel != null)
                    {
                        objDetalle.rango = objNivel.nombre;
                    }
                    else
                    {
                        objDetalle.rango = "";
                    }
                    objDetalle.listAplicaciones = obtenerDetalleAplicacionXId(param.usuarioLogin, param.idComisionDetalle);

                }
               
                return objDetalle;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {param.usuarioLogin} error catch get detalle aplicaciones () mensaje : {ex}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }
        private List<WwObtenerComisionesDetalleAplicacionesModel> obtenerDetalleAplicacionXId(string usuario, int idComisionDetalle)
        {
            try
            {
                Logger.LogWarning($" usuario: {usuario} inicio el repository obtenerDetalleAplicacionXId()  idComisionDetalle: {idComisionDetalle} ");
                Logger.LogWarning($" usuario: {usuario} parametros: idComisionDetalle:{idComisionDetalle} ");
                var ListComisiones = contextMulti.VwObtenerComisionesDetalleAplicaciones.Where(x => x.IdComisionDetalle == idComisionDetalle ).Select(p => new WwObtenerComisionesDetalleAplicacionesModel(p.IdAplicacionDetalleProducto, p.IdComisionDetalle, p.Descripcion, p.Monto, p.Cantidad, p.Subtotal, p.IdProyecto, p.IdEmpresa, p.NombreEmpresa, p.CodigoProducto)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch obtenerDetalleAplicacionXId() mensaje : {ex}");
                List<WwObtenerComisionesDetalleAplicacionesModel> list = new List<WwObtenerComisionesDetalleAplicacionesModel>();
                return list;
            }
        }

    }

}
