using admin.Modelos;
using admin.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace admin.Servicios
{
    public class NotificacionSocketService : INotificacionSocketService
    {
        private readonly IConfiguration Config;
        private readonly ILogger<NotificacionSocketService> Logger;

        public NotificacionSocketService(IConfiguration config, ILogger<NotificacionSocketService> logger)
        {
            Config = config;
            Logger = logger;
        }

        private async Task<bool> SocketSionTrigger(string canal, string evento, object data)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var socketData = new SocketSionData<object> { Canal = canal, Data = data, Evento = evento };
                    StringContent content = new StringContent(JsonConvert.SerializeObject(socketData), Encoding.UTF8, "application/json");
                    string url = Config.GetValue<string>("SocketSionBaseUrl") + "/send";
                    string apiKey = Config.GetValue<string>("SocketSionApiKey");
                    httpClient.DefaultRequestHeaders.Add("token", apiKey);
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var respuestaApi = JsonConvert.DeserializeObject<SocketSionResponse<string>>(apiResponse);
                        return respuestaApi.Code == 0;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"SocketSionTrigger: catch error f,fin {ex.Message}");
                    return false;
                }
            }
        }

        public async Task<bool> NotificarUnLogin(string usuario, string token)
        {
            string canal = "GESTOR_" + usuario;
            string evento = "unlogin";
            return await SocketSionTrigger(canal, evento, new { token });
        }
    }
}
