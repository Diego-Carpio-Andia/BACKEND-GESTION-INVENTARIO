using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio
{
    public class Producto
    {
        public Guid Productoid { get; set; }
        public string Nombre { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; }
        public string Frecuencia { get; set; }
        public string Categoria { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioProveedor { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DolarActual { get; set; }
        public int CantidadInventario { get; set; }
        public byte[] Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        //SOLO LOS LINK tiene esa modificacion para agregar 
        public ICollection<ProductoCompra> CompraLink { get; set; }
        public ICollection<ProductoVenta> VentaLink { get; set; }
        public ICollection<ProductoPronosticoDemanda> PronosticoDemandaLink { get; set; }
        public Favoritos Favoritos { get; set; }
        //como la clase usuario HEREDA de entityUser entonces 
        //automaticamente se hace la union de uno a muchos 
        public Usuario Usuario { get; set; }
        public Guid ProveedorId {  get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
