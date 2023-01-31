using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Usuario
{
    public class UsuarioSelectModel
    {
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

    }
}
