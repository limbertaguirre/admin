using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Utils
{
    public static class MessageHandler
    {
        public static string NotFoundRegister(int id, string nameOfClass)
        {
            return $"No se encontro ningun {nameOfClass} con el Id:{id}";
        }
    }
}
