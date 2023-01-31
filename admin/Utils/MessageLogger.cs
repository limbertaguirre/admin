using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Utils
{
    public static class MessageLogger
    {
        public static string FunctionIn(string usuarioLoged, string functionName)
        {
            return $"Usuario : {usuarioLoged}, Inicio: {functionName}";
        }

        public static string FunctionOut(string usuarioLoged, string functionName)
        {
            return $"Usuario : {usuarioLoged}, Finalizo: {functionName}";
        }

        public static string ExcepcionMessage(string usuarioLoged, string functionName)
        {
            return $"Usuario : {usuarioLoged}, Excepcion: {functionName}";
        }
    }
}
