using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Servicios.Interfaces
{
    public interface ISeguridadService
    {
        public string EncriptarAes(string Cadena);
        public string DesEncriptarAes(string Cadena);
    }
}
