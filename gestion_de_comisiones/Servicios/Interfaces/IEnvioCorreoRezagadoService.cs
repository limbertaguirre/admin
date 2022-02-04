using System;
using System.Collections.Generic;
using gestion_de_comisiones.MultinivelModel;


namespace gestion_de_comisiones.Servicios.Interfaces
{
    public  interface IEnvioCorreoRezagadoService
    {
        public object EnviarCorreoRezagados(List<VwObtenerRezagadosPago> rezagados, string asunto, string username, string serverIp);
        void EnviarCorreoLog(Exception ex, string asunto, string username);
    }
}
