using gestion_de_comisiones.Modelos.Usuario;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class UsuarioRepository: IUsuarioRepository
    {

        private readonly BDMultinivelContext multinivelDbContext;
        public UsuarioRepository(BDMultinivelContext multinivelDbContext)
        {
            this.multinivelDbContext = multinivelDbContext;
        }
        public UsuarioRepository()
        {

        }

        public async Task<List<UsuarioSelectModel>> GetUsuarios(string usuario)
        {
            var usuarios = await multinivelDbContext.Usuarios
                .Select(u=>  new UsuarioSelectModel { IdUsuario = u.IdUsuario, Nombres = u.Nombres, Apellidos = u.Apellidos, Login = u.Usuario1  })
                .ToListAsync();
            return usuarios;
        }

        public async Task<bool> SetRolByUsuario(SetRolModel model)
        {
            //Check user
            var usuario =await multinivelDbContext.Usuarios.FindAsync(model.UsuarioId);
            if (usuario is null)
            {
                throw new Exception(MessageHandler.NotFoundRegister(model.UsuarioId, nameof(Usuario)));
            }

            //Check rol
            var rol =await multinivelDbContext.Rols.FindAsync(model.RolId);
            if (rol is null)
            {
                throw new Exception(MessageHandler.NotFoundRegister(model.RolId, nameof(Rol)));
            }

            //Check if exist roles by user and disable
            bool existRoles = multinivelDbContext.UsuariosRoles
                                .Where(ur =>ur.Estado.Equals(true) && ur.IdUsuario.Equals(model.UsuarioId))
                                .Any();
            if (existRoles)
            {
                //Disable rol if exist
                var rolesByUser = multinivelDbContext.UsuariosRoles
                                    .Where(ur => ur.IdUsuario.Equals(model.UsuarioId) && ur.IdRol.Equals(model.RolId) && ur.Estado.Equals(true));
                foreach (var rolUser in rolesByUser)
                {
                    rolUser.Estado = false;
                    rolUser.FechaActualizacion = DateTime.Now;
                }
            }

            //Create UsuarioRol
            UsuariosRole usuariosRole = new UsuariosRole();
            usuariosRole.IdUsuario = usuario.IdUsuario;
            usuariosRole.IdRol = rol.IdRol;
            usuariosRole.Estado = true;
            usuariosRole.UsuarioId = model.UserOperationId;

            await multinivelDbContext.UsuariosRoles.AddAsync(usuariosRole);
            await multinivelDbContext.SaveChangesAsync();

            return (usuariosRole.IdUsuariosRoles >0);
        }


        BDMultinivelContext contextMulti = new BDMultinivelContext();

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


    }
}
