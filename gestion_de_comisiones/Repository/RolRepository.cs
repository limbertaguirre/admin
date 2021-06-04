using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace gestion_de_comisiones.Repository
{
    public class RolRepository : IRolRepository
    {
        BDMultinivelContext contextMulti = new BDMultinivelContext();
        private readonly ILogger<RolRepository> Logger;
        public RolRepository(ILogger<RolRepository> logger)
        {
            Logger = logger;
        }

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
        public List<RolResulModel> obtenerRolesAll(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario: {usuario} inicio el obtenerRolesAll");

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
        //public RolResulModel obtenerRolPagina(int idRol)
        //{
        //    try
        //    {
        //        var ListRoles = contextMulti.RolPaginaIs.Where(x => x.Habilitado == true && x.IdRol == idRol).Select(p => new RolPaginaI(p.IdRol, p.Nombre, p.Descripcion, p.Habilitado)).First();
        //        return ListRoles;
        //    }
        //    catch (Exception ex)
        //    {
        //        RolResulModel list = new RolResulModel();
        //        return list;
        //    }
        //}
        public bool actualizarRoles(int idRol, string nombreRol, string descripcionRol, List<PaginaResulModelWithPermisos> paginas, int idUsuario)
        {
            
            using (BDMultinivelContext context = new BDMultinivelContext())
            {
                using(var dbcontextTransaction= context.Database.BeginTransaction())
                {
            
                    try
                    {
                         var rolOld = obtenerRolXId(idRol);          
                        if(rolOld.IdRol != 0)
                        {
                            var rolNew = context.Rols.Where(x => x.IdRol == idRol ).First();
                            rolNew.Nombre = nombreRol;
                            rolNew.Descripcion = descripcionRol;
                            context.SaveChanges();
                            //actualizar rol here
                            foreach (var itemPag in paginas)
                            {
                                var tieneActivo = validarPaginaTienePermisoActivos(itemPag.permisos);
                                var rolPaginaOld = context.RolPaginaIs.Where(x => x.IdRol == idRol && x.IdPagina == itemPag.idPagina).FirstOrDefault();
                                
                                if (tieneActivo == false && rolPaginaOld == null)//aqui validamos e exita la pagina y tenga permisos seleccionados caso contrario no hace nada 
                                { 

                                }else {
                                    
                                        foreach(var itemPer in itemPag.permisos)
                                        {
                                            var permisoOld = context.RolPaginaPermisoIs.Where(x => x.IdRolPagina == rolPaginaOld.IdRolPaginaI && x.IdPermiso == itemPer.idPermiso).FirstOrDefault();
                                            if(itemPer.estado == true && permisoOld == null)
                                            {
                                                //add permiso nuevo
                                                RolPaginaPermisoI objRolPaginaPermiso = new RolPaginaPermisoI();
                                                objRolPaginaPermiso.Habilitado = true;
                                                objRolPaginaPermiso.IdRolPagina = rolPaginaOld.IdRolPaginaI;
                                                objRolPaginaPermiso.IdPermiso = itemPer.idPermiso;
                                                objRolPaginaPermiso.IdUsuario = idUsuario;
                                                context.RolPaginaPermisoIs.Add(objRolPaginaPermiso);
                                                context.SaveChanges();
                                            } else{//aqui si existe permiso y si son diferentes de recien se le actualiza el estado
                                              
                                                if ( permisoOld != null)
                                                {
                                                    if (itemPer.estado != permisoOld.Habilitado)
                                                    {
                                                        permisoOld.Habilitado = itemPer.estado;
                                                        context.SaveChanges();
                                                    }

                                                }                                       
                                            }
                                        }

                                    

                                    if (tieneActivo == false && rolPaginaOld != null)
                                    {
                                        rolPaginaOld.Habilitado = false;
                                    }
                                    if (tieneActivo == true && rolPaginaOld != null)
                                    {
                                        rolPaginaOld.Habilitado = true;
                                    }
                                    context.SaveChanges();


                                }

                            }
                         
                            dbcontextTransaction.Commit();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                       
                       
                }
                catch (Exception ex)
                {
                    dbcontextTransaction.Rollback();
                    return false;
                }

            }

            }
                
          
        }

        public bool validarPaginaTienePermisoActivos(List<RolPermiso> permisos)
        {
            bool estado = false;
            foreach (var item in permisos)
            {
                if(item.estado == true)
                {
                    estado = true;
                }

            }
             return estado;
        }
    }
}
