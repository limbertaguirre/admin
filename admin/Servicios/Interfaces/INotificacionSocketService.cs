using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Servicios.Interfaces
{
    public interface INotificacionSocketService
    {
        Task<bool> NotificarUnLogin(string usuario, string token);
    }
}
