﻿using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class GpEstadoProrrateoDetalleIncentivo
    {
        public int IdGpEstadoProrrateoDetalleIncentivo { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
