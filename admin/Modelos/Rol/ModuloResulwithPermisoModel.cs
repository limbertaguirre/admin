using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class ModuloResulwithPermisoModel
    {
        public int idModulo { get; set; }
        public string nombre { get; set; }
        public List<PaginaResulModelWithPermisos> listmodulos { get; set; }

    }
}
