using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class SucursalRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public object obtenerlistadoSucursales()
        {
            var objUsuario = contextMulti.Sucursals.Where(x => x.Habilitado == true).ToList();
            return objUsuario;
        }
    }
}
