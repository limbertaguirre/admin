using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.GestionPagosRezagados
{
    public class PagoRezagadoInput
    {
        [JsonPropertyName("usuarioLogin")]
        public string UsuarioLogin { get; set; }
        [JsonPropertyName("idUsuario")]
        public int IdUsuario { get; set; }
        [JsonPropertyName("idComsion")]
        public int IdComision { get; set; }
    }
}
