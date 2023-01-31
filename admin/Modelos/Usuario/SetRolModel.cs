using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Modelos.Usuario
{
    public class SetRolModel
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public int UserOperationId { get; set; }
        public string UserOperationUsername { get; set; }
    }
}
