﻿using gestion_de_comisiones.Modelos.GestionPagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IGestionPagosService
    {
        public object GetCiclos(string usuario);
        public object GetComisionesDePagos(ComisionesPagosInput param);
        public object GetFormaPagosDisponibles(FiltroFormaPagosInput param);
    }
}
