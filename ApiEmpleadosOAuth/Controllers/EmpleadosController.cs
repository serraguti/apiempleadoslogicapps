using ApiEmpleadosOAuth.Models;
using ApiEmpleadosOAuth.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiEmpleadosOAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<Empleado>> GetEmpleados()
        {
            return this.repo.GetEmpleados();
        }

        [HttpGet("{id}")]
        public ActionResult<Empleado> BuscarEmpleado(int id)
        {
            return this.repo.BuscarEmpleado(id);
        }

        //LOGICAMENTE, NECESITAMOS QUE ESTE METODO TENGA SEGURIDAD
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public ActionResult<Empleado> PerfilEmpleado()
        {
            //UNA VEZ QUE NOS HEMOS VALIDADO CON EL TOKEN
            //ESTAMOS AQUI Y, EN NUESTRO API, TAMBIEN ESTAMOS
            //VALIDADOS
            //DEBEMOS RECUPERAR EL CLAIM DE UserData 
            //DE LOS CLAIMS DEL USUARIO DE LA APP
            List<Claim> claims =
                HttpContext.User.Claims.ToList();
            //BUSCAMOS EL JSON DEL EMPLEADO, GUARDADO CON LA KEY UserData
            String jsonempleado =
                claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Empleado empleado =
                JsonConvert.DeserializeObject<Empleado>(jsonempleado);
            return empleado;
        }
    }
}
