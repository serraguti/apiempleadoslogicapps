using ApiEmpleadosLogicApps.Models;
using ApiEmpleadosLogicApps.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosLogicApps.Controllers
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

        [HttpPut]
        [Route("[action]/{idempleado}/{incremento}")]
        public void IncrementarSalario(int idempleado, int incremento)
        {
            this.repo.IncrementarSalario(idempleado, incremento);
        }

        [HttpDelete("{id}")]
        public void EliminarEmpleado(int id)
        {
            this.repo.EliminarEmpleado(id);
        }

        [HttpPost]
        [Route("[action]")]
        public void InsertarTarea(Tarea tarea)
        {
            this.repo.InsertarTarea(tarea.Nombre, tarea.Descripcion, tarea.IdEmpleado);
        }

        [HttpGet]
        [Route("[action]")]
        public List<Tarea> Tareas()
        {
            return this.repo.GetTareas();
        }

        [HttpGet]
        [Route("[action]/{idempleado}/tareas")]
        public ActionResult<object> TareasEmpleado(int idempleado)
        {
            //var list = _userRepository.GetAll(); // <-- if this is not (yet) an Array or a List, then force single evaluation by adding .ToArray() or .ToList()
            var model = new
            {
                value = this.repo.GetTareasEmpleado(idempleado)
            };
            //return Ok(model);
            return Ok(model);
        }

        [HttpGet]
        [Route("[action]/{idtarea}")]
        public Tarea BuscarTarea(int idtarea)
        {
            return this.repo.FindTarea(idtarea);
        }
    }
}
