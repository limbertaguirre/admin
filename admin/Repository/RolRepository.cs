using admin.Modelos.Modulo;
using admin.Modelos.Pagina;
using admin.Modelos.Rol;
using admin.Modelos.Rol.Perfiles;
using admin.OperacionModel;
using admin.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace admin.Repository
{
    public class RolRepository : IRolRepository
    {
        BDOperacionContext contextMulti = new BDOperacionContext();
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
            
            using (BDOperacionContext context = new BDOperacionContext())
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

                                    if (rolPaginaOld == null && tieneActivo == true)//---agregar nuevo rolpagina
                                    {
                                        RolPaginaI objRolPa = new RolPaginaI();
                                        objRolPa.Habilitado = true;
                                        objRolPa.IdRol = idRol;
                                        objRolPa.IdPagina = itemPag.idPagina;
                                        objRolPa.IdUsuario = idUsuario;
                                        context.RolPaginaIs.Add(objRolPa);
                                        context.SaveChanges();
                                        int idRolPaginaPK = objRolPa.IdRolPaginaI;
                                        foreach (var itemPer in itemPag.permisos)
                                        {
                                            var permisoOld = context.RolPaginaPermisoIs.Where(x => x.IdRolPagina == idRolPaginaPK && x.IdPermiso == itemPer.idPermiso).FirstOrDefault();
                                            if (itemPer.estado == true && permisoOld == null)
                                            {
                                                //add permiso nuevo
                                                RolPaginaPermisoI objRolPaginaPermiso = new RolPaginaPermisoI();
                                                objRolPaginaPermiso.Habilitado = true;
                                                objRolPaginaPermiso.IdRolPagina = idRolPaginaPK;
                                                objRolPaginaPermiso.IdPermiso = itemPer.idPermiso;
                                                objRolPaginaPermiso.IdUsuario = idUsuario;
                                                context.RolPaginaPermisoIs.Add(objRolPaginaPermiso);
                                                context.SaveChanges();
                                            }
                                            else
                                            {//aqui si existe permiso y si son diferentes de recien se le actualiza el estado

                                                if (permisoOld != null)
                                                {
                                                    if (itemPer.estado != permisoOld.Habilitado)
                                                    {
                                                        permisoOld.Habilitado = itemPer.estado;
                                                        context.SaveChanges();
                                                    }

                                                }
                                            }


                                        }
                                      //-nuevo rola pagina finish---------------------------------------
                                    }
                                    else
                                    {
                                        //---------------------------------------
                                        foreach (var itemPer in itemPag.permisos)
                                        {
                                            var permisoOld = context.RolPaginaPermisoIs.Where(x => x.IdRolPagina == rolPaginaOld.IdRolPaginaI && x.IdPermiso == itemPer.idPermiso).FirstOrDefault();
                                            if (itemPer.estado == true && permisoOld == null)
                                            {
                                                //add permiso nuevo
                                                RolPaginaPermisoI objRolPaginaPermiso = new RolPaginaPermisoI();
                                                objRolPaginaPermiso.Habilitado = true;
                                                objRolPaginaPermiso.IdRolPagina = rolPaginaOld.IdRolPaginaI;
                                                objRolPaginaPermiso.IdPermiso = itemPer.idPermiso;
                                                objRolPaginaPermiso.IdUsuario = idUsuario;
                                                context.RolPaginaPermisoIs.Add(objRolPaginaPermiso);
                                                context.SaveChanges();
                                            }
                                            else
                                            {//aqui si existe permiso y si son diferentes de recien se le actualiza el estado

                                                if (permisoOld != null)
                                                {
                                                    if (itemPer.estado != permisoOld.Habilitado)
                                                    {
                                                        permisoOld.Habilitado = itemPer.estado;
                                                        context.SaveChanges();
                                                    }

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

        public RolUserResulModel obtenerRolxUsuario(int idUsuario)
        {
            try
            {
                Logger.LogInformation($" inicio el  obtenerRolxUsuario() la busqueda de rol por usuario");
                var obj = contextMulti.Rols.Join(contextMulti.UsuariosRoles,
                                                      Rol => Rol.IdRol,
                                                     UsuariosRole => UsuariosRole.IdRol,
                                                      (Rol, UsuriosRole) => new RolUserResulModel
                                                      {
                                                          idRol = Rol.IdRol,
                                                          idUsuario=UsuriosRole.IdUsuario,
                                                          nombre = Rol.Nombre,
                                                          estadoRol = (bool)Rol.Habilitado,
                                                          estadoRolUsuario = (bool)UsuriosRole.Estado,
                                                      }).Where(x => x.estadoRol == true && x.estadoRolUsuario == true && x.idUsuario == idUsuario).FirstOrDefault();
                Logger.LogInformation($" fin busqueda,  se busco los roles de los usuarios iduser: {idUsuario} retorno {JsonConvert.SerializeObject(obj)}");
                return obj;
            }
            catch (Exception ex)
            {               
                return null;
            }
        }
        public List<ModuloModel> obtnerModulosPadres(string usuario)
        {
            try
            {
                Logger.LogInformation($" inicio el  obtnerModulosPadres()");
                List<ModuloModel> modulos = new List<ModuloModel>();
                modulos = contextMulti.Moduloes.Where(x => x.IdModuloPadre == null && x.Habilitado == true).Select( m => new ModuloModel(m.IdModulo, m.Nombre, m.Icono, m.Orden, m.Habilitado, m.IdModuloPadre, m.IdUsuario, m.FechaCreacion, m.FechaActualizacion)).ToList();
                Logger.LogInformation($"fin busqueda modulos padres  {usuario} retorno modulos padres {JsonConvert.SerializeObject(modulos)}");
                return modulos;
            }
            catch (Exception ex)
            {
                List<ModuloModel> lisVacio = new List<ModuloModel>();
                return lisVacio;
            }
        }
        public List<ModuloModel> obtnerSubModulosXIdPadre(string usuario, int IdModulo)
        {
            try
            {
                Logger.LogInformation($" inicio el  obtnerModulosPadres()");
                List<ModuloModel> modulos = new List<ModuloModel>();
                modulos = contextMulti.Moduloes.Where(x => x.IdModuloPadre == IdModulo && x.Habilitado == true).Select(m => new ModuloModel(m.IdModulo, m.Nombre, m.Icono, m.Orden, m.Habilitado, m.IdModuloPadre, m.IdUsuario, m.FechaCreacion, m.FechaActualizacion)).ToList();
                Logger.LogInformation($"fin busqueda modulos padres  {usuario} retorno modulos padres {JsonConvert.SerializeObject(modulos)}");
                return modulos;
            }
            catch (Exception ex)
            {
                List<ModuloModel> lisVacio = new List<ModuloModel>();
                return lisVacio;
            }
        }
        public List<PaginaModel> obtenerPaginasXModulo(string usuario, int IdModulo)
        {
            try
            {
                Logger.LogInformation($" usuario : {usuario} - inicio  el  obtenerPaginasXModulo()");
                List<PaginaModel> modulos = new List<PaginaModel>();
                modulos = contextMulti.Paginas.Where(x => x.IdModulo == IdModulo && x.Habilitado == true).Select(m => new PaginaModel(m.IdPagina, m.Nombre, m.UrlPagina, m.Icono, m.Orden, m.Habilitado, m.IdModulo, m.IdUsuario, m.FechaCreacion, m.FechaActualizacion )).ToList();
                Logger.LogInformation($"usuario: {usuario} - fin busqueda modulos padres   retorno modulos padres {JsonConvert.SerializeObject(modulos)}");
                return modulos;
            }
            catch (Exception ex)
            {
                List<PaginaModel> lisVacio = new List<PaginaModel>();
                return lisVacio;
            }
        }
        public RolPaginaModel obtenerRolPaginaXPagina(string usuario, int IdPagina, int IdRol)
        {
            try
            {
                Logger.LogInformation($" usuario : {usuario} - inicio  el  obtenerRolPaginaXPagina()");
                RolPaginaModel rolPagina = new RolPaginaModel();
                rolPagina = contextMulti.RolPaginaIs.Where(x => x.IdPagina== IdPagina && x.IdRol== IdRol && x.Habilitado == true).Select(r => new RolPaginaModel(r.IdRolPaginaI, r.Habilitado, r.IdRol, r.IdPagina, r.IdUsuario, r.FechaCreacion, r.FechaActualizacion)).FirstOrDefault();
                Logger.LogInformation($"usuario: {usuario} - fin busqueda de rol pagina {JsonConvert.SerializeObject(rolPagina)}");
                return rolPagina;
            }
            catch (Exception ex)
            {
                RolPaginaModel lisVacio = new RolPaginaModel();
                return lisVacio;
            }
        }
        public List<PerfilHash> obtenerPermisoXPagina(string usuario, int idRolPagina, string nombrePagina, string pathPagina)
        {
            try
            {
                Logger.LogInformation($" usuario : {usuario} - inicio  el  obtenerPermisoXPagina()");
                List<RolPermisoHashModel> modulos = new List<RolPermisoHashModel>();
                List<RolPaginaPermisoModel> rolpagina = new List<RolPaginaPermisoModel>();
                List<PerfilHash> listaPerfiles = new List<PerfilHash>();
                rolpagina = contextMulti.RolPaginaPermisoIs.Where(x => x.IdRolPagina == idRolPagina && x.Habilitado == true).Select(m => new RolPaginaPermisoModel(m.IdRolPaginaPermisoI, m.Habilitado, m.IdRolPagina, m.IdPermiso, m.IdUsuario, m.FechaCreacion, m.FechaActualizacion)).ToList();

                foreach(var item in rolpagina)
                {
                    RolPermisoHashModel permiso = contextMulti.RolPaginaPermisoIs.Join(contextMulti.Permisoes,
                            RolPaginaPermisoI => RolPaginaPermisoI.IdPermiso,
                            Permiso => Permiso.IdPermiso,
                            (RolPaginaPermisoI, Permiso) => new RolPermisoHashModel
                            {
                                idRolPaginaPermiso = RolPaginaPermisoI.IdRolPaginaPermisoI,
                                permiso = Permiso.Permiso1,
                                estadoRolPaginaPermiso= (bool)RolPaginaPermisoI.Habilitado,
                            }).Where(x => x.estadoRolPaginaPermiso == true && x.idRolPaginaPermiso == item.IdRolPaginaPermisoI).FirstOrDefault();                 
                    if(permiso != null)
                    {
                        PerfilHash hash = new PerfilHash();
                        hash.IdhashPagina = item.IdRolPaginaPermisoI;
                        hash.permiso = permiso.permiso;
                        hash.Hash = (nombrePagina + permiso.permiso).Replace(" ", String.Empty); 
                        hash.pagina = nombrePagina;
                        listaPerfiles.Add(hash);
                    }
                }
                Logger.LogInformation($"usuario: {usuario} - finde las busqueda de rol pagina + hash :  {JsonConvert.SerializeObject(listaPerfiles)}");
                return listaPerfiles;
            }
            catch (Exception ex)
            {
                List<PerfilHash> lisVacio = new List<PerfilHash>();
                return lisVacio;
            }
        }

    }
}


