﻿using System;
using gestion_de_comisiones.Modelos.GestionPagos;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IGestionPagosRezagadosService
    {
        public object GetCiclos(string usuario);
        public object GetComisionesDePagos(ComisionesPagosInput param);
    }
}
