using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Usuario
{
    public class SetRolModel
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public int UserOperationId { get; set; }
        public int UserOperationUsername { get; set; }
    }
}
