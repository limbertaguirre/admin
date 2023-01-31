using admin.Modelos.Modulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Servicios.Interfaces
{
    public interface ILoginService
    {
        public Task<object> VerificarUsuarioAsync(string usuario);
        public object cargarPerfilesModulos(int idRol, string usuario, int idUsurio, List<ModuloModel> moduloPadres);
        public object verificarSession(string usuario, string netSessionId, int estado);
    }
}
