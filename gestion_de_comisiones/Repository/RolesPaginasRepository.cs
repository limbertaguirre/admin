using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class RolesPaginasRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        public int AgregarRolPagina(bool habilitado ,int idRol, int idPagina, int usuarioId)
        {
            try
            {
                RolPaginaI objRolPagina = new RolPaginaI();
                objRolPagina.Habilitado = habilitado;
                objRolPagina.IdRol = idRol;
                objRolPagina.IdPagina = idPagina;
                objRolPagina.IdUsuario = usuarioId;
                contextMulti.RolPaginaIs.Add(objRolPagina);
                contextMulti.SaveChanges();
                int id = objRolPagina.IdRolPaginaI;
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}
