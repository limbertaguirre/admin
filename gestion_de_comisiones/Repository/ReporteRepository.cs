using System;
using System.Globalization;
using gestion_de_comisiones.MultinivelModel;
using System.Collections.Generic;
using gestion_de_comisiones.Modelos.Reporte;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using gestion_de_comisiones.Repository.Interfaces;
using System.Data;

namespace gestion_de_comisiones.Repository
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly BDMultinivelContext contextMulti;

        public ReporteRepository(BDMultinivelContext multinivelContext)
        {
            this.contextMulti = multinivelContext;
        }

        public List<Ficha> listaFichaClientes(string query)
        {
            string[] stringSplitted = query.Split(" ");

            var predicate = PredicateBuilder.False<Ficha>();
            foreach (string queryString in stringSplitted)
            {
                predicate = predicate.Or(p => p.Ci.Contains(queryString));
                predicate = predicate.Or(p => p.Nombres.Contains(queryString));
                predicate = predicate.Or(p => p.Apellidos.Contains(queryString));
            }
                
            var result = contextMulti.Fichas.AsQueryable()
               .Where(predicate)
               .Take(50)
               .ToList();

            return result;
        }

        public List<ReporteCicloModel> listaReporteCiclos(int idCiclo, int mode)
        {
            using (var command = contextMulti.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"select * from ffReportePorCiclo({idCiclo},{mode}) order by monto_neto desc";
                command.CommandType = System.Data.CommandType.Text;
                contextMulti.Database.OpenConnection();
                using(var resultQuery = command.ExecuteReader())
                {
                    List<ReporteCicloModel> entities = new List<ReporteCicloModel>();
                    if (resultQuery.HasRows)
                    {
                        while (resultQuery.Read())
                        {
                            entities.Add(new ReporteCicloModel(
                                Convert.ToInt32(resultQuery["id_comision_detalle"]),
                                resultQuery["nombres"].ToString(),
                                resultQuery["apellidos"].ToString(),
                                resultQuery["ci"].ToString(),
                                float.Parse(resultQuery["monto_neto"].ToString()),
                                resultQuery["nro_cuenta"].ToString(),
                                resultQuery["cuenta_bancaria"].ToString(),
                                resultQuery["tipo_pago"].ToString(),
                                Convert.ToInt32(resultQuery["id_tipo_pago"])
                                ));
                        }
                    }
                    return entities;
                }
            }
            //    var result = contextMulti.Database.Sql("SELECT * FROM ffObtenerReporteCiclo(@IDCICLO, @MODE)",).OrderByDescending(t => t.montoNeto).ToList();
            //return result;
        }

        public List<ReporteDetalleCicloModel> listaReporteDetalleCiclo(int idComisionDetalle)
        {
            using (var command = contextMulti.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"select * from ffReporteDetalleCiclo({idComisionDetalle}) order by monto_neto desc";
                command.CommandType = System.Data.CommandType.Text;
                contextMulti.Database.OpenConnection();
                using (var resultQuery = command.ExecuteReader())
                {
                    List<ReporteDetalleCicloModel> entities = new List<ReporteDetalleCicloModel>();
                    if (resultQuery.HasRows)
                    {
                        while (resultQuery.Read())
                        {
                            entities.Add(new ReporteDetalleCicloModel(
                                Convert.ToInt32(resultQuery["id_comision_detalle_empresa"]),
                                float.Parse(resultQuery["monto_neto"].ToString()),
                                resultQuery["nombre_empresa"].ToString(),
                                resultQuery["tipo_comision"].ToString(),
                                Convert.ToInt32(resultQuery["id_tipo_comision"])
                            ));
                        }
                    }
                    return entities;
                }
            }      
        }

        public List<ReporteFreelancerModel> listaReportePorFreelancer(int idFicha)
        {
            using (var command = contextMulti.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"select * from ffReporteFreelancer({idFicha}) order by monto_neto desc";
                command.CommandType = System.Data.CommandType.Text;
                contextMulti.Database.OpenConnection();
                using (var resultQuery = command.ExecuteReader())
                {
                    List<ReporteFreelancerModel> entities = new List<ReporteFreelancerModel>();
                    if (resultQuery.HasRows)
                    {
                        while (resultQuery.Read())
                        {
                            entities.Add(new ReporteFreelancerModel(
                                Convert.ToInt32(resultQuery["id_comision_detalle"]),
                                resultQuery["ciclo"].ToString(),
                                resultQuery["tipo_pago"].ToString(),
                                float.Parse(resultQuery["monto_neto"].ToString()),
                                resultQuery["nro_cuenta"].ToString(),
                                resultQuery["cuenta_banco"].ToString()
                            ));
                        }
                    }
                    return entities;
                }
            }
        }
    }
}
