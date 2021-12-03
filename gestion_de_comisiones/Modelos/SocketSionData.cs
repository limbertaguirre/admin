using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos
{
    public class SocketSionData<T>
    {
        public SocketSionData() { }

        [JsonProperty("room")]
        public string Canal { get; set; }

        [JsonProperty("event")]
        public string Evento { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
