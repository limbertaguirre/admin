﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IAplicacionesService
    {
        public object GetCiclos(string usuario);
        public object GetAplicacionesPendientes(string usuario, int idCiclo);

    }
}
