using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace gestion_de_comisiones.Utils
{
    public static class Utils
    {
        public static void ShowValueFields(Object o, ILogger Logger) {

            Type t = o.GetType();
            PropertyInfo[] property_infos = t.GetProperties(
                    BindingFlags.FlattenHierarchy |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Static);
            string s = "Parametros de entrada => ";            
            for (int i = 0; i < property_infos.Length; i++)
            {
                s += $"{property_infos[i].Name}: {property_infos[i].GetValue(o)}, ";
            }
            Logger.LogInformation(s[0..^2]);
        }
    }
}
