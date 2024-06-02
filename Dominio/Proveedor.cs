using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Proveedor
    {
        public Guid ProveedorId { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string NumeroCelular { get; set; }
        //como la clase usuario HEREDA de entityUser entonces 
        //automaticamente se hace la union de uno a muchos 
        public Usuario Usuario { get; set; }
        public ICollection<Producto> ListaProductos { get; set; }   
    }
}
