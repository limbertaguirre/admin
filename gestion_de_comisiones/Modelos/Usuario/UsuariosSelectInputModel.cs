using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Usuario
{
    public class UsuariosSelectInputModel
    {
        public int IdUsuario { get; set; }
        public string UsuarioLogin { get; set; }
        public int Operation { get; set; }
    }
}
