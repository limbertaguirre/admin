using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Extensions.Configuration;

namespace gestion_de_comisiones.Servicios
{
    public class EnvioCorreoRezagadoService : IEnvioCorreoRezagadoService
    {
   

        private readonly ILogger<EnvioCorreoRezagadoService> Logger;
        private readonly IConfiguration Config;
        public EnvioCorreoRezagadoService(IConfiguration config, ILogger<EnvioCorreoRezagadoService> logger)
        {
            Config = config;
            Logger = logger;
        }
        private string armarMensajeCorreoRezagado(List<VwObtenerRezagadosPago> rezagados)
        {
            String style = @"<style>
            table.minimalistBlack {
                font - family: 'Lucida Console', Monaco, monospace;
            border: 3px solid #000000;
                width: 100 %;
                text - align: center;
                border - collapse: collapse;
            }
            table.minimalistBlack td, table.minimalistBlack th {
            border: 1px solid #000000;
                padding: 5px 4px;
            }
            table.minimalistBlack tbody td {
                font - size: 13px;
            }
            table.minimalistBlack thead {
            background: #CFCFCF;
                background: -moz - linear - gradient(top, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
                background: -webkit - linear - gradient(top, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
                background: linear - gradient(to bottom, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
                border - bottom: 3px solid #000000;
            }
            table.minimalistBlack thead th {
                font - size: 15px;
                font - weight: bold;
            color: #000000;
                text - align: center;
            }
            table.minimalistBlack tfoot td {
                font - size: 14px;
            }
        </ style > ",
        body = "";

            foreach (VwObtenerRezagadosPago rezagado in rezagados)
            {

                body += $@"<tr>
                <td>{rezagado.NombreDeCliente}</td>
                <td>{rezagado.DocDeIdentidad}</td>  
                <td>{rezagado.NombreBanco}</td>                
                <td>{rezagado.NroDeCuenta}</td>
                <td>{rezagado.ImportePorEmpresa}</td>
                <td>{rezagado.Empresa}</td>
                <td>{rezagado.Glosa}</td>
                
                </tr>";
            }

            String html = $@"<p>estos son los freelancers rechazados
            </p> 

                  
        </br> 
        
        <table class='minimalistBlack'>
            <thead>
                <tr>                    
                    <th>NOMBRE</th>
                    <th>CEDULA IDENTIDAD</th>
                    <th>BANCO</th>
                    <th>NUMERO DE CUENTA BANCARIA</th>
                    <th>MONTO POR EMPRESA</th>
                    <th>EMPRESA</th>
                    <th>CICLO</th>                    
                </tr>
            </thead>
            <tbody>
                ${ body}
            </tbody>
        </table>
        </br>
            <p>
            Este es un correo generado automáticamente no responda a este.
            </p>
        ";
            return style + html;
        }
        private bool envioCorreoRezagados(string mensaje, string asunto, List<string> destinatarios)
        {
            Logger.LogInformation($" ingreso a la funcion enviarCorreoRezagados");
            MailMessage correo = new MailMessage();
            string sender = Config.GetValue<string>("ConfiguracionCorreo:support:sender:from");
            string host = Config.GetValue<string>("ConfiguracionCorreo:support:config:host");
            int puerto = Config.GetValue<int>("ConfiguracionCorreo:support:config:port");
            string user = Config.GetValue<string>("ConfiguracionCorreo:support:config:auth:user");
            string password = Config.GetValue<string>("ConfiguracionCorreo:support:config:auth:pass");
            correo.From = new MailAddress(sender, "Kyocode", System.Text.Encoding.UTF8);//Correo de salida
            foreach (string elem in destinatarios)
            {
                correo.To.Add(elem);
            }           
            correo.Subject = asunto; 
            correo.Body = mensaje; 
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = host; //Host del servidor de correo //
            smtp.Port = puerto; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential(user, password);//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);

            return true;
        }

        public object EnviarCorreoRezagados(List<VwObtenerRezagadosPago> rezagados, string asunto)
        {
            if (rezagados.Count > 0)
            {
                string mensaje = armarMensajeCorreoRezagado(rezagados);
                //string asunto = "Lista de Rechazados en ciclo " + rezagados.ElementAt(0).Glosa + " Por Empresa " + rezagados.ElementAt(0).Empresa;
                List<String> destinatarios = new List<string>();
                string destinatario = Config.GetValue<string>("DestinatariosRezagados");
                destinatarios.Add(destinatario);
                return envioCorreoRezagados(mensaje, asunto, destinatarios);
            }
            else
            {
                return false;
            }

        }
    }
}
