using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace admin.Modelos.Rol
{
    public class RolPermiso
    {
        [JsonPropertyName("id_permiso")]
        public int idPermiso { get; set; }
        [JsonPropertyName("permiso")]
        public string permiso1 { get; set; }
        public bool estado { get; set; }
    }
}
