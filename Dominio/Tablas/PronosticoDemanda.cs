﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class PronosticoDemanda
    {  
        public Guid PronosticoDemandaId { get; set; }
        public int CantidadPronosticada {  get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<ProductoPronosticoDemanda> ProductoLink { get; set; }
        public Notificacion Notificacion { get; set; }
    }
}
