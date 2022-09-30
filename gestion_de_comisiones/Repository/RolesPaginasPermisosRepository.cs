using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class RolesPaginasPermisosRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();

        public int AgregarRolPagina(bool habilitado, int idRolPagina, int idPermiso, int usuarioId)
        {
            try
            {
                RolPaginaPermisoI objRolPaginaPermiso = new RolPaginaPermisoI();
                objRolPaginaPermiso.Habilitado = habilitado;
                objRolPaginaPermiso.IdRolPagina = idRolPagina;
                objRolPaginaPermiso.IdPermiso = idPermiso;
                objRolPaginaPermiso.IdUsuario = usuarioId;
                contextMulti.RolPaginaPermisoIs.Add(objRolPaginaPermiso);
                contextMulti.SaveChanges();
                int id = objRolPaginaPermiso.IdRolPaginaPermisoI;
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
