using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Usuario
{
    public class UsuarioRolListViewModel
    {
        public int UsuarioRolId { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
    }
}
