using admin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Modelos;
using admin.Modelos.Usuario;
using Newtonsoft.Json;
using admin.Servicios.Interfaces;
using Microsoft.Extensions.Logging;
using admin.Repository.Interfaces;
using admin.Modelos.Modulo;
using admin.Modelos.Pagina;
using admin.Modelos.Rol.Perfiles;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using admin.Modelos.Rol;

namespace admin.Servicios
{
    public class LoginService : ILoginService
    {
        ConfiguracionService Respuesta = new ConfiguracionService();
        private readonly ILogger<LoginService> Logger;
        private readonly IConfiguration Config;
        private readonly INotificacionSocketService NotificacionSocketService;

        public LoginService(ILogger<LoginService> logger, IRolRepository rolRepository, IConfiguration config, IUsuarioRepository usuarioRepository,
            INotificacionSocketService notificacionSocketService)
        {
            Logger = logger;
            RolRepository = rolRepository;
            Config = config;
            UsuarioRepository = usuarioRepository;
            NotificacionSocketService = notificacionSocketService;
        }
        public IRolRepository RolRepository { get; }
        private IUsuarioRepository UsuarioRepository { get; }

        private string getToken(string usuario)
        {
            var secretKey = Config.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            string bear_token = tokenHandler.WriteToken(createdToken);
            return bear_token;
        }

        public async Task<object> VerificarUsuarioAsync(string usuario)
        {
            try
            {
                Logger.LogInformation($" usuario : {usuario} inicio la funcionalidad VerificarUsuario()");
                LoginRespuesta resp = new LoginRespuesta();
                var objetoo = UsuarioRepository.ObtenerUsuarioPorId(usuario);
                if (objetoo != null)
                {
                    //-----------------------------------------------------------------------------------------------------
                    var rol = RolRepository.obtenerRolxUsuario(objetoo.IdUsuario);
                    if (rol != null)
                    {
                        var nn = rol.nombre;
                        var listModulePadre = RolRepository.obtnerModulosPadres(usuario);
                        resp.perfil = (PerfilModel)this.cargarPerfilesModulos(rol.idRol, usuario, objetoo.IdUsuario, listModulePadre);
                        resp.token = this.getToken(usuario);
                        await NotificacionSocketService.NotificarUnLogin(usuario, resp.token);
                        return Respuesta.ReturnResultdo(0, "roles obtenidos", resp);
                    }
                    else
                    {
                        //-------------------------------------------------------------------------------------------------------
                        // var resulte = Respuesta.ReturnResultdo(1, "No exite rol", "");
                        PerfilModel objPerfil = new PerfilModel();
                        Logger.LogInformation($" usuario : {usuario} repuesta obtener usuario: {JsonConvert.SerializeObject(objetoo)}");
                        return Respuesta.ReturnResultdo(0, "Aun no tiene rol asignado.", resp);
                    }
                }
                else
                {
                    Logger.LogWarning($" usuario : {usuario} no se encontro el registro");
                    var Result = new GenericDataJson<string> { Code = 2, Message = "El usaurio no se encuentra registrado" };
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
        public object cargarPerfilesModulos(int idRol, string usuario, int idUsurio, List<ModuloModel> moduloPadres)
        {
            try
            {
                PerfilModel objPerfil = new PerfilModel();
                List<PerfilHash> listaHash = new List<PerfilHash>();
                List<MenuModel> ListMenu = new List<MenuModel>();
                var objUser = UsuarioRepository.ObtenerUsuarioPorUsuario(usuario);
                foreach (var item in moduloPadres.OrderBy(mp => mp.Orden))
                {
                    var listModulohijo = RolRepository.obtnerSubModulosXIdPadre(usuario, item.IdModulo);
                    List<SubMenuModel> ListSubMenu = new List<SubMenuModel>();
                    foreach (var itemPadre in listModulohijo.OrderBy(mh => mh.Orden))
                    {
                        List<PaginaModel> oldPaginas = RolRepository.obtenerPaginasXModulo(usuario, itemPadre.IdModulo);
                        List<PaginaOutputModel> ListPages = new List<PaginaOutputModel>();
                        foreach (var itempag in oldPaginas.OrderBy(p => p.Orden))
                        {
                            var tienePagina = RolRepository.obtenerRolPaginaXPagina(usuario, itempag.IdPagina, idRol);
                            if (tienePagina != null)
                            {   //add paginas q tiene                                
                                PaginaOutputModel page = new PaginaOutputModel();
                                page.idPage = itempag.IdPagina;
                                page.title = itempag.Nombre;
                                page.descripion = itempag.Nombre;
                                page.namePage = itempag.Nombre.Replace(" ", String.Empty);
                                page.path = itempag.UrlPagina;
                                page.icon = itempag.Icono;
                                ListPages.Add(page);

                                List<PerfilHash> permisosHash = RolRepository.obtenerPermisoXPagina(usuario, (int)tienePagina.IdRolPaginaI, itempag.Nombre, itempag.UrlPagina);
                                listaHash.AddRange(permisosHash);
                            }
                        }
                        if (ListPages.Count > 0)
                        {//add submodulo
                            SubMenuModel submodulo = new SubMenuModel();
                            submodulo.idSubMenu = itemPadre.IdModulo;
                            submodulo.titleSubMenu = itemPadre.Nombre;
                            submodulo.iconsSubMenu = itemPadre.Icono;
                            submodulo.listaSubMenu = ListPages;
                            ListSubMenu.Add(submodulo);
                        }
                    }
                    if (ListSubMenu.Count > 0)
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
                objPerfil.listaHash = listaHash;
                objPerfil.usuario = usuario;
                objPerfil.idUsuario = idUsurio;
                if (objUser != null)
                {
                    objPerfil.nombre = objUser.Nombres;
                    objPerfil.Apellido = objUser.Apellidos;
                }
                else
                {
                    objPerfil.nombre = "";
                    objPerfil.Apellido = "";
                }
                return objPerfil;
            }
            catch (Exception ex)
            {
                Logger.LogError($" usuario : {usuario} catch error f,fin {ex.Message}");
                PerfilModel objPerfil = new PerfilModel();
                return objPerfil;
            }
        }

        public object verificarSession(string usuario, string netSessionId, int estado)
        {
            /*
                estado 1 : login exitoso;
                estado 0 : login error;
             */
            Logger.LogError($" usuario : {usuario} inicio verificarSession()");
            bool RespuestaVerificar = false;
            var RespuestasControlUsuario = UsuarioRepository.VerificarSession(usuario, netSessionId, estado);

            if (RespuestasControlUsuario != null && estado == 1 && (int)RespuestasControlUsuario.Estado == 1)
            {
                Logger.LogError($" usuario : {usuario} inicio ActaulizarIntentoUsuario({usuario},{netSessionId},{((int)RespuestasControlUsuario.CantidadIntentos + 1)}, 1)");
                RespuestaVerificar = UsuarioRepository.ActualizarIntentoUsuario(usuario, netSessionId, ((int)RespuestasControlUsuario.CantidadIntentos + 1), 1);
            }
            else if (RespuestasControlUsuario != null && estado == 0 && (int)RespuestasControlUsuario.Estado == 1)
            {
                Logger.LogError($" usuario : {usuario} inicio ActaulizarIntentoUsuario({usuario},{netSessionId},{((int)RespuestasControlUsuario.CantidadIntentos + 1)}, 0)");
                RespuestaVerificar = UsuarioRepository.ActualizarIntentoUsuario(usuario, netSessionId, ((int)RespuestasControlUsuario.CantidadIntentos + 1), 0);
            }
            else if (RespuestasControlUsuario == null && estado == 0)
            {
                Logger.LogError($" usuario : {usuario} inicio InsertarIntentoUsuario({usuario},{netSessionId}, 1)");
                RespuestaVerificar = UsuarioRepository.InsertarIntentoUsuario(usuario, netSessionId, 1);
            }
            else if (RespuestasControlUsuario != null && (int)RespuestasControlUsuario.Estado == 2)
            {
                DateTime fechaFin = (DateTime)RespuestasControlUsuario.FechaDesbloqueo;
                DateTime fechaActual = DateTime.Now;
                if (fechaActual > fechaFin)
                {
                    Logger.LogError($" usuario : {usuario} inicio InsertarIntentoUsuario({usuario},{netSessionId}, 1)");
                    _ = UsuarioRepository.InsertarIntentoUsuario(usuario, netSessionId, 1);
                    Logger.LogError($" usuario : {usuario} inicio ActaulizarIntentoUsuario({usuario},{netSessionId},{((int)RespuestasControlUsuario.CantidadIntentos + 1)}, 2)");
                    RespuestaVerificar = UsuarioRepository.ActualizarIntentoUsuario(usuario, netSessionId, ((int)RespuestasControlUsuario.CantidadIntentos + 1), 2);
                }
                else
                {
                    var diffFecha = fechaFin - DateTime.Now;
                    int segundosCalculados = (diffFecha.Hours * 3600) + (diffFecha.Minutes * 60) + diffFecha.Seconds;
                    return segundosCalculados;
                }
            }
            if (RespuestaVerificar)
            {
                Logger.LogError($" usuario : {usuario} fin verificarSession() se ejecuto correctamente la verificacion del usuario ");
            }
            else
            {
                Logger.LogError($" usuario : {usuario} fin verificarSession() se produjo un problema con la verificacion del usuario");
            }
            return null;
        }
    }
}
