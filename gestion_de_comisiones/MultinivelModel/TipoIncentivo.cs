﻿using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class TipoIncentivo
    {
        public int IdTipoIncentivo { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}