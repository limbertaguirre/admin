﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.FormaPago
{
    public class AutorizacionVerificarParam
    {
        public string usuarioLogin { get; set; }
        public int idUsuario { get; set; }
        public int idCiclo { get; set; }
        public int comisionId { get; set; }
    }
}
