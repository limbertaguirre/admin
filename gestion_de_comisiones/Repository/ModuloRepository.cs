using gestion_de_comisiones.Modelos.Modulo;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class ModuloRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public List<ModuloModel> ObtenerModulos()
        {
            try
            {
                var objUsuario = contextMulti.Moduloes.Where(x => x.Habilitado == true &&  x.IdModuloPadre != null).Select(p => new ModuloModel(p.IdModulo, p.Nombre, p.Icono,p.Orden, p.Habilitado, p.IdModuloPadre,p.IdUsuario,p.FechaCreacion,p.FechaActualizacion)).ToList();              
                return objUsuario;
            }
            catch (Exception ex)
            {
                List<ModuloModel> modelos = new List<ModuloModel>();
                return modelos;
            }
        }

    }
}
