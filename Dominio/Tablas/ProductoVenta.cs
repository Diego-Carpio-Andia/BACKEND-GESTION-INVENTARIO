using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class ProductoVenta
    {
        public Guid ProductoId {  get; set; }
        public Producto producto { get; set; }
        public Guid VentaId { get; set; }
        public Venta venta { get; set; } 

    }
}
