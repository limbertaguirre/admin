﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface IClienteService
    {
        public object ObtenerClientes(string usuario);
        public object buscarClientesNombre(string usuario, string criterio);

    }
}
