using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class RolRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public int RegistrarRol( string nombre, string descripcion, int usuarioId )
        {
            try
            {
                Rol objRol = new Rol();
                objRol.Nombre = nombre;
                objRol.Descripcion = descripcion;
                objRol.Habilitado = true;
                objRol.IdUsuario = usuarioId;
             
                contextMulti.Rols.Add(objRol);
                contextMulti.SaveChanges();
                int id = objRol.IdRol;
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}
