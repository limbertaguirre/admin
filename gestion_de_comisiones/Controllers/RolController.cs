using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{

    public class RolController : Controller
    {
        RolService sevice = new RolService();
        private readonly ILogger<RolController> _logger;
        public RolController(ILogger<RolController> logger)
        {
            _logger = logger;
        }
        // GET: RolController
        public ActionResult Index()
        {
            return View();
        }


        // GET: RolController/Listamodulos
        public ActionResult Listamodulos()
        {
            
            try
            {
                var resultado = sevice.ObtenerMooduloPaginas();
                return Ok(resultado);
            }
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO SE ENCONTRO AREAS" };
                return Ok(Result);
            }
        }
        // GET: RolController/Listapermisos
        public ActionResult Listapermisos()
        {

            try
            {
                var resultado = sevice.ObtenerPermiso();
                return Ok(resultado);
            }
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO EXITE PERMISOS" };
                return Ok(Result);
            }
        }


        // POST: RolController/Registrar
        [HttpPost]
        public ActionResult Registrar([FromBody] RolRegisterInputModel objetdatao)
        {
            RolService refRol = new RolService();

            try
            {
                var resulRol = refRol.RegistraRol(objetdatao);
                return Ok(resulRol);
            }
            catch (Exception ex)
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "Intente mas tarde.." };
                return Ok(Result);
            }
        }

        // GET: RolController/ObtenerListaXRol
        public ActionResult ObtenerListaXRol([FromHeader]int idRol)
        {
            try
            {
                var resultado = sevice.ObtenerListaRol(idRol);
                return Ok(resultado);
            } 
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO EXITE PERMISOS PARA ESTE ROL" };
                return Ok(Result);
            }
        }
        // GET: RolController/ObtenerRolesAllModules
        public ActionResult ObtenerRolesAllModules([FromHeader] string userLogin)
        {
            try
            {
                
                _logger.LogInformation($" es el usuario : {userLogin} inicio el servicio ObtenerRolesAllModules ");
                var resultado = sevice.ObtenerListaRolesWithModulos();
                _logger.LogInformation($" es el usuario : {userLogin} Fin del servicio ObtenerRolesAllModules ");
                return Ok(resultado);
            }
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO EXITE PERMISOS PARA ESTE ROL" };
                return Ok(Result);
            }
        }

        // POST: RolController/Actualizar
        [HttpPost]
        public ActionResult Actualizar([FromBody] RolActualizarInputModel objetdatao)
        {
            try
            {
                var result = sevice.ActualizarRol(objetdatao);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "Intente mas tarde.." };
                return Ok(Result);
            }
        }

    }
}
