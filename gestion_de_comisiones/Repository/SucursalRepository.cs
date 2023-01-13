using gestion_de_comisiones.Modelos.Sucursal;
using gestion_de_comisiones.OperacionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class SucursalRepository
    {
        BDOperacionContext contextMulti = new BDOperacionContext();
        public List<SucursalResultModel> obtenerlistadoSucursales()
        {
            var objUsuario = contextMulti.Sucursals.Where(x => x.Habilitado == true).Select(p => new SucursalResultModel(p.IdSucursal, p.Nombre)).ToList();
            return objUsuario;
        }
    }
}
