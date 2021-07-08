using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Area;
using gestion_de_comisiones.Modelos.Sucursal;
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
        }
        public Result<List<SucursalResultModel>> ObtenerListSucursales()
        {
            Result<List<SucursalResultModel>> resultado;
            SucursalRepository repoSucursal = new SucursalRepository();
            var Sucursales = repoSucursal.obtenerlistadoSucursales();
            resultado = this.ReturnResult(0, "OK", Sucursales);
            return resultado;
        }

        protected Result<T> ReturnResult<T>(int code, string message, T data)
        {
            Result<T> result = new Result<T>();
            result.Code = code;
            result.Message = message;
            result.Data = data;
            return result;
        }
        public Result<T> ReturnResultdo<T>(int code, string message, T data)
        {
            Result<T> result = new Result<T>();
            result.Code = code;
            result.Message = message;
            result.Data = data;
            return result;
        }


    }
}
