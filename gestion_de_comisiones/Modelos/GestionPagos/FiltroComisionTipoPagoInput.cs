﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagos
{
    public class FiltroComisionTipoPagoInput
    {
        public string usuarioLogin { get; set; }
        public int idCiclo { get; set; }
        public int idTipoPago { get; set; }
    }
}