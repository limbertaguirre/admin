using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol.Perfiles
{
    public class PerfilModel
    {
        public List<MenuModel> menus { get; set; }
        public List<PerfilHash> listaHash { get; set; }
        public string usuario { get; set; }
        public int idUsuario { get; set; }

    }
}
