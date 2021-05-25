using gestion_de_comisiones.Modelos;
using gestion_de_comisiones.Modelos.Rol;
using gestion_de_comisiones.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_comisiones.Controllers
{

    public class RolController : Controller
    {
        RolService refeRol = new RolService();
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
                var resultado = refeRol.ObtenerMooduloPaginas();
               // var Result = new GenericDataJson<string> { Code = 0, Message = "SE LISTADO" };
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
                var resultado = refeRol.ObtenerPermiso();
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
               // var Result = new GenericDataJson<string> { Code = 0, Message = "SE REGISTRO CORRECTAMENTE" };
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
                var resultado = refeRol.ObtenerListaRoles(idRol);
                return Ok(resultado);
            } 
            catch
            {
                var Result = new GenericDataJson<string> { Code = 1, Message = "NO EXITE PERMISOS PARA ESTE ROL" };
                return Ok(Result);
            }
        }
    }
}
