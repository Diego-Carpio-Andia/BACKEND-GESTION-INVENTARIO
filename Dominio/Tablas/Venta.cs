using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Tablas
{
    public class Venta
    {
        public Guid VentaId {  get; set; }
        public int Cantidad {  get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<Producto> ProductoLink { get; set; }
    }
}
