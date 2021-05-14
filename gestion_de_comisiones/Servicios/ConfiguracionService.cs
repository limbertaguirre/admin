using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Area;
using gestion_de_comisiones.Models;
using gestion_de_comisiones.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class ConfiguracionService
    {
        public object ObtenerListAreas()
        {
            Result<List<AreaResultModel>> resultado;
            AreaRepository repoArea = new AreaRepository();
           

            var areas = repoArea.obtenerlistadoAreas();
            return this.ReturnResult(0, "OK", areas);
           // return resultado;
        }
        public object ObtenerListSucursales()
        {
            SucursalRepository repoSucursal = new SucursalRepository();
            var Sucursales = repoSucursal.obtenerlistadoSucursales();
            var Result = new GenericListJson<object> { Code = 1, Message = "NO SE REGISTRO EL CLIENTE"};
            return Sucursales;
        }
        public List<AreaResultModel> ConvertAreaAListModel(dynamic listaAreas)
        {
            List<AreaResultModel> list = new List<AreaResultModel>();

            if (listaAreas != null)
            {
                foreach (var item in listaAreas)
                {
                    AreaResultModel objaarea = new AreaResultModel();
                    var dd= item;
                    objaarea.idArea = item.idArea;
                    objaarea.nombre = item.nombre;

                    list.Add(objaarea);
                }
            }
            return list;
        }

        protected Result<T> ReturnResult<T>(int code, string message, T data)
        {
            Result<T> result = new Result<T>();
            result.Code = code;
            result.Message = message;
            result.Data = data;

            return result;
        }


    }
}
