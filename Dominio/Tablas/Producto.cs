using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class Producto
    {
        public Guid Productoid { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<ProductoCompra> CompraLink { get; set; }
        public ICollection<ProductoVenta> VentaLink { get; set; }
    }
}
