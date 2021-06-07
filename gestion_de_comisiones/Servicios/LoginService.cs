using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using Newtonsoft.Json;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Modelos.Modulo;
using gestion_de_comisiones.Modelos.Pagina;
using gestion_de_comisiones.Modelos.Rol.Perfiles;

namespace gestion_de_comisiones.Servicios
{
    public class LoginService : ILoginService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<LoginService> Logger;
        public LoginService(ILogger<LoginService> logger, IRolRepository rolRepository)
        {
            Logger = logger;
            RolRepository = rolRepository;
        }
        public IRolRepository RolRepository { get; }

        public object VerificarUsuario(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario : {usuario} inicio la funcionalidad VerificarUsuario()");
                UsuarioRepository UserRepos = new UsuarioRepository();
                var objetoo = UserRepos.ObtenerUsuarioPorId(usuario);
                if (objetoo != null)
                {
                    //-----------------------------------------------------------------------------------------------------
                    var rol = RolRepository.obtenerRolxUsuario(objetoo.IdUsuario);
                    if(rol != null)
                    {
                        var nn = rol.nombre;
                        var listModulePadre = RolRepository.obtnerModulosPadres(usuario);
                        var perfil = this.cargarPerfilesModulos(rol.idRol, usuario, listModulePadre);

                        var Result = Respuesta.ReturnResultdo(0, "roles obtenidos", perfil);
                        return Result;

                    }
                    else
                    {
                        //-------------------------------------------------------------------------------------------------------
                        // var resulte = Respuesta.ReturnResultdo(1, "No exite rol", "");
                        PerfilModel objPerfil = new PerfilModel();
                        Logger.LogInformation($" usuario : {usuario} repuesta obtener usuario: {JsonConvert.SerializeObject(objetoo)}");
                        var Result = Respuesta.ReturnResultdo(0, "Aun no tiene rol asignado.", objPerfil);
                        return Result;
                    }
                   
                }
                else
                {
                    Logger.LogWarning($" usuario : {usuario} no se encontro el registro");
                    var Result = new GenericDataJson<string> { Code = 2, Message = "El usaurio no se encuentra registrado"};
                    return Result;
                }

            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario : {usuario} catch error f,fin {ex.Message}");
                var Result = new GenericDataJson<string> { Code = 2, Message = "Intente mas tarde" };
                return Result;
            }
        }
        public object cargarPerfilesModulos(int idRol, string usuario, List<ModuloModel> moduloPadres)
        {
            try {
                    PerfilModel objPerfil = new PerfilModel();
                    List<MenuModel> ListMenu = new List<MenuModel>();
                    foreach(var item in moduloPadres)
                    {
                    var listModulohijo = RolRepository.obtnerSubModulosXIdPadre(usuario, item.IdModulo);
                    List<SubMenuModel> ListSubMenu = new List<SubMenuModel>();
                    foreach(var itemPadre in listModulohijo)
                    {
                        List<PaginaModel> oldPaginas = RolRepository.obtenerPaginasXModulo(usuario, itemPadre.IdModulo);
                        List<PaginaOutputModel> ListPages = new List<PaginaOutputModel>();
                        foreach(var itempag in oldPaginas)
                        {
                            var tienePagina = RolRepository.obtenerRolPaginaXPagina(usuario, itempag.IdPagina, idRol);
                            if(tienePagina != null)
                            {   //add paginas q tiene
                                PaginaOutputModel page = new PaginaOutputModel();
                                page.idPage = itempag.IdPagina;
                                page.title = itempag.Nombre;
                                page.descripion= itempag.Nombre;
                                page.namePage = itempag.Nombre;
                                page.path = itempag.UrlPagina;
                                page.icon = itempag.Icono;
                                ListPages.Add(page);
                            }
                        }
                        if(ListPages.Count > 0)
                        {//add submodulo
                            SubMenuModel submodulo = new SubMenuModel();
                            submodulo.idSubMenu = itemPadre.IdModulo;
                            submodulo.titleSubMenu = itemPadre.Nombre;
                            submodulo.iconsSubMenu = itemPadre.Icono;
                            submodulo.listaSubMenu = ListPages;
                            ListSubMenu.Add(submodulo);
                        }

                    }
                    if(ListSubMenu.Count > 0)
                    {
                        //add menu
                        MenuModel Menu = new MenuModel();
                        Menu.idMenu = item.IdModulo;
                        Menu.titleMenu = item.Nombre;
                        Menu.iconMenu = item.Icono;
                        Menu.listaMenu = ListSubMenu;
                        ListMenu.Add(Menu);

                    }

                    }
                    objPerfil.menus = ListMenu;
                    //objPerfil.PermisoPaginas= listanoexite
                    return objPerfil;
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario : {usuario} catch error f,fin {ex.Message}");
                PerfilModel objPerfil = new PerfilModel();
                return objPerfil;
            }

         }


    }
}
