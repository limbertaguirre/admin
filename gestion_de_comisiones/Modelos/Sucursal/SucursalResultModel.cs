﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Sucursal
{
    public class SucursalResultModel
    {
        public SucursalResultModel(int idSucursal, string nombre)
        {
            this.IdSucursal = idSucursal;
            this.nombre = nombre;
        }

        public string nombre { get; set; }
        public int IdSucursal { get; }
    }
}
