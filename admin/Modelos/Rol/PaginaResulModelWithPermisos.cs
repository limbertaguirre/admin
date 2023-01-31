using admin.Modelos.Permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class PaginaResulModelWithPermisos
    {
        [JsonPropertyName("id_pagina")]
        public int idPagina { get; set; }
        public string nombre { get; set; }
        public List<RolPermiso> permisos { get; set; }  
    }
}
