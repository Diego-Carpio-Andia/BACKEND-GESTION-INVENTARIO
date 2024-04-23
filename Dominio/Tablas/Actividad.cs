using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class Actividad
    {
        public Guid ActividadId {  get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string TipoActividad { get; set; }
        public string DescripcionActividad { get; set; }    
        public DateTime FechaHora { get; set; }

    }
}
