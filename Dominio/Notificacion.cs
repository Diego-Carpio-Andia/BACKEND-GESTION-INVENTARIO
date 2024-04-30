using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Notificacion
    {
        public Guid NotificacionId { get; set; }
        public Guid PronosticoDemandaId { get; set; }
        public PronosticoDemanda PronosticoDemanda { get; set; }
        public int SegundosNotificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
