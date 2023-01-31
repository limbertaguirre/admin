using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class RolRegisterInputModel
    {

  
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int idUsuario { get; set; }
        public List<ModuloInputModel> modulos { get; set; }

    }
}
