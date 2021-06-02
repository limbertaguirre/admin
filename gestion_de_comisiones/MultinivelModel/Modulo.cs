﻿using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class Modulo
    {
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public int? Orden { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdModuloPadre { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}