using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace gestion_de_comisiones.Servicios
{
    public class SeguridadService : ISeguridadService
    {        
        private readonly ILogger<SeguridadService> Logger;
		private readonly IConfiguration Config;	
		string KeyEncript = "b14ca5898a4e4133bbce2ea2315a1916";
		public SeguridadService(IConfiguration config, ILogger<SeguridadService> logger)
        {
            Config = config;
            Logger = logger;
        }

        public SeguridadService()
        {

		}

        public string EncriptarAes(string Cadena)
        {			
			return EncryptString(KeyEncript, Cadena);
        }
		public string DesEncriptarAes(string Cadena)
		{		
			return DecryptString(KeyEncript, Cadena);
		}
		public static string EncryptString(string key, string plainInput)
		{
			byte[] iv = new byte[16];
			byte[] array;
			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(key);
				aes.IV = iv;
				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
						{
							streamWriter.Write(plainInput);
						}

						array = memoryStream.ToArray();
					}
				}
			}

			return Convert.ToBase64String(array);
		}
		public static string DecryptString(string key, string cipherText)
		{
			byte[] iv = new byte[16];
			byte[] buffer = Convert.FromBase64String(cipherText);
			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(key);
				aes.IV = iv;
				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
						{
							return streamReader.ReadToEnd();
						}
					}
				}
			}
		}



	}
}
