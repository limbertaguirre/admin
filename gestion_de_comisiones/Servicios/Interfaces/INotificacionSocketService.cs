using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios.Interfaces
{
    public interface INotificacionSocketService
    {
        Task<bool> NotificarUnLogin(string usuario, string token);
    }
}
