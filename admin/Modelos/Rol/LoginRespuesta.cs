using admin.Modelos.Rol.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class LoginRespuesta
    {
        public PerfilModel perfil { get; set; }
        public string token { get; set; }

    }
}
