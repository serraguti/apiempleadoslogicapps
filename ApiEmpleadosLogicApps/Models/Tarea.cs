using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosLogicApps.Models
{
    [Table("TAREASEMPLEADOS")]
    public class Tarea
    {
        [Key]
        [Column("IDTAREA")]
        public int IdTarea { get; set; }
        [Column("NOMBRETAREA")]
        public String Nombre { get; set; }
        [Column("DESCRIPCION")]
        public String Descripcion { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("IDEMPLEADO")]
        public int IdEmpleado { get; set; }
    }
}
