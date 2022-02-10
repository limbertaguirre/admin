using System;
using System.Linq;
using System.Net;
using System.Reflection;
using gestion_de_comisiones.MultinivelModel;
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

        public static async void SetToTableLog(BDMultinivelContext contextBD, Object o, int empresaIdCnx, string descripcion, string nombreSp, int codigoRespSp, int usuarioId, ILogger Logger)
        {
            Logger.LogInformation($"Inicio Utils.SetToTableLog()");
            if (o == null)
            {
                LogPagoComisionTransferenciaPorEmpresa log = new LogPagoComisionTransferenciaPorEmpresa
                {
                    CicloId = 0,
                    ComisionId = 0,
                    ComisionDetalleId = 0,
                    ComisionDetalleEmpresaId = 0,
                    EmpresaId = 0,
                    EmpresaIdCnx = empresaIdCnx,
                    FichaId = 0,
                    Ci = "",
                    NombreCompleto = "",
                    NroCuentaBanco = "",
                    Banco = "",
                    Monto = 0,
                    Descripcion = descripcion,
                    NombreSp = nombreSp,
                    CodigoRespSp = codigoRespSp,
                    UsuarioId = usuarioId
                };
                contextBD.LogPagoComisionTransferenciaPorEmpresas.Add(log);
                await contextBD.SaveChangesAsync();
                Logger.LogInformation($"Fin Utils.SetToTableLog()");
                return;
            }
            if (o is VwObtenerInfoExcelFormatoBanco e)
            {
                LogPagoComisionTransferenciaPorEmpresa log = new LogPagoComisionTransferenciaPorEmpresa
                {
                    CicloId = (int) (e != null?e.IdCiclo:0),
                    ComisionId = (int) (e != null?e.IdComision:0),
                    ComisionDetalleId = (int)(e != null?e.IdComisionesDetalle:0),
                    ComisionDetalleEmpresaId = (int)(e != null?e.IdComisionDetalleEmpresa:0),
                    EmpresaId = (int)(e != null?e.IdEmpresa:0),
                    EmpresaIdCnx = empresaIdCnx,
                    FichaId = (int)(e != null?e.IdFicha:0),
                    Ci = (e != null?e.DocDeIdentidad:""),
                    NombreCompleto = (e != null?e.NombreDeCliente:""),
                    NroCuentaBanco = (e != null?e.NroDeCuenta:""),
                    Banco = (e != null?e.NombreBanco:""),
                    Monto = (decimal) (e != null?e.ImportePorEmpresa:0),
                    Descripcion = descripcion,
                    NombreSp = nombreSp,
                    CodigoRespSp = codigoRespSp,
                    UsuarioId = usuarioId
                };
                contextBD.LogPagoComisionTransferenciaPorEmpresas.Add(log);
                await contextBD.SaveChangesAsync();
                Logger.LogInformation($"Fin Utils.SetToTableLog()");
            }
        }

        public static string GetIPAddress()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily.ToString().ToUpper().Equals("INTERNETWORK")).FirstOrDefault().ToString();
            }
            catch (Exception e)
            {
                return "Error al consultar la ip local: \n" + e.Message;
            }

        }
    } 
}
