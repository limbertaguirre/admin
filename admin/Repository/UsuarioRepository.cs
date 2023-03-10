using admin.Modelos.Usuario;
using admin.Repository.Interfaces;
using admin.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using admin.OperacionModel;

namespace admin.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly BDOperacionContext multinivelDbContext;
        private readonly ILogger<UsuarioRepository> logger;
        private readonly IConfiguration Config;
        public UsuarioRepository(IConfiguration config, BDOperacionContext multinivelDbContext, ILogger<UsuarioRepository> logger)
        {
            this.multinivelDbContext = multinivelDbContext;
            this.logger = logger;
            this.Config = config;
        }

        /// <summary>
        /// Obtiene todos los usuarios con estado activo(true).
        /// Model.Operation=0: todos los usuarios sin rol.
        /// Model.Operation=1: todos los usuarios con rol.
        /// </summary>
        public async Task<List<UsuarioSelectModel>> GetUsuariosForSelect(UsuariosSelectInputModel model)
        {
            logger.LogInformation(MessageLogger.FunctionIn(model.UsuarioLogin, nameof(UsuarioRepository.GetUsuariosForSelect)));
            var result = new List<UsuarioSelectModel>();

            switch (model.Operation)
            {
                case 0://new operation
                    var usuarios = multinivelDbContext.Usuarios;
                    var usuariosRoles = multinivelDbContext.UsuariosRoles.Where(ur => ur.Estado.Equals(true));
                    result = await usuarios
                        .Where(u => u.Estado.Equals(true) && !usuariosRoles.Any(ur => ur.IdUsuario.Equals(u.IdUsuario)))
                        .Select(u => new UsuarioSelectModel { IdUsuario = u.IdUsuario, Nombres = u.Nombres, Apellidos = u.Apellidos, Login = u.Usuario1 })
                        .ToListAsync();
                    break;
                case 1://edit operation
                    result = await multinivelDbContext.UsuariosRoles.Where(ur => ur.Estado.Equals(true)).Join(
                    multinivelDbContext.Usuarios,
                    ur => ur.IdUsuario,
                    us => us.IdUsuario,
                    (ur, us) =>
                    new UsuarioSelectModel
                    {
                        IdUsuario = us.IdUsuario,
                        Nombres = us.Nombres,
                        Apellidos = us.Apellidos,
                        Login = us.Usuario1,
                    })
                    .ToListAsync();
                    break;
            }

            logger.LogInformation(MessageLogger.FunctionIn(model.UsuarioLogin, nameof(UsuarioRepository.GetUsuariosForSelect)));
            return result;
        }

        public async Task<bool> DeleteUsuarioRol(DeleteUserRolInputModel model)
        {
            var userRol = await multinivelDbContext.UsuariosRoles.FirstOrDefaultAsync(ur => ur.IdUsuariosRoles.Equals(model.UsuarioRolId));
            if (userRol is null)
            {
                throw new Exception($"Registro no encontrado");
            }

            userRol.Estado = false;
            userRol.FechaActualizacion = DateTime.Now;
            await multinivelDbContext.SaveChangesAsync();

            return true;

        }
        public async Task<List<UsuarioRolListViewModel>> GetUsuariosRol(string usuario)
        {
            var usuariosRol = await multinivelDbContext.UsuariosRoles.Where(ur => ur.Estado.Equals(true)).Join(
                multinivelDbContext.Usuarios,
                ur => ur.IdUsuario,
                us => us.IdUsuario,
                (ur, us) =>
                new
                {
                    UsuarioRolId = ur.IdUsuariosRoles,
                    UsuarioId = us.IdUsuario,
                    Usuario = us.Usuario1,
                    Nombres = us.Nombres,
                    Apellidos = us.Apellidos,
                    RolId = ur.IdRol
                }).Join(
                multinivelDbContext.Rols,
                ur => ur.RolId,
                r => r.IdRol,
                (ur, r) => new UsuarioRolListViewModel
                {
                    UsuarioRolId = ur.UsuarioRolId,
                    UsuarioId = ur.UsuarioId,
                    Usuario = ur.Usuario,
                    Nombres = ur.Nombres,
                    Apellidos = ur.Apellidos,
                    RolId = r.IdRol,
                    Rol = r.Nombre
                })
                .ToListAsync();
            return usuariosRol;
        }

        public async Task<bool> SetRolByUsuario(SetRolModel model)
        {
            //Check user
            var usuario = await multinivelDbContext.Usuarios.FindAsync(model.UsuarioId);
            if (usuario is null)
            {
                throw new Exception(MessageHandler.NotFoundRegister(model.UsuarioId, nameof(Usuario)));
            }

            //Check rol
            var rol = await multinivelDbContext.Rols.FindAsync(model.RolId);
            if (rol is null)
            {
                throw new Exception(MessageHandler.NotFoundRegister(model.RolId, nameof(Rol)));
            }

            //Check if exist roles by user and disable
            bool existUsuarioRol = multinivelDbContext.UsuariosRoles
                                .Where(ur => ur.IdUsuario.Equals(model.UsuarioId))
                                .Any();
            if (existUsuarioRol)
            {
                //Disable rol if exist
                var userRol = multinivelDbContext.UsuariosRoles
                                    .FirstOrDefault(ur => ur.IdUsuario.Equals(model.UsuarioId));


                userRol.IdRol = model.RolId;
                userRol.Estado = true;
                userRol.FechaActualizacion = DateTime.Now;


            }
            else
            {
                //Create UsuarioRol
                UsuariosRole usuariosRole = new UsuariosRole();
                usuariosRole.IdUsuario = usuario.IdUsuario;
                usuariosRole.IdRol = rol.IdRol;
                usuariosRole.Estado = true;
                usuariosRole.UsuarioId = model.UserOperationId;

                multinivelDbContext.UsuariosRoles.Add(usuariosRole);
            }


            await multinivelDbContext.SaveChangesAsync();

            return true;
        }


        BDOperacionContext contextMulti = new BDOperacionContext();

        public UsuarioModel ObtenerUsuarioPorId(string usuario)
        {
            try
            {
                var objUsuario = contextMulti.Usuarios.Where(x => x.Usuario1 == usuario).Select(p => new UsuarioModel(p.IdUsuario, p.Usuario1, p.Nombres, p.Apellidos, p.Telefono, p.Corporativo, p.FechaNacimiento, p.IdRol, p.IdSucursal, p.IdArea, p.UsuarioId, p.FechaCreacion, p.FechaActualizacion)).SingleOrDefault();
                return objUsuario;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public bool RegistrarUsuario(UsuarioRegisterInputModel param)
        {
            try
            {
                Usuario objUser = new Usuario();
                objUser.Usuario1 = param.userName;
                objUser.Nombres = param.nombre;
                objUser.Apellidos = param.apellido;
                objUser.Telefono = param.telefono.ToString();
                objUser.Corporativo = param.corporativo;
                objUser.FechaNacimiento = Convert.ToDateTime(param.fechaNacimiento);
                objUser.IdArea = param.area;
                objUser.IdSucursal = param.sucursal;
                objUser.IdRol = 0;
                objUser.UsuarioId = 0;
                contextMulti.Usuarios.Add(objUser);
                contextMulti.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UsuarioModel ObtenerUsuarioPorUsuario(string usuario)
        {
            try
            {
                var objUsuario = contextMulti.Usuarios.Where(x => x.Usuario1 == usuario).Select(p => new UsuarioModel(p.IdUsuario, p.Usuario1, p.Nombres, p.Apellidos, p.Telefono, p.Corporativo, p.FechaNacimiento, p.IdRol, p.IdSucursal, p.IdArea, p.UsuarioId, p.FechaCreacion, p.FechaActualizacion)).SingleOrDefault();
                return objUsuario;
            }
            catch (Exception ex)
            {
                UsuarioModel obj = new UsuarioModel();
                return obj;
            }
        }

        public ControlUsuarioModel VerificarSession(string usuario, string netSessionId, int estado)
        {
            try
            {
                var responseOBJControlusuario = contextMulti.ControlUsuarios.Where(x => x.Usuario == usuario && (x.Estado == 1 || x.Estado == 2)).Select(c => new ControlUsuarioModel(c.IdControlUsuario, c.Usuario, c.CantidadIntentos, c.FechaBloquedo, c.FechaDesbloqueo, c.NetSessionId, c.Estado)).SingleOrDefault();
                return responseOBJControlusuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool InsertarIntentoUsuario(string usuario, string netSessionId, int cantidad)
        {
            try
            {
                ControlUsuario controlUsuario = new ControlUsuario();
                controlUsuario.Usuario = usuario;
                controlUsuario.NetSessionId = netSessionId;
                controlUsuario.CantidadIntentos = cantidad;
                controlUsuario.Estado = 1;
                contextMulti.ControlUsuarios.Add(controlUsuario);
                contextMulti.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool ActualizarIntentoUsuario(string usuario, string netSessionId, int cantidad, int estado)
        {
            try
            {
                int tiempoBloqueoUsuario = Config.GetValue<int>("TiempoBloqueoUsuario");
                int cantidaddIntentoBloqueo = Config.GetValue<int>("CantidaddIntentoBloqueo");
                if (estado == 2)
                {
                    var CtrlUsuarioRow = contextMulti.ControlUsuarios.Where(x => x.Usuario == usuario && x.Estado == 2).First();
                    CtrlUsuarioRow.Estado = 0;
                }
                else
                {
                    var CtrlUsuarioRow = contextMulti.ControlUsuarios.Where(x => x.Usuario == usuario && x.Estado == 1).First();

                    CtrlUsuarioRow.NetSessionId = netSessionId;
                    if (estado == 1) { CtrlUsuarioRow.Estado = 0; } else { CtrlUsuarioRow.CantidadIntentos = cantidad; }
                    if (cantidad < cantidaddIntentoBloqueo)
                    {
                        CtrlUsuarioRow.FechaBloquedo = DateTime.Now;
                        CtrlUsuarioRow.FechaDesbloqueo = DateTime.Now.AddMinutes(tiempoBloqueoUsuario);
                        CtrlUsuarioRow.Estado = 2;
                    }
                }
                contextMulti.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
