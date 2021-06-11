using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Cliente
{
    public class FichaClienteOutPutModel
    {
        public int idFicha { get; set; }
        public string codigo { get; set; }
        public string nombreCompleto { get; set; }
        public string ci { get; set; }
        public bool? tieneCuentaBancaria { get; set; }
        public int? idBanco { get; set; }
        public string nombreBanco { get; set; }
        public string codigoBanco { get; set; }
        public string cuentaBancaria { get; set; }
        public int? estado { get; set; }
        public string avatar { get; set; }

        //public string CorreoElectronico { get; set; }        
        //public DateTime? FechaRegistro { get; set; }        
        //public string TelOficina { get; set; }        
        //public string TelMovil { get; set; }        
        //public string TelFijo { get; set; }        
        //public string Direccion { get; set; }        
        //public DateTime? FechaNacimiento { get; set; }        
        //public string Contrasena { get; set; }        
        //public string Comentario { get; set; }        
        //public bool? FacturaHabilitado { get; set; }
        //public string RazonSocial { get; set; }        
        //public string Nit { get; set; }        


    }
}
