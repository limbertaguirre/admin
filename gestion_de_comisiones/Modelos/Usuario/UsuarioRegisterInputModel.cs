using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Usuario
{
    public class UsuarioRegisterInputModel
    {
        public string userName { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int telefono { get; set; }
        public string corporativo { get; set; }
        public string fechaNacimiento { get; set; }
        public int area { get; set; }
        public int sucursal { get; set; }
    }
}
