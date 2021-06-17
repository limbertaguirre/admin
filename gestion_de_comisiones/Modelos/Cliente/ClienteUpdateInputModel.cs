using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Cliente
{
    public class ClienteUpdateInputModel
    {
        public string usuarioNameLogueado { get; set; }
        public int usuarioIDLogueado { get; set; }

        public bool nuevoAvatar { get; set; }
        public string avatar { get; set; }

        public int codigo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string ci { get; set; }
        public int telOficina { get; set; }
        public int telMovil { get; set; }
        public int telFijo { get; set; }
        public string direccion { get; set; }

        public int idCiudad { get; set; }
        public int idPais { get; set; }
        public string correoElectronico { get; set; }
        public string fechaNacimiento { get; set; }
        public string codigoPatrocinador { get; set; }
        public string nombrePatrocinador { get; set; }
        public int idNivel { get; set; }
        public string comentario { get; set; }

        public bool tieneCuenta { get; set; }
        public int idBanco { get; set; }
        public string cuentaBancaria { get; set; }

        public bool tieneFactura { get; set; }
        public string razonSocial { get; set; }
        public string nit { get; set; }

        public bool tieneBaja { get; set; }
        public int idFichaTipoBaja { get; set; }
        public int idTipoBaja { get; set; }
        public string fechaBaja { get; set; }
        public string motivoBaja { get; set; }



    }
}
