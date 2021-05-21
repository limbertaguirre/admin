using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Servicios
{
    public class RolService
    {

        public object RegistraRol(RolRegisterInputModel data)
        {

            RolRepository rolRepo = new RolRepository();
               var objRolId = rolRepo.RegistrarRol(data.nombre, data.descripcion, data.idUsuario);
                if(objRolId >= 1) {
                foreach (var item in data.modulos) {
                    int idprueba = 0;
                    foreach(var itempagina in item.paginas)
                    {
                        int idpagina = itempagina.idPagina;
                        string nombrepagina = itempagina.nombrePagina;
                        if(idpagina > 0)
                        {                        
                            RolesPaginasRepository RepoRolPagina = new RolesPaginasRepository();
                            var objRolPaginaId = RepoRolPagina.AgregarRolPagina(true, objRolId, itempagina.idPagina, data.idUsuario);
                            //aqui hacer el forreach para add las cuentas
                            if (objRolPaginaId > 0)
                            {
                                foreach (var itemPermisos in itempagina.permisos)
                                {
                                    int idpermiso = itemPermisos.idPermiso;
                                    RolesPaginasPermisosRepository RepoPermisos = new RolesPaginasPermisosRepository();
                                    var objPermisos = RepoPermisos.AgregarRolPagina(true, objRolPaginaId, idpermiso, data.idUsuario);

                                }
                            }
                        }
                    }
                
                }

                var Result = new GenericDataJson<string> { Code = 1, Message = "El usuario se encuentra ya registrado" };
                return Result;

                }
                else
                {
                    var Result = new GenericDataJson<string> { Code = 1, Message = "El usuario se encuentra ya registrado" };
                    return Result;
                }                     
        }
    }
}
