using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Cliente
{
    public class ClienteOutputModel
    {
      
        public ClienteOutputModel()
        {
        }

        public ClienteOutputModel(int idFicha, int codigo, string nombreCompleto, string ci, bool tieneCuentaBancaria, int idBanco, string nombreBanco, string codigoBanco, string cuentaBancaria, int estado, string avatar, string nivel)
        {
            this.idFicha = idFicha;
            this.codigo = codigo;
            this.nombreCompleto = nombreCompleto;
            this.ci = ci;
            this.tieneCuentaBancaria = tieneCuentaBancaria;
            this.idBanco = idBanco;
            this.nombreBanco = nombreBanco;
            this.codigoBanco = codigoBanco;
            this.cuentaBancaria = cuentaBancaria;
            this.estado = estado;
            this.avatar = avatar;
            this.nivel = nivel;
        }

        public int idFicha { get; set; }     
        public int codigo { get; set; }        
        public string nombreCompleto { get; set; }         
        public string ci { get; set; }
        public bool? tieneCuentaBancaria { get; set; }
        public int? idBanco { get; set; }
        public string nombreBanco { get; set; }
        public string codigoBanco { get; set; }
        public string cuentaBancaria { get; set; }
        public int? estado { get; set; }
        public string avatar { get; set; }
        public string nivel { get; set; }


    }
}
