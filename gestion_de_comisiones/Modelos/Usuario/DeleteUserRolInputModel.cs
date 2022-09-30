using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Modelos.Usuario
{
    public class DeleteUserRolInputModel
    {
        public int UsuarioRolId { get; set; }
        public int OperationUserId { get; set; }
        public string OperationLogin { get; set; }
    }
}
