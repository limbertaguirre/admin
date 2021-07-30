using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Usuario;
using gestion_de_comisiones.Servicios;
using gestion_de_comisiones.Servicios.Interfaces;
using gestion_de_comisiones.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;
        private readonly ILogger<UsuarioController> logger;

        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            this.usuarioService = usuarioService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetUsuariosForSelect([FromBody] UsuariosSelectInputModel model)
        {
            //TODO: jaflores register log
            try
            {
                logger.LogInformation(MessageLogger.FunctionIn(model.UsuarioLogin, nameof(UsuarioController.GetUsuariosForSelect)));
                var responseApi = new ResponseApi<List<UsuarioSelectModel>>(await usuarioService.GetUsuarios(model));
                logger.LogInformation(MessageLogger.FunctionOut(model.UsuarioLogin, nameof(UsuarioController.GetUsuariosForSelect)));
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, MessageLogger.ExcepcionMessage(model.UsuarioLogin, nameof(UsuarioController.GetUsuariosForSelect)));
                return Ok(new ResponseApi<List<UsuarioSelectModel>>(ex.Message) { Data = new List<UsuarioSelectModel>() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsuarioRol([FromBody] DeleteUserRolInputModel model)
        {
            //TODO: jaflores register log
            try
            {
                var responseApi = new ResponseApi<bool>(await usuarioService.DeleteUserRol(model));
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseApi<bool>(ex.Message) { Data = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuariosRol([FromHeader] string usuarioLogin)
        {
            //TODO: jaflores register log
            try
            {
                var responseApi = new ResponseApi<List<UsuarioRolListViewModel>>(await usuarioService.GetUsuariosRol(usuarioLogin));
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseApi<List<UsuarioRolListViewModel>>(ex.Message) { Data = new List<UsuarioRolListViewModel>() });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SetRol([FromBody] SetRolModel model)
        {
            //TODO:jaflores Register log
            try
            {
                var responseApi = new ResponseApi<bool>(await usuarioService.SetRolByUsuario(model));
                
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
