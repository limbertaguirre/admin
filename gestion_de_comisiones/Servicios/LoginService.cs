using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class LoginService
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();

        public object ObtenerUsuario(string usuario)
        {
            try
            {
                var objUsuario = contextMulti.Usuarios.Where(x => x.Usuario1 == usuario).SingleOrDefault();
                return objUsuario;

            }
            catch (Exception ex)
            {
                return ex;
            }
        }


    }
}
