using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosLogicApps.Models
{
    public class ListEmpleados
    {
        [JsonProperty("value")]
        public List<Tarea> Tareas { get; set; }
    }
}
