using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Tablas
{
    public class Producto
    {
        public Guid Productoid { get; set; }
        public string Nombre { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }        
        public string Categoria { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<ProductoCompra> CompraLink { get; set; }
        public ICollection<ProductoVenta> VentaLink { get; set; }
        public ICollection<ProductoPronosticoDemanda> PronosticoDemandaLink {  get; set; }
        public Favoritos Favoritos { get; set; }
    }
}
