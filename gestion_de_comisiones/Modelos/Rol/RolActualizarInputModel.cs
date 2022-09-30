using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol
{
    public class RolActualizarInputModel
    {
        public int idRol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int idUsuario { get; set; }
        public List<ModuloResulwithPermisoModel> modulos { get; set; }

    }
}
