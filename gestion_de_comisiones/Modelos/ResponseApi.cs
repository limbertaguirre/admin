using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos
{
    public enum enumResponseApi
    {
        success,
        error
    }
    public class ResponseApi<T>:Result<T>
    {
        public ResponseApi(T data)
        {
            Code = (int)enumResponseApi.success;
            Message = "Ok";
            Data = data;
        }

        //En caso de excepcion
        public ResponseApi(string message)
        {
            Code = (int)enumResponseApi.error;
            Message = message;
        }
    }
}
