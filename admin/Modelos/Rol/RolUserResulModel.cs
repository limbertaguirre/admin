using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class RolUserResulModel
    {
        public int idRol { get; set; }
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public bool estadoRol { get; set; }
        public bool estadoRolUsuario { get; set; }
    }
}
