using gestion_de_comisiones.Modelos.Permiso;
using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class PermisoRepository
    {

        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public List<PermisoResulModel> obtenerPermisos()
        {
            try
            {
                var ListPermiso = contextMulti.Permisoes.Where(x => x.IdPermiso  > 0).Select(p => new PermisoResulModel(p.IdPermiso, p.Permiso1)).ToList();
                return ListPermiso;
            }
            catch (Exception ex)
            {
                List<PermisoResulModel> list = new List<PermisoResulModel>();
                return list;
            }
        }
        public RolPaginaPermisoResulModel ObtenerPermisoPorROl(int IdPagina, int IdPermiso, int IdRol )
        {
            try
            {
                var obj = contextMulti.RolPaginaIs.Join(contextMulti.RolPaginaPermisoIs,
                                                      RolPaginaI => RolPaginaI.IdRolPaginaI,
                                                     // s => new { s.IdRolPaginaI },
                                                     RolPaginaPermisoI => RolPaginaPermisoI.IdRolPagina,
                                                      //h => new { h.IdRolPagina, },
                                                      (RolPaginaI, RolPaginaPermisoI) => new RolPaginaPermisoResulModel
                                                      {
                                                          idPagina= (int)RolPaginaI.IdPagina,
                                                          idRol= (int)RolPaginaI.IdRol,
                                                          idPermiso = (int)RolPaginaPermisoI.IdPermiso,
                                                          habilitado= (bool)RolPaginaPermisoI.Habilitado,                                                          
                                                      }).Where(x => x.idPermiso == IdPermiso &&  x.idRol == IdRol && x.idPagina == IdPagina && x.habilitado == true).First();
                
                return obj;
            }
            catch (Exception ex)
            {
                RolPaginaPermisoResulModel list = new RolPaginaPermisoResulModel();
                return list;
            }
        }
    }
}
