using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Compra
    {
        public Guid CompraId { get; set; }
        public int Cantidad { get; set; }
        //como la clase usuario HEREDA de entityUser entonces 
        //automaticamente se hace la union de uno a muchos 
        public Usuario Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<ProductoCompra> ProductoLink { get; set; }
    }
}
