using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class ProductoPronosticoDemanda
    {
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public Guid PronosticoDemandaId { get; set; }
        public PronosticoDemanda PronosticoDemanda { get; set; }
    }
}
