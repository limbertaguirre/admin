﻿using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpComisionEstadoComisionI
    {
        public int IdComisionEstadoComisionI { get; set; }
        public bool? Habilitado { get; set; }
        public int? IdComision { get; set; }
        public int? IdEstadoComision { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}