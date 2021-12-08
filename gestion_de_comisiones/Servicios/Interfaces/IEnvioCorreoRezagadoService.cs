using System;
using System.Collections.Generic;
using gestion_de_comisiones.MultinivelModel;


namespace gestion_de_comisiones.Servicios.Interfaces
{
    public  interface IEnvioCorreoRezagadoService
    {
        public object EnviarCorreoRezagados(List<VwObtenerRezagadosPago> rezagados);
    }
}
