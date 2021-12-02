using gestion_de_comisiones.Modelos.Rol.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol
{
    public class LoginRespuesta
    {
        public PerfilModel perfil { get; set; }
        public string token { get; set; }

    }
}
