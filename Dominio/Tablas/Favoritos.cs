using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class Favoritos
    {
        public Guid FavoritosId { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
