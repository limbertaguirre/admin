using gestion_de_comisiones.Modelos.Usuario;
using gestion_de_comisiones.MultinivelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Repository
{
    public class UsuarioRepository
    {
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
