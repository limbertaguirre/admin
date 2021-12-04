﻿using gestion_de_comisiones.Modelos.Modulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface ILoginService
    {
        public object VerificarUsuario(string usuario);
        public object cargarPerfilesModulos(int idRol, string usuario, int idUsurio, List<ModuloModel> moduloPadres);
        public object verificarSession(string usuario, string netSessionId, int estado);
    }
}
