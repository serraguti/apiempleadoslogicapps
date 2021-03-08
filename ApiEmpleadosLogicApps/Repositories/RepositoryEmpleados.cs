using ApiEmpleadosLogicApps.Data;
using ApiEmpleadosLogicApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosLogicApps.Repositories
{
    public class RepositoryEmpleados
    {
        EmpleadosContext context;
        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }

        public Empleado BuscarEmpleado(int idempleado)
        {
            return this.context.Empleados
                .SingleOrDefault(x => x.IdEmpleado == idempleado);
        }

        public void EliminarEmpleado(int idempleado)
        {
            Empleado empleado = this.BuscarEmpleado(idempleado);
            this.context.Empleados.Remove(empleado);
            this.context.SaveChanges();
        }

        public void IncrementarSalario(int idempleado, int incremento)
        {
            Empleado empleado = this.BuscarEmpleado(idempleado);
            empleado.Salario += incremento;
            this.context.SaveChanges();
        }

        public List<Tarea> GetTareas()
        {
            return this.context.Tareas.ToList();
        }

        public List<Tarea> GetTareasEmpleado(int idempleado)
        {
            //BUSCAMOS LAS TAREAS QUE TODAVIA NO SE HAN PROCESADO
            return this.context.Tareas.Where(x => x.IdEmpleado == idempleado
            && x.Estado == "NEW")
                .OrderByDescending(x => x.IdTarea)
                .ToList(); 
        }

        public Tarea FindTarea(int idtarea)
        {
            return this.context.Tareas.SingleOrDefault(x => x.IdEmpleado == idtarea);
        }

        private int GetMaxtarea()
        {
            return this.context.Tareas.Max(x => x.IdTarea) + 1;
        }

        public void InsertarTarea(String nombre, String descripcion
            , int idempleado)
        {
            Tarea t = new Tarea();
            t.IdTarea = this.GetMaxtarea();
            t.Nombre = nombre;
            t.Descripcion = descripcion;
            t.IdEmpleado = idempleado;
            t.Estado = "NEW";
            this.context.Tareas.Add(t);
            this.context.SaveChanges();
        }
    }
}
