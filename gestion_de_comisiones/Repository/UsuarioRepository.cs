﻿using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class UsuarioRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public object ObtenerUsuarioPorId(string usuario)
        {
            try
            {
                var objUsuario = contextMulti.Usuarios.Where(x => x.Usuario1 == usuario).SingleOrDefault();
                return objUsuario;

            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
