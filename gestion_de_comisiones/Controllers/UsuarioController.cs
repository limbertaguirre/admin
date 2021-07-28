using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using gestion_de_comisiones.Servicios;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuariosForSelect([FromHeader] string usuarioLogin)
        {
            try
            {
                var responseApi = new ResponseApi<List<UsuarioSelectModel>>(await usuarioService.GetUsuarios(usuarioLogin));
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseApi<List<UsuarioSelectModel>>(ex.Message) { Data = new List<UsuarioSelectModel>() });
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUsuariosRol([FromHeader] string usuarioLogin)
        //{
        //    try
        //    {
        //        var responseApi = new ResponseApi<List<UsuarioSelectModel>>(await usuarioService.GetUsuarios(usuarioLogin));
        //        return Ok(responseApi);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ResponseApi<List<UsuarioSelectModel>>(ex.Message) { Data = new List<UsuarioSelectModel>() });
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> SetRol([FromBody] SetRolModel model)
        {
            //TODO:jaflores Register log
            try
            {
                var responseApi = new ResponseApi<bool>(await usuarioService.SetRolByUsuario(model));
                //var responseApi = new ResponseApi<bool>(true);
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseApi<bool>(ex.Message) { Data = false});
            }
        }

        [HttpPost]
        public ActionResult Registro([FromBody] UsuarioRegisterInputModel model)
        {
            try
            {
                UsuarioService usuario = (UsuarioService)usuarioService;
                var Result = usuario.RegistraUsuario(model);
                return Ok(Result);
            }
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO SE REGISTRO EL CLIENTE" };
                return Ok(Result);
            }
        }

    }
}
