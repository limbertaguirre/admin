using gestion_de_comisiones.Dtos;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class FormaPagoRespository : IFormaPagoRepository
    {
        private readonly ILogger<FormaPagoRespository> Logger;
        private readonly IConfiguration Config;
        private readonly BDMultinivelContext ContextMulti;

        public FormaPagoRespository(ILogger<FormaPagoRespository> logger, IConfiguration config, BDMultinivelContext contextMulti )
        {
            Logger = logger;
            Config = config;
            this.ContextMulti = contextMulti;
        }

        public object GetCiclos(string usuario, int idEstadoComision)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario}, idEstadoComision: {idEstadoComision} getCiclos");
                var ciclosR = ContextMulti.VwObtenerCiclos.Where(x => x.IdEstadoComision == idEstadoComision).ToList();
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
                Logger.LogWarning($" usuario: {usuario} error catch getCiclos() mensaje : {ex.Message}");
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
                var ListComisiones = ContextMulti.VwObtenercomisiones.Where(x => x.IdCiclo == idCiclo && x.IdEstadoComision == idEstadoComision || ( x.EstadoFacturoId == idEstadoDetalleSifacturo || x.EstadoFacturoId == idEstadoDetalleNoPresentaFactura)).ToList();
                return ListComisiones;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($" usuario: {usuario} error catch getComisiones() mensaje : {ex.Message}");
                List<VwObtenercomisione> list = new List<VwObtenercomisione>();
                return list;
            }
        }



    }
}
