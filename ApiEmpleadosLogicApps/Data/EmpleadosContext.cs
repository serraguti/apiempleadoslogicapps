using ApiEmpleadosLogicApps.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosLogicApps.Data
{
    public class EmpleadosContext : DbContext
    {
        public EmpleadosContext(DbContextOptions<EmpleadosContext> options): 
            base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }

}
