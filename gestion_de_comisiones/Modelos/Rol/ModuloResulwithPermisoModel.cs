﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Rol
{
    public class ModuloResulwithPermisoModel
    {
        public int idModulo { get; set; }
        public string nombre { get; set; }
        public List<PaginaResulModelWithPermisos> listmodulos { get; set; }

    }
}