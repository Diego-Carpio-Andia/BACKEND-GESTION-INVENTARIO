using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Favoritos
    {
        public Guid FavoritosId { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public DateTime FechaCreacion { get; set; }
        //como la clase usuario HEREDA de entityUser entonces 
        //automaticamente se hace la union de uno a muchos 
        public Usuario Usuario { get; set; }
    }
}
