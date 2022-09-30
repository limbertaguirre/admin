using gestion_de_comisiones.Modelos.Pagina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol.Perfiles
{
    public class SubMenuModel
    {
        public int idSubMenu { get; set; }
        public string titleSubMenu { get; set; }
        public string iconsSubMenu { get; set;  }
        public List<PaginaOutputModel> listaSubMenu { get; set; }

    }
}
