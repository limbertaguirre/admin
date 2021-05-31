using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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
        public List<RolResulModel> obtenerRolesAll()
        {
            try
            {
                var ListRoles = contextMulti.Rols.Where(x => x.Habilitado == true).Select(p => new RolResulModel( p.IdRol, p.Nombre, p.Descripcion, p.Habilitado)).ToList();
                return ListRoles;
            }
            catch (Exception ex)
            {
                List<RolResulModel> list = new List<RolResulModel>();
                return list;
            }
        }
        public RolResulModel obtenerRolXId(int idRol)
        {
            try
            {
                var ListRoles = contextMulti.Rols.Where(x => x.Habilitado == true && x.IdRol == idRol).Select(p => new RolResulModel(p.IdRol, p.Nombre, p.Descripcion, p.Habilitado)).First();
                return ListRoles;
            }
            catch (Exception ex)
            {
                RolResulModel list = new RolResulModel();
                return list;
            }
        }
        public object actualizarRoles(int idRol, string nombreRol, string descripcionRol, List<PaginaResulModelWithPermisos> paginas)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {




                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {                
                return false;
            }
        }
    }
}
