﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Pagina
{
    public class PaginaResulModel
    {
        [JsonPropertyName("id_pagina")]
        public int idPagina { get; set; }
        public string nombre { get; set; }

    }
}
