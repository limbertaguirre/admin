﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class PagarSionPayInput
    {
        public string UsuarioLogin { get; set; }
        public int idUsuario { get; set; }
        public int idCiclo { get; set; }

    }
}
