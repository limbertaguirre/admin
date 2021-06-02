﻿using System;
using System.Collections.Generic;

#nullable disable

namespace gestion_de_comisiones.MultinivelModel
{
    public partial class UsuriosRole
    {
        public int IdUsuariosRoles { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public bool? Estado { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}