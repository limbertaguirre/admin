﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class CerrarPagoParam
    {
        public string usuarioLogin { get; set; }        
        public int usuarioId { get; set; }
        public int idCiclo { get; set; }

    }
}
