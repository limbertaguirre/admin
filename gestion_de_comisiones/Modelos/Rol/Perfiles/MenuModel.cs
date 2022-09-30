using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol.Perfiles
{
    public class MenuModel
    {
        public int idMenu { get; set; }
        public string titleMenu { get; set; }
        public string iconMenu { get; set; }
        public List<SubMenuModel> listaMenu { get; set; }
    }
}