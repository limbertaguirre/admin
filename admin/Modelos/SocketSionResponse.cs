using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace admin.Modelos
{
    public class SocketSionResponse<T>
    {
        public SocketSionResponse() { }
        [JsonProperty("status")]
        public int Code { get; set; }
        [JsonProperty("mensaje")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
