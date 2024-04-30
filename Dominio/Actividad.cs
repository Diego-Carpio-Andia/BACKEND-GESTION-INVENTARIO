using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Actividad
    {
        public Guid ActividadId { get; set; }
        //como la clase usuario HEREDA de entityUser entonces 
        //automaticamente se hace la union de uno a muchos 
        public Usuario Usuario { get; set; }
        public string TipoActividad { get; set; }
        public string DescripcionActividad { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
